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
} from "../components/dev";

interface Donem {
  id: string;
  kod: string;
  ad: string;
  aciklama: string;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  aciklama: "",
  durum: true,
};

export default function DonemPage() {
  const auth = useAuth();
  const [donemler, setDonemler] = useState<Donem[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingDonem, setEditingDonem] = useState<Donem | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);

  const fetchDonemler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/donem", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setDonemler(response.data.items);
    } catch (error) {
      console.error("Dönemler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/donem/code", {
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
    if (auth.isAuthenticated) fetchDonemler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingDonem(null);
    setDrawerOpen(true);
  };

  const handleEdit = async (donem: Donem) => {
    try {
      const response = await apiClient.get(`/api/app/donem/${donem.id}`);
      const d = response.data;
      setEditingDonem(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        aciklama: d.aciklama || "",
        durum: d.durum,
      });
      setDrawerOpen(true);
    } catch (error) {
      console.error("Dönem detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingDonem) {
        await apiClient.put(`/api/app/donem/${editingDonem.id}`, form);
      } else {
        await apiClient.post("/api/app/donem", form);
      }
      setDrawerOpen(false);
      setEditingDonem(null);
      setForm({ ...emptyForm });
      fetchDonemler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/donem/${id}`);
      fetchDonemler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingDonem(null);
    setForm({ ...emptyForm });
  };

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 180 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 250 },
    { title: "Açıklama", dataIndex: "aciklama", key: "aciklama" },
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
        <DevGridLayoutItem columnSpan={2}>
          <DevMemoEdit
            label="Açıklama"
            value={form.aciklama}
            onChange={(v) => setForm({ ...form, aciklama: v })}
            maxLength={200}
          />
        </DevGridLayoutItem>
      </DevGridLayout>
    </Form>
  );

  return (
    <DevListPageLayout
      loading={loading && donemler.length === 0}
      loadingText="Dönemler yükleniyor..."
      editVisible={drawerOpen}
      editTitle={editingDonem ? "Dönem Düzenle" : "Yeni Dönem"}
      editContent={editForm}
      editWidth={500}
      onEditClose={handleClose}
      editExtra={
        <Space>
          <Button onClick={handleClose}>İptal</Button>
          <Button type="primary" onClick={handleSubmit} loading={saving}>
            {editingDonem ? "Güncelle" : "Kaydet"}
          </Button>
        </Space>
      }
    >
      <Card>
        <DevDataGrid
          columns={columns}
          dataSource={donemler}
          loading={loading}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onNew={handleNew}
          onRefresh={fetchDonemler}
          onDoubleClick={handleEdit}
          title="Dönemler"
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
  );
}