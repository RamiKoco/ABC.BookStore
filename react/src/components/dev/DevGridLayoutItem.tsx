import type { ReactNode, CSSProperties } from "react";

interface DevGridLayoutItemProps {
  children: ReactNode;
  column?: number;
  columnSpan?: number;
  row?: number;
  rowSpan?: number;
  visible?: boolean;
  style?: CSSProperties;
}

export default function DevGridLayoutItem({
  children,
  column,
  columnSpan = 1,
  row,
  rowSpan = 1,
  visible = true,
  style,
}: DevGridLayoutItemProps) {
  if (!visible) return null;

  return (
    <div
      style={{
        gridColumn: column ? `${column} / span ${columnSpan}` : `span ${columnSpan}`,
        gridRow: row ? `${row} / span ${rowSpan}` : `span ${rowSpan}`,
        ...style,
      }}
    >
      {children}
    </div>
  );
}