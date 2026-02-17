import { Input, Button, Space } from "antd";
import { EditOutlined } from "@ant-design/icons";

interface DevButtonEditProps {
  value?: string;
  onChange?: (value: string) => void;
  onButtonClick?: () => void;
  readOnly?: boolean;
  placeholder?: string;
  disabled?: boolean;
}

export default function DevButtonEdit({
  value = "",
  onChange,
  onButtonClick,
  readOnly = true,
  placeholder,
  disabled = false,
}: DevButtonEditProps) {
  return (
    <Space.Compact style={{ width: "100%" }}>
      <Input
        value={value}
       onChange={(e: React.ChangeEvent<HTMLInputElement>) => onChange?.(e.target.value)}
        readOnly={readOnly}
        placeholder={placeholder}
        disabled={disabled}
      />
      <Button
        icon={<EditOutlined />}
        onClick={onButtonClick}
        disabled={disabled}
      />
    </Space.Compact>
  );
}