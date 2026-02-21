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
} from "../components/dev";
import OzelKodSelectModal from "./OzelKodSelectModal";

interface Stok {
  id: string;
  kod: string;
  ad: string;
  kdvOrani: number;
  birimFiyat: number;
  barkod: string;
  en: string;
  boy: string;
  yukseklik: string;
  alan: string;
  netHacim: string;
  brutHacim: string;
  netAgirlik: string;
  brutAgirlik: string;
  birimId: string;
  birimAdi: string;
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
  aciklama: string;
  aciklama2: string;
  aciklama3: string;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  kdvOrani: 20,
  birimFiyat: 0,
  barkod: "",
  en: "",
  boy: "",
  yukseklik: "",
  alan: "",
  netHacim: "",
  brutHacim: "",
  netAgirlik: "",
  brutAgirlik: "",
  birimId: null as string | null,
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  ozelKod3Id: null as string | null,
  ozelKod4Id: null as string | null,
  ozelKod5Id: null as string | null,
  aciklama: "",
  aciklama2: "",
  aciklama3: "",
  durum: true,
};

const KART_TURU_STOK = 15;

export default function StokPage() {
  const auth = useAuth();
  const [stoklar, setStoklar] = useState<Stok[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingStok, setEditingStok] = useState<Stok | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // Birim
  const [birimAdi, setBirimAdi] = useState("");
  const [birimModalOpen, setBirimModalOpen] = useState(false);
  const [birimlerList, setBirimlerList] = useState<any[]>([]);

  // OzelKod
  const [ozelKodNames, setOzelKodNames] = useState({
    ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "",
  });
  const [ozelKodModalOpen, setOzelKodModalOpen] = useState(false);
  const [ozelKodField, setOzelKodField] = useState<string>("");

  const fetchStoklar = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/stok", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setStoklar(response.data.items);
    } catch (error) {
      console.error("Stoklar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/stok/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchStoklar();
  }, [auth.isAuthenticated, durumFilter]);

  const fetchBirimler = async () => {
    try {
      const res = await apiClient.get("/api/app/birim", {
        params: { durum: true, skipCount: 0, maxResultCount: 5000 },
      });
      setBirimlerList(res.data.items);
    } catch {}
  };

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingStok(null);
    setBirimAdi("");
    setOzelKodNames({ ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "" });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (stok: Stok) => {
    try {
      const response = await apiClient.get(`/api/app/stok/${stok.id}`);
      const d = response.data;
      setEditingStok(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        kdvOrani: d.kdvOrani,
        birimFiyat: d.birimFiyat,
        barkod: d.barkod || "",
        en: d.en || "",
        boy: d.boy || "",
        yukseklik: d.yukseklik || "",
        alan: d.alan || "",
        netHacim: d.netHacim || "",
        brutHacim: d.brutHacim || "",
        netAgirlik: d.netAgirlik || "",
        brutAgirlik: d.brutAgirlik || "",
        birimId: d.birimId,
        ozelKod1Id: d.ozelKod1Id,
        ozelKod2Id: d.ozelKod2Id,
        ozelKod3Id: d.ozelKod3Id,
        ozelKod4Id: d.ozelKod4Id,
        ozelKod5Id: d.ozelKod5Id,
        aciklama: d.aciklama || "",
        aciklama2: d.aciklama2 || "",
        aciklama3: d.aciklama3 || "",
        durum: d.durum,
      });
      setBirimAdi(d.birimAdi || "");
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
      console.error("Stok detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    if (!form.birimId) {
      alert("Lütfen Birim seçin.");
      return;
    }
    setSaving(true);
    try {
      if (editingStok) {
        await apiClient.put(`/api/app/stok/${editingStok.id}`, form);
      } else {
        await apiClient.post("/api/app/stok", form);
      }
      setDrawerOpen(false);
      setEditingStok(null);
      setForm({ ...emptyForm });
      fetchStoklar();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/stok/${id}`);
      fetchStoklar();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingStok(null);
    setForm({ ...emptyForm });
  };

  // OzelKod
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

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 140 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 220 },
    { title: "KDV %", dataIndex: "kdvOrani", key: "kdvOrani", width: 80 },
    { title: "Birim Fiyat", dataIndex: "birimFiyat", key: "birimFiyat", width: 120,
      render: (v: number) => v?.toLocaleString("tr-TR", { minimumFractionDigits: 2 }) },
    { title: "Barkod", dataIndex: "barkod", key: "barkod", width: 140 },
    { title: "Birim", dataIndex: "birimAdi", key: "birimAdi", width: 100 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 110 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 110 },
  ];

  // Tab 1: Genel
  const genelTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit label="Kod" value={form.kod} onChange={(v) => setForm({ ...form, kod: v })} required maxLength={20} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <div style={{ display: "flex", justifyContent: "flex-end", paddingTop: 24 }}>
          <DevCheckEdit value={form.durum} onChange={(v) => setForm({ ...form, durum: v })} checkedText="Aktif" uncheckedText="Pasif" />
        </div>
      </DevGridLayoutItem>
      <DevGridLayoutItem columnSpan={2}>
        <DevTextEdit label="Ad" value={form.ad} onChange={(v) => setForm({ ...form, ad: v })} required maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="KDV Oranı (%)" value={String(form.kdvOrani)} onChange={(v) => setForm({ ...form, kdvOrani: Number(v) || 0 })} required maxLength={3} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Birim Fiyat" value={String(form.birimFiyat)} onChange={(v) => setForm({ ...form, birimFiyat: Number(v) || 0 })} required maxLength={20} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Barkod" value={form.barkod} onChange={(v) => setForm({ ...form, barkod: v })} maxLength={128} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevButtonEdit
          label="Birim"
          value={birimAdi}
          onButtonClick={() => { fetchBirimler(); setBirimModalOpen(true); }}
          onClear={() => { setForm({ ...form, birimId: null }); setBirimAdi(""); }}
          placeholder="Birim Seçin"
          readOnly
          required
        />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 2: Ölçüler
  const olculerTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit label="En" value={form.en} onChange={(v) => setForm({ ...form, en: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Boy" value={form.boy} onChange={(v) => setForm({ ...form, boy: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Yükseklik" value={form.yukseklik} onChange={(v) => setForm({ ...form, yukseklik: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Alan" value={form.alan} onChange={(v) => setForm({ ...form, alan: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Net Hacim" value={form.netHacim} onChange={(v) => setForm({ ...form, netHacim: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Brüt Hacim" value={form.brutHacim} onChange={(v) => setForm({ ...form, brutHacim: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Net Ağırlık" value={form.netAgirlik} onChange={(v) => setForm({ ...form, netAgirlik: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Brüt Ağırlık" value={form.brutAgirlik} onChange={(v) => setForm({ ...form, brutAgirlik: v })} maxLength={200} />
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
    <DevGridLayout columnCount={1}>
      <DevGridLayoutItem>
        <DevMemoEdit label="Açıklama" value={form.aciklama} onChange={(v) => setForm({ ...form, aciklama: v })} maxLength={500} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevMemoEdit label="Açıklama 2" value={form.aciklama2} onChange={(v) => setForm({ ...form, aciklama2: v })} maxLength={500} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevMemoEdit label="Açıklama 3" value={form.aciklama3} onChange={(v) => setForm({ ...form, aciklama3: v })} maxLength={500} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  const tabItems = [
    { key: "genel", label: "Genel Bilgiler", children: genelTab },
    { key: "olculer", label: "Ölçüler", children: olculerTab },
    { key: "ozelKodlar", label: "Özel Kodlar", children: ozelKodlarTab },
    { key: "aciklama", label: "Açıklama", children: aciklamaTab },
  ];

  const editForm = (
    <Form layout="vertical">
      <DevTabEdit items={tabItems} activeKey={activeTab} onChange={setActiveTab} />
    </Form>
  );

  return (
    <>
      <DevListPageLayout
        loading={loading && stoklar.length === 0}
        loadingText="Stoklar yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingStok ? "Stok Düzenle" : "Yeni Stok"}
        editContent={editForm}
        editWidth={650}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingStok ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={stoklar}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchStoklar}
            onDoubleClick={handleEdit}
            title="Stoklar"
            extraToolbar={
              <DevCheckEdit value={durumFilter} onChange={(v) => setDurumFilter(v)} checkedText="Aktif" uncheckedText="Pasif" />
            }
            pageSize={15}
            scrollY={400}
          />
        </Card>
      </DevListPageLayout>

      {/* Birim Seçim Modalı */}
      <Modal
        title="Birim Seçin"
        open={birimModalOpen}
        onCancel={() => setBirimModalOpen(false)}
        width={500}
        footer={null}
        destroyOnClose
      >
        <div style={{ maxHeight: 350, overflow: "auto" }}>
          {birimlerList.map((b: any) => (
            <div
              key={b.id}
              onClick={() => {
                setForm({ ...form, birimId: b.id });
                setBirimAdi(b.ad);
                setBirimModalOpen(false);
              }}
              style={{
                padding: "10px 14px", cursor: "pointer", borderRadius: 6,
                marginBottom: 4, border: "1px solid #f0f0f0", transition: "all 0.2s",
              }}
              onMouseEnter={(e) => { e.currentTarget.style.background = "#e6f4ff"; e.currentTarget.style.borderColor = "#1677ff"; }}
              onMouseLeave={(e) => { e.currentTarget.style.background = "transparent"; e.currentTarget.style.borderColor = "#f0f0f0"; }}
            >
              <strong>{b.kod}</strong> — {b.ad}
            </div>
          ))}
          {birimlerList.length === 0 && (
            <div style={{ textAlign: "center", padding: 20, color: "#999" }}>Birim bulunamadı.</div>
          )}
        </div>
      </Modal>

      <OzelKodSelectModal
        visible={ozelKodModalOpen}
        kartTuru={KART_TURU_STOK}
        kodTuru={Number(ozelKodField)}
        onCancel={() => setOzelKodModalOpen(false)}
        onSelect={handleOzelKodSelect}
      />
    </>
  );
}