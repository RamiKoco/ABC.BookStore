import { useState, useEffect } from "react";
import { Modal, Table, Space, Button, Input, Form, message, Popconfirm } from "antd";
import {
  SearchOutlined,
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";

interface Ilce {
  id: string;
  kod: string;
  ad: string;
}

interface IlceSelectModalProps {
  visible: boolean;
  ilId: string | null;
  title?: string;
  onSelect: (item: Ilce) => void;
  onCancel: () => void;
}

const emptyIlceForm = {
  kod: "",
  ad: "",
  durum: true,
};

export default function IlceSelectModal({
  visible,
  ilId,
  title = "İlçe Kartları",
  onSelect,
  onCancel,
}: IlceSelectModalProps) {
  const [data, setData] = useState<Ilce[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState("");
  const [selectedRowKey, setSelectedRowKey] = useState<string | null>(null);

  // CRUD state
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<Ilce | null>(null);
  const [ilceForm, setIlceForm] = useState({ ...emptyIlceForm });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (visible && ilId) {
      fetchData();
      setSearchText("");
      setSelectedRowKey(null);
    }
  }, [visible, ilId]);

  const fetchData = async () => {
    if (!ilId) return;
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/ilce", {
        params: {
          ilId,
          durum: true,
          skipCount: 0,
          maxResultCount: 5000,
        },
      });
      setData(response.data.items);
    } catch (error) {
      console.error("İlçeler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/ilce/code", {
        params: { ilId, durum: true },
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
    setIlceForm({ ...emptyIlceForm, kod: newCode });
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
    setIlceForm({
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
      await apiClient.delete(`/api/app/ilce/${selectedRowKey}`);
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
    if (!ilceForm.kod || !ilceForm.ad) {
      message.warning("Kod ve Ad alanları zorunludur.");
      return;
    }
    setSaving(true);
    try {
      if (editingItem) {
        await apiClient.put(`/api/app/ilce/${editingItem.id}`, {
          kod: ilceForm.kod,
          ad: ilceForm.ad,
          ilId: ilId,
          durum: ilceForm.durum,
        });
        message.success("Kayıt güncellendi.");
      } else {
        await apiClient.post("/api/app/ilce", {
          kod: ilceForm.kod,
          ad: ilceForm.ad,
          ilId: ilId,
          durum: ilceForm.durum,
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

  const handleRowDoubleClick = (record: Ilce) => {
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
        title={editingItem ? "İlçe Düzenle" : "Yeni İlçe"}
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
              value={ilceForm.kod}
              onChange={(e) =>
                setIlceForm({ ...ilceForm, kod: e.target.value })
              }
              maxLength={20}
            />
          </Form.Item>
          <Form.Item label="Ad" required>
            <Input
              value={ilceForm.ad}
              onChange={(e) =>
                setIlceForm({ ...ilceForm, ad: e.target.value })
              }
              maxLength={128}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}