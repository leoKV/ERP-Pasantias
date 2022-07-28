using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

/// <summary>
/// Descripción breve de DotCell
/// </summary>
public class DotCell : IPdfPCellEvent
{
    public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
    {
        cell.Border = Rectangle.TOP_BORDER;
        PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
        cb.SetLineDash(3f, 3f);
        cb.Rectangle(position.Left + 11, position.Bottom, position.Width - 20, 0);
        cb.Stroke();
    }
}