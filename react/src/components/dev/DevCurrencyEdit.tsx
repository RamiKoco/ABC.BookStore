import { InputNumber, Form } from "antd";

interface DevCurrencyEditProps {
  label?: string;
  value?: number;
  onChange?: (value: number) => void;
  required?: boolean;
  disabled?: boolean;
  min?: number;
  max?: number;
  prefix?: string;
}

export default function DevCurrencyEdit({
  label,
  value = 0,
  onChange,
  required = false,
  disabled = false,
  min = 0,
  max,
  prefix = "₺",
}: DevCurrencyEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <InputNumber
        value={value}
        onChange={(val) => onChange?.(val ?? 0)}
        style={{ width: "100%" }}
        min={min}
        max={max}
        prefix={prefix}
        precision={2}
        disabled={disabled}
        formatter={(val) => `${val}`.replace(/\B(?=(\d{3})+(?!\d))/g, ",")}
        parser={(val) => Number(val?.replace(/,/g, "") || 0)}
      />
    </Form.Item>
  );
}