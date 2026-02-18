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
  DevButtonEdit,
  DevDataGrid,
} from "../components/dev";
import IlSelectModal from "./IlSelectModal";

interface Ilce {
  id: string;
  kod: string;
  ad: string;
  ilId: string;
  ilAdi: string | null;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  ilId: null as string | null,
  durum: true,
};

export default function IlcePage() {
  const auth = useAuth();
  const [ilceler, setIlceler] = useState<Ilce[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingIlce, setEditingIlce] = useState<Ilce | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);

  // İl modal state
  const [ilModalVisible, setIlModalVisible] = useState(false);
  const [ilAdi, setIlAdi] = useState("");

  const fetchIlceler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/ilce", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setIlceler(response.data.items);
    } catch (error) {
      console.error("İlçeler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/ilce/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchIlceler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingIlce(null);
    setIlAdi("");
    setDrawerOpen(true);
  };

  const handleEdit = async (ilce: Ilce) => {
    try {
      const response = await apiClient.get(`/api/app/ilce/${ilce.id}`);
      const detail = response.data;
      setEditingIlce(detail);
      setForm({
        kod: detail.kod,
        ad: detail.ad,
        ilId: detail.ilId,
        durum: detail.durum,
      });
      setIlAdi(detail.ilAdi || "");
      setDrawerOpen(true);
    } catch (error) {
      console.error("İlçe detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
      if (!form.ilId) {
      alert("Lütfen bir İl seçin.");
      return;
    }
    setSaving(true);
    try {
      if (editingIlce) {
        await apiClient.put(`/api/app/ilce/${editingIlce.id}`, form);
      } else {
        await apiClient.post("/api/app/ilce", form);
      }
      setDrawerOpen(false);
      setEditingIlce(null);
      setForm({ ...emptyForm });
      setIlAdi("");
      fetchIlceler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/ilce/${id}`);
      fetchIlceler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingIlce(null);
    setForm({ ...emptyForm });
    setIlAdi("");
  };

  // İl seçimi
  const handleIlSelect = (item: { id: string; ad: string }) => {
    setForm({ ...form, ilId: item.id });
    setIlAdi(item.ad);
    setIlModalVisible(false);
  };

  const clearIl = () => {
    setForm({ ...form, ilId: null });
    setIlAdi("");
  };

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 150 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
    { title: "İl", dataIndex: "ilAdi", key: "ilAdi", width: 150 },
  ];

  const editForm = (
    <Form layout="vertical">
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
          <DevTextEdit
            label="Ad"
            value={form.ad}
            onChange={(v) => setForm({ ...form, ad: v })}
            required
            maxLength={128}
          />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevButtonEdit
            label="İl"
            value={ilAdi}
            onButtonClick={() => setIlModalVisible(true)}
            onClear={clearIl}
            placeholder="İl Seçin"
            readOnly
            required
          />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevCheckEdit
            label="Durum"
            value={form.durum}
            onChange={(v) => setForm({ ...form, durum: v })}
          />
        </DevGridLayoutItem>
      </DevGridLayout>
    </Form>
  );

  return (
    <>
      <DevListPageLayout
        loading={loading && ilceler.length === 0}
        loadingText="İlçeler yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingIlce ? "İlçe Düzenle" : "Yeni İlçe"}
        editContent={editForm}
        editWidth={500}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingIlce ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={ilceler}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchIlceler}
            onDoubleClick={handleEdit}
            title="İlçeler"
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

      <IlSelectModal
        visible={ilModalVisible}
        onSelect={handleIlSelect}
        onCancel={() => setIlModalVisible(false)}
      />
    </>
  );
}