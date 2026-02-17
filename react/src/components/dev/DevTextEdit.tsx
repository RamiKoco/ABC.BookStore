import { Input, Form } from "antd";

interface DevTextEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  maxLength?: number;
  placeholder?: string;
  disabled?: boolean;
}

export default function DevTextEdit({
  label,
  value = "",
  onChange,
  required = false,
  maxLength,
  placeholder,
  disabled = false,
}: DevTextEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <Input
        value={value}
        onChange={(e) => onChange?.(e.target.value)}
        maxLength={maxLength}
        placeholder={placeholder}
        disabled={disabled}
      />
    </Form.Item>
  );
}