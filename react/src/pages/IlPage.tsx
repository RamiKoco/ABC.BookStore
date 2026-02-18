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
  DevDataGrid,
} from "../components/dev";

interface Il {
  id: string;
  kod: string;
  ad: string;
  durum: boolean;
}

const emptyForm = {
  kod: "",
  ad: "",
  durum: true,
};

export default function IlPage() {
  const auth = useAuth();
  const [iller, setIller] = useState<Il[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingIl, setEditingIl] = useState<Il | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);

  const fetchIller = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/il", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setIller(response.data.items);
    } catch (error) {
      console.error("İller yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/il/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchIller();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingIl(null);
    setDrawerOpen(true);
  };

  const handleEdit = async (il: Il) => {
    try {
      const response = await apiClient.get(`/api/app/il/${il.id}`);
      const detail = response.data;
      setEditingIl(detail);
      setForm({
        kod: detail.kod,
        ad: detail.ad,
        durum: detail.durum,
      });
      setDrawerOpen(true);
    } catch (error) {
      console.error("İl detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingIl) {
        await apiClient.put(`/api/app/il/${editingIl.id}`, form);
      } else {
        await apiClient.post("/api/app/il", form);
      }
      setDrawerOpen(false);
      setEditingIl(null);
      setForm({ ...emptyForm });
      fetchIller();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/il/${id}`);
      fetchIller();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingIl(null);
    setForm({ ...emptyForm });
  };

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 150 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
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
    <DevListPageLayout
      loading={loading && iller.length === 0}
      loadingText="İller yükleniyor..."
      editVisible={drawerOpen}
      editTitle={editingIl ? "İl Düzenle" : "Yeni İl"}
      editContent={editForm}
      editWidth={500}
      onEditClose={handleClose}
      editExtra={
        <Space>
          <Button onClick={handleClose}>İptal</Button>
          <Button type="primary" onClick={handleSubmit} loading={saving}>
            {editingIl ? "Güncelle" : "Kaydet"}
          </Button>
        </Space>
      }
    >
      <Card>
        <DevDataGrid
          columns={columns}
          dataSource={iller}
          loading={loading}
          onEdit={handleEdit}
          onDelete={handleDelete}
          onNew={handleNew}
          onRefresh={fetchIller}
          onDoubleClick={handleEdit}
          title="İller"
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