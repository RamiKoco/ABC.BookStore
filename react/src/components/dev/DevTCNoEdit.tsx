import { Input, Form } from "antd";
import { IdcardOutlined } from "@ant-design/icons";

interface DevTCNoEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
}

export default function DevTCNoEdit({
  label = "T.C. Kimlik No",
  value = "",
  onChange,
  required = false,
  disabled = false,
}: DevTCNoEditProps) {
  const handleChange = (val: string) => {
    const digits = val.replace(/\D/g, "").slice(0, 11);
    onChange?.(digits);
  };

  return (
    <Form.Item
      label={label}
      required={required}
      validateStatus={value && value.length !== 11 ? "error" : ""}
      help={value && value.length !== 11 ? "T.C. Kimlik No 11 haneli olmalıdır" : ""}
    >
      <Input
        value={value}
        onChange={(e) => handleChange(e.target.value)}
        prefix={<IdcardOutlined />}
        placeholder="XXXXXXXXXXX"
        disabled={disabled}
        maxLength={11}
      />
    </Form.Item>
  );
}