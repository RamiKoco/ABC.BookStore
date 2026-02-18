import { Input, Button, Space } from "antd";
import { EllipsisOutlined, CloseCircleOutlined } from "@ant-design/icons";

interface DevButtonEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  onButtonClick?: () => void;
  onClear?: () => void;
  readOnly?: boolean;
  placeholder?: string;
  disabled?: boolean;
  required?: boolean;
}

export default function DevButtonEdit({
  label,
  value = "",
  onChange,
  onButtonClick,
  onClear,
  readOnly = true,
  placeholder,
  disabled = false,
  required = false,
}: DevButtonEditProps) {
  return (
    <div style={{ marginBottom: 8 }}>
      {label && (
        <label
          style={{
            display: "block",
            marginBottom: 4,
            fontSize: 14,
            fontWeight: 500,
          }}
        >
          {required && <span style={{ color: "#ff4d4f", marginRight: 4 }}>*</span>}
          {label}
        </label>
      )}
      <Space.Compact style={{ width: "100%" }}>
        <Input
          value={value}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            onChange?.(e.target.value)
          }
          readOnly={readOnly}
          placeholder={placeholder}
          disabled={disabled}
        />
        {value && onClear && !disabled && (
          <Button
            icon={<CloseCircleOutlined />}
            onClick={onClear}
            title="Temizle"
          />
        )}
        <Button
          icon={<EllipsisOutlined />}
          onClick={onButtonClick}
          disabled={disabled}
          title="Seç"
        />
      </Space.Compact>
    </div>
  );
}