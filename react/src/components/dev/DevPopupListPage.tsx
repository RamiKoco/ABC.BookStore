import { Modal } from "antd";
import type { ReactNode } from "react";

interface DevPopupListPageProps {
  visible: boolean;
  title?: string;
  children: ReactNode;
  onClose: () => void;
  width?: number | string;
  footer?: ReactNode;
}

export default function DevPopupListPage({
  visible,
  title,
  children,
  onClose,
  width = "80%",
  footer,
}: DevPopupListPageProps) {
  return (
    <Modal
      open={visible}
      title={title}
      onCancel={onClose}
      width={width}
      footer={footer}
      destroyOnClose
    >
      {children}
    </Modal>
  );
}