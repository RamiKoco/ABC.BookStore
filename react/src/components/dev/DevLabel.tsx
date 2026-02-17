import { Typography } from "antd";

interface DevLabelProps {
  text: string;
  bold?: boolean;
  type?: "secondary" | "success" | "warning" | "danger";
  size?: "small" | "default" | "large";
}

export default function DevLabel({
  text,
  bold = false,
  type,
  size = "default",
}: DevLabelProps) {
  const fontSize = size === "small" ? 12 : size === "large" ? 18 : 14;

  return (
    <Typography.Text
      strong={bold}
      type={type}
      style={{ fontSize }}
    >
      {text}
    </Typography.Text>
  );
}