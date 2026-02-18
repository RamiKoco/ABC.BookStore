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
  Tabs,
  Tree,
  message,
  Popconfirm,
  Table,
  Tag,
} from "antd";
import {
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
  LockOutlined,
  ReloadOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";

// --- Interfaces ---
interface User {
  id: string;
  userName: string;
  name: string;
  surname: string;
  email: string;
  phoneNumber: string;
  isActive: boolean;
  lockoutEnabled: boolean;
}

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
  allowedProviders: string[];
}

// --- Component ---
export default function UsersPage() {
  const auth = useAuth();
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);

  // User form modal
  const [formModalVisible, setFormModalVisible] = useState(false);
  const [editingUser, setEditingUser] = useState<User | null>(null);
  const [saving, setSaving] = useState(false);
  const [activeFormTab, setActiveFormTab] = useState("info");

  // User form fields
  const [userForm, setUserForm] = useState({
    userName: "",
    name: "",
    surname: "",
    email: "",
    phoneNumber: "",
    password: "",
    isActive: true,
    lockoutEnabled: true,
  });

  // Roles
  const [allRoles, setAllRoles] = useState<Role[]>([]);
  const [userRoleNames, setUserRoleNames] = useState<string[]>([]);

  // Permission modal
  const [permModalVisible, setPermModalVisible] = useState(false);
  const [permUserId, setPermUserId] = useState<string>("");
  const [permUserName, setPermUserName] = useState<string>("");
  const [permGroups, setPermGroups] = useState<PermissionGroup[]>([]);
  const [permActiveGroup, setPermActiveGroup] = useState<string>("");
  const [permSaving, setPermSaving] = useState(false);

  // --- Fetch users ---
  const fetchUsers = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/identity/users", {
        params: { skipCount: 0, maxResultCount: 100 },
      });
      setUsers(response.data.items);
    } catch (error) {
      console.error("Kullanıcılar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  // --- Fetch all roles ---
  const fetchRoles = async () => {
    try {
      const response = await apiClient.get("/api/identity/roles", {
        params: { skipCount: 0, maxResultCount: 100 },
      });
      setAllRoles(response.data.items);
    } catch (error) {
      console.error("Roller yüklenemedi:", error);
    }
  };

  // --- Fetch user roles ---
  const fetchUserRoles = async (userId: string) => {
    try {
      const response = await apiClient.get(
        `/api/identity/users/${userId}/roles`
      );
      setUserRoleNames(
        response.data.items.map((r: { name: string }) => r.name)
      );
    } catch (error) {
      console.error("Kullanıcı rolleri yüklenemedi:", error);
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) {
      fetchUsers();
      fetchRoles();
    }
  }, [auth.isAuthenticated]);

  // --- New user ---
  const handleNew = () => {
    setEditingUser(null);
    setUserForm({
      userName: "",
      name: "",
      surname: "",
      email: "",
      phoneNumber: "",
      password: "",
      isActive: true,
      lockoutEnabled: true,
    });
    setUserRoleNames([]);
    setActiveFormTab("info");
    setFormModalVisible(true);
  };

  // --- Edit user ---
  const handleEdit = async (user: User) => {
    setEditingUser(user);
    setUserForm({
      userName: user.userName,
      name: user.name || "",
      surname: user.surname || "",
      email: user.email,
      phoneNumber: user.phoneNumber || "",
      password: "",
      isActive: user.isActive,
      lockoutEnabled: user.lockoutEnabled,
    });
    await fetchUserRoles(user.id);
    setActiveFormTab("info");
    setFormModalVisible(true);
  };

  // --- Save user ---
  const handleSaveUser = async () => {
    if (!userForm.userName || !userForm.email) {
      message.warning("Kullanıcı adı ve e-posta zorunludur.");
      return;
    }
    if (!editingUser && !userForm.password) {
      message.warning("Yeni kullanıcı için şifre zorunludur.");
      return;
    }

    setSaving(true);
    try {
      const payload: any = {
        userName: userForm.userName,
        name: userForm.name,
        surname: userForm.surname,
        email: userForm.email,
        phoneNumber: userForm.phoneNumber,
        isActive: userForm.isActive,
        lockoutEnabled: userForm.lockoutEnabled,
        roleNames: userRoleNames,
      };

      if (userForm.password) {
        payload.password = userForm.password;
      }

      if (editingUser) {
        await apiClient.put(`/api/identity/users/${editingUser.id}`, payload);
        message.success("Kullanıcı güncellendi.");
      } else {
        await apiClient.post("/api/identity/users", payload);
        message.success("Kullanıcı oluşturuldu.");
      }

      setFormModalVisible(false);
      fetchUsers();
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "Kayıt hatası"
      );
    } finally {
      setSaving(false);
    }
  };

  // --- Delete user ---
  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/identity/users/${id}`);
      message.success("Kullanıcı silindi.");
      fetchUsers();
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "Silme hatası"
      );
    }
  };

  // --- Permissions ---
  const openPermissions = async (user: User) => {
    setPermUserId(user.id);
    setPermUserName(user.userName);
    setPermSaving(false);
    try {
      const response = await apiClient.get(
        "/api/permission-management/permissions",
        {
          params: { providerName: "U", providerKey: user.id },
        }
      );
      const groups: PermissionGroup[] = response.data.groups;
      setPermGroups(groups);
      if (groups.length > 0) {
        setPermActiveGroup(groups[0].name);
      }
      setPermModalVisible(true);
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "İzinler yüklenemedi"
      );
    }
  };

  // Toggle permission
  const togglePermission = (groupName: string, permName: string) => {
    setPermGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        return {
          ...g,
          permissions: g.permissions.map((p) => {
            if (p.name === permName) {
              return { ...p, isGranted: !p.isGranted };
            }
            return p;
          }),
        };
      })
    );
  };

  // Toggle all in group
  const toggleGroupAll = (groupName: string, checked: boolean) => {
    setPermGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        return {
          ...g,
          permissions: g.permissions.map((p) => ({
            ...p,
            isGranted: checked,
          })),
        };
      })
    );
  };

  // Toggle all permissions
  const toggleAll = (checked: boolean) => {
    setPermGroups((prev) =>
      prev.map((g) => ({
        ...g,
        permissions: g.permissions.map((p) => ({
          ...p,
          isGranted: checked,
        })),
      }))
    );
  };

  // Save permissions
  const savePermissions = async () => {
    setPermSaving(true);
    try {
      const permissions: { name: string; isGranted: boolean }[] = [];
      permGroups.forEach((g) => {
        g.permissions.forEach((p) => {
          permissions.push({ name: p.name, isGranted: p.isGranted });
        });
      });

      await apiClient.put(
        "/api/permission-management/permissions",
        { permissions },
        {
          params: { providerName: "U", providerKey: permUserId },
        }
      );
      message.success("İzinler kaydedildi.");
      setPermModalVisible(false);
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "İzin kayıt hatası"
      );
    } finally {
      setPermSaving(false);
    }
  };

  // All granted check
  const allGranted = permGroups.every((g) =>
    g.permissions.every((p) => p.isGranted)
  );

  // --- Columns ---
  const columns = [
    {
      title: "Kullanıcı Adı",
      dataIndex: "userName",
      key: "userName",
      width: 180,
    },
    {
      title: "E-posta",
      dataIndex: "email",
      key: "email",
      width: 250,
    },
    {
      title: "Telefon",
      dataIndex: "phoneNumber",
      key: "phoneNumber",
      width: 150,
    },
    {
      title: "Durum",
      dataIndex: "isActive",
      key: "isActive",
      width: 80,
      render: (v: boolean) =>
        v ? <Tag color="green">Aktif</Tag> : <Tag color="red">Pasif</Tag>,
    },
    {
      title: "İşlemler",
      key: "actions",
      width: 200,
      render: (_: any, record: User) => (
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
          <Popconfirm
            title="Bu kullanıcıyı silmek istediğinize emin misiniz?"
            onConfirm={() => handleDelete(record.id)}
            okText="Evet"
            cancelText="Hayır"
          >
            <Button
              type="link"
              danger
              icon={<DeleteOutlined />}
              size="small"
            >
              Sil
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  // --- Active permission group ---
  const activeGroup = permGroups.find((g) => g.name === permActiveGroup);
  const activeGroupAllGranted = activeGroup
    ? activeGroup.permissions.every((p) => p.isGranted)
    : false;

  // --- Render ---
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
            <h3 style={{ margin: 0 }}>Kullanıcılar</h3>
            <Button
              icon={<ReloadOutlined />}
              onClick={fetchUsers}
              size="small"
              title="Yenile"
            />
            <Button
              type="primary"
              icon={<PlusOutlined />}
              onClick={handleNew}
              size="small"
            >
              Yeni Kullanıcı
            </Button>
          </Space>
        </div>

        <Table
          columns={columns}
          dataSource={users}
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

      {/* Kullanıcı Formu Modalı */}
      <Modal
        title={editingUser ? "Düzenle" : "Yeni Kullanıcı"}
        open={formModalVisible}
        onCancel={() => setFormModalVisible(false)}
        width={500}
        footer={
          <Space>
            <Button onClick={() => setFormModalVisible(false)}>Vazgeç</Button>
            <Button type="primary" onClick={handleSaveUser} loading={saving}>
              Kaydet
            </Button>
          </Space>
        }
        destroyOnClose
      >
        <Tabs
          activeKey={activeFormTab}
          onChange={setActiveFormTab}
          items={[
            {
              key: "info",
              label: "Kullanıcı Bilgileri",
              children: (
                <Form layout="vertical">
                  <Form.Item label="Kullanıcı adı" required>
                    <Input
                      value={userForm.userName}
                      onChange={(e) =>
                        setUserForm({ ...userForm, userName: e.target.value })
                      }
                    />
                  </Form.Item>
                  <Form.Item label="Adı">
                    <Input
                      value={userForm.name}
                      onChange={(e) =>
                        setUserForm({ ...userForm, name: e.target.value })
                      }
                    />
                  </Form.Item>
                  <Form.Item label="Soyadı">
                    <Input
                      value={userForm.surname}
                      onChange={(e) =>
                        setUserForm({ ...userForm, surname: e.target.value })
                      }
                    />
                  </Form.Item>
                  <Form.Item
                    label="Şifre"
                    required={!editingUser}
                  >
                    <Input.Password
                      value={userForm.password}
                      onChange={(e) =>
                        setUserForm({ ...userForm, password: e.target.value })
                      }
                      placeholder={
                        editingUser
                          ? "Değiştirmek istemiyorsanız boş bırakın"
                          : ""
                      }
                    />
                  </Form.Item>
                  <Form.Item label="E-posta adresi" required>
                    <Input
                      value={userForm.email}
                      onChange={(e) =>
                        setUserForm({ ...userForm, email: e.target.value })
                      }
                    />
                  </Form.Item>
                  <Form.Item label="Telefon numarası">
                    <Input
                      value={userForm.phoneNumber}
                      onChange={(e) =>
                        setUserForm({
                          ...userForm,
                          phoneNumber: e.target.value,
                        })
                      }
                    />
                  </Form.Item>
                  <Space direction="vertical">
                    <Checkbox
                      checked={userForm.isActive}
                      onChange={(e) =>
                        setUserForm({
                          ...userForm,
                          isActive: e.target.checked,
                        })
                      }
                    >
                      Aktif
                    </Checkbox>
                    <Checkbox
                      checked={userForm.lockoutEnabled}
                      onChange={(e) =>
                        setUserForm({
                          ...userForm,
                          lockoutEnabled: e.target.checked,
                        })
                      }
                    >
                      Başarısız giriş denemeleri sonrası hesabı kilitleme
                    </Checkbox>
                  </Space>
                </Form>
              ),
            },
            {
              key: "roles",
              label: "Roller",
              children: (
                <div>
                  {allRoles.map((role) => (
                    <div key={role.id} style={{ marginBottom: 8 }}>
                      <Checkbox
                        checked={userRoleNames.includes(role.name)}
                        onChange={(e) => {
                          if (e.target.checked) {
                            setUserRoleNames([...userRoleNames, role.name]);
                          } else {
                            setUserRoleNames(
                              userRoleNames.filter((r) => r !== role.name)
                            );
                          }
                        }}
                      >
                        {role.name}
                      </Checkbox>
                    </div>
                  ))}
                </div>
              ),
            },
          ]}
        />
      </Modal>

      {/* İzin Yönetimi Modalı */}
      <Modal
        title={`İzinler - ${permUserName}`}
        open={permModalVisible}
        onCancel={() => setPermModalVisible(false)}
        width={700}
        footer={
          <Space>
            <Button onClick={() => setPermModalVisible(false)}>Vazgeç</Button>
            <Button
              type="primary"
              onClick={savePermissions}
              loading={permSaving}
            >
              Kaydet
            </Button>
          </Space>
        }
        destroyOnClose
      >
        {/* Tüm izinleri ver */}
        <div style={{ marginBottom: 16, borderBottom: "1px solid #f0f0f0", paddingBottom: 12 }}>
          <Checkbox
            checked={allGranted}
            onChange={(e) => toggleAll(e.target.checked)}
          >
            Tüm izinleri ver
          </Checkbox>
        </div>

        <div style={{ display: "flex", gap: 16 }}>
          {/* Sol: Grup listesi */}
          <div
            style={{
              width: 220,
              borderRight: "1px solid #f0f0f0",
              paddingRight: 16,
            }}
          >
            {permGroups.map((g) => {
              const grantedCount = g.permissions.filter(
                (p) => p.isGranted
              ).length;
              return (
                <div
                  key={g.name}
                  onClick={() => setPermActiveGroup(g.name)}
                  style={{
                    padding: "8px 12px",
                    cursor: "pointer",
                    borderRadius: 6,
                    marginBottom: 4,
                    backgroundColor:
                      permActiveGroup === g.name ? "#1677ff" : "transparent",
                    color: permActiveGroup === g.name ? "#fff" : "#333",
                    fontWeight: permActiveGroup === g.name ? 600 : 400,
                  }}
                >
                  {g.displayName} ({grantedCount})
                </div>
              );
            })}
          </div>

          {/* Sağ: İzin listesi */}
          <div style={{ flex: 1 }}>
            {activeGroup && (
              <>
                <h4 style={{ marginTop: 0 }}>{activeGroup.displayName}</h4>
                <div
                  style={{
                    marginBottom: 12,
                    borderBottom: "1px solid #f0f0f0",
                    paddingBottom: 8,
                  }}
                >
                  <Checkbox
                    checked={activeGroupAllGranted}
                    onChange={(e) =>
                      toggleGroupAll(activeGroup.name, e.target.checked)
                    }
                  >
                    Hepsini seç
                  </Checkbox>
                </div>
                {activeGroup.permissions.map((perm) => (
                  <div
                    key={perm.name}
                    style={{
                      marginBottom: 6,
                      paddingLeft: perm.parentName ? 24 : 0,
                    }}
                  >
                    <Checkbox
                      checked={perm.isGranted}
                      onChange={() =>
                        togglePermission(activeGroup.name, perm.name)
                      }
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