import { Input, Form } from "antd";
import { CreditCardOutlined } from "@ant-design/icons";

interface DevIbanNoEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
}

export default function DevIbanNoEdit({
  label = "IBAN",
  value = "",
  onChange,
  required = false,
  disabled = false,
}: DevIbanNoEditProps) {
  const formatIban = (val: string) => {
    const clean = val.replace(/\s/g, "").toUpperCase().slice(0, 26);
    return clean.replace(/(.{4})/g, "$1 ").trim();
  };

  return (
    <Form.Item
      label={label}
      required={required}
      validateStatus={value && value.replace(/\s/g, "").length !== 26 ? "error" : ""}
      help={value && value.replace(/\s/g, "").length !== 26 ? "IBAN 26 karakter olmalıdır" : ""}
    >
      <Input
        value={value}
        onChange={(e) => onChange?.(formatIban(e.target.value))}
        prefix={<CreditCardOutlined />}
        placeholder="TR00 0000 0000 0000 0000 0000 00"
        disabled={disabled}
        maxLength={32}
      />
    </Form.Item>
  );
}