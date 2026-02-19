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

interface Sube {
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

export default function SubePage() {
  const auth = useAuth();
  const [subeler, setSubeler] = useState<Sube[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingSube, setEditingSube] = useState<Sube | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);

  const fetchSubeler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/sube", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setSubeler(response.data.items);
    } catch (error) {
      console.error("Şubeler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/sube/code", {
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
    if (auth.isAuthenticated) fetchSubeler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingSube(null);
    setDrawerOpen(true);
  };

  const handleEdit = async (sube: Sube) => {
    try {
      const response = await apiClient.get(`/api/app/sube/${sube.id}`);
      const d = response.data;
      setEditingSube(d);
      setForm({
        kod: d.kod,
        ad: d.ad,
        aciklama: d.aciklama || "",
        durum: d.durum,
      });
      setDrawerOpen(true);
    } catch (error) {
      console.error("Şube detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingSube) {
        await apiClient.put(`/api/app/sube/${editingSube.id}`, form);
      } else {
        await apiClient.post("/api/app/sube", form);
      }
      setDrawerOpen(false);
      setEditingSube(null);
      setForm({ ...emptyForm });
      fetchSubeler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/sube/${id}`);
      fetchSubeler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingSube(null);
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
            maxLength={50}
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
      loading={loading && subeler.length === 0}
      loadingText="Şubeler yükleniyor..."
      editVisible={drawerOpen}
      editTitle={editingSube ? "Şube Düzenle" : "Yeni Şube"}
      editContent={editForm}
      editWidth={500}
      onEditClose={handleClose}
      editExtra={
        <Space>
          <Button onClick={handleClose}>İptal</Button>
          <Button type="primary" onClick={handleSubmit} loading={saving}>
            {editingSube ? "Güncelle" : "Kaydet"}
          </Button>
        </Space>
      }
    >
      <Card>
        <DevDataGrid
          columns={columns}
          dataSource={subeler}
          loading={loading}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onNew={handleNew}
          onRefresh={fetchSubeler}
          onDoubleClick={handleEdit}
          title="Şubeler"
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