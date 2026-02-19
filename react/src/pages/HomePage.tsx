import { useAuth } from "react-oidc-context";
import { Button, Typography, Avatar, Row, Col } from "antd";
import {
  LoginOutlined,
  UserOutlined,
  BookOutlined,
  BankOutlined,
  TeamOutlined,
  EnvironmentOutlined,
  CalendarOutlined,
  ShopOutlined,
  SettingOutlined,
  RightOutlined,
  LockOutlined,
} from "@ant-design/icons";

const { Title, Text } = Typography;

const quickLinks = [
  { href: "/books", label: "Kitaplar", icon: <BookOutlined />, color: "#fa8c16", desc: "Kitap kartlarını yönet" },
  { href: "/bankalar", label: "Bankalar", icon: <BankOutlined />, color: "#1677ff", desc: "Banka ve şube kartları" },
  { href: "/kisiler", label: "Kişiler", icon: <TeamOutlined />, color: "#fa541c", desc: "Kişi kayıtları" },
  { href: "/iller", label: "İller", icon: <EnvironmentOutlined />, color: "#52c41a", desc: "İl ve ilçe tanımları" },
  { href: "/donemler", label: "Dönemler", icon: <CalendarOutlined />, color: "#faad14", desc: "Dönem tanımları" },
  { href: "/subeler", label: "Şubeler", icon: <ShopOutlined />, color: "#2f54eb", desc: "Şube tanımları" },
];

const adminLinks = [
  { href: "/kullanicilar", label: "Kullanıcılar", icon: <UserOutlined />, color: "#eb2f96" },
  { href: "/roller", label: "Roller", icon: <SettingOutlined />, color: "#13c2c2" },
  { href: "/permissions", label: "İzin Yönetimi", icon: <LockOutlined />, color: "#722ed1" },
];

export default function HomePage() {
  const auth = useAuth();

  if (auth.isLoading) {
    return (
      <div style={{ display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh" }}>
        <div style={{ textAlign: "center" }}>
          <div style={{
            width: 48, height: 48, border: "4px solid #f0f0f0", borderTopColor: "#1677ff",
            borderRadius: "50%", animation: "spin 0.8s linear infinite", margin: "0 auto 16px",
          }} />
          <Text style={{ color: "#888" }}>Yükleniyor...</Text>
          <style>{`@keyframes spin { to { transform: rotate(360deg); } }`}</style>
        </div>
      </div>
    );
  }

  if (!auth.isAuthenticated) {
    return (
      <div style={{
        display: "flex", justifyContent: "center", alignItems: "center", minHeight: "75vh",
      }}>
        <div style={{
          textAlign: "center", padding: "60px 48px",
          background: "linear-gradient(135deg, #ffffff 0%, #f8faff 100%)",
          borderRadius: 20, boxShadow: "0 8px 40px rgba(0,0,0,0.06)",
          border: "1px solid rgba(0,0,0,0.04)", maxWidth: 480,
        }}>
          <div style={{
            width: 88, height: 88, borderRadius: 20,
            background: "linear-gradient(135deg, #fa8c16, #ff6b35)",
            display: "flex", alignItems: "center", justifyContent: "center",
            margin: "0 auto 28px", boxShadow: "0 8px 24px rgba(250,140,22,0.3)",
          }}>
            <BookOutlined style={{ fontSize: 40, color: "#fff" }} />
          </div>
          <Title level={2} style={{ margin: 0, fontWeight: 700, letterSpacing: -0.5 }}>
            BookStore
          </Title>
          <Text style={{ fontSize: 16, color: "#8c8c8c", display: "block", margin: "8px 0 36px" }}>
            Kurumsal Yönetim Paneli
          </Text>
          <Button
            type="primary"
            icon={<LoginOutlined />}
            size="large"
            onClick={() => auth.signinRedirect()}
            style={{
              borderRadius: 12, height: 52, fontSize: 16, paddingInline: 48,
              background: "linear-gradient(135deg, #1677ff, #4096ff)",
              border: "none", boxShadow: "0 4px 16px rgba(22,119,255,0.3)",
              fontWeight: 600,
            }}
          >
            Giriş Yap
          </Button>
        </div>
      </div>
    );
  }

  const userName = auth.user?.profile.name || auth.user?.profile.preferred_username || "Kullanıcı";
  const hour = new Date().getHours();
  const greeting = hour < 12 ? "Günaydın" : hour < 18 ? "İyi Günler" : "İyi Akşamlar";

  return (
    <div style={{ maxWidth: 960, margin: "0 auto" }}>
      {/* Welcome Header */}
      <div style={{
        background: "linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%)",
        borderRadius: 20, padding: "36px 40px", marginBottom: 24,
        position: "relative", overflow: "hidden",
      }}>
        <div style={{
          position: "absolute", top: -30, right: -30, width: 120, height: 120,
          borderRadius: "50%", background: "rgba(255,255,255,0.04)",
        }} />
        <div style={{
          position: "absolute", bottom: -40, right: 80, width: 180, height: 180,
          borderRadius: "50%", background: "rgba(255,255,255,0.02)",
        }} />
        <div style={{
          position: "absolute", top: 20, right: 160, width: 8, height: 8,
          borderRadius: "50%", background: "rgba(250,140,22,0.6)",
        }} />
        <div style={{
          position: "absolute", bottom: 30, right: 40, width: 6, height: 6,
          borderRadius: "50%", background: "rgba(22,119,255,0.6)",
        }} />

        <div style={{ display: "flex", alignItems: "center", gap: 20, position: "relative", zIndex: 1 }}>
          <Avatar
            size={64}
            icon={<UserOutlined />}
            style={{
              background: "linear-gradient(135deg, #fa8c16, #ff6b35)",
              boxShadow: "0 4px 16px rgba(250,140,22,0.4)",
              fontSize: 28,
            }}
          />
          <div>
            <Text style={{ color: "rgba(255,255,255,0.5)", fontSize: 14, display: "block" }}>
              {greeting} 👋
            </Text>
            <Title level={3} style={{ margin: 0, color: "#fff", fontWeight: 700 }}>
              {userName}
            </Title>
          </div>
        </div>
      </div>

      {/* Hızlı Erişim */}
      <div style={{ marginBottom: 20 }}>
        <Text style={{ fontSize: 15, fontWeight: 600, color: "#555", display: "block", marginBottom: 12, paddingLeft: 4 }}>
          Hızlı Erişim
        </Text>
        <Row gutter={[16, 16]}>
          {quickLinks.map((link) => (
            <Col xs={24} sm={12} md={8} key={link.href}>
              <a href={link.href} style={{ textDecoration: "none" }}>
                <div style={{
                  background: "#fff", borderRadius: 14, padding: "20px",
                  border: "1px solid #f0f0f0", cursor: "pointer",
                  transition: "all 0.25s ease",
                  display: "flex", alignItems: "center", gap: 14,
                }}
                  onMouseEnter={(e) => {
                    e.currentTarget.style.transform = "translateY(-2px)";
                    e.currentTarget.style.boxShadow = "0 8px 24px rgba(0,0,0,0.08)";
                    e.currentTarget.style.borderColor = link.color;
                  }}
                  onMouseLeave={(e) => {
                    e.currentTarget.style.transform = "translateY(0)";
                    e.currentTarget.style.boxShadow = "none";
                    e.currentTarget.style.borderColor = "#f0f0f0";
                  }}
                >
                  <div style={{
                    width: 44, height: 44, borderRadius: 12,
                    background: `${link.color}14`, color: link.color,
                    display: "flex", alignItems: "center", justifyContent: "center",
                    fontSize: 20, flexShrink: 0,
                  }}>
                    {link.icon}
                  </div>
                  <div style={{ flex: 1, minWidth: 0 }}>
                    <Text style={{ fontSize: 15, fontWeight: 600, color: "#1a1a1a", display: "block" }}>
                      {link.label}
                    </Text>
                    <Text style={{ fontSize: 12, color: "#8c8c8c" }}>
                      {link.desc}
                    </Text>
                  </div>
                  <RightOutlined style={{ color: "#d0d0d0", fontSize: 12 }} />
                </div>
              </a>
            </Col>
          ))}
        </Row>
      </div>

      {/* Yönetim */}
      <div>
        <Text style={{ fontSize: 15, fontWeight: 600, color: "#555", display: "block", marginBottom: 12, paddingLeft: 4 }}>
          Yönetim
        </Text>
        <Row gutter={[16, 16]}>
          {adminLinks.map((link) => (
            <Col xs={24} sm={8} key={link.href}>
              <a href={link.href} style={{ textDecoration: "none" }}>
                <div style={{
                  background: "#fff", borderRadius: 14, padding: "16px 20px",
                  border: "1px solid #f0f0f0", cursor: "pointer",
                  transition: "all 0.25s ease",
                  display: "flex", alignItems: "center", gap: 14,
                }}
                  onMouseEnter={(e) => {
                    e.currentTarget.style.transform = "translateY(-2px)";
                    e.currentTarget.style.boxShadow = "0 8px 24px rgba(0,0,0,0.08)";
                    e.currentTarget.style.borderColor = link.color;
                  }}
                  onMouseLeave={(e) => {
                    e.currentTarget.style.transform = "translateY(0)";
                    e.currentTarget.style.boxShadow = "none";
                    e.currentTarget.style.borderColor = "#f0f0f0";
                  }}
                >
                  <div style={{
                    width: 40, height: 40, borderRadius: 10,
                    background: `${link.color}14`, color: link.color,
                    display: "flex", alignItems: "center", justifyContent: "center",
                    fontSize: 18, flexShrink: 0,
                  }}>
                    {link.icon}
                  </div>
                  <Text style={{ fontSize: 14, fontWeight: 600, color: "#1a1a1a" }}>
                    {link.label}
                  </Text>
                  <RightOutlined style={{ color: "#d0d0d0", fontSize: 12, marginLeft: "auto" }} />
                </div>
              </a>
            </Col>
          ))}
        </Row>
      </div>
    </div>
  );
}