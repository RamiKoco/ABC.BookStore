import { DatePicker, Form } from "antd";
import dayjs from "dayjs";

interface DevDateEditProps {
  label?: string;
  value?: string;
  onChange?: (value: string) => void;
  required?: boolean;
  disabled?: boolean;
}

export default function DevDateEdit({
  label,
  value,
  onChange,
  required = false,
  disabled = false,
}: DevDateEditProps) {
  return (
    <Form.Item label={label} required={required}>
      <DatePicker
        value={value ? dayjs(value) : null}
        onChange={(date) => onChange?.(date ? date.format("YYYY-MM-DD") : "")}
        format="DD.MM.YYYY"
        style={{ width: "100%" }}
        disabled={disabled}
      />
    </Form.Item>
  );
}