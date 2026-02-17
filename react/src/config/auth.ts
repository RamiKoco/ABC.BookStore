import { WebStorageStateStore } from "oidc-client-ts";

export const oidcConfig = {
  authority: "https://localhost:44335",
  client_id: "BookStore_React",
  redirect_uri: "http://localhost:5173/callback",
  post_logout_redirect_uri: "http://localhost:5173",
  response_type: "code",
  scope: "openid profile email phone roles BookStore",
  userStore: new WebStorageStateStore({ store: window.localStorage }),
};