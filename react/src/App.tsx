import CallbackPage from "./pages/CallbackPage";
import HomePage from "./pages/HomePage";
import BooksPage from "./pages/BooksPage";
import PermissionsPage from "./pages/PermissionsPage";
import { useAuth } from "react-oidc-context";

function App() {
  const path = window.location.pathname;
  const auth = useAuth();

  if (path === "/callback") {
    return <CallbackPage />;
  }

  const nav = (
    <nav style={{ marginBottom: "20px" }}>
      <a href="/" style={{ marginRight: "15px" }}>Ana Sayfa</a>
      <a href="/books" style={{ marginRight: "15px" }}>Kitaplar</a>
      <a href="/permissions" style={{ marginRight: "15px" }}>İzin Yönetimi</a>
      {auth.isAuthenticated && (
        <button onClick={() => auth.removeUser()}>Çıkış Yap</button>
      )}
    </nav>
  );

  const content = () => {
    if (path === "/books") return <BooksPage />;
    if (path === "/permissions") return <PermissionsPage />;
    return <HomePage />;
  };

  return (
    <div style={{ padding: "20px" }}>
      {nav}
      {content()}
    </div>
  );
}

export default App;