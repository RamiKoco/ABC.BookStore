import CallbackPage from "./pages/CallbackPage";
import HomePage from "./pages/HomePage";
import BooksPage from "./pages/BooksPage";
import BankaPage from "./pages/BankaPage";
import IlPage from "./pages/IlPage";
import UsersPage from "./pages/UsersPage";
import RolesPage from "./pages/RolesPage";
import PermissionsPage from "./pages/PermissionsPage";
import { useAuth } from "react-oidc-context";
import { Button, Space } from "antd";
import {
  HomeOutlined,
  BookOutlined,
  BankOutlined,
  EnvironmentOutlined,
  LockOutlined,
  UserOutlined,
  LogoutOutlined,
} from "@ant-design/icons";

function App() {
  const path = window.location.pathname;
  const auth = useAuth();

  if (path === "/callback") {
    return <CallbackPage />;
  }

  const navItems = [
    { href: "/", label: "Ana Sayfa", icon: <HomeOutlined />, color: "#1677ff" },
    { href: "/books", label: "Kitaplar", icon: <BookOutlined />, color: "#fa8c16" },
    { href: "/bankalar", label: "Bankalar", icon: <BankOutlined />, color: "#1677ff" },
    { href: "/iller", label: "İller", icon: <EnvironmentOutlined />, color: "#52c41a" },
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
      <Space size="middle">
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
      {auth.isAuthenticated && (
        <Button
          danger
          icon={<LogoutOutlined />}
          onClick={() => auth.removeUser()}
        >
          Çıkış Yap
        </Button>
      )}
    </div>
  );

  const content = () => {
    if (path === "/books") return <BooksPage />;
    if (path === "/bankalar") return <BankaPage />;
    if (path === "/iller") return <IlPage />;
    if (path === "/kullanicilar") return <UsersPage />;
    if (path === "/roller") return <RolesPage />;
    if (path === "/permissions") return <PermissionsPage />;
    return <HomePage />;
  };

  return (
    <div style={{ padding: "20px", background: "#f5f5f5", minHeight: "100vh" }}>
      {nav}
      {content()}
    </div>
  );
}

export default App;