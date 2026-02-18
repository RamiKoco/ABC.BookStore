import { useAuth } from "react-oidc-context";
import { Button, Card, Typography, Space, Avatar } from "antd";
import {
  LoginOutlined,
  UserOutlined,
  BookOutlined,
} from "@ant-design/icons";

const { Title, Text } = Typography;

export default function HomePage() {
  const auth = useAuth();

  if (auth.isLoading) return <div>Yükleniyor...</div>;

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        minHeight: "70vh",
      }}
    >
      <Card
        style={{
          width: 420,
          textAlign: "center",
          borderRadius: 12,
          boxShadow: "0 4px 20px rgba(0,0,0,0.08)",
        }}
      >
        {auth.isAuthenticated ? (
          <Space direction="vertical" size="large" style={{ width: "100%" }}>
            <Avatar
              size={72}
              icon={<UserOutlined />}
              style={{ backgroundColor: "#1677ff" }}
            />
            <div>
              <Title level={3} style={{ margin: 0 }}>
                Hoş Geldin!
              </Title>
              <Text
                style={{ fontSize: 18, color: "#555", display: "block", marginTop: 8 }}
              >
                {auth.user?.profile.name ||
                  auth.user?.profile.preferred_username ||
                  "Kullanıcı"}
              </Text>
            </div>
            <Button
              type="primary"
              icon={<BookOutlined />}
              size="large"
              href="/books"
              style={{ borderRadius: 8 }}
            >
              Kitaplara Git
            </Button>
          </Space>
        ) : (
          <Space direction="vertical" size="large" style={{ width: "100%" }}>
            <BookOutlined style={{ fontSize: 64, color: "#fa8c16" }} />
            <div>
              <Title level={2} style={{ margin: 0 }}>
                BookStore
              </Title>
              <Text style={{ fontSize: 16, color: "#888" }}>
                Yönetim Paneli
              </Text>
            </div>
            <Button
              type="primary"
              icon={<LoginOutlined />}
              size="large"
              onClick={() => auth.signinRedirect()}
              style={{
                borderRadius: 8,
                height: 48,
                fontSize: 16,
                paddingInline: 40,
              }}
            >
              Giriş Yap
            </Button>
          </Space>
        )}
      </Card>
    </div>
  );
}