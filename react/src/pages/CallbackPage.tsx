import { useAuth } from "react-oidc-context";
import { useEffect } from "react";

export default function CallbackPage() {
  const auth = useAuth();

  useEffect(() => {
    if (!auth.isLoading && auth.isAuthenticated) {
      window.location.href = "/";
    }
  }, [auth.isLoading, auth.isAuthenticated]);

  return <div>Giriş yapılıyor...</div>;
}