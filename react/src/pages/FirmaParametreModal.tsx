import { useState, useEffect } from "react";
import { Modal, Button, Space, Spin } from "antd";
import { CheckOutlined } from "@ant-design/icons";
import apiClient from "../config/api";
import { DevButtonEdit } from "../components/dev";

// Basit seçim modalı (Şube ve Dönem için)
function SimpleSelectModal({
  visible,
  title,
  apiUrl,
  onSelect,
  onCancel,
}: {
  visible: boolean;
  title: string;
  apiUrl: string;
  onSelect: (item: { id: string; ad: string }) => void;
  onCancel: () => void;
}) {
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [selectedId, setSelectedId] = useState<string | null>(null);

  useEffect(() => {
    if (visible) {
      fetchData();
      setSelectedId(null);
    }
  }, [visible]);

  const fetchData = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get(apiUrl, {
        params: { durum: true, skipCount: 0, maxResultCount: 5000 },
      });
      setData(response.data.items);
    } catch (error) {
      console.error("Veri yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  const selectedItem = data.find((d) => d.id === selectedId);

  return (
    <Modal
      title={title}
      open={visible}
      onCancel={onCancel}
      width={500}
      footer={
        <Space>
          <Button onClick={onCancel}>İptal</Button>
          <Button
            type="primary"
            disabled={!selectedId}
            onClick={() => {
              if (selectedItem) {
                onSelect({ id: selectedItem.id, ad: selectedItem.ad });
              }
            }}
          >
            Seç
          </Button>
        </Space>
      }
      destroyOnClose
    >
      <div style={{ maxHeight: 300, overflow: "auto" }}>
        {loading ? (
          <div style={{ textAlign: "center", padding: 40 }}>
            <Spin />
          </div>
        ) : (
          data.map((item) => (
            <div
              key={item.id}
              onClick={() => setSelectedId(item.id)}
              style={{
                padding: "8px 12px",
                cursor: "pointer",
                borderRadius: 6,
                marginBottom: 4,
                background: selectedId === item.id ? "#e6f4ff" : "transparent",
                border: selectedId === item.id ? "1px solid #1677ff" : "1px solid transparent",
                display: "flex",
                justifyContent: "space-between",
                alignItems: "center",
              }}
            >
              <span>
                <strong>{item.kod}</strong> — {item.ad}
              </span>
              {selectedId === item.id && (
                <CheckOutlined style={{ color: "#1677ff" }} />
              )}
            </div>
          ))
        )}
        {!loading && data.length === 0 && (
          <div style={{ textAlign: "center", padding: 20, color: "#999" }}>
            Kayıt bulunamadı.
          </div>
        )}
      </div>
    </Modal>
  );
}

interface FirmaParametreModalProps {
  visible: boolean;
  userId: string;
  onComplete: (params: { subeId: string; subeAdi: string; donemId: string; donemAdi: string }) => void;
}

export default function FirmaParametreModal({
  visible,
  userId,
  onComplete,
}: FirmaParametreModalProps) {
  const [subeId, setSubeId] = useState("");
  const [subeAdi, setSubeAdi] = useState("");
  const [donemId, setDonemId] = useState("");
  const [donemAdi, setDonemAdi] = useState("");
  const [saving, setSaving] = useState(false);
  const [loading, setLoading] = useState(true);
  const [hasExisting, setHasExisting] = useState(false);

  // Şube / Dönem seçim modalları
  const [subeModalVisible, setSubeModalVisible] = useState(false);
  const [donemModalVisible, setDonemModalVisible] = useState(false);

  useEffect(() => {
    if (visible && userId) {
      loadExisting();
    }
  }, [visible, userId]);

  const loadExisting = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get(`/api/app/firma-parametre/${userId}`);
      const data = response.data;
      if (data && data.subeId) {
        setSubeId(data.subeId);
        setSubeAdi(data.subeAdi || "");
        setDonemId(data.donemId);
        setDonemAdi(data.donemAdi || "");
        setHasExisting(true);
      } else {
        setHasExisting(false);
      }
    } catch {
      setHasExisting(false);
    } finally {
      setLoading(false);
    }
  };

  const handleDevam = async () => {
    if (!subeId) {
      alert("Lütfen Şube seçin.");
      return;
    }
    if (!donemId) {
      alert("Lütfen Dönem seçin.");
      return;
    }

    setSaving(true);
    try {
      const payload = { subeId, donemId };

      // Her zaman önce PUT dene
      try {
        await apiClient.put(`/api/app/firma-parametre/${userId}`, payload);
        onComplete({ subeId, subeAdi, donemId, donemAdi });
        return;
      } catch {
        // PUT başarısız = kayıt yok, POST dene
      }

      // PUT başarısız olduysa POST dene
      try {
        await apiClient.post("/api/app/firma-parametre", { userId, ...payload });
        onComplete({ subeId, subeAdi, donemId, donemAdi });
      } catch (error: any) {
        const msg = error?.response?.data?.error?.message || "Kayıt hatası";
        alert(msg);
      }
    } finally {
      setSaving(false);
    }
  };

  return (
    <>
      <Modal
        title="Şube ve Dönem Seçimi"
        open={visible}
        closable={false}
        maskClosable={false}
        keyboard={false}
        width={450}
        footer={
          <Button
            type="primary"
            icon={<CheckOutlined />}
            onClick={handleDevam}
            loading={saving}
            disabled={!subeId || !donemId}
          >
            Devam
          </Button>
        }
      >
        {loading ? (
          <div style={{ textAlign: "center", padding: 40 }}>
            <Spin tip="Yükleniyor..." />
          </div>
        ) : (
          <div style={{ display: "flex", flexDirection: "column", gap: 12 }}>
            <DevButtonEdit
              label="Şube"
              value={subeAdi}
              onButtonClick={() => setSubeModalVisible(true)}
              onClear={() => {
                setSubeId("");
                setSubeAdi("");
              }}
              placeholder="Şube Seçin"
              readOnly
            />
            <DevButtonEdit
              label="Dönem"
              value={donemAdi}
              onButtonClick={() => setDonemModalVisible(true)}
              onClear={() => {
                setDonemId("");
                setDonemAdi("");
              }}
              placeholder="Dönem Seçin"
              readOnly
            />
          </div>
        )}
      </Modal>

      {/* Şube Seçim Modalı */}
      <SimpleSelectModal
        visible={subeModalVisible}
        title="Şube Seçin"
        apiUrl="/api/app/sube"
        onSelect={(item) => {
          setSubeId(item.id);
          setSubeAdi(item.ad);
          setSubeModalVisible(false);
        }}
        onCancel={() => setSubeModalVisible(false)}
      />

      {/* Dönem Seçim Modalı */}
      <SimpleSelectModal
        visible={donemModalVisible}
        title="Dönem Seçin"
        apiUrl="/api/app/donem"
        onSelect={(item) => {
          setDonemId(item.id);
          setDonemAdi(item.ad);
          setDonemModalVisible(false);
        }}
        onCancel={() => setDonemModalVisible(false)}
      />
    </>
  );
}