import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card } from "antd";
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

interface Birim {
  id: string;
  kod: string;
  ad: string;
  aciklama: string;
  durum: boolean;
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

const KART_TURU_BIRIM = 4;

export default function BirimPage() {
  const auth = useAuth();
  const [birimler, setBirimler] = useState<Birim[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingBirim, setEditingBirim] = useState<Birim | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  const [ozelKodNames, setOzelKodNames] = useState({
    ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "",
  });
  const [ozelKodModalOpen, setOzelKodModalOpen] = useState(false);
  const [ozelKodField, setOzelKodField] = useState<string>("");

  const fetchBirimler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/birim", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setBirimler(response.data.items);
    } catch (error) {
      console.error("Birimler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/birim/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchBirimler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingBirim(null);
    setOzelKodNames({ ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "" });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (birim: Birim) => {
    try {
      const response = await apiClient.get(`/api/app/birim/${birim.id}`);
      const d = response.data;
      setEditingBirim(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        aciklama: d.aciklama || "",
        durum: d.durum,
        ozelKod1Id: d.ozelKod1Id,
        ozelKod2Id: d.ozelKod2Id,
        ozelKod3Id: d.ozelKod3Id,
        ozelKod4Id: d.ozelKod4Id,
        ozelKod5Id: d.ozelKod5Id,
      });
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
      console.error("Birim detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingBirim) {
        await apiClient.put(`/api/app/birim/${editingBirim.id}`, form);
      } else {
        await apiClient.post("/api/app/birim", form);
      }
      setDrawerOpen(false);
      setEditingBirim(null);
      setForm({ ...emptyForm });
      fetchBirimler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/birim/${id}`);
      fetchBirimler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingBirim(null);
    setForm({ ...emptyForm });
  };

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
    { title: "Kod", dataIndex: "kod", key: "kod", width: 160 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 250 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 120 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 120 },
    { title: "Özel Kod-3", dataIndex: "ozelKod3Adi", key: "ozelKod3Adi", width: 120 },
    { title: "Özel Kod-4", dataIndex: "ozelKod4Adi", key: "ozelKod4Adi", width: 120 },
    { title: "Özel Kod-5", dataIndex: "ozelKod5Adi", key: "ozelKod5Adi", width: 120 },
  ];

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
    </DevGridLayout>
  );

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

  const aciklamaTab = (
    <DevGridLayout columnCount={1}>
      <DevGridLayoutItem>
        <DevMemoEdit label="Açıklama" value={form.aciklama} onChange={(v) => setForm({ ...form, aciklama: v })} maxLength={500} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  const tabItems = [
    { key: "genel", label: "Genel Bilgiler", children: genelTab },
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
        loading={loading && birimler.length === 0}
        loadingText="Birimler yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingBirim ? "Birim Düzenle" : "Yeni Birim"}
        editContent={editForm}
        editWidth={600}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingBirim ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={birimler}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchBirimler}
            onDoubleClick={handleEdit}
            title="Birimler"
            extraToolbar={
              <DevCheckEdit value={durumFilter} onChange={(v) => setDurumFilter(v)} checkedText="Aktif" uncheckedText="Pasif" />
            }
            pageSize={15}
            scrollY={400}
          />
        </Card>
      </DevListPageLayout>

      <OzelKodSelectModal
        visible={ozelKodModalOpen}
        kartTuru={KART_TURU_BIRIM}
        kodTuru={Number(ozelKodField)}
        onCancel={() => setOzelKodModalOpen(false)}
        onSelect={handleOzelKodSelect}
      />
    </>
  );
}