using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;

/// <summary>
/// Descripción breve de DottedCells
/// </summary>
public class DottedCells : IPdfPTableEvent
{
    public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases)
    {
        PdfContentByte canvas = canvases[PdfPTable.LINECANVAS];
        canvas.SetLineDash(3f, 3f);
        float llx = widths[0][0];
        float urx = widths[0][widths[0].Length - 1];
        for (int i = 0; i < heights.Length; i++)
        {
            canvas.MoveTo(llx, heights[i]);
            canvas.LineTo(urx, heights[i]);
        }
        for (int i = 0; i < widths.Length; i++)
        {
            for (int j = 0; j < widths[i].Length; j++)
            {
                canvas.MoveTo(widths[i][j], heights[i]);
                canvas.LineTo(widths[i][j], heights[i + 1]);
            }
        }
        canvas.Stroke();
    }
}