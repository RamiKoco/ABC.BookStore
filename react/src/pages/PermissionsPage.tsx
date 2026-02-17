import { useState, useEffect } from "react";
import apiClient from "../config/api";

interface Permission {
  name: string;
  displayName: string;
  isGranted: boolean;
  parentName: string | null;
  allowedProviders: string[];
}

interface PermissionGroup {
  name: string;
  displayName: string;
  permissions: Permission[];
}

export default function PermissionsPage() {
  const [groups, setGroups] = useState<PermissionGroup[]>([]);
  const [selectedGroup, setSelectedGroup] = useState<string>("");
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [roleName, setRoleName] = useState("admin");

  const fetchPermissions = async () => {
    setLoading(true);
    try {
      const response = await apiClient.get("/api/permission-management/permissions", {
        params: { providerName: "R", providerKey: roleName },
      });
      const data = response.data;
      setGroups(data.groups);
      if (data.groups.length > 0 && !selectedGroup) {
        setSelectedGroup(data.groups[0].name);
      }
    } catch (error) {
      console.error("Permission yüklenemedi:", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchPermissions();
  }, [roleName]);

  const togglePermission = (groupName: string, permName: string) => {
    setGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        return {
          ...g,
          permissions: g.permissions.map((p) => {
            if (p.name === permName) {
              const newGranted = !p.isGranted;
              return { ...p, isGranted: newGranted };
            }
            return p;
          }),
        };
      })
    );
  };

  const toggleAll = (groupName: string) => {
    setGroups((prev) =>
      prev.map((g) => {
        if (g.name !== groupName) return g;
        const allGranted = g.permissions.every((p) => p.isGranted);
        return {
          ...g,
          permissions: g.permissions.map((p) => ({ ...p, isGranted: !allGranted })),
        };
      })
    );
  };

  const toggleAllPermissions = () => {
    const allGranted = groups.every((g) => g.permissions.every((p) => p.isGranted));
    setGroups((prev) =>
      prev.map((g) => ({
        ...g,
        permissions: g.permissions.map((p) => ({ ...p, isGranted: !allGranted })),
      }))
    );
  };

 const handleSave = async () => {
    setSaving(true);
    try {
      const permissions: { name: string; isGranted: boolean }[] = [];
      groups.forEach((g) => {
        g.permissions.forEach((p) => {
          permissions.push({ name: p.name, isGranted: p.isGranted });
        });
      });

      await apiClient.put(
        `/api/permission-management/permissions?providerName=R&providerKey=${roleName}`,
        { permissions }
      );
      alert("İzinler başarıyla kaydedildi!");
      await fetchPermissions();
    } catch (error: any) {
      const msg = error?.response?.data?.error?.message || "İzinler kaydedilemedi!";
      console.error("Kayıt hatası:", error?.response?.data);
      alert(msg);
    } finally {
      setSaving(false);
    }
  };

  const currentGroup = groups.find((g) => g.name === selectedGroup);
  const allGranted = groups.every((g) => g.permissions.every((p) => p.isGranted));

  if (loading) return <div>Yükleniyor...</div>;

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <h2>İzinler - {roleName}</h2>
        <div>
          <label>Rol: </label>
          <select value={roleName} onChange={(e) => { setRoleName(e.target.value); setSelectedGroup(""); }} style={inputStyle}>
            <option value="admin">admin</option>
          </select>
        </div>
      </div>

      <div style={{ marginBottom: "15px" }}>
        <label>
          <input type="checkbox" checked={allGranted} onChange={toggleAllPermissions} />
          <strong> Tüm izinleri ver</strong>
        </label>
      </div>

      <div style={{ display: "flex", gap: "20px" }}>
        {/* Sol Panel - Gruplar */}
        <div style={sidebarStyle}>
          {groups.map((g) => {
            const grantedCount = g.permissions.filter((p) => p.isGranted).length;
            return (
              <div
                key={g.name}
                onClick={() => setSelectedGroup(g.name)}
                style={{
                  ...sidebarItem,
                  backgroundColor: selectedGroup === g.name ? "#3498db" : "transparent",
                  color: selectedGroup === g.name ? "white" : "#333",
                }}
              >
                {g.displayName} ({grantedCount})
              </div>
            );
          })}
        </div>

        {/* Sağ Panel - Permission'lar */}
        <div style={{ flex: 1 }}>
          {currentGroup && (
            <div>
              <h3>{currentGroup.displayName}</h3>
              <div style={{ marginBottom: "10px" }}>
                <label>
                  <input
                    type="checkbox"
                    checked={currentGroup.permissions.every((p) => p.isGranted)}
                    onChange={() => toggleAll(currentGroup.name)}
                  />
                  <strong> Hepsini seç</strong>
                </label>
              </div>
              {currentGroup.permissions.map((p) => {
                const isChild = p.parentName !== null;
                return (
                  <div key={p.name} style={{ marginLeft: isChild ? "25px" : "0", marginBottom: "8px" }}>
                    <label>
                      <input
                        type="checkbox"
                        checked={p.isGranted}
                        onChange={() => togglePermission(currentGroup.name, p.name)}
                      />
                      {" "}{p.displayName}
                    </label>
                  </div>
                );
              })}
            </div>
          )}
        </div>
      </div>

      <div style={{ marginTop: "20px", textAlign: "right" }}>
        <button onClick={() => fetchPermissions()} style={{ ...btnStyle, backgroundColor: "#888", marginRight: "10px" }}>
          Vazgeç
        </button>
        <button onClick={handleSave} disabled={saving} style={btnStyle}>
          {saving ? "Kaydediliyor..." : "Kaydet"}
        </button>
      </div>
    </div>
  );
}

const btnStyle: React.CSSProperties = { backgroundColor: "#3498db", color: "white", border: "none", padding: "8px 16px", borderRadius: "4px", cursor: "pointer" };
const inputStyle: React.CSSProperties = { padding: "6px 10px", borderRadius: "4px", border: "1px solid #ccc" };
const sidebarStyle: React.CSSProperties = { width: "250px", borderRight: "1px solid #ddd", paddingRight: "15px" };
const sidebarItem: React.CSSProperties = { padding: "10px", cursor: "pointer", borderRadius: "4px", marginBottom: "4px" };