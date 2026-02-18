import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import {
  Button,
  Form,
  Space,
  Card,
  Modal,
  Input,
  Checkbox,
  Table,
  Tag,
  message,
  Popconfirm,
} from "antd";
import {
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  LockOutlined,
  ReloadOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";

interface Role {
  id: string;
  name: string;
  isDefault: boolean;
  isStatic: boolean;
  isPublic: boolean;
}

interface PermissionGroup {
  name: string;
  displayName: string;
  permissions: Permission[];
}

interface Permission {
  name: string;
  displayName: string;
  isGranted: boolean;
  parentName: string | null;
}

export default function RolesPage() {
  const auth = useAuth();
  const [roles, setRoles] = useState<Role[]>([]);
  const [loading, setLoading] = useState(true);

  // Form modal
  const [formModalVisible, setFormModalVisible] = useState(false);
  const [editingRole, setEditingRole] = useState<Role | null>(null);
  const [saving, setSaving] = useState(false);
  const [roleForm, setRoleForm] = useState({
    name: "",
    isDefault: false,
    isPublic: false,
  });

  // Permission modal
  const [permModalVisible, setPermModalVisible] = useState(false);
  const [permRoleId, setPermRoleId] = useState("");
  const [permRoleName, setPermRoleName] = useState("");
  const [permGroups, setPermGroups] = useState<PermissionGroup[]>([]);
  const [permActiveGroup, setPermActiveGroup] = useState("");
  const [permSaving, setPermSaving] = useState(false);

  const fetchRoles = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/identity/roles", {
        params: { skipCount: 0, maxResultCount: 100 },
      });
      setRoles(response.data.items);
    } catch (error) {
      console.error("Roller yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchRoles();
  }, [auth.isAuthenticated]);

  // --- New ---
  const handleNew = () => {
    setEditingRole(null);
    setRoleForm({ name: "", isDefault: false, isPublic: false });
    setFormModalVisible(true);
  };

  // --- Edit ---
  const handleEdit = (role: Role) => {
    setEditingRole(role);
    setRoleForm({
      name: role.name,
      isDefault: role.isDefault,
      isPublic: role.isPublic,
    });
    setFormModalVisible(true);
  };

  // --- Save ---
  const handleSave = async () => {
    if (!roleForm.name) {
      message.warning("Rol adı zorunludur.");
      return;
    }
    setSaving(true);
    try {
      if (editingRole) {
        await apiClient.put(`/api/identity/roles/${editingRole.id}`, roleForm);
        message.success("Rol güncellendi.");
      } else {
        await apiClient.post("/api/identity/roles", roleForm);
        message.success("Rol oluşturuldu.");
      }
      setFormModalVisible(false);
      fetchRoles();
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "Kayıt hatası");
    } finally {
      setSaving(false);
    }
  };

  // --- Delete ---
  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/identity/roles/${id}`);
      message.success("Rol silindi.");
      fetchRoles();
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  // --- Permissions ---
  const openPermissions = async (role: Role) => {
    setPermRoleId(role.id);
    setPermRoleName(role.name);
    setPermSaving(false);
    try {
      const response = await apiClient.get(
        "/api/permission-management/permissions",
        { params: { providerName: "R", providerKey: role.name } }
      );
      const groups: PermissionGroup[] = response.data.groups;
      setPermGroups(groups);
      if (groups.length > 0) setPermActiveGroup(groups[0].name);
      setPermModalVisible(true);
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "İzinler yüklenemedi");
    }
  };

  const togglePermission = (groupName: string, permName: string) => {
    setPermGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        return {
          ...g,
          permissions: g.permissions.map((p) =>
            p.name === permName ? { ...p, isGranted: !p.isGranted } : p
          ),
        };
      })
    );
  };

  const toggleGroupAll = (groupName: string, checked: boolean) => {
    setPermGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        return {
          ...g,
          permissions: g.permissions.map((p) => ({ ...p, isGranted: checked })),
        };
      })
    );
  };

  const toggleAll = (checked: boolean) => {
    setPermGroups((prev) =>
      prev.map((g) => ({
        ...g,
        permissions: g.permissions.map((p) => ({ ...p, isGranted: checked })),
      }))
    );
  };

  const savePermissions = async () => {
    setPermSaving(true);
    try {
      const permissions: { name: string; isGranted: boolean }[] = [];
      permGroups.forEach((g) =>
        g.permissions.forEach((p) =>
          permissions.push({ name: p.name, isGranted: p.isGranted })
        )
      );
      await apiClient.put(
        "/api/permission-management/permissions",
        { permissions },
        { params: { providerName: "R", providerKey: permRoleName } }
      );
      message.success("İzinler kaydedildi.");
      setPermModalVisible(false);
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "İzin kayıt hatası");
    } finally {
      setPermSaving(false);
    }
  };

  const allGranted = permGroups.every((g) =>
    g.permissions.every((p) => p.isGranted)
  );
  const activeGroup = permGroups.find((g) => g.name === permActiveGroup);
  const activeGroupAllGranted = activeGroup
    ? activeGroup.permissions.every((p) => p.isGranted)
    : false;

  // --- Columns ---
  const columns = [
    {
      title: "Rol Adı",
      key: "name",
      render: (_: any, record: Role) => (
        <Space>
          {record.name}
          {record.isDefault && <Tag color="green">Varsayılan</Tag>}
          {record.isPublic && <Tag color="red">Herkese Açık</Tag>}
        </Space>
      ),
    },
    {
      title: "İşlemler",
      key: "actions",
      width: 250,
      render: (_: any, record: Role) => (
        <Space size="small">
          <Button
            type="link"
            icon={<EditOutlined />}
            onClick={() => handleEdit(record)}
            size="small"
            style={{ color: "#fa8c16" }}
          >
            Düzenle
          </Button>
          <Button
            type="link"
            icon={<LockOutlined />}
            onClick={() => openPermissions(record)}
            size="small"
            style={{ color: "#722ed1" }}
          >
            İzinler
          </Button>
          {!record.isStatic && (
            <Popconfirm
              title="Bu rolü silmek istediğinize emin misiniz?"
              onConfirm={() => handleDelete(record.id)}
              okText="Evet"
              cancelText="Hayır"
            >
              <Button type="link" danger icon={<DeleteOutlined />} size="small">
                Sil
              </Button>
            </Popconfirm>
          )}
        </Space>
      ),
    },
  ];

  return (
    <>
      <Card>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            marginBottom: 16,
          }}
        >
          <Space align="center">
            <h3 style={{ margin: 0 }}>Roller</h3>
            <Button icon={<ReloadOutlined />} onClick={fetchRoles} size="small" title="Yenile" />
            <Button type="primary" icon={<PlusOutlined />} onClick={handleNew} size="small">
              Yeni Rol
            </Button>
          </Space>
        </div>

        <Table
          columns={columns}
          dataSource={roles}
          rowKey="id"
          loading={loading}
          size="small"
          pagination={{
            pageSize: 15,
            showSizeChanger: true,
            showTotal: (total) => `Toplam ${total} kayıt`,
            size: "small",
          }}
        />
      </Card>

      {/* Rol Formu Modalı */}
      <Modal
        title={editingRole ? "Rol Düzenle" : "Yeni Rol"}
        open={formModalVisible}
        onCancel={() => setFormModalVisible(false)}
        width={400}
        footer={
          <Space>
            <Button onClick={() => setFormModalVisible(false)}>Vazgeç</Button>
            <Button type="primary" onClick={handleSave} loading={saving}>
              Kaydet
            </Button>
          </Space>
        }
        destroyOnClose
      >
        <Form layout="vertical">
          <Form.Item label="Rol adı" required>
            <Input
              value={roleForm.name}
              onChange={(e) => setRoleForm({ ...roleForm, name: e.target.value })}
            />
          </Form.Item>
          <Space direction="vertical">
            <Checkbox
              checked={roleForm.isDefault}
              onChange={(e) => setRoleForm({ ...roleForm, isDefault: e.target.checked })}
            >
              Varsayılan
            </Checkbox>
            <Checkbox
              checked={roleForm.isPublic}
              onChange={(e) => setRoleForm({ ...roleForm, isPublic: e.target.checked })}
            >
              Herkese açık
            </Checkbox>
          </Space>
        </Form>
      </Modal>

      {/* İzin Yönetimi Modalı */}
      <Modal
        title={`İzinler - ${permRoleName}`}
        open={permModalVisible}
        onCancel={() => setPermModalVisible(false)}
        width={700}
        footer={
          <Space>
            <Button onClick={() => setPermModalVisible(false)}>Vazgeç</Button>
            <Button type="primary" onClick={savePermissions} loading={permSaving}>
              Kaydet
            </Button>
          </Space>
        }
        destroyOnClose
      >
        <div style={{ marginBottom: 16, borderBottom: "1px solid #f0f0f0", paddingBottom: 12 }}>
          <Checkbox checked={allGranted} onChange={(e) => toggleAll(e.target.checked)}>
            Tüm izinleri ver
          </Checkbox>
        </div>

        <div style={{ display: "flex", gap: 16 }}>
          <div style={{ width: 220, borderRight: "1px solid #f0f0f0", paddingRight: 16 }}>
            {permGroups.map((g) => {
              const grantedCount = g.permissions.filter((p) => p.isGranted).length;
              return (
                <div
                  key={g.name}
                  onClick={() => setPermActiveGroup(g.name)}
                  style={{
                    padding: "8px 12px",
                    cursor: "pointer",
                    borderRadius: 6,
                    marginBottom: 4,
                    backgroundColor: permActiveGroup === g.name ? "#1677ff" : "transparent",
                    color: permActiveGroup === g.name ? "#fff" : "#333",
                    fontWeight: permActiveGroup === g.name ? 600 : 400,
                  }}
                >
                  {g.displayName} ({grantedCount})
                </div>
              );
            })}
          </div>

          <div style={{ flex: 1 }}>
            {activeGroup && (
              <>
                <h4 style={{ marginTop: 0 }}>{activeGroup.displayName}</h4>
                <div style={{ marginBottom: 12, borderBottom: "1px solid #f0f0f0", paddingBottom: 8 }}>
                  <Checkbox
                    checked={activeGroupAllGranted}
                    onChange={(e) => toggleGroupAll(activeGroup.name, e.target.checked)}
                  >
                    Hepsini seç
                  </Checkbox>
                </div>
                {activeGroup.permissions.map((perm) => (
                  <div
                    key={perm.name}
                    style={{ marginBottom: 6, paddingLeft: perm.parentName ? 24 : 0 }}
                  >
                    <Checkbox
                      checked={perm.isGranted}
                      onChange={() => togglePermission(activeGroup.name, perm.name)}
                    >
                      {perm.displayName}
                    </Checkbox>
                  </div>
                ))}
              </>
            )}
          </div>
        </div>
      </Modal>
    </>
  );
}