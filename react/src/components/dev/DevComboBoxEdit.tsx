import { Select, Form } from "antd";

interface Option {
  value: string | number;
  label: string;
}

interface DevComboBoxEditProps {
  label?: string;
  value?: string | number;
  onChange?: (value: string | number) => void;
  options: Option[];
  placeholder?: string;
  required?: boolean;
  disabled?: boolean;
  allowClear?: boolean;
}

export default function DevComboBoxEdit({
  label,
  value,
  onChange,
  options,
  placeholder,
  required = false,
  disabled = false,
  allowClear = true,
}: DevComboBoxEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <Select
        value={value}
        onChange={onChange}
        options={options}
        placeholder={placeholder}
        disabled={disabled}
        allowClear={allowClear}
      />
    </Form.Item>
  );
}