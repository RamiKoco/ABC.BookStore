import { InputNumber, Form } from "antd";

interface DevSpinEditProps {
  label?: string;
  value?: number;
  onChange?: (value: number) => void;
  required?: boolean;
  disabled?: boolean;
  min?: number;
  max?: number;
  step?: number;
  precision?: number;
}

export default function DevSpinEdit({
  label,
  value = 0,
  onChange,
  required = false,
  disabled = false,
  min,
  max,
  step = 1,
  precision = 0,
}: DevSpinEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <InputNumber
        value={value}
        onChange={(val) => onChange?.(val ?? 0)}
        style={{ width: "100%" }}
        min={min}
        max={max}
        step={step}
        precision={precision}
        disabled={disabled}
      />
    </Form.Item>
  );
}