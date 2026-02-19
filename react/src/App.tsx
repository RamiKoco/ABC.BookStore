import { useState } from "react";
import CallbackPage from "./pages/CallbackPage";
import HomePage from "./pages/HomePage";
import BooksPage from "./pages/BooksPage";
import BankaPage from "./pages/BankaPage";
import KisiPage from "./pages/KisiPage";
import DonemPage from "./pages/DonemPage";
import SubePage from "./pages/SubePage";
import IlPage from "./pages/IlPage";
import UsersPage from "./pages/UsersPage";
import RolesPage from "./pages/RolesPage";
import PermissionsPage from "./pages/PermissionsPage";
import FirmaParametreModal from "./pages/FirmaParametreModal";
import { useAuth } from "react-oidc-context";
import { Button, Space, Tag } from "antd";
import {
  HomeOutlined,
  BookOutlined,
  BankOutlined,
  EnvironmentOutlined,
  LockOutlined,
  UserOutlined,
  TeamOutlined,
  CalendarOutlined,
  ShopOutlined,
  LogoutOutlined,
} from "@ant-design/icons";

function App() {
  const path = window.location.pathname;
  const auth = useAuth();

  // Firma parametre state - sessionStorage ile persist
  const [firmaParamReady, setFirmaParamReady] = useState(() => {
    return sessionStorage.getItem("firmaParamReady") === "true";
  });
  const [firmaParam, setFirmaParam] = useState(() => {
    const saved = sessionStorage.getItem("firmaParam");
    if (saved) {
      try { return JSON.parse(saved); } catch { }
    }
    return { subeId: "", subeAdi: "", donemId: "", donemAdi: "" };
  });

  if (path === "/callback") {
    return <CallbackPage />;
  }

  const userId = auth.user?.profile?.sub || "";
  const isLoggedIn = auth.isAuthenticated;

  // Login sonrası FirmaParametre seçimi
  const showFirmaModal = isLoggedIn && !firmaParamReady;

  const navItems = [
    { href: "/", label: "Ana Sayfa", icon: <HomeOutlined />, color: "#1677ff" },
    { href: "/books", label: "Kitaplar", icon: <BookOutlined />, color: "#fa8c16" },
    { href: "/bankalar", label: "Bankalar", icon: <BankOutlined />, color: "#1677ff" },
    { href: "/kisiler", label: "Kişiler", icon: <TeamOutlined />, color: "#fa541c" },
    { href: "/iller", label: "İller", icon: <EnvironmentOutlined />, color: "#52c41a" },
    { href: "/donemler", label: "Dönemler", icon: <CalendarOutlined />, color: "#faad14" },
    { href: "/subeler", label: "Şubeler", icon: <ShopOutlined />, color: "#2f54eb" },
    { href: "/permissions", label: "İzin Yönetimi", icon: <LockOutlined />, color: "#722ed1" },
    { href: "/kullanicilar", label: "Kullanıcılar", icon: <UserOutlined />, color: "#eb2f96" },
    { href: "/roller", label: "Roller", icon: <LockOutlined />, color: "#13c2c2" },
  ];

  const nav = (
    <div
      style={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        marginBottom: 20,
        padding: "12px 20px",
        background: "#fff",
        borderRadius: 8,
        boxShadow: "0 1px 4px rgba(0,0,0,0.1)",
      }}
    >
      <Space size="middle" wrap>
        {navItems.map((item) => (
          <Button
            key={item.href}
            type={path === item.href ? "primary" : "default"}
            icon={item.icon}
            href={item.href}
            style={
              path === item.href
                ? { backgroundColor: item.color, borderColor: item.color }
                : { color: item.color, borderColor: item.color }
            }
          >
            {item.label}
          </Button>
        ))}
      </Space>
      <Space>
        {isLoggedIn && firmaParamReady && (
          <Space size="small">
            <Tag color="blue">{firmaParam.subeAdi}</Tag>
            <Tag color="orange">{firmaParam.donemAdi}</Tag>
          </Space>
        )}
        {isLoggedIn && (
          <Button
            danger
            icon={<LogoutOutlined />}
            onClick={() => {
              setFirmaParamReady(false);
              sessionStorage.removeItem("firmaParamReady");
              sessionStorage.removeItem("firmaParam");
              auth.removeUser();
            }}
          >
            Çıkış Yap
          </Button>
        )}
      </Space>
    </div>
  );

  const content = () => {
    if (path === "/books") return <BooksPage />;
    if (path === "/bankalar") return <BankaPage />;
    if (path === "/kisiler") return <KisiPage />;
    if (path === "/iller") return <IlPage />;
    if (path === "/donemler") return <DonemPage />;
    if (path === "/subeler") return <SubePage />;
    if (path === "/kullanicilar") return <UsersPage />;
    if (path === "/roller") return <RolesPage />;
    if (path === "/permissions") return <PermissionsPage />;
    return <HomePage />;
  };

  return (
    <div style={{ padding: "20px", background: "#f5f5f5", minHeight: "100vh" }}>
      {nav}
      {content()}

      {/* Login sonrası Şube/Dönem seçimi */}
      <FirmaParametreModal
        visible={showFirmaModal}
        userId={userId}
        onComplete={(params) => {
          setFirmaParam(params);
          setFirmaParamReady(true);
          sessionStorage.setItem("firmaParamReady", "true");
          sessionStorage.setItem("firmaParam", JSON.stringify(params));
        }}
      />
    </div>
  );
}

export default App;