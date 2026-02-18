import { useState, useEffect } from "react";
import { Modal, Table, Space, Button, Input, Form, message, Popconfirm } from "antd";
import {
  SearchOutlined,
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";

interface OzelKod {
  id: string;
  kod: string;
  ad: string;
  aciklama: string;
}

interface OzelKodSelectModalProps {
  visible: boolean;
  kodTuru: number;
  kartTuru: number;
  title?: string;
  onSelect: (item: OzelKod) => void;
  onCancel: () => void;
}

const emptyOzelKodForm = {
  kod: "",
  ad: "",
  aciklama: "",
  durum: true,
};

export default function OzelKodSelectModal({
  visible,
  kodTuru,
  kartTuru,
  title = "Özel Kod Kartları",
  onSelect,
  onCancel,
}: OzelKodSelectModalProps) {
  const [data, setData] = useState<OzelKod[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState("");
  const [selectedRowKey, setSelectedRowKey] = useState<string | null>(null);

  // CRUD state
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<OzelKod | null>(null);
  const [ozelKodForm, setOzelKodForm] = useState({ ...emptyOzelKodForm });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (visible) {
      fetchData();
      setSearchText("");
      setSelectedRowKey(null);
    }
  }, [visible, kodTuru, kartTuru]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/ozel-kod", {
        params: {
          kodTuru,
          kartTuru,
          durum: true,
          skipCount: 0,
          maxResultCount: 5000,
        },
      });
      setData(response.data.items);
    } catch (error) {
      console.error("Özel kodlar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/ozel-kod/code", {
        params: { kodTuru, kartTuru, durum: true },
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
      item.ad.toLowerCase().includes(searchText.toLowerCase()) ||
      (item.aciklama || "").toLowerCase().includes(searchText.toLowerCase())
  );

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 180 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
    { title: "Açıklama", dataIndex: "aciklama", key: "aciklama" },
  ];

  // Yeni kayıt
  const handleNew = async () => {
    const newCode = await getNewCode();
    setOzelKodForm({ ...emptyOzelKodForm, kod: newCode });
    setEditingItem(null);
    setEditModalVisible(true);
  };

  // Düzenle
  const handleEditItem = () => {
    const selected = data.find((d) => d.id === selectedRowKey);
    if (!selected) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    setEditingItem(selected);
    setOzelKodForm({
      kod: selected.kod,
      ad: selected.ad,
      aciklama: selected.aciklama || "",
      durum: true,
    });
    setEditModalVisible(true);
  };

  // Sil
  const handleDelete = async () => {
    if (!selectedRowKey) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    try {
      await apiClient.delete(`/api/app/ozel-kod/${selectedRowKey}`);
      message.success("Kayıt silindi.");
      setSelectedRowKey(null);
      fetchData();
    } catch (error: any) {
      message.error(
        error?.response?.data?.error?.message || "Silme hatası"
      );
    }
  };

  // Kaydet / Güncelle
  const handleSave = async () => {
    if (!ozelKodForm.kod || !ozelKodForm.ad) {
      message.warning("Kod ve Ad alanları zorunludur.");
      return;
    }
    setSaving(true);
    try {
      if (editingItem) {
        await apiClient.put(`/api/app/ozel-kod/${editingItem.id}`, {
          kod: ozelKodForm.kod,
          ad: ozelKodForm.ad,
          aciklama: ozelKodForm.aciklama,
          durum: ozelKodForm.durum,
        });
        message.success("Kayıt güncellendi.");
      } else {
        await apiClient.post("/api/app/ozel-kod", {
          kod: ozelKodForm.kod,
          ad: ozelKodForm.ad,
          kodTuru: kodTuru,
          kartTuru: kartTuru,
          aciklama: ozelKodForm.aciklama,
          durum: ozelKodForm.durum,
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

  const handleRowDoubleClick = (record: OzelKod) => {
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
        width={700}
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
        title={editingItem ? "Özel Kod Düzenle" : "Yeni Özel Kod"}
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
        width={450}
        destroyOnClose
      >
        <Form layout="vertical">
          <Form.Item label="Kod" required>
            <Input
              value={ozelKodForm.kod}
              onChange={(e) =>
                setOzelKodForm({ ...ozelKodForm, kod: e.target.value })
              }
              maxLength={20}
            />
          </Form.Item>
          <Form.Item label="Ad" required>
            <Input
              value={ozelKodForm.ad}
              onChange={(e) =>
                setOzelKodForm({ ...ozelKodForm, ad: e.target.value })
              }
              maxLength={128}
            />
          </Form.Item>
          <Form.Item label="Açıklama">
            <Input.TextArea
              value={ozelKodForm.aciklama}
              onChange={(e) =>
                setOzelKodForm({ ...ozelKodForm, aciklama: e.target.value })
              }
              maxLength={500}
              rows={3}
            />
          </Form.Item>
        </Form>
      </Modal>
    </>
  );
}