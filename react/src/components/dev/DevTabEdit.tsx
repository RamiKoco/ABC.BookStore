import { Tabs } from "antd";
import type { ReactNode } from "react";

interface TabItem {
  key: string;
  label: string;
  children: ReactNode;
  icon?: ReactNode;
  disabled?: boolean;
}

interface DevTabEditProps {
  items: TabItem[];
  activeKey?: string;
  onChange?: (key: string) => void;
  type?: "line" | "card";
}

export default function DevTabEdit({
  items,
  activeKey,
  onChange,
  type = "line",
}: DevTabEditProps) {
  return (
    <Tabs
      activeKey={activeKey}
      onChange={onChange}
      type={type}
      items={items}
    />
  );
}