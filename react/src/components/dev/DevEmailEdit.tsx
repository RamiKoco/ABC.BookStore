import { Input, Form } from "antd";
import { MailOutlined } from "@ant-design/icons";

interface DevEmailEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
  placeholder?: string;
}

export default function DevEmailEdit({
  label,
  value = "",
  onChange,
  required = false,
  disabled = false,
  placeholder = "ornek@email.com",
}: DevEmailEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <Input
        type="email"
        value={value}
        onChange={(e) => onChange?.(e.target.value)}
        prefix={<MailOutlined />}
        placeholder={placeholder}
        disabled={disabled}
      />
    </Form.Item>
  );
}