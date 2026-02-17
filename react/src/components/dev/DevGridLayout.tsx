import type { ReactNode, CSSProperties } from "react";

interface DevGridLayoutProps {
  children: ReactNode;
  columnCount?: number;
  rowCount?: number;
  columnWidths?: string[];
  rowHeights?: string[];
  columnSpacing?: string;
  rowSpacing?: string;
  defaultRowHeight?: string;
  style?: CSSProperties;
}

export default function DevGridLayout({
  children,
  columnCount = 1,
  columnWidths,
  columnSpacing = "5px",
  rowSpacing = "5px",
  style,
}: DevGridLayoutProps) {
  const gridTemplateColumns = columnWidths
    ? columnWidths.join(" ")
    : Array(columnCount).fill("1fr").join(" ");

  return (
    <div
      style={{
        display: "grid",
        gridTemplateColumns,
        columnGap: columnSpacing,
        rowGap: rowSpacing,
        ...style,
      }}
    >
      {children}
    </div>
  );
}