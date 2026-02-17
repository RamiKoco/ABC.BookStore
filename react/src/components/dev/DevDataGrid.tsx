import { Table, Button, Space, Popconfirm, Input, Typography } from "antd";
import {
  EditOutlined,
  DeleteOutlined,
  PlusOutlined,
  FilterOutlined,
  ReloadOutlined,
} from "@ant-design/icons";
import { useState } from "react";
import type { ColumnsType } from "antd/es/table";

interface DevDataGridProps<T = any> {
  columns: ColumnsType<T>;
  dataSource: T[];
  loading?: boolean;
  onEdit?: (record: T) => void;
  onDelete?: (id: string) => void;
  onDoubleClick?: (record: T) => void;
  onNew?: () => void;
  onRefresh?: () => void;
  rowKey?: string;
  title?: string;
  // Pager
  pageSize?: number;
  pageSizeOptions?: number[];
  showPagination?: boolean;
  // Selection
  selectionMode?: "single" | "multiple" | "none";
  onSelectionChange?: (selectedRows: T[]) => void;
  selectedRowKeys?: React.Key[];
  // Features
  showFilterRow?: boolean;
  showToolbar?: boolean;
  toolbarNewVisible?: boolean;
  toolbarEditVisible?: boolean;
  toolbarDeleteVisible?: boolean;
  toolbarRefreshVisible?: boolean;
  toolbarFilterVisible?: boolean;
  // Size
  scrollY?: number;
  size?: "small" | "middle" | "large";
  // Summary
  summary?: (data: readonly T[]) => React.ReactNode;
  // Extra toolbar
  extraToolbar?: React.ReactNode;
}

export default function DevDataGrid<T extends Record<string, any> = any>({
  columns,
  dataSource,
  loading = false,
  onEdit,
  onDelete,
  onDoubleClick,
  onNew,
  onRefresh,
  rowKey = "id",
  title,
  pageSize = 15,
  pageSizeOptions = [15, 25, 30, 40, 50, 100],
  showPagination = true,
  selectionMode = "single",
  onSelectionChange,
  selectedRowKeys: externalSelectedKeys,
  showFilterRow = false,
  showToolbar = true,
  toolbarNewVisible = true,
  toolbarEditVisible = true,
  toolbarDeleteVisible = true,
  toolbarRefreshVisible = true,
  toolbarFilterVisible = true,
  scrollY = 450,
  size = "small",
  summary,
  extraToolbar,
}: DevDataGridProps<T>) {
  const [filterVisible, setFilterVisible] = useState(showFilterRow);
  const [selectedKeys, setSelectedKeys] = useState<React.Key[]>([]);
  const [searchText, setSearchText] = useState<Record<string, string>>({});

  const activeSelectedKeys = externalSelectedKeys ?? selectedKeys;

  // Action column
  const actionColumn: any = {
    title: "İşlemler",
    key: "actions",
    width: 130,
    fixed: "right" as const,
    render: (_: any, record: T) => (
      <Space size="small">
        {onEdit && toolbarEditVisible && (
          <Button
            type="link"
            icon={<EditOutlined />}
            onClick={(e) => { e.stopPropagation(); onEdit(record); }}
            size="small"
            style={{ color: "#fa8c16" }}
          >
            Düzenle
          </Button>
        )}
        {onDelete && toolbarDeleteVisible && (
          <Popconfirm
            title="Silmek istediğinize emin misiniz?"
            onConfirm={() => onDelete(record[rowKey])}
            okText="Evet"
            cancelText="Hayır"
          >
            <Button
              type="link"
              danger
              icon={<DeleteOutlined />}
              onClick={(e) => e.stopPropagation()}
              size="small"
            >
              Sil
            </Button>
          </Popconfirm>
        )}
      </Space>
    ),
  };

  // Filter columns
  const filteredColumns = columns.map((col: any) => {
    if (!filterVisible) return col;
    return {
      ...col,
      filterDropdown: undefined,
      title: (
        <div>
          <div>{col.title}</div>
          <Input
            size="small"
            placeholder={`Ara...`}
            value={searchText[col.dataIndex] || ""}
            onChange={(e) => {
              setSearchText((prev) => ({ ...prev, [col.dataIndex]: e.target.value }));
            }}
            style={{ marginTop: 4 }}
            allowClear
          />
        </div>
      ),
    };
  });

  const allColumns = onEdit || onDelete ? [...filteredColumns, actionColumn] : filteredColumns;

  // Filter data
  const filteredData = dataSource.filter((record) => {
    return Object.entries(searchText).every(([key, value]) => {
      if (!value) return true;
      const cellValue = record[key];
      if (cellValue == null) return false;
      return String(cellValue).toLowerCase().includes(value.toLowerCase());
    });
  });

  // Row selection
  const rowSelection =
    selectionMode !== "none"
      ? {
          type: (selectionMode === "multiple" ? "checkbox" : "radio") as any,
          selectedRowKeys: activeSelectedKeys,
          onChange: (keys: React.Key[], rows: T[]) => {
            setSelectedKeys(keys);
            onSelectionChange?.(rows);
          },
        }
      : undefined;

  return (
    <div>
      {/* Toolbar */}
      {showToolbar && (
        <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 12, alignItems: "center" }}>
          <div>
            {title && <Typography.Title level={5} style={{ margin: 0 }}>{title}</Typography.Title>}
          </div>
          <Space>
            {extraToolbar}
            {toolbarFilterVisible && (
              <Button
                icon={<FilterOutlined />}
                onClick={() => setFilterVisible(!filterVisible)}
                type={filterVisible ? "primary" : "default"}
                size="small"
                title="Filtre"
              />
            )}
            {toolbarRefreshVisible && onRefresh && (
              <Button
                icon={<ReloadOutlined />}
                onClick={onRefresh}
                size="small"
                title="Yenile"
              />
            )}
            {toolbarNewVisible && onNew && (
              <Button
                type="primary"
                icon={<PlusOutlined />}
                onClick={onNew}
                size="small"
              >
                Yeni
              </Button>
            )}
          </Space>
        </div>
      )}

      {/* DataGrid */}
      <Table
        columns={allColumns}
        dataSource={filteredData}
        loading={loading}
        rowKey={rowKey}
        size={size}
        rowSelection={rowSelection}
        scroll={{ y: scrollY }}
        summary={summary}
        onRow={(record) => ({
          onDoubleClick: () => onDoubleClick?.(record),
          style: { cursor: onDoubleClick ? "pointer" : "default" },
        })}
        pagination={
          showPagination
            ? {
                pageSize,
                pageSizeOptions: pageSizeOptions.map(String),
                showSizeChanger: true,
                showTotal: (total) => `Toplam ${total} kayıt`,
                size: "small",
              }
            : false
        }
      />
    </div>
  );
}