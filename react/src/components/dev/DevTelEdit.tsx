import { Input, Form } from "antd";
import { PhoneOutlined } from "@ant-design/icons";

interface DevTelEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
  placeholder?: string;
}

export default function DevTelEdit({
  label,
  value = "",
  onChange,
  required = false,
  disabled = false,
  placeholder = "0 (5XX) XXX XX XX",
}: DevTelEditProps) {
  const formatPhone = (val: string) => {
    const digits = val.replace(/\D/g, "").slice(0, 11);
    if (digits.length <= 1) return digits;
    if (digits.length <= 4) return `${digits[0]} (${digits.slice(1)}`;
    if (digits.length <= 7) return `${digits[0]} (${digits.slice(1, 4)}) ${digits.slice(4)}`;
    if (digits.length <= 9) return `${digits[0]} (${digits.slice(1, 4)}) ${digits.slice(4, 7)} ${digits.slice(7)}`;
    return `${digits[0]} (${digits.slice(1, 4)}) ${digits.slice(4, 7)} ${digits.slice(7, 9)} ${digits.slice(9)}`;
  };

  return (
    <Form.Item label={label} required={required}>
      <Input
        value={value}
        onChange={(e) => onChange?.(formatPhone(e.target.value))}
        prefix={<PhoneOutlined />}
        placeholder={placeholder}
        disabled={disabled}
        maxLength={17}
      />
    </Form.Item>
  );
}