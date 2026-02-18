import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card } from "antd";
import apiClient from "../config/api";
import {
  DevListPageLayout,
  DevGridLayout,
  DevGridLayoutItem,
  DevTextEdit,
  DevComboBoxEdit,
  DevDateEdit,
  DevCurrencyEdit,
  DevCheckEdit,
  DevMemoEdit,
  DevDataGrid,
  DevButtonEdit,
  DevTabEdit,
} from "../components/dev";
import OzelKodSelectModal from "./OzelKodSelectModal";
import IlSelectModal from "./IlSelectModal";
import IlceSelectModal from "./IlceSelectModal";

interface Book {
  id: string;
  kod: string;
  ad: string;
  type: number;
  publishDate: string;
  price: number;
  aciklama: string;
  durum: boolean;
  ilId: string | null;
  ilAdi: string | null;
  ilceId: string | null;
  ilceAdi: string | null;
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

const bookTypeOptions = [
  { value: 0, label: "Tanımsız" },
  { value: 1, label: "Macera" },
  { value: 2, label: "Biyografi" },
  { value: 3, label: "Distopya" },
  { value: 4, label: "Fantastik" },
  { value: 5, label: "Korku" },
  { value: 6, label: "Bilim" },
  { value: 7, label: "Bilim Kurgu" },
  { value: 8, label: "Şiir" },
];

const emptyForm = {
  kod: "",
  ad: "",
  type: 0,
  publishDate: new Date().toISOString().split("T")[0],
  price: 0,
  aciklama: "",
  durum: true,
  ilId: null as string | null,
  ilceId: null as string | null,
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  ozelKod3Id: null as string | null,
  ozelKod4Id: null as string | null,
  ozelKod5Id: null as string | null,
};

const KART_TURU_BOOK = 1;

export default function BooksPage() {
  const auth = useAuth();
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingBook, setEditingBook] = useState<Book | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // OzelKod modal state
  const [ozelKodModalVisible, setOzelKodModalVisible] = useState(false);
  const [activeKodTuru, setActiveKodTuru] = useState<number>(1);

  // İl / İlçe modal state
  const [ilModalVisible, setIlModalVisible] = useState(false);
  const [ilceModalVisible, setIlceModalVisible] = useState(false);

  // Display names
  const [ozelKodNames, setOzelKodNames] = useState<Record<string, string>>({
    ozelKod1: "",
    ozelKod2: "",
    ozelKod3: "",
    ozelKod4: "",
    ozelKod5: "",
  });
  const [ilAdi, setIlAdi] = useState("");
  const [ilceAdi, setIlceAdi] = useState("");

  const fetchBooks = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/book", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setBooks(response.data.items);
    } catch (error) {
      console.error("Kitaplar yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/book/code", {
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
    if (auth.isAuthenticated) fetchBooks();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingBook(null);
    setOzelKodNames({
      ozelKod1: "",
      ozelKod2: "",
      ozelKod3: "",
      ozelKod4: "",
      ozelKod5: "",
    });
    setIlAdi("");
    setIlceAdi("");
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (book: Book) => {
    try {
      const response = await apiClient.get(`/api/app/book/${book.id}`);
      const detail = response.data;
      setEditingBook(detail);
      setForm({
        kod: detail.kod,
        ad: detail.ad,
        type: detail.type,
        publishDate: detail.publishDate.split("T")[0],
        price: detail.price,
        aciklama: detail.aciklama || "",
        durum: detail.durum,
        ilId: detail.ilId,
        ilceId: detail.ilceId,
        ozelKod1Id: detail.ozelKod1Id,
        ozelKod2Id: detail.ozelKod2Id,
        ozelKod3Id: detail.ozelKod3Id,
        ozelKod4Id: detail.ozelKod4Id,
        ozelKod5Id: detail.ozelKod5Id,
      });
      setIlAdi(detail.ilAdi || "");
      setIlceAdi(detail.ilceAdi || "");
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
      console.error("Kitap detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    setSaving(true);
    try {
      if (editingBook) {
        await apiClient.put(`/api/app/book/${editingBook.id}`, form);
      } else {
        await apiClient.post("/api/app/book", form);
      }
      setDrawerOpen(false);
      setEditingBook(null);
      setForm({ ...emptyForm });
      fetchBooks();
    } catch (error: any) {
      const msg =
        error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/book/${id}`);
      fetchBooks();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingBook(null);
    setForm({ ...emptyForm });
  };

  // İl seçimi
  const handleIlSelect = (item: { id: string; ad: string }) => {
    setForm({ ...form, ilId: item.id, ilceId: null });
    setIlAdi(item.ad);
    setIlceAdi("");
    setIlModalVisible(false);
  };

  const clearIl = () => {
    setForm({ ...form, ilId: null, ilceId: null });
    setIlAdi("");
    setIlceAdi("");
  };

  // İlçe seçimi
  const handleIlceSelect = (item: { id: string; ad: string }) => {
    setForm({ ...form, ilceId: item.id });
    setIlceAdi(item.ad);
    setIlceModalVisible(false);
  };

  const clearIlce = () => {
    setForm({ ...form, ilceId: null });
    setIlceAdi("");
  };

  const openIlceModal = () => {
    if (!form.ilId) {
      alert("Lütfen önce bir İl seçin.");
      return;
    }
    setIlceModalVisible(true);
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
    { title: "Kod", dataIndex: "kod", key: "kod", width: 100 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
    {
      title: "Tür",
      dataIndex: "type",
      key: "type",
      width: 120,
      render: (v: number) =>
        bookTypeOptions.find((o) => o.value === v)?.label,
    },
    {
      title: "Yayın Tarihi",
      dataIndex: "publishDate",
      key: "publishDate",
      width: 130,
      render: (v: string) => new Date(v).toLocaleDateString("tr-TR"),
    },
    {
      title: "Fiyat",
      dataIndex: "price",
      key: "price",
      width: 100,
      render: (v: number) => `${v.toLocaleString("tr-TR")} ₺`,
    },
    { title: "İl", dataIndex: "ilAdi", key: "ilAdi", width: 120 },
    { title: "İlçe", dataIndex: "ilceAdi", key: "ilceAdi", width: 120 },
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
            <DevCurrencyEdit
              label="Fiyat"
              value={form.price}
              onChange={(v) => setForm({ ...form, price: v })}
              required
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevDateEdit
              label="Yayın Tarihi"
              value={form.publishDate}
              onChange={(v) => setForm({ ...form, publishDate: v })}
              required
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevComboBoxEdit
              label="Tür"
              value={form.type}
              onChange={(v) => setForm({ ...form, type: v as number })}
              options={bookTypeOptions}
              required
            />
          </DevGridLayoutItem>
        </DevGridLayout>
      ),
    },
    {
      key: "adres",
      label: "Adres",
      children: (
        <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
          <DevGridLayoutItem>
            <DevButtonEdit
              label="İl"
              value={ilAdi}
              onButtonClick={() => setIlModalVisible(true)}
              onClear={clearIl}
              placeholder="İl Seçin"
              readOnly
            />
          </DevGridLayoutItem>
          <DevGridLayoutItem>
            <DevButtonEdit
              label="İlçe"
              value={ilceAdi}
              onButtonClick={openIlceModal}
              onClear={clearIlce}
              placeholder={form.ilId ? "İlçe Seçin" : "Önce İl Seçin"}
              readOnly
              disabled={!form.ilId}
            />
          </DevGridLayoutItem>
        </DevGridLayout>
      ),
    },
    {
      key: "ozelKodlar",
      label: "Özel Kodlar",
      children: (
        <DevGridLayout columnCount={1} columnSpacing="16px" rowSpacing="0px">
          {[1, 2, 3, 4, 5].map((kodTuru) => (
            <DevGridLayoutItem key={`ozelKod${kodTuru}`}>
              <DevButtonEdit
                label={`Özel Kod-${kodTuru}`}
                value={ozelKodNames[`ozelKod${kodTuru}` as keyof typeof ozelKodNames]}
                onButtonClick={() => openOzelKodModal(kodTuru)}
                onClear={() => clearOzelKod(kodTuru)}
                placeholder="Kart Seçin"
                readOnly
              />
            </DevGridLayoutItem>
          ))}
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

  // Edit Form
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
        loading={loading && books.length === 0}
        loadingText="Kitaplar yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingBook ? "Kitap Düzenle" : "Yeni Kitap"}
        editContent={editForm}
        editWidth={600}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button
              type="primary"
              onClick={handleSubmit}
              loading={saving}
            >
              {editingBook ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={books}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchBooks}
            onDoubleClick={handleEdit}
            title="Kitaplar"
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
        ilId={form.ilId}
        title="İlçe Kartları"
        onSelect={handleIlceSelect}
        onCancel={() => setIlceModalVisible(false)}
      />

      {/* Özel Kod Seçim Modalı */}
      <OzelKodSelectModal
        visible={ozelKodModalVisible}
        kodTuru={activeKodTuru}
        kartTuru={KART_TURU_BOOK}
        title={`Özel Kod-${activeKodTuru} Kartları`}
        onSelect={handleOzelKodSelect}
        onCancel={() => setOzelKodModalVisible(false)}
      />
    </>
  );
}