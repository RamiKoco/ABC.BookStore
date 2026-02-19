import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card } from "antd";
import { BranchesOutlined } from "@ant-design/icons";
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
import BankaSubeSelectModal from "./BankaSubeSelectModal";

interface Banka {
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

const KART_TURU_BANKA = 2;

export default function BankaPage() {
  const auth = useAuth();
  const [bankalar, setBankalar] = useState<Banka[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingBanka, setEditingBanka] = useState<Banka | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");
  const [selectedBanka, setSelectedBanka] = useState<Banka | null>(null);

  // OzelKod modal state
  const [ozelKodModalVisible, setOzelKodModalVisible] = useState(false);
  const [activeKodTuru, setActiveKodTuru] = useState<number>(1);

  // OzelKod display names
  const [ozelKodNames, setOzelKodNames] = useState<Record<string, string>>({
    ozelKod1: "",
    ozelKod2: "",
    ozelKod3: "",
    ozelKod4: "",
    ozelKod5: "",
  });

  // BankaSube modal state
  const [subeModalVisible, setSubeModalVisible] = useState(false);
  const [selectedBankaForSube, setSelectedBankaForSube] = useState<Banka | null>(null);

  const fetchBankalar = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/banka", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setBankalar(response.data.items);
    } catch (error) {
      console.error("Bankalar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/banka/code", {
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
    if (auth.isAuthenticated) fetchBankalar();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingBanka(null);
    setOzelKodNames({
      ozelKod1: "",
      ozelKod2: "",
      ozelKod3: "",
      ozelKod4: "",
      ozelKod5: "",
    });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (banka: Banka) => {
    try {
      const response = await apiClient.get(`/api/app/banka/${banka.id}`);
      const detail = response.data;
      setEditingBanka(detail);
      setForm({
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
      setDrawerOpen(true);
    } catch (error) {
      console.error("Banka detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingBanka) {
        await apiClient.put(`/api/app/banka/${editingBanka.id}`, form);
      } else {
        await apiClient.post("/api/app/banka", form);
      }
      setDrawerOpen(false);
      setEditingBanka(null);
      setForm({ ...emptyForm });
      fetchBankalar();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/banka/${id}`);
      fetchBankalar();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingBanka(null);
    setForm({ ...emptyForm });
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

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 180 },
    { title: "Ad", dataIndex: "ad", key: "ad", width: 180 },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 150 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 150 },
    { title: "Özel Kod-3", dataIndex: "ozelKod3Adi", key: "ozelKod3Adi", width: 150 },
    { title: "Özel Kod-4", dataIndex: "ozelKod4Adi", key: "ozelKod4Adi", width: 150 },
    { title: "Özel Kod-5", dataIndex: "ozelKod5Adi", key: "ozelKod5Adi", width: 150 },
    { title: "Açıklama", dataIndex: "aciklama", key: "aciklama" },
  ];

  // Tab içerikleri
  const tabItems = [
    {
      key: "genel",
      label: "Genel Bilgiler",
      children: (
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
          value={form.aciklama}
          onChange={(v) => setForm({ ...form, aciklama: v })}
          maxLength={500}
        />
      ),
    },
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
        loading={loading && bankalar.length === 0}
        loadingText="Bankalar yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingBanka ? "Banka Düzenle" : "Yeni Banka"}
        editContent={editForm}
        editWidth={600}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingBanka ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={bankalar}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchBankalar}
            onDoubleClick={handleEdit}
            onSelectionChange={(rows) => setSelectedBanka(rows[0] || null)}
            title="Banka Kartları"
            extraToolbar={
              <Space>
                <Button
                  icon={<BranchesOutlined />}
                  onClick={() => {
                    if (!selectedBanka) {
                      alert("Lütfen bir banka seçin.");
                      return;
                    }
                    setSelectedBankaForSube(selectedBanka);
                    setSubeModalVisible(true);
                  }}
                  size="small"
                >
                  Şube Kartları
                </Button>
                <DevCheckEdit
                  value={durumFilter}
                  onChange={(v) => setDurumFilter(v)}
                  checkedText="Aktif"
                  uncheckedText="Pasif"
                />
              </Space>
            }
            pageSize={15}
            scrollY={400}
          />
        </Card>
      </DevListPageLayout>

      <OzelKodSelectModal
        visible={ozelKodModalVisible}
        kodTuru={activeKodTuru}
        kartTuru={KART_TURU_BANKA}
        title={`Özel Kod-${activeKodTuru} Kartları`}
        onSelect={handleOzelKodSelect}
        onCancel={() => setOzelKodModalVisible(false)}
      />

      <BankaSubeSelectModal
        visible={subeModalVisible}
        bankaId={selectedBankaForSube?.id || null}
        bankaAdi={selectedBankaForSube?.ad || ""}
        onCancel={() => setSubeModalVisible(false)}
      />
    </>
  );
}