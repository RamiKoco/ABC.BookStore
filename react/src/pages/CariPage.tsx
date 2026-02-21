import { useAuth } from "react-oidc-context";
import { useState, useEffect } from "react";
import { Button, Form, Space, Card, Modal, Table, Popconfirm } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
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
import IlSelectModal from "./IlSelectModal";
import IlceSelectModal from "./IlceSelectModal";

interface Cari {
  id: string;
  kod: string;
  ad: string;
  soyad: string;
  unvan: string;
  hesapTuru: number;
  hesapTuruAdi: string;
  vergiDairesi: string;
  vdKodu: string;
  tcNo: string;
  vergiNo: string;
  telefon: string;
  email: string;
  adres: string;
  ilId: string;
  ilceId: string;
  ilAdi: string;
  ilceAdi: string;
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
  borc: number;
  alacak: number;
  borcBakiye: number;
  alacakBakiye: number;
  aciklama: string;
  durum: boolean;
}

interface CariSube {
  id: string;
  cariId: string;
  hareketTuru: number;
  hareketTuruAdi: string;
  hareketAdi: string;
  hareketKodu: string;
  aciklama: string;
}

const emptyForm = {
  kod: "",
  ad: "",
  soyad: "",
  unvan: "",
  hesapTuru: 1,
  vergiDairesi: "",
  vdKodu: "",
  tcNo: "",
  vergiNo: "",
  telefon: "",
  email: "",
  adres: "",
  ilId: "",
  ilceId: "",
  ozelKod1Id: null as string | null,
  ozelKod2Id: null as string | null,
  ozelKod3Id: null as string | null,
  ozelKod4Id: null as string | null,
  ozelKod5Id: null as string | null,
  aciklama: "",
  durum: true,
};

const KART_TURU_CARI = 6;

const hesapTuruOptions = [
  { value: 1, label: "Alıcı" },
  { value: 2, label: "Satıcı" },
  { value: 3, label: "Alıcı ve Satıcı" },
  { value: 4, label: "Grup Şirketi" },
  { value: 5, label: "Toptancı" },
  { value: 6, label: "Kefil" },
  { value: 7, label: "Müstahsil" },
  { value: 8, label: "Diğer" },
];

const cariSubeTuruOptions = [
  { value: 1, label: "Şube" },
  { value: 2, label: "Adres" },
  { value: 3, label: "Bağlantı" },
];

export default function CariPage() {
  const auth = useAuth();
  const [cariler, setCariler] = useState<Cari[]>([]);
  const [loading, setLoading] = useState(true);
  const [drawerOpen, setDrawerOpen] = useState(false);
  const [editingCari, setEditingCari] = useState<any | null>(null);
  const [form, setForm] = useState({ ...emptyForm });
  const [durumFilter, setDurumFilter] = useState(true);
  const [saving, setSaving] = useState(false);
  const [activeTab, setActiveTab] = useState("genel");

  // CariSubeler
  const [cariSubeler, setCariSubeler] = useState<CariSube[]>([]);
  const [subeEditModalOpen, setSubeEditModalOpen] = useState(false);
  const [editingSube, setEditingSube] = useState<CariSube | null>(null);
  const [subeForm, setSubeForm] = useState({
    id: "" as string,
    hareketTuru: 1,
    hareketAdi: "",
    hareketKodu: "",
    aciklama: "",
  });

  // Display names
  const [ilAdi, setIlAdi] = useState("");
  const [ilceAdi, setIlceAdi] = useState("");
  const [ozelKodNames, setOzelKodNames] = useState({
    ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "",
  });

  // Modal states
  const [ilModalOpen, setIlModalOpen] = useState(false);
  const [ilceModalOpen, setIlceModalOpen] = useState(false);
  const [ozelKodModalOpen, setOzelKodModalOpen] = useState(false);
  const [ozelKodField, setOzelKodField] = useState<string>("");

  const fetchCariler = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/app/cari", {
        params: { durum: durumFilter, skipCount: 0, maxResultCount: 5000 },
      });
      setCariler(response.data.items);
    } catch (error) {
      console.error("Cariler yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const getNewCode = async () => {
    try {
      const response = await apiClient.get("/api/app/cari/code", {
        params: { durum: true },
      });
      return typeof response.data === "string" ? response.data : String(response.data);
    } catch {
      return "";
    }
  };

  useEffect(() => {
    if (auth.isAuthenticated) fetchCariler();
  }, [auth.isAuthenticated, durumFilter]);

  const handleNew = async () => {
    const newCode = await getNewCode();
    setForm({ ...emptyForm, kod: newCode });
    setEditingCari(null);
    setCariSubeler([]);
    setIlAdi("");
    setIlceAdi("");
    setOzelKodNames({ ozelKod1: "", ozelKod2: "", ozelKod3: "", ozelKod4: "", ozelKod5: "" });
    setActiveTab("genel");
    setDrawerOpen(true);
  };

  const handleEdit = async (cari: Cari) => {
    try {
      const response = await apiClient.get(`/api/app/cari/${cari.id}`);
      const d = response.data;
      setEditingCari(d);
      setForm({
        kod: d.kod,
        ad: d.ad || "",
        soyad: d.soyad || "",
        unvan: d.unvan || "",
        hesapTuru: d.hesapTuru,
        vergiDairesi: d.vergiDairesi || "",
        vdKodu: d.vdKodu || "",
        tcNo: d.tcNo || "",
        vergiNo: d.vergiNo || "",
        telefon: d.telefon || "",
        email: d.email || "",
        adres: d.adres || "",
        ilId: d.ilId || "",
        ilceId: d.ilceId || "",
        ozelKod1Id: d.ozelKod1Id,
        ozelKod2Id: d.ozelKod2Id,
        ozelKod3Id: d.ozelKod3Id,
        ozelKod4Id: d.ozelKod4Id,
        ozelKod5Id: d.ozelKod5Id,
        aciklama: d.aciklama || "",
        durum: d.durum,
      });
      setCariSubeler(d.cariSubeler || []);
      setIlAdi(d.ilAdi || "");
      setIlceAdi(d.ilceAdi || "");
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
      console.error("Cari detayı yüklenemedi:", error);
    }
  };

  const handleSubmit = async () => {
    if (!form.ilId) {
      alert("Lütfen İl seçin.");
      return;
    }
    if (!form.ilceId) {
      alert("Lütfen İlçe seçin.");
      return;
    }
    setSaving(true);
    try {
      const payload = {
        ...form,
        cariSubeler: cariSubeler.map((s) => ({
          id: s.id,
          hareketTuru: s.hareketTuru,
          hareketAdi: s.hareketAdi,
          hareketKodu: s.hareketKodu,
          aciklama: s.aciklama,
        })),
      };
      if (editingCari) {
        await apiClient.put(`/api/app/cari/${editingCari.id}`, payload);
      } else {
        await apiClient.post("/api/app/cari", payload);
      }
      setDrawerOpen(false);
      setEditingCari(null);
      setForm({ ...emptyForm });
      setCariSubeler([]);
      fetchCariler();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "Kayıt hatası";
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const handleDelete = async (id: string) => {
    try {
      await apiClient.delete(`/api/app/cari/${id}`);
      fetchCariler();
    } catch (error: any) {
      alert(error?.response?.data?.error?.message || "Silme hatası");
    }
  };

  const handleClose = () => {
    setDrawerOpen(false);
    setEditingCari(null);
    setForm({ ...emptyForm });
    setCariSubeler([]);
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

  // CariSube helpers
  const handleSubeNew = () => {
    setEditingSube(null);
    setSubeForm({ id: "", hareketTuru: 1, hareketAdi: "", hareketKodu: "", aciklama: "" });
    setSubeEditModalOpen(true);
  };
  const handleSubeEdit = (sube: CariSube) => {
    setEditingSube(sube);
    setSubeForm({
      id: sube.id,
      hareketTuru: sube.hareketTuru,
      hareketAdi: sube.hareketAdi || "",
      hareketKodu: sube.hareketKodu || "",
      aciklama: sube.aciklama || "",
    });
    setSubeEditModalOpen(true);
  };
  const handleSubeSave = () => {
    if (editingSube) {
      setCariSubeler(cariSubeler.map((s) =>
        s.id === editingSube.id
          ? { ...s, ...subeForm }
          : s
      ));
    } else {
      const tempId = `temp-${Date.now()}`;
      setCariSubeler([...cariSubeler, {
        id: tempId,
        cariId: editingCari?.id || "",
        hareketTuru: subeForm.hareketTuru,
        hareketTuruAdi: cariSubeTuruOptions.find((o) => o.value === subeForm.hareketTuru)?.label || "",
        hareketAdi: subeForm.hareketAdi,
        hareketKodu: subeForm.hareketKodu,
        aciklama: subeForm.aciklama,
      }]);
    }
    setSubeEditModalOpen(false);
  };
  const handleSubeDelete = (id: string) => {
    setCariSubeler(cariSubeler.filter((s) => s.id !== id));
  };

  // --- COLUMNS ---
  const columns = [
    { title: "Kod", dataIndex: "kod", key: "kod", width: 150 },
    { title: "Ünvan", dataIndex: "unvan", key: "unvan", width: 220 },
    { title: "Hesap Türü", dataIndex: "hesapTuruAdi", key: "hesapTuruAdi", width: 140 },
    { title: "Telefon", dataIndex: "telefon", key: "telefon", width: 130 },
    { title: "Email", dataIndex: "email", key: "email", width: 180 },
    { title: "İl", dataIndex: "ilAdi", key: "ilAdi", width: 100 },
    { title: "İlçe", dataIndex: "ilceAdi", key: "ilceAdi", width: 100 },
    { title: "Borç", dataIndex: "borc", key: "borc", width: 120, render: (v: number) => v?.toLocaleString("tr-TR", { minimumFractionDigits: 2 }) },
    { title: "Alacak", dataIndex: "alacak", key: "alacak", width: 120, render: (v: number) => v?.toLocaleString("tr-TR", { minimumFractionDigits: 2 }) },
    { title: "Özel Kod-1", dataIndex: "ozelKod1Adi", key: "ozelKod1Adi", width: 110 },
    { title: "Özel Kod-2", dataIndex: "ozelKod2Adi", key: "ozelKod2Adi", width: 110 },
  ];

  // --- TAB İÇERİKLERİ ---

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
        <DevTextEdit label="Ünvan" value={form.unvan} onChange={(v) => setForm({ ...form, unvan: v })} required maxLength={500} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Ad" value={form.ad} onChange={(v) => setForm({ ...form, ad: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Soyad" value={form.soyad} onChange={(v) => setForm({ ...form, soyad: v })} maxLength={200} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevComboBoxEdit label="Hesap Türü" value={form.hesapTuru} onChange={(v) => setForm({ ...form, hesapTuru: v as number })} options={hesapTuruOptions} required />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 2: Vergi
  const vergiTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit label="Vergi Dairesi" value={form.vergiDairesi} onChange={(v) => setForm({ ...form, vergiDairesi: v })} maxLength={50} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="VD Kodu" value={form.vdKodu} onChange={(v) => setForm({ ...form, vdKodu: v })} maxLength={20} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="Vergi No" value={form.vergiNo} onChange={(v) => setForm({ ...form, vergiNo: v })} maxLength={11} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="TC No" value={form.tcNo} onChange={(v) => setForm({ ...form, tcNo: v })} maxLength={11} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 3: İletişim
  const iletisimTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevTextEdit label="Telefon" value={form.telefon} onChange={(v) => setForm({ ...form, telefon: v })} maxLength={15} />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevTextEdit label="E-posta" value={form.email} onChange={(v) => setForm({ ...form, email: v })} maxLength={100} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 4: Adres
  const adresTab = (
    <DevGridLayout columnCount={2} columnSpacing="16px" rowSpacing="0px">
      <DevGridLayoutItem>
        <DevButtonEdit
          label="İl"
          value={ilAdi}
          onButtonClick={() => setIlModalOpen(true)}
          onClear={() => { setForm({ ...form, ilId: "", ilceId: "" }); setIlAdi(""); setIlceAdi(""); }}
          placeholder="İl Seçin"
          readOnly
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem>
        <DevButtonEdit
          label="İlçe"
          value={ilceAdi}
          onButtonClick={() => {
            if (!form.ilId) { alert("Önce İl seçin."); return; }
            setIlceModalOpen(true);
          }}
          onClear={() => { setForm({ ...form, ilceId: "" }); setIlceAdi(""); }}
          placeholder="İlçe Seçin"
          readOnly
          required
        />
      </DevGridLayoutItem>
      <DevGridLayoutItem columnSpan={2}>
        <DevMemoEdit label="Adres" value={form.adres} onChange={(v) => setForm({ ...form, adres: v })} maxLength={500} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 5: Özel Kodlar
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

  // Tab 6: Açıklama
  const aciklamaTab = (
    <DevGridLayout columnCount={1}>
      <DevGridLayoutItem>
        <DevMemoEdit label="Açıklama" value={form.aciklama} onChange={(v) => setForm({ ...form, aciklama: v })} maxLength={500} />
      </DevGridLayoutItem>
    </DevGridLayout>
  );

  // Tab 7: Şubeler
  const subelerTab = (
    <div>
      <div style={{ marginBottom: 12 }}>
        <Button type="primary" icon={<PlusOutlined />} size="small" onClick={handleSubeNew}>
          Yeni Şube/Adres
        </Button>
      </div>
      <Table
        dataSource={cariSubeler}
        rowKey="id"
        size="small"
        pagination={false}
        scroll={{ y: 200 }}
        columns={[
          {
            title: "Tür", dataIndex: "hareketTuru", key: "hareketTuru", width: 100,
            render: (v: number) => cariSubeTuruOptions.find((o) => o.value === v)?.label || "",
          },
          { title: "Kod", dataIndex: "hareketKodu", key: "hareketKodu", width: 120 },
          { title: "Ad", dataIndex: "hareketAdi", key: "hareketAdi", width: 200 },
          { title: "Açıklama", dataIndex: "aciklama", key: "aciklama" },
          {
            title: "İşlem", key: "actions", width: 100,
            render: (_: any, r: CariSube) => (
              <Space size="small">
                <Button size="small" icon={<EditOutlined />} onClick={() => handleSubeEdit(r)} />
                <Popconfirm title="Silmek istediğinize emin misiniz?" onConfirm={() => handleSubeDelete(r.id)} okText="Evet" cancelText="Hayır">
                  <Button size="small" danger icon={<DeleteOutlined />} />
                </Popconfirm>
              </Space>
            ),
          },
        ]}
      />
    </div>
  );

  const tabItems = [
    { key: "genel", label: "Genel Bilgiler", children: genelTab },
    { key: "vergi", label: "Vergi Bilgileri", children: vergiTab },
    { key: "iletisim", label: "İletişim", children: iletisimTab },
    { key: "adres", label: "Adres", children: adresTab },
    { key: "ozelKodlar", label: "Özel Kodlar", children: ozelKodlarTab },
    { key: "aciklama", label: "Açıklama", children: aciklamaTab },
    { key: "subeler", label: "Şubeler", children: subelerTab },
  ];

  const editForm = (
    <Form layout="vertical">
      <DevTabEdit items={tabItems} activeKey={activeTab} onChange={setActiveTab} />
    </Form>
  );

  return (
    <>
      <DevListPageLayout
        loading={loading && cariler.length === 0}
        loadingText="Cariler yükleniyor..."
        editVisible={drawerOpen}
        editTitle={editingCari ? "Cari Düzenle" : "Yeni Cari"}
        editContent={editForm}
        editWidth={700}
        onEditClose={handleClose}
        editExtra={
          <Space>
            <Button onClick={handleClose}>İptal</Button>
            <Button type="primary" onClick={handleSubmit} loading={saving}>
              {editingCari ? "Güncelle" : "Kaydet"}
            </Button>
          </Space>
        }
      >
        <Card>
          <DevDataGrid
            columns={columns}
            dataSource={cariler}
            loading={loading}
            onEdit={handleEdit}
            onDelete={handleDelete}
            onNew={handleNew}
            onRefresh={fetchCariler}
            onDoubleClick={handleEdit}
            title="Cariler"
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
        visible={ilModalOpen}
        onCancel={() => setIlModalOpen(false)}
        onSelect={(il: any) => {
          setForm({ ...form, ilId: il.id, ilceId: "" });
          setIlAdi(il.ad);
          setIlceAdi("");
          setIlModalOpen(false);
        }}
      />

      {/* İlçe Seçim Modalı */}
      <IlceSelectModal
        visible={ilceModalOpen}
        ilId={form.ilId}
        onCancel={() => setIlceModalOpen(false)}
        onSelect={(ilce: any) => {
          setForm({ ...form, ilceId: ilce.id });
          setIlceAdi(ilce.ad);
          setIlceModalOpen(false);
        }}
      />

      {/* Özel Kod Seçim Modalı */}
      <OzelKodSelectModal
        visible={ozelKodModalOpen}
        kartTuru={KART_TURU_CARI}
        kodTuru={Number(ozelKodField)}
        onCancel={() => setOzelKodModalOpen(false)}
        onSelect={handleOzelKodSelect}
      />

      {/* Cari Şube Düzenleme Modalı */}
      <Modal
        title={editingSube ? "Şube/Adres Düzenle" : "Yeni Şube/Adres"}
        open={subeEditModalOpen}
        onCancel={() => setSubeEditModalOpen(false)}
        onOk={handleSubeSave}
        okText={editingSube ? "Güncelle" : "Ekle"}
        cancelText="İptal"
        width={450}
        destroyOnClose
      >
        <Form layout="vertical" style={{ marginTop: 16 }}>
          <DevComboBoxEdit
            label="Tür"
            value={subeForm.hareketTuru}
            onChange={(v) => setSubeForm({ ...subeForm, hareketTuru: v as number })}
            options={cariSubeTuruOptions}
            required
          />
          <div style={{ marginTop: 12 }}>
            <DevTextEdit
              label="Kod"
              value={subeForm.hareketKodu}
              onChange={(v) => setSubeForm({ ...subeForm, hareketKodu: v })}
              maxLength={20}
            />
          </div>
          <div style={{ marginTop: 12 }}>
            <DevTextEdit
              label="Ad"
              value={subeForm.hareketAdi}
              onChange={(v) => setSubeForm({ ...subeForm, hareketAdi: v })}
              maxLength={200}
            />
          </div>
          <div style={{ marginTop: 12 }}>
            <DevMemoEdit
              label="Açıklama"
              value={subeForm.aciklama}
              onChange={(v) => setSubeForm({ ...subeForm, aciklama: v })}
              maxLength={500}
            />
          </div>
        </Form>
      </Modal>
    </>
  );
}