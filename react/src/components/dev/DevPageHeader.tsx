import { Typography, Divider } from "antd";
import type { ReactNode } from "react";

interface DevPageHeaderProps {
  caption: string;
  extra?: ReactNode;
}

export default function DevPageHeader({
  caption,
  extra,
}: DevPageHeaderProps) {
  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Typography.Title level={4} style={{ margin: 0 }}>
          {caption}
        </Typography.Title>
        {extra && <div>{extra}</div>}
      </div>
      <Divider style={{ margin: "12px 0" }} />
    </div>
  );
}