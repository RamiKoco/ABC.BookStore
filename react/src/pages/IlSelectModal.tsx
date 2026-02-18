import { useState, useEffect } from "react";
import { Modal, Table, Space, Button, Input, Form, message, Popconfirm } from "antd";
import {
  SearchOutlined,
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";

interface Il {
  id: string;
  kod: string;
  ad: string;
}

interface IlSelectModalProps {
  visible: boolean;
  title?: string;
  onSelect: (item: Il) => void;
  onCancel: () => void;
}

const emptyIlForm = {
  kod: "",
  ad: "",
  durum: true,
};

export default function IlSelectModal({
  visible,
  title = "İl Kartları",
  onSelect,
  onCancel,
}: IlSelectModalProps) {
  const [data, setData] = useState<Il[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState("");
  const [selectedRowKey, setSelectedRowKey] = useState<string | null>(null);

  // CRUD state
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<Il | null>(null);
  const [ilForm, setIlForm] = useState({ ...emptyIlForm });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (visible) {
      fetchData();
      setSearchText("");
      setSelectedRowKey(null);
    }
  }, [visible]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/il", {
        params: {
          durum: true,
          skipCount: 0,
          maxResultCount: 5000,
        },
      });
      setData(response.data.items);
    } catch (error) {
      console.error("İller yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/il/code", {
        params: { durum: true },
      });
      return typeof response.data === "string"
        ? response.data
        : String(response.data);
    } catch {
      return "";
    }
  };

  const filteredData = data.filter(
    (item) =>
      item.kod.toLowerCase().includes(searchText.toLowerCase()) ||
      item.ad.toLowerCase().includes(searchText.toLowerCase())
  );

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 150 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
  ];

  const handleNew = async () => {
    const newCode = await getNewCode();
    setIlForm({ ...emptyIlForm, kod: newCode });
    setEditingItem(null);
    setEditModalVisible(true);
  };

  const handleEditItem = () => {
    const selected = data.find((d) => d.id === selectedRowKey);
    if (!selected) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    setEditingItem(selected);
    setIlForm({
      kod: selected.kod,
      ad: selected.ad,
      durum: true,
    });
    setEditModalVisible(true);
  };

  const handleDelete = async () => {
    if (!selectedRowKey) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    try {
      await apiClient.delete(`/api/app/il/${selectedRowKey}`);
      message.success("Kayıt silindi.");
      setSelectedRowKey(null);
      fetchData();
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "Silme hatası"
      );
    }
  };

  const handleSave = async () => {
    if (!ilForm.kod || !ilForm.ad) {
      message.warning("Kod ve Ad alanları zorunludur.");
      return;
    }
    setSaving(true);
    try {
      if (editingItem) {
        await apiClient.put(`/api/app/il/${editingItem.id}`, {
          kod: ilForm.kod,
          ad: ilForm.ad,
          durum: ilForm.durum,
        });
        message.success("Kayıt güncellendi.");
      } else {
        await apiClient.post("/api/app/il", {
          kod: ilForm.kod,
          ad: ilForm.ad,
          durum: ilForm.durum,
        });
        message.success("Kayıt oluşturuldu.");
      }
      setEditModalVisible(false);
      fetchData();
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "Kayıt hatası"
      );
    } finally {
      setSaving(false);
    }
  };

  const handleRowDoubleClick = (record: Il) => {
    onSelect(record);
  };

  const handleOk = () => {
    const selected = data.find((d) => d.id === selectedRowKey);
    if (selected) onSelect(selected);
  };

  return (
    <>
      <Modal
        title={title}
        open={visible}
        onCancel={onCancel}
        width={550}
        footer={
          <Space>
            <Button onClick={onCancel}>İptal</Button>
            <Button
              type="primary"
              onClick={handleOk}
              disabled={!selectedRowKey}
            >
              Seç
            </Button>
          </Space>
        }
        destroyOnClose
      >
        {/* Toolbar */}
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            marginBottom: 12,
          }}
        >
          <Space>
            <Button icon={<PlusOutlined />} onClick={handleNew}>
              Yeni
            </Button>
            <Button
              icon={<EditOutlined />}
              onClick={handleEditItem}
              disabled={!selectedRowKey}
            >
              Güncelle
            </Button>
            <Popconfirm
              title="Bu kaydı silmek istediğinize emin misiniz?"
              onConfirm={handleDelete}
              okText="Evet"
              cancelText="Hayır"
              disabled={!selectedRowKey}
            >
              <Button
                icon={<DeleteOutlined />}
                danger
                disabled={!selectedRowKey}
              >
                Sil
              </Button>
            </Popconfirm>
          </Space>
        </div>

        {/* Arama */}
        <div style={{ marginBottom: 12 }}>
          <Input
            placeholder="Ara..."
            prefix={<SearchOutlined />}
            value={searchText}
            onChange={(e) => setSearchText(e.target.value)}
            allowClear
          />
        </div>

        {/* Tablo */}
        <Table
          columns={columns}
          dataSource={filteredData}
          rowKey="id"
          loading={loading}
          size="small"
          pagination={{ pageSize: 10, showSizeChanger: false }}
          scroll={{ y: 300 }}
          rowSelection={{
            type: "radio",
            selectedRowKeys: selectedRowKey ? [selectedRowKey] : [],
            onChange: (keys) => setSelectedRowKey(keys[0] as string),
          }}
          onRow={(record) => ({
            onDoubleClick: () => handleRowDoubleClick(record),
            onClick: () => setSelectedRowKey(record.id),
            style: { cursor: "pointer" },
          })}
        />
      </Modal>

      {/* Yeni / Düzenle Modalı */}
      <Modal
        title={editingItem ? "İl Düzenle" : "Yeni İl"}
        open={editModalVisible}
        onCancel={() => setEditModalVisible(false)}
        footer={
          <Space>
            <Button onClick={() => setEditModalVisible(false)}>İptal</Button>
            <Button type="primary" onClick={handleSave} loading={saving}>
              {editingItem ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
        width={400}
        destroyOnClose
      >
        <Form layout="vertical">
          <Form.Item label="Kod" required>
            <Input
              value={ilForm.kod}
              onChange={(e) =>
                setIlForm({ ...ilForm, kod: e.target.value })
              }
              maxLength={20}
            />
          </Form.Item>
          <Form.Item label="Ad" required>
            <Input
              value={ilForm.ad}
              onChange={(e) =>
                setIlForm({ ...ilForm, ad: e.target.value })
              }
              maxLength={128}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}