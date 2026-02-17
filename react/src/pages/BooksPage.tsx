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
} from "../components/dev";

interface Book {
  id: string;
  kod: string;
  ad: string;
  type: number;
  publishDate: string;
  price: number;
  aciklama: string;
  durum: boolean;
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
  kod: "", ad: "", type: 0,
  publishDate: new Date().toISOString().split("T")[0],
  price: 0, aciklama: "", durum: true,
};

export default function BooksPage() {
  const auth = useAuth();
  const [books, setBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingBook, setEditingBook] = useState<Book | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);

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
      const response = await apiClient.get("/api/app/book/code", { params: { durum: true } });
      return typeof response.data === "string" ? response.data : String(response.data);
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
    setDrawerOpen(true);
  };

  const handleEdit = (book: Book) => {
    setEditingBook(book);
    setForm({
      kod: book.kod, ad: book.ad, type: book.type,
      publishDate: book.publishDate.split("T")[0],
      price: book.price, aciklama: book.aciklama || "", durum: book.durum,
    });
    setDrawerOpen(true);
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
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
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

  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 100 },
    { title: "Ad", dataIndex: "ad", key: "ad" },
    { title: "Tür", dataIndex: "type", key: "type", width: 120, render: (v: number) => bookTypeOptions.find((o) => o.value === v)?.label },
    { title: "Yayın Tarihi", dataIndex: "publishDate", key: "publishDate", width: 130, render: (v: string) => new Date(v).toLocaleDateString("tr-TR") },
    { title: "Fiyat", dataIndex: "price", key: "price", width: 100, render: (v: number) => `${v.toLocaleString("tr-TR")} ₺` },
    { title: "Açıklama", dataIndex: "aciklama", key: "aciklama" },
  ];

  // Edit Form
  const editForm = (
    <Form layout="vertical">
      <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
        <DevGridLayoutItem>
          <DevTextEdit label="Kod" value={form.kod} onChange={(v) => setForm({ ...form, kod: v })} required maxLength={20} />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevTextEdit label="Ad" value={form.ad} onChange={(v) => setForm({ ...form, ad: v })} required maxLength={128} />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevComboBoxEdit label="Tür" value={form.type} onChange={(v) => setForm({ ...form, type: v as number })} options={bookTypeOptions} required />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevDateEdit label="Yayın Tarihi" value={form.publishDate} onChange={(v) => setForm({ ...form, publishDate: v })} required />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevCurrencyEdit label="Fiyat" value={form.price} onChange={(v) => setForm({ ...form, price: v })} required />
        </DevGridLayoutItem>
        <DevGridLayoutItem>
          <DevCheckEdit label="Durum" value={form.durum} onChange={(v) => setForm({ ...form, durum: v })} />
        </DevGridLayoutItem>
        <DevGridLayoutItem columnSpan={2}>
          <DevMemoEdit label="Açıklama" value={form.aciklama} onChange={(v) => setForm({ ...form, aciklama: v })} maxLength={500} />
        </DevGridLayoutItem>
      </DevGridLayout>
    </Form>
  );

  return (
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
          <Button type="primary" onClick={handleSubmit} loading={saving}>
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
  );
}