import { Upload, Form, Image } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import { useState } from "react";

interface DevImageEditProps {
  label?: string;
  value?: string;
  onChange?: (base64: string) => void;
  disabled?: boolean;
  width?: number;
  height?: number;
}

export default function DevImageEdit({
  label,
  value,
  onChange,
  disabled = false,
  width = 150,
  height = 150,
}: DevImageEditProps) {
  const [preview, setPreview] = useState(false);

  const handleChange = (file: File) => {
    const reader = new FileReader();
    reader.onload = () => {
      onChange?.(reader.result as string);
    };
    reader.readAsDataURL(file);
  };

  return (
    <Form.Item label={label}>
      {value ? (
        <div>
          <Image
            src={value}
            width={width}
            height={height}
            style={{ objectFit: "cover", borderRadius: 4 }}
            preview={{ visible: preview, onVisibleChange: setPreview }}
          />
          {!disabled && (
            <Upload
              showUploadList={false}
              beforeUpload={(file) => { handleChange(file); return false; }}
              accept="image/*"
            >
              <a style={{ display: "block", marginTop: 8 }}>Değiştir</a>
            </Upload>
          )}
        </div>
      ) : (
        <Upload
          showUploadList={false}
          beforeUpload={(file) => { handleChange(file); return false; }}
          accept="image/*"
          disabled={disabled}
        >
          <div style={{ width, height, border: "1px dashed #d9d9d9", borderRadius: 4, display: "flex", alignItems: "center", justifyContent: "center", cursor: "pointer" }}>
            <PlusOutlined style={{ fontSize: 24, color: "#999" }} />
          </div>
        </Upload>
      )}
    </Form.Item>
  );
}