import { useAuth } from "react-oidc-context";
import { useState } from "react";
import apiClient from "../config/api";

export default function HomePage() {
  const auth = useAuth();
  const [apiResult, setApiResult] = useState<string>("");

  const testApi = async () => {
    try {
      const response = await apiClient.get("/api/abp/application-configuration");
      setApiResult(JSON.stringify(response.data.currentUser, null, 2));
    } catch (error) {
      setApiResult("API hatası: " + error);
    }
  };

  if (auth.isLoading) return <div>Yükleniyor...</div>;

  return (
    <div style={{ padding: "20px" }}>
      <h1>BookStore React</h1>
      {auth.isAuthenticated ? (
        <div>
          <p>Hoş geldin, <strong>{auth.user?.profile.name || auth.user?.profile.preferred_username || "Kullanıcı"}</strong>!</p>
          <button onClick={testApi}>API Test</button>
          <button onClick={() => auth.removeUser()} style={{ marginLeft: "10px" }}>
            Çıkış Yap
          </button>
          {apiResult && <pre style={{ marginTop: "20px" }}>{apiResult}</pre>}
        </div>
      ) : (
        <div>
          <p>Giriş yapmadınız.</p>
          <button onClick={() => auth.signinRedirect()}>Giriş Yap</button>
        </div>
      )}
    </div>
  );
}