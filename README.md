BookStore React Projesi — Teknoloji & Yapı Özeti
Backend (C# / .NET)

ABP Framework — Modüler uygulama altyapısı
Entity Framework Core — ORM, migration, Include/WithDetailsAsync
Domain Driven Design (DDD) — Entity, Repository, Manager, AppService katmanları
AutoMapper — Entity ↔ DTO mapping
OpenIddict — Authentication (OAuth2/OIDC)
Permission System — Rol ve kullanıcı bazlı yetkilendirme
SQL Server — Veritabanı

Frontend (React / TypeScript)

React 18 + TypeScript — SPA framework
Vite — Build tool / dev server
Ant Design (antd) — UI component library (Table, Modal, Form, Tabs, Button, Tag, Space, Popconfirm vb.)
Ant Design Icons — İkon kütüphanesi
Axios — HTTP client (apiClient, interceptor ile token yönetimi)
react-oidc-context — OpenID Connect authentication

Özel Bileşenler (DevExpress Tarzı)
Kendi oluşturduğumuz components/dev/ bileşenleri:

DevTextEdit — Text input
DevComboBoxEdit — Dropdown select
DevDateEdit — Tarih seçici
DevCurrencyEdit — Para birimi input
DevCheckEdit — Toggle switch (Aktif/Pasif)
DevMemoEdit — Çok satırlı text area
DevButtonEdit — Ellipsis butonlu input (modal ile seçim)
DevDataGrid — Toolbar'lı, filtrelenebilir, sayfalanabilir tablo
DevListPageLayout — Drawer'lı liste-detay layout
DevGridLayout / DevGridLayoutItem — CSS Grid tabanlı form layout
DevTabEdit — Tab kontrol bileşeni

Sayfalar

HomePage — Giriş/karşılama sayfası
BooksPage — Kitap CRUD (tab'lı: Genel Bilgiler, Adres, Özel Kodlar, Açıklama)
IlPage — İl CRUD
UsersPage — Kullanıcı yönetimi (oluştur, düzenle, sil, izin ata)
RolesPage — Rol yönetimi (oluştur, düzenle, sil, izin ata)
PermissionsPage — İzin yönetimi

Seçim Modalları

OzelKodSelectModal — Özel Kod seçim/CRUD (KodTuru + KartTuru filtrelenmiş)
IlSelectModal — İl seçim/CRUD
IlceSelectModal — İlçe seçim/CRUD (seçili İl'e göre filtrelenmiş)

Temel Özellikler

JWT Bearer token authentication
Otomatik kod üretimi (her entity için /code endpoint)
Rol bazlı yetkilendirme (ABP Permission sistemi)
Durum filtresi (Aktif/Pasif toggle)
Çift tıkla düzenleme
Navigation property include (WithDetailsAsync override)
İl → İlçe bağımlı seçim (İl seçilmeden İlçe seçilemez)
Renkli, ikonlu navigasyon bar'ı

Kısaca: ABP Backend + React/TypeScript/Ant Design Frontend + Özel Dev bileşenleri ile tam kapsamlı bir ERP tarzı uygulama.
