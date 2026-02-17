import { Switch, Form } from "antd";

interface DevCheckEditProps {
  label?: string;
  value?: boolean;
  onChange?: (value: boolean) => void;
  disabled?: boolean;
  checkedText?: string;
  uncheckedText?: string;
}

export default function DevCheckEdit({
  label,
  value = false,
  onChange,
  disabled = false,
  checkedText = "Aktif",
  uncheckedText = "Pasif",
}: DevCheckEditProps) {
  return (
    <Form.Item label={label}>
      <Switch
        checked={value}
        onChange={onChange}
        disabled={disabled}
        checkedChildren={checkedText}
        unCheckedChildren={uncheckedText}
      />
    </Form.Item>
  );
}