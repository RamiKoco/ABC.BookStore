import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card, Upload, Avatar, message } from "antd";
import { UserOutlined, UploadOutlined } from "@ant-design/icons";
import apiClient from "../config/api";
import {
  DevListPageLayout,
  DevGridLayout,
  DevGridLayoutItem,
  DevTextEdit,
  DevCheckEdit,
  DevMemoEdit,
  DevDataGrid,
  DevButtonEdit,
  DevTabEdit,
  DevComboBoxEdit,
  DevDateEdit,
} from "../components/dev";
import OzelKodSelectModal from "./OzelKodSelectModal";
import IlSelectModal from "./IlSelectModal";
import IlceSelectModal from "./IlceSelectModal";

interface Kisi {
  id: string;
  kod: string;
  ad: string;
  soyad: string;
  image: string;
  tcNo: string;
  telefon: string;
  email: string;
  dogumTarihi: string;
  dogumYeri: string;
  cinsiyet: number;
  kanGrubu: number;
  medeniHal: number;
  ilId: string;
  ilceId: string;
  ilAdi: string;
  ilceAdi: string;
  acikAdres: string;
  ozelKod1Id: string | null;
  ozelKod2Id: string | null;
  ozelKod1Adi: string | null;
  ozelKod2Adi: string | null;
  aciklama: string;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  soyad: "",
  image: "",
  tcNo: "",
  telefon: "",
  email: "",
  dogumTarihi: new Date().toISOString().substring(0, 10),
  dogumYeri: "",
  cinsiyet: 1,
  kanGrubu: 1,
  medeniHal: 1,
  ilId: "" as string,
  ilceId: "" as string,
  acikAdres: "",
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  aciklama: "",
  durum: true,
};

const KART_TURU_KISI = 4;

const cinsiyetOptions = [
  { value: 1, label: "Erkek" },
  { value: 2, label: "Kadın" },
];

const kanGrubuOptions = [
  { value: 1, label: "A+" },
  { value: 2, label: "A-" },
  { value: 3, label: "B+" },
  { value: 4, label: "B-" },
  { value: 5, label: "AB+" },
  { value: 6, label: "AB-" },
  { value: 7, label: "0+" },
  { value: 8, label: "0-" },
];

const medeniHalOptions = [
  { value: 1, label: "Bekar" },
  { value: 2, label: "Evli" },
  { value: 3, label: "Boşanmış" },
  { value: 4, label: "Dul" },
  { value: 5, label: "Nişanlı" },
  { value: 6, label: "Ayrı Yaşayan" },
];

export default function KisiPage() {
  const auth = useAuth();
  const [kisiler, setKisiler] = useState<Kisi[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingKisi, setEditingKisi] = useState<Kisi | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // İl / İlçe modal
  const [ilModalVisible, setIlModalVisible] = useState(false);
  const [ilceModalVisible, setIlceModalVisible] = useState(false);
  const [ilAdi, setIlAdi] = useState("");
  const [ilceAdi, setIlceAdi] = useState("");

  // OzelKod modal
  const [ozelKodModalVisible, setOzelKodModalVisible] = useState(false);
  const [activeKodTuru, setActiveKodTuru] = useState<number>(1);
  const [ozelKodNames, setOzelKodNames] = useState({
    ozelKod1: "",
    ozelKod2: "",
  });

  const fetchKisiler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/kisi", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setKisiler(response.data.items);
    } catch (error) {
      console.error("Kişiler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/kisi/code", {
        params: { durum: true },
      });
      return typeof response.data === "string"
        ? response.data
        : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchKisiler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingKisi(null);
    setIlAdi("");
    setIlceAdi("");
    setOzelKodNames({ ozelKod1: "", ozelKod2: "" });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (kisi: Kisi) => {
    try {
      const response = await apiClient.get(`/api/app/kisi/${kisi.id}`);
      const d = response.data;
      setEditingKisi(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        soyad: d.soyad || "",
        image: d.image || "",
        tcNo: d.tcNo || "",
        telefon: d.telefon || "",
        email: d.email || "",
        dogumTarihi: d.dogumTarihi ? d.dogumTarihi.substring(0, 10) : "",
        dogumYeri: d.dogumYeri || "",
        cinsiyet: d.cinsiyet,
        kanGrubu: d.kanGrubu,
        medeniHal: d.medeniHal,
        ilId: d.ilId || "",
        ilceId: d.ilceId || "",
        acikAdres: d.acikAdres || "",
        ozelKod1Id: d.ozelKod1Id,
        ozelKod2Id: d.ozelKod2Id,
        aciklama: d.aciklama || "",
        durum: d.durum,
      });
      setIlAdi(d.ilAdi || "");
      setIlceAdi(d.ilceAdi || "");
      setOzelKodNames({
        ozelKod1: d.ozelKod1Adi || "",
        ozelKod2: d.ozelKod2Adi || "",
      });
      setActiveTab("genel");
      setDrawerOpen(true);
    } catch (error) {
      console.error("Kişi detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    if (!form.ilId) {
      alert("Lütfen İl seçin.");
      return;
    }
    if (!form.ilceId) {
      alert("Lütfen İlçe seçin.");
      return;
    }
    setSaving(true);
    try {
      if (editingKisi) {
        await apiClient.put(`/api/app/kisi/${editingKisi.id}`, form);
      } else {
        await apiClient.post("/api/app/kisi", form);
      }
      setDrawerOpen(false);
      setEditingKisi(null);
      setForm({ ...emptyForm });
      fetchKisiler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/kisi/${id}`);
      fetchKisiler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingKisi(null);
    setForm({ ...emptyForm });
  };

  // İl seçimi
  const handleIlSelect = (item: { id: string; ad: string }) => {
    setForm({ ...form, ilId: item.id, ilceId: "" });
    setIlAdi(item.ad);
    setIlceAdi("");
    setIlModalVisible(false);
  };

  // İlçe seçimi
  const handleIlceSelect = (item: { id: string; ad: string }) => {
    setForm({ ...form, ilceId: item.id });
    setIlceAdi(item.ad);
    setIlceModalVisible(false);
  };

  // OzelKod
  const openOzelKodModal = (kodTuru: number) => {
    setActiveKodTuru(kodTuru);
    setOzelKodModalVisible(true);
  };

  const handleOzelKodSelect = (item: { id: string; ad: string }) => {
    const idField = `ozelKod${activeKodTuru}Id` as keyof typeof form;
    const nameField = `ozelKod${activeKodTuru}` as keyof typeof ozelKodNames;
    setForm({ ...form, [idField]: item.id });
    setOzelKodNames({ ...ozelKodNames, [nameField]: item.ad });
    setOzelKodModalVisible(false);
  };

  const clearOzelKod = (kodTuru: number) => {
    const idField = `ozelKod${kodTuru}Id` as keyof typeof form;
    const nameField = `ozelKod${kodTuru}` as keyof typeof ozelKodNames;
    setForm({ ...form, [idField]: null });
    setOzelKodNames({ ...ozelKodNames, [nameField]: "" });
  };

  // Enum label helpers
  const getCinsiyetLabel = (val: number) =>
    cinsiyetOptions.find((o) => o.value === val)?.label || "";
  const getKanGrubuLabel = (val: number) =>
    kanGrubuOptions.find((o) => o.value === val)?.label || "";
  const getMedeniHalLabel = (val: number) =>
    medeniHalOptions.find((o) => o.value === val)?.label || "";

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 120 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 130 },
    { title: "Soyad", dataIndex: "soyad", key: "soyad", width: 130 },
    { title: "TC No", dataIndex: "tcNo", key: "tcNo", width: 130 },
    { title: "Telefon", dataIndex: "telefon", key: "telefon", width: 130 },
    { title: "Email", dataIndex: "email", key: "email", width: 180 },
    {
      title: "Cinsiyet",
      dataIndex: "cinsiyet",
      key: "cinsiyet",
      width: 90,
      render: (v: number) => getCinsiyetLabel(v),
    },
    { title: "İl", dataIndex: "ilAdi", key: "ilAdi", width: 100 },
    { title: "İlçe", dataIndex: "ilceAdi", key: "ilceAdi", width: 100 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 130 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 130 },
  ];

  // File upload handler
  const handleImageUpload = (file: File) => {
    const reader = new FileReader();
    reader.onload = () => {
      setForm({ ...form, image: reader.result as string });
    };
    reader.readAsDataURL(file);
    return false; // prevent auto upload
  };

  // --- TAB İÇERİKLERİ ---

  // Tab 1: Genel Bilgiler
  const genelTab = (
    <>
      {/* Profil Resmi */}
      <div style={{ display: "flex", alignItems: "center", gap: 16, marginBottom: 16 }}>
        <Avatar
          size={80}
          src={form.image || undefined}
          icon={!form.image ? <UserOutlined /> : undefined}
          style={{ backgroundColor: form.image ? "transparent" : "#1677ff", flexShrink: 0 }}
        />
        <Space direction="vertical" size="small">
          <Upload
            accept="image/*"
            showUploadList={false}
            beforeUpload={handleImageUpload}
          >
            <Button icon={<UploadOutlined />} size="small">
              Resim Yükle
            </Button>
          </Upload>
          {form.image && (
            <Button
              size="small"
              danger
              onClick={() => setForm({ ...form, image: "" })}
            >
              Resmi Kaldır
            </Button>
          )}
        </Space>
      </div>

      <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit
          label="Kod"
          value={form.kod}
          onChange={(v) => setForm({ ...form, kod: v })}
          required
          maxLength={20}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <div style={{ display: "flex", justifyContent: "flex-end", paddingTop: 24 }}>
          <DevCheckEdit
            value={form.durum}
            onChange={(v) => setForm({ ...form, durum: v })}
            checkedText="Aktif"
            uncheckedText="Pasif"
          />
        </div>
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="Ad"
          value={form.ad}
          onChange={(v) => setForm({ ...form, ad: v })}
          required
          maxLength={128}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="Soyad"
          value={form.soyad}
          onChange={(v) => setForm({ ...form, soyad: v })}
          maxLength={128}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="TC No"
          value={form.tcNo}
          onChange={(v) => setForm({ ...form, tcNo: v })}
          maxLength={11}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit
          label="Cinsiyet"
          value={form.cinsiyet}
          onChange={(v) => setForm({ ...form, cinsiyet: v as number })}
          options={cinsiyetOptions}
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevDateEdit
          label="Doğum Tarihi"
          value={form.dogumTarihi}
          onChange={(v) => setForm({ ...form, dogumTarihi: v })}
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="Doğum Yeri"
          value={form.dogumYeri}
          onChange={(v) => setForm({ ...form, dogumYeri: v })}
          maxLength={128}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit
          label="Kan Grubu"
          value={form.kanGrubu}
          onChange={(v) => setForm({ ...form, kanGrubu: v as number })}
          options={kanGrubuOptions}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit
          label="Medeni Hal"
          value={form.medeniHal}
          onChange={(v) => setForm({ ...form, medeniHal: v as number })}
          options={medeniHalOptions}
        />
      </DevGridLayoutItem>
    </DevGridLayout>
    </>
  );

  // Tab 2: İletişim
  const iletisimTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit
          label="Telefon"
          value={form.telefon}
          onChange={(v) => setForm({ ...form, telefon: v })}
          maxLength={15}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="E-posta"
          value={form.email}
          onChange={(v) => setForm({ ...form, email: v })}
          maxLength={128}
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 3: Adres
  const adresTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevButtonEdit
          label="İl"
          value={ilAdi}
          onButtonClick={() => setIlModalVisible(true)}
          onClear={() => {
            setForm({ ...form, ilId: "", ilceId: "" });
            setIlAdi("");
            setIlceAdi("");
          }}
          placeholder="İl Seçin"
          readOnly
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevButtonEdit
          label="İlçe"
          value={ilceAdi}
          onButtonClick={() => {
            if (!form.ilId) {
              alert("Lütfen önce İl seçin.");
              return;
            }
            setIlceModalVisible(true);
          }}
          onClear={() => {
            setForm({ ...form, ilceId: "" });
            setIlceAdi("");
          }}
          placeholder="İlçe Seçin"
          readOnly
          disabled={!form.ilId}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem columnSpan={2}>
        <DevMemoEdit
          label="Açık Adres"
          value={form.acikAdres}
          onChange={(v) => setForm({ ...form, acikAdres: v })}
          maxLength={256}
          rows={3}
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 4: Özel Kodlar
  const ozelKodlarTab = (
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
    </DevGridLayout>
  );

  // Tab 5: Açıklama
  const aciklamaTab = (
    <DevMemoEdit
      label="Açıklama"
      value={form.aciklama}
      onChange={(v) => setForm({ ...form, aciklama: v })}
      maxLength={500}
    />
  );

  const tabItems = [
    { key: "genel", label: "Genel Bilgiler", children: genelTab },
    { key: "iletisim", label: "İletişim", children: iletisimTab },
    { key: "adres", label: "Adres", children: adresTab },
    { key: "ozelKodlar", label: "Özel Kodlar", children: ozelKodlarTab },
    { key: "aciklama", label: "Açıklama", children: aciklamaTab },
  ];

  const editForm = (
    <Form layout="vertical">
      <DevTabEdit
        items={tabItems}
        activeKey={activeTab}
        onChange={setActiveTab}
      />
    </Form>
  );

  return (
    <>
      <DevListPageLayout
        loading={loading && kisiler.length === 0}
        loadingText="Kişiler yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingKisi ? "Kişi Düzenle" : "Yeni Kişi"}
        editContent={editForm}
        editWidth={650}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingKisi ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={kisiler}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchKisiler}
            onDoubleClick={handleEdit}
            title="Kişiler"
            extraToolbar={
              <DevCheckEdit
                value={durumFilter}
                onChange={(v) => setDurumFilter(v)}
                checkedText="Aktif"
                uncheckedText="Pasif"
              />
            }
            pageSize={15}
            scrollY={400}
          />
        </Card>
      </DevListPageLayout>

      {/* İl Seçim Modalı */}
      <IlSelectModal
        visible={ilModalVisible}
        onSelect={handleIlSelect}
        onCancel={() => setIlModalVisible(false)}
      />

      {/* İlçe Seçim Modalı */}
      <IlceSelectModal
        visible={ilceModalVisible}
        ilId={form.ilId || null}
        onSelect={handleIlceSelect}
        onCancel={() => setIlceModalVisible(false)}
      />

      {/* OzelKod Seçim Modalı */}
      <OzelKodSelectModal
        visible={ozelKodModalVisible}
        kodTuru={activeKodTuru}
        kartTuru={KART_TURU_KISI}
        title={`Özel Kod-${activeKodTuru} Kartları`}
        onSelect={handleOzelKodSelect}
        onCancel={() => setOzelKodModalVisible(false)}
      />
    </>
  );
}