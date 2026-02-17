import { Spin, Typography, Drawer } from "antd";
import type { ReactNode } from "react";

interface DevListPageLayoutProps {
  children: ReactNode;
  caption?: string;
  loading?: boolean;
  loadingText?: string;
  // Edit drawer
  editVisible?: boolean;
  editTitle?: string;
  editContent?: ReactNode;
  editWidth?: number;
  onEditClose?: () => void;
  editExtra?: ReactNode;
}

export default function DevListPageLayout({
  children,
  caption,
  loading = false,
  loadingText = "Yükleniyor...",
  editVisible = false,
  editTitle,
  editContent,
  editWidth = 500,
  onEditClose,
  editExtra,
}: DevListPageLayoutProps) {
  if (loading) {
    return (
      <div style={{ display: "flex", flexDirection: "column", alignItems: "center", justifyContent: "center", minHeight: 300 }}>
        <Spin size="large" />
        <Typography.Text style={{ marginTop: 16 }}>{loadingText}</Typography.Text>
      </div>
    );
  }

  return (
    <div>
      {caption && (
        <Typography.Title level={3} style={{ marginBottom: 16 }}>
          {caption}
        </Typography.Title>
      )}

      {children}

      {editContent && (
        <Drawer
          title={editTitle}
          open={editVisible}
          onClose={onEditClose}
          width={editWidth}
          extra={editExtra}
          destroyOnClose
        >
          {editContent}
        </Drawer>
      )}
    </div>
  );
}