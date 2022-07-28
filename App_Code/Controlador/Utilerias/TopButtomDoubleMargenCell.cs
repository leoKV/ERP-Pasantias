using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

/// <summary>
/// Descripción breve de TopButtomDoubleMargenCell
/// </summary>
public class TopButtomDoubleMargenCell : IPdfPCellEvent
{
    public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
    {
        cell.Border = Rectangle.TOP_BORDER;
        PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
        cb.SetLineDash(2f, 2f);
        cb.Rectangle(position.Left, position.Bottom, position.Width, 2);
        cb.Rectangle(position.Left, position.Bottom + position.Height, position.Width, 2);
        cb.Stroke();
    }
}