import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card, Modal } from "antd";
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
} from "../components/dev";
import OzelKodSelectModal from "./OzelKodSelectModal";

interface BankaHesap {
  id: string;
  kod: string;
  ad: string;
  hesapTuru: number;
  hesapTuruAdi: string;
  dovizT: number;
  hesapNo: string;
  ibanNo: string;
  bankaId: string;
  bankaAdi: string;
  bankaSubeId: string;
  bankaSubeAdi: string;
  cariId: string;
  cariAdi: string;
  ozelKod1Id: string | null;
  ozelKod2Id: string | null;
  ozelKod3Id: string | null;
  ozelKod4Id: string | null;
  ozelKod5Id: string | null;
  ozelKod1Adi: string | null;
  ozelKod2Adi: string | null;
  ozelKod3Adi: string | null;
  ozelKod4Adi: string | null;
  ozelKod5Adi: string | null;
  subeId: string;
  aciklama: string;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  hesapTuru: 1,
  dovizT: 1,
  hesapNo: "",
  ibanNo: "",
  bankaSubeId: null as string | null,
  cariId: null as string | null,
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  ozelKod3Id: null as string | null,
  ozelKod4Id: null as string | null,
  ozelKod5Id: null as string | null,
  subeId: null as string | null,
  aciklama: "",
  durum: true,
};

const KART_TURU_BANKA_HESAP = 3;

const hesapTuruOptions = [
  { value: 1, label: "Vadesiz Mevduat Hesabı" },
  { value: 2, label: "Vadeli Mevduat Hesabı" },
  { value: 3, label: "Kredi Hesabı" },
  { value: 4, label: "Pos Bloke Hesabı" },
  { value: 5, label: "Fon Hesabı" },
  { value: 6, label: "Yatırım Hesabı" },
];

const dovizTuruOptions = [
  { value: 1, label: "TRY" },
  { value: 2, label: "USD" },
  { value: 3, label: "EUR" },
  { value: 4, label: "GBP" },
  { value: 5, label: "JPY" },
];

export default function BankaHesapPage() {
  const auth = useAuth();
  const [hesaplar, setHesaplar] = useState<BankaHesap[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingHesap, setEditingHesap] = useState<BankaHesap | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // Display names
  const [bankaAdi, setBankaAdi] = useState("");
  const [bankaSubeAdi, setBankaSubeAdi] = useState("");
  const [bankaIdForSube, setBankaIdForSube] = useState<string | null>(null);
  const [cariAdi, setCariAdi] = useState("");
  const [ozelKodNames, setOzelKodNames] = useState({
    ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "",
  });

  // Modal states
  const [bankaSelectModalOpen, setBankaSelectModalOpen] = useState(false);
  const [bankaSubeModalOpen, setBankaSubeModalOpen] = useState(false);
  const [bankalarList, setBankalarList] = useState<any[]>([]);
  const [bankaSubelerList, setBankaSubelerList] = useState<any[]>([]);
  const [ozelKodModalOpen, setOzelKodModalOpen] = useState(false);
  const [ozelKodField, setOzelKodField] = useState<string>("");

  const fetchHesaplar = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/banka-hesap", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setHesaplar(response.data.items);
    } catch (error) {
      console.error("Hesaplar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/banka-hesap/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchHesaplar();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    // SubeId'yi sessionStorage'dan al
    const firmaParam = sessionStorage.getItem("firmaParam");
    let subeId = null;
    if (firmaParam) {
      try { subeId = JSON.parse(firmaParam).subeId || null; } catch {}
    }
    setForm({ ...emptyForm, kod: newCode, subeId });
    setEditingHesap(null);
    setBankaAdi("");
    setBankaSubeAdi("");
    setBankaIdForSube(null);
    setCariAdi("");
    setOzelKodNames({ ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "" });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (hesap: BankaHesap) => {
    try {
      const response = await apiClient.get(`/api/app/banka-hesap/${hesap.id}`);
      const d = response.data;
      setEditingHesap(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        hesapTuru: d.hesapTuru,
        dovizT: d.dovizT,
        hesapNo: d.hesapNo || "",
        ibanNo: d.ibanNo || "",
        bankaSubeId: d.bankaSubeId,
        cariId: d.cariId || null,
        ozelKod1Id: d.ozelKod1Id,
        ozelKod2Id: d.ozelKod2Id,
        ozelKod3Id: d.ozelKod3Id,
        ozelKod4Id: d.ozelKod4Id,
        ozelKod5Id: d.ozelKod5Id,
        subeId: d.subeId || null,
        aciklama: d.aciklama || "",
        durum: d.durum,
      });
      setBankaAdi(d.bankaAdi || "");
      setBankaSubeAdi(d.bankaSubeAdi || "");
      setBankaIdForSube(d.bankaId || null);
      setCariAdi(d.cariAdi || "");
      setOzelKodNames({
        ozelKod1: d.ozelKod1Adi || "",
        ozelKod2: d.ozelKod2Adi || "",
        ozelKod3: d.ozelKod3Adi || "",
        ozelKod4: d.ozelKod4Adi || "",
        ozelKod5: d.ozelKod5Adi || "",
      });
      setActiveTab("genel");
      setDrawerOpen(true);
    } catch (error) {
      console.error("Hesap detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    if (!form.bankaSubeId) {
      alert("Lütfen Banka Şube seçin.");
      return;
    }
    setSaving(true);
    try {
      if (editingHesap) {
        await apiClient.put(`/api/app/banka-hesap/${editingHesap.id}`, form);
      } else {
        await apiClient.post("/api/app/banka-hesap", form);
      }
      setDrawerOpen(false);
      setEditingHesap(null);
      setForm({ ...emptyForm });
      fetchHesaplar();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/banka-hesap/${id}`);
      fetchHesaplar();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingHesap(null);
    setForm({ ...emptyForm });
  };

  // Banka listesini yükle
  const fetchBankalar = async () => {
    try {
      const res = await apiClient.get("/api/app/banka", {
        params: { durum: true, skipCount: 0, maxResultCount: 5000 },
      });
      setBankalarList(res.data.items);
    } catch {}
  };

  // Banka şube listesini yükle
  const fetchBankaSubeler = async (bId: string) => {
    try {
      const res = await apiClient.get("/api/app/banka-sube", {
        params: { bankaId: bId, durum: true, skipCount: 0, maxResultCount: 5000 },
      });
      setBankaSubelerList(res.data.items);
    } catch {}
  };

  const handleBankaSelect = (banka: any) => {
    setBankaAdi(banka.ad);
    setBankaIdForSube(banka.id);
    // Önceki şube seçimini temizle
    setForm({ ...form, bankaSubeId: null });
    setBankaSubeAdi("");
    setBankaSelectModalOpen(false);
    // Şube seçimi aç
    fetchBankaSubeler(banka.id);
    setBankaSubeModalOpen(true);
  };

  const handleBankaSubeSelect = (sube: any) => {
    setForm({ ...form, bankaSubeId: sube.id });
    setBankaSubeAdi(sube.ad);
    setBankaSubeModalOpen(false);
  };

  const openBankaSecimi = () => {
    fetchBankalar();
    setBankaSelectModalOpen(true);
  };

  // OzelKod helpers
  const openOzelKodModal = (fieldNum: string) => {
    setOzelKodField(fieldNum);
    setOzelKodModalOpen(true);
  };

  const handleOzelKodSelect = (item: { id: string; ad: string }) => {
    const idField = `ozelKod${ozelKodField}Id` as keyof typeof form;
    const nameField = `ozelKod${ozelKodField}` as keyof typeof ozelKodNames;
    setForm({ ...form, [idField]: item.id });
    setOzelKodNames({ ...ozelKodNames, [nameField]: item.ad });
    setOzelKodModalOpen(false);
  };

  const clearOzelKod = (fieldNum: string) => {
    const idField = `ozelKod${fieldNum}Id` as keyof typeof form;
    const nameField = `ozelKod${fieldNum}` as keyof typeof ozelKodNames;
    setForm({ ...form, [idField]: null });
    setOzelKodNames({ ...ozelKodNames, [nameField]: "" });
  };

  // --- COLUMNS ---
  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 160 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 200 },
    { title: "Hesap Türü", key: "hesapTuru", width: 180,
      render: (_: any, r: BankaHesap) => hesapTuruOptions.find(o => o.value === r.hesapTuru)?.label || "" },
    { title: "Döviz", key: "dovizT", width: 80,
      render: (_: any, r: BankaHesap) => dovizTuruOptions.find(o => o.value === r.dovizT)?.label || "" },
    { title: "Hesap No", dataIndex: "hesapNo", key: "hesapNo", width: 150 },
    { title: "IBAN", dataIndex: "ibanNo", key: "ibanNo", width: 220 },
    { title: "Banka", dataIndex: "bankaAdi", key: "bankaAdi", width: 150 },
    { title: "Banka Şube", dataIndex: "bankaSubeAdi", key: "bankaSubeAdi", width: 150 },
    { title: "Cari", dataIndex: "cariAdi", key: "cariAdi", width: 150 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 120 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 120 },
    { title: "Özel Kod-3", dataIndex: "ozelKod3Adi", key: "ozelKod3Adi", width: 120 },
    { title: "Özel Kod-4", dataIndex: "ozelKod4Adi", key: "ozelKod4Adi", width: 120 },
    { title: "Özel Kod-5", dataIndex: "ozelKod5Adi", key: "ozelKod5Adi", width: 120 },
  ];

  // --- TAB İÇERİKLERİ ---

  // Tab 1: Genel Bilgiler
  const genelTab = (
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
      <DevGridLayoutItem columnSpan={2}>
        <DevTextEdit
          label="Ad"
          value={form.ad}
          onChange={(v) => setForm({ ...form, ad: v })}
          required
          maxLength={200}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit
          label="Hesap Türü"
          value={form.hesapTuru}
          onChange={(v) => setForm({ ...form, hesapTuru: v as number })}
          options={hesapTuruOptions}
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit
          label="Döviz Türü"
          value={form.dovizT}
          onChange={(v) => setForm({ ...form, dovizT: v as number })}
          options={dovizTuruOptions}
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="Hesap No"
          value={form.hesapNo}
          onChange={(v) => setForm({ ...form, hesapNo: v })}
          maxLength={26}
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit
          label="IBAN No"
          value={form.ibanNo}
          onChange={(v) => setForm({ ...form, ibanNo: v })}
          maxLength={34}
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 2: Banka Bilgileri
  const bankaTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevButtonEdit
          label="Banka"
          value={bankaAdi}
          onButtonClick={openBankaSecimi}
          onClear={() => {
            setBankaAdi("");
            setBankaIdForSube(null);
            setBankaSubeAdi("");
            setForm({ ...form, bankaSubeId: null });
          }}
          placeholder="Banka Seçin"
          readOnly
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevButtonEdit
          label="Banka Şube"
          value={bankaSubeAdi}
          onButtonClick={() => {
            if (!bankaIdForSube) {
              alert("Önce Banka seçin.");
              return;
            }
            fetchBankaSubeler(bankaIdForSube);
            setBankaSubeModalOpen(true);
          }}
          onClear={() => {
            setForm({ ...form, bankaSubeId: null });
            setBankaSubeAdi("");
          }}
          placeholder="Banka Şube Seçin"
          readOnly
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem columnSpan={2}>
        <DevTextEdit
          label="Cari"
          value={cariAdi}
          onChange={() => {}}
          disabled
          placeholder="(Opsiyonel)"
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 3: Özel Kodlar
  const ozelKodlarTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      {[1, 2, 3, 4, 5].map((num) => (
        <DevGridLayoutItem key={num}>
          <DevButtonEdit
            label={`Özel Kod-${num}`}
            value={ozelKodNames[`ozelKod${num}` as keyof typeof ozelKodNames]}
            onButtonClick={() => openOzelKodModal(String(num))}
            onClear={() => clearOzelKod(String(num))}
            readOnly
          />
        </DevGridLayoutItem>
      ))}
    </DevGridLayout>
  );

  // Tab 4: Açıklama
  const aciklamaTab = (
    <DevGridLayout columnCount={1} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevMemoEdit
          label="Açıklama"
          value={form.aciklama}
          onChange={(v) => setForm({ ...form, aciklama: v })}
          maxLength={500}
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  const tabItems = [
    { key: "genel", label: "Genel Bilgiler", children: genelTab },
    { key: "banka", label: "Banka Bilgileri", children: bankaTab },
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
        loading={loading && hesaplar.length === 0}
        loadingText="Banka Hesaplar yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingHesap ? "Banka Hesap Düzenle" : "Yeni Banka Hesap"}
        editContent={editForm}
        editWidth={650}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingHesap ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={hesaplar}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchHesaplar}
            onDoubleClick={handleEdit}
            title="Banka Hesaplar"
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

      {/* Banka Seçim Modalı */}
      <Modal
        title="Banka Seçin"
        open={bankaSelectModalOpen}
        onCancel={() => setBankaSelectModalOpen(false)}
        width={500}
        footer={null}
        destroyOnClose
      >
        <div style={{ maxHeight: 350, overflow: "auto" }}>
          {bankalarList.map((b: any) => (
            <div
              key={b.id}
              onClick={() => handleBankaSelect(b)}
              style={{
                padding: "10px 14px", cursor: "pointer", borderRadius: 6,
                marginBottom: 4, border: "1px solid #f0f0f0",
                transition: "all 0.2s",
              }}
              onMouseEnter={(e) => { e.currentTarget.style.background = "#e6f4ff"; e.currentTarget.style.borderColor = "#1677ff"; }}
              onMouseLeave={(e) => { e.currentTarget.style.background = "transparent"; e.currentTarget.style.borderColor = "#f0f0f0"; }}
            >
              <strong>{b.kod}</strong> — {b.ad}
            </div>
          ))}
          {bankalarList.length === 0 && (
            <div style={{ textAlign: "center", padding: 20, color: "#999" }}>Banka bulunamadı.</div>
          )}
        </div>
      </Modal>

      {/* Banka Şube Seçim Modalı */}
      <Modal
        title={`${bankaAdi} - Şube Seçin`}
        open={bankaSubeModalOpen}
        onCancel={() => setBankaSubeModalOpen(false)}
        width={500}
        footer={null}
        destroyOnClose
      >
        <div style={{ maxHeight: 350, overflow: "auto" }}>
          {bankaSubelerList.map((s: any) => (
            <div
              key={s.id}
              onClick={() => handleBankaSubeSelect(s)}
              style={{
                padding: "10px 14px", cursor: "pointer", borderRadius: 6,
                marginBottom: 4, border: "1px solid #f0f0f0",
                transition: "all 0.2s",
              }}
              onMouseEnter={(e) => { e.currentTarget.style.background = "#e6f4ff"; e.currentTarget.style.borderColor = "#1677ff"; }}
              onMouseLeave={(e) => { e.currentTarget.style.background = "transparent"; e.currentTarget.style.borderColor = "#f0f0f0"; }}
            >
              <strong>{s.kod}</strong> — {s.ad}
            </div>
          ))}
          {bankaSubelerList.length === 0 && (
            <div style={{ textAlign: "center", padding: 20, color: "#999" }}>Şube bulunamadı.</div>
          )}
        </div>
      </Modal>

      {/* Özel Kod Seçim Modalı */}
      <OzelKodSelectModal
        visible={ozelKodModalOpen}
        kartTuru={KART_TURU_BANKA_HESAP}
        kodTuru={Number(ozelKodField)}
        onCancel={() => setOzelKodModalOpen(false)}
        onSelect={handleOzelKodSelect}
      />
    </>
  );
}