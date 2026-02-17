import { Table, Button, Space, Popconfirm } from "antd";
import { EditOutlined, DeleteOutlined } from "@ant-design/icons";

interface DevDataGridProps {
  columns: any[];
  dataSource: any[];
  loading?: boolean;
  onEdit?: (record: any) => void;
  onDelete?: (id: string) => void;
  rowKey?: string;
}

export default function DevDataGrid({
  columns,
  dataSource,
  loading = false,
  onEdit,
  onDelete,
  rowKey = "id",
}: DevDataGridProps) {
  const actionColumn = {
    title: "İşlemler",
    key: "actions",
    width: 120,
    render: (_: any, record: any) => (
      <Space>
        {onEdit && (
          <Button type="link" icon={<EditOutlined />} onClick={() => onEdit(record)} size="small">
            Düzenle
          </Button>
        )}
        {onDelete && (
          <Popconfirm title="Silmek istediğinize emin misiniz?" onConfirm={() => onDelete(record[rowKey])} okText="Evet" cancelText="Hayır">
            <Button type="link" danger icon={<DeleteOutlined />} size="small">
              Sil
            </Button>
          </Popconfirm>
        )}
      </Space>
    ),
  };

  const allColumns = onEdit || onDelete ? [...columns, actionColumn] : columns;

  return (
    <Table
      columns={allColumns}
      dataSource={dataSource}
      loading={loading}
      rowKey={rowKey}
      size="small"
      pagination={{ pageSize: 20, showSizeChanger: true, showTotal: (total) => `Toplam ${total} kayıt` }}
    />
  );
}