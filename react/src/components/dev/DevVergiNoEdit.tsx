import { Input, Form } from "antd";
import { BankOutlined } from "@ant-design/icons";

interface DevVergiNoEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
}

export default function DevVergiNoEdit({
  label = "Vergi No",
  value = "",
  onChange,
  required = false,
  disabled = false,
}: DevVergiNoEditProps) {
  const handleChange = (val: string) => {
    const digits = val.replace(/\D/g, "").slice(0, 10);
    onChange?.(digits);
  };

  return (
    <Form.Item
      label={label}
      required={required}
      validateStatus={value && value.length !== 10 ? "error" : ""}
      help={value && value.length !== 10 ? "Vergi No 10 haneli olmalıdır" : ""}
    >
      <Input
        value={value}
        onChange={(e) => handleChange(e.target.value)}
        prefix={<BankOutlined />}
        placeholder="XXXXXXXXXX"
        disabled={disabled}
        maxLength={10}
      />
    </Form.Item>
  );
}