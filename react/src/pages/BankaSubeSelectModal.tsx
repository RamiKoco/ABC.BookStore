import { useState, useEffect } from "react";
import {
  Modal,
  Table,
  Space,
  Button,
  Input,
  Form,
  message,
  Popconfirm,
  Tabs,
} from "antd";
import {
  SearchOutlined,
  PlusOutlined,
  EditOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import apiClient from "../config/api";
import {
  DevGridLayout,
  DevGridLayoutItem,
  DevTextEdit,
  DevCheckEdit,
  DevMemoEdit,
  DevButtonEdit,
  DevTabEdit,
} from "../components/dev";
import OzelKodSelectModal from "./OzelKodSelectModal";

interface BankaSube {
  id: string;
  kod: string;
  ad: string;
  ozelKod1Adi: string | null;
  ozelKod2Adi: string | null;
  ozelKod3Adi: string | null;
  ozelKod4Adi: string | null;
  ozelKod5Adi: string | null;
}

interface BankaSubeSelectModalProps {
  visible: boolean;
  bankaId: string | null;
  bankaAdi?: string;
  onCancel: () => void;
}

const emptyForm = {
  kod: "",
  ad: "",
  aciklama: "",
  durum: true,
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  ozelKod3Id: null as string | null,
  ozelKod4Id: null as string | null,
  ozelKod5Id: null as string | null,
};

const KART_TURU_BANKA_SUBE = 3;

export default function BankaSubeSelectModal({
  visible,
  bankaId,
  bankaAdi = "",
  onCancel,
}: BankaSubeSelectModalProps) {
  const [data, setData] = useState<BankaSube[]>([]);
  const [loading, setLoading] = useState(false);
  const [searchText, setSearchText] = useState("");
  const [selectedRowKey, setSelectedRowKey] = useState<string | null>(null);
  const [durumFilter, setDurumFilter] = useState(true);

  // CRUD state
  const [editModalVisible, setEditModalVisible] = useState(false);
  const [editingItem, setEditingItem] = useState<BankaSube | null>(null);
  const [subeForm, setSubeForm] = useState({ ...emptyForm });
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // OzelKod modal state
  const [ozelKodModalVisible, setOzelKodModalVisible] = useState(false);
  const [activeKodTuru, setActiveKodTuru] = useState<number>(1);
  const [ozelKodNames, setOzelKodNames] = useState<Record<string, string>>({
    ozelKod1: "",
    ozelKod2: "",
    ozelKod3: "",
    ozelKod4: "",
    ozelKod5: "",
  });

  useEffect(() => {
    if (visible && bankaId) {
      fetchData();
      setSearchText("");
      setSelectedRowKey(null);
    }
  }, [visible, bankaId, durumFilter]);

  const fetchData = async () => {
    if (!bankaId) return;
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/banka-sube", {
        params: {
          bankaId,
          durum: durumFilter,
          skipCount: 0,
          maxResultCount: 5000,
        },
      });
      setData(response.data.items);
    } catch (error) {
      console.error("Banka şubeleri yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/banka-sube/code", {
        params: { bankaId, durum: true },
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
    { title: "Kod", dataIndex: "kod", key: "kod", width: 180 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 180 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 130 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 130 },
    { title: "Özel Kod-3", dataIndex: "ozelKod3Adi", key: "ozelKod3Adi", width: 130 },
    { title: "Özel Kod-4", dataIndex: "ozelKod4Adi", key: "ozelKod4Adi", width: 130 },
    { title: "Özel Kod-5", dataIndex: "ozelKod5Adi", key: "ozelKod5Adi", width: 130 },
  ];

  // --- New ---
  const handleNew = async () => {
    const newCode = await getNewCode();
    setSubeForm({ ...emptyForm, kod: newCode });
    setEditingItem(null);
    setOzelKodNames({
      ozelKod1: "",
      ozelKod2: "",
      ozelKod3: "",
      ozelKod4: "",
      ozelKod5: "",
    });
    setActiveTab("genel");
    setEditModalVisible(true);
  };

  // --- Edit ---
  const handleEditItem = async () => {
    if (!selectedRowKey) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    try {
      const response = await apiClient.get(`/api/app/banka-sube/${selectedRowKey}`);
      const detail = response.data;
      setEditingItem(detail);
      setSubeForm({
        kod: detail.kod,
        ad: detail.ad,
        aciklama: detail.aciklama || "",
        durum: detail.durum,
        ozelKod1Id: detail.ozelKod1Id,
        ozelKod2Id: detail.ozelKod2Id,
        ozelKod3Id: detail.ozelKod3Id,
        ozelKod4Id: detail.ozelKod4Id,
        ozelKod5Id: detail.ozelKod5Id,
      });
      setOzelKodNames({
        ozelKod1: detail.ozelKod1Adi || "",
        ozelKod2: detail.ozelKod2Adi || "",
        ozelKod3: detail.ozelKod3Adi || "",
        ozelKod4: detail.ozelKod4Adi || "",
        ozelKod5: detail.ozelKod5Adi || "",
      });
      setActiveTab("genel");
      setEditModalVisible(true);
    } catch (error) {
      message.error("Şube detayı yüklenemedi.");
    }
  };

  // --- Delete ---
  const handleDelete = async () => {
    if (!selectedRowKey) {
      message.warning("Lütfen bir kayıt seçin.");
      return;
    }
    try {
      await apiClient.delete(`/api/app/banka-sube/${selectedRowKey}`);
      message.success("Kayıt silindi.");
      setSelectedRowKey(null);
      fetchData();
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  // --- Save ---
  const handleSave = async () => {
    if (!subeForm.kod || !subeForm.ad) {
      message.warning("Kod ve Ad alanları zorunludur.");
      return;
    }
    setSaving(true);
    try {
      const payload = {
        ...subeForm,
        bankaId,
      };

      if (editingItem) {
        await apiClient.put(`/api/app/banka-sube/${editingItem.id}`, payload);
        message.success("Kayıt güncellendi.");
      } else {
        await apiClient.post("/api/app/banka-sube", payload);
        message.success("Kayıt oluşturuldu.");
      }
      setEditModalVisible(false);
      fetchData();
    } catch (error: any) {
      message.error(error?.response?.data?.error?.message || "Kayıt hatası");
    } finally {
      setSaving(false);
    }
  };

  // --- OzelKod ---
  const openOzelKodModal = (kodTuru: number) => {
    setActiveKodTuru(kodTuru);
    setOzelKodModalVisible(true);
  };

  const handleOzelKodSelect = (item: { id: string; ad: string }) => {
    const idField = `ozelKod${activeKodTuru}Id` as keyof typeof subeForm;
    const nameField = `ozelKod${activeKodTuru}` as keyof typeof ozelKodNames;
    setSubeForm({ ...subeForm, [idField]: item.id });
    setOzelKodNames({ ...ozelKodNames, [nameField]: item.ad });
    setOzelKodModalVisible(false);
  };

  const clearOzelKod = (kodTuru: number) => {
    const idField = `ozelKod${kodTuru}Id` as keyof typeof subeForm;
    const nameField = `ozelKod${kodTuru}` as keyof typeof ozelKodNames;
    setSubeForm({ ...subeForm, [idField]: null });
    setOzelKodNames({ ...ozelKodNames, [nameField]: "" });
  };

  // --- Tab items for edit modal ---
  const tabItems = [
    {
      key: "genel",
      label: "Genel Bilgiler",
      children: (
        <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
          <DevGridLayoutItem>
            <DevTextEdit
              label="Kod"
              value={subeForm.kod}
              onChange={(v) => setSubeForm({ ...subeForm, kod: v })}
              required
              maxLength={20}
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <div style={{ display: "flex", justifyContent: "flex-end", paddingTop: 24 }}>
              <DevCheckEdit
                value={subeForm.durum}
                onChange={(v) => setSubeForm({ ...subeForm, durum: v })}
                checkedText="Aktif"
                uncheckedText="Pasif"
              />
            </div>
          </DevGridLayoutItem>
          <DevGridLayoutItem columnSpan={2}>
            <DevTextEdit
              label="Ad"
              value={subeForm.ad}
              onChange={(v) => setSubeForm({ ...subeForm, ad: v })}
              required
              maxLength={128}
            />
          </DevGridLayoutItem>
        </DevGridLayout>
      ),
    },
    {
      key: "ozelKodlar",
      label: "Özel Kodlar",
      children: (
        <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
          <DevGridLayoutItem>
            <DevButtonEdit
              label="Özel Kod-1"
              value={ozelKodNames.ozelKod1}
              onButtonClick={() => openOzelKodModal(1)}
              onClear={() => clearOzelKod(1)}
              placeholder="Kart Seçin"
              readOnly
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevButtonEdit
              label="Özel Kod-2"
              value={ozelKodNames.ozelKod2}
              onButtonClick={() => openOzelKodModal(2)}
              onClear={() => clearOzelKod(2)}
              placeholder="Kart Seçin"
              readOnly
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevButtonEdit
              label="Özel Kod-3"
              value={ozelKodNames.ozelKod3}
              onButtonClick={() => openOzelKodModal(3)}
              onClear={() => clearOzelKod(3)}
              placeholder="Kart Seçin"
              readOnly
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevButtonEdit
              label="Özel Kod-4"
              value={ozelKodNames.ozelKod4}
              onButtonClick={() => openOzelKodModal(4)}
              onClear={() => clearOzelKod(4)}
              placeholder="Kart Seçin"
              readOnly
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevButtonEdit
              label="Özel Kod-5"
              value={ozelKodNames.ozelKod5}
              onButtonClick={() => openOzelKodModal(5)}
              onClear={() => clearOzelKod(5)}
              placeholder="Kart Seçin"
              readOnly
            />
          </DevGridLayoutItem>
        </DevGridLayout>
      ),
    },
    {
      key: "aciklama",
      label: "Açıklama",
      children: (
        <DevMemoEdit
          label="Açıklama"
          value={subeForm.aciklama}
          onChange={(v) => setSubeForm({ ...subeForm, aciklama: v })}
          maxLength={500}
        />
      ),
    },
  ];

  return (
    <>
      {/* Ana Liste Modalı */}
      <Modal
        title={`Banka Şube Kartları (${bankaAdi})`}
        open={visible}
        onCancel={onCancel}
        width={900}
        footer={
          <Button onClick={onCancel}>Kapat</Button>
        }
        destroyOnClose
      >
        {/* Toolbar */}
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
            marginBottom: 12,
            alignItems: "center",
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
          <DevCheckEdit
            value={durumFilter}
            onChange={(v) => setDurumFilter(v)}
            checkedText="Aktif"
            uncheckedText="Pasif"
          />
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
          scroll={{ y: 350, x: 1000 }}
          rowSelection={{
            type: "radio",
            selectedRowKeys: selectedRowKey ? [selectedRowKey] : [],
            onChange: (keys) => setSelectedRowKey(keys[0] as string),
          }}
          onRow={(record) => ({
            onDoubleClick: () => {
              setSelectedRowKey(record.id);
              handleEditItem();
            },
            onClick: () => setSelectedRowKey(record.id),
            style: { cursor: "pointer" },
          })}
        />
      </Modal>

      {/* Yeni / Düzenle Modalı */}
      <Modal
        title={editingItem ? "Banka Şube Düzenle" : "Yeni Banka Şube"}
        open={editModalVisible}
        onCancel={() => setEditModalVisible(false)}
        width={550}
        footer={
          <Space>
            <Button onClick={() => setEditModalVisible(false)}>Vazgeç</Button>
            <Button type="primary" onClick={handleSave} loading={saving}>
              {editingItem ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
        destroyOnClose
      >
        <Form layout="vertical">
          <DevTabEdit
            items={tabItems}
            activeKey={activeTab}
            onChange={setActiveTab}
          />
        </Form>
      </Modal>

      {/* OzelKod Seçim Modalı */}
      <OzelKodSelectModal
        visible={ozelKodModalVisible}
        kodTuru={activeKodTuru}
        kartTuru={KART_TURU_BANKA_SUBE}
        title={`Özel Kod-${activeKodTuru} Kartları`}
        onSelect={handleOzelKodSelect}
        onCancel={() => setOzelKodModalVisible(false)}
      />
    </>
  );
}