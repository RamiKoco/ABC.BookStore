import { Input, Form } from "antd";

interface DevMemoEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  maxLength?: number;
  rows?: number;
  placeholder?: string;
}

export default function DevMemoEdit({
  label,
  value = "",
  onChange,
  maxLength,
  rows = 3,
  placeholder,
}: DevMemoEditProps) {
  return (
    <Form.Item label={label}>
      <Input.TextArea
        value={value}
        onChange={(e) => onChange?.(e.target.value)}
        maxLength={maxLength}
        rows={rows}
        placeholder={placeholder}
        showCount={!!maxLength}
      />
    </Form.Item>
  );
}