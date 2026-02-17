import { Select, Form } from "antd";

interface DevComboBoxEnumEditProps {
  label?: string;
  value?: number;
  onChange?: (value: number) => void;
  enumObject: Record<string | number, string | number>;
  required?: boolean;
  disabled?: boolean;
  placeholder?: string;
}

export default function DevComboBoxEnumEdit({
  label,
  value,
  onChange,
  enumObject,
  required = false,
  disabled = false,
  placeholder,
}: DevComboBoxEnumEditProps) {
  const options = Object.entries(enumObject)
    .filter(([key]) => isNaN(Number(key)))
    .map(([key, val]) => ({ label: key, value: val as number }));

  return (
    <Form.Item label={label} required={required}>
      <Select
        value={value}
        onChange={onChange}
        options={options}
        placeholder={placeholder}
        disabled={disabled}
        allowClear
      />
    </Form.Item>
  );
}