using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Slalom.Stacks.Documentation
{
    public class DocumentTable
    {
        private int[] _columnWidths;

        private readonly WordprocessingDocument _document;
        private Table _table;
        private TableRow _row;

        public DocumentTable(WordprocessingDocument document, params int[] columnWidths)
        {
            this._columnWidths = columnWidths;
            _document = document;

            _table = new Table();

            var border = new EnumValue<BorderValues>(BorderValues.BasicThinLines);
            var tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder { Val = border, Color = "CCCCCC" },
                    new BottomBorder { Val = border, Color = "CCCCCC" },
                    new LeftBorder { Val = border, Color = "CCCCCC" },
                    new RightBorder { Val = border, Color = "CCCCCC" },
                    new InsideHorizontalBorder { Val = border, Color = "CCCCCC" },
                    new InsideVerticalBorder { Val = border, Color = "CCCCCC" }
                )
            );

            _table.AppendChild(tblProp);

            document.MainDocumentPart.Document.Body.Append(_table);
        }

        public void AppendRow(params string[] values)
        {
            _row = new TableRow();
            _table.Append(_row);
            foreach (var value in values)
            {
                this.AppendCell(value);
            }
        }

        public void AppendCell(string value)
        {
            var cell = new TableCell();

            var index = _row.Descendants<TableCell>().Count();
            if (index < this._columnWidths.Length)
            {
                cell.Append(new TableCellProperties(
                    new TableCellWidth { Width = this._columnWidths[index].ToString() }));
            }

            var paragraph = new Paragraph(new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(value)));
            paragraph.ParagraphProperties = new ParagraphProperties(
                new ParagraphStyleId
                {
                    Val = _document.GetStyleIdFromStyleName(_table.Descendants<TableRow>().Count() > 1 ? "Table Cell" : "Table Header")
                });
            cell.Append(paragraph);
            _row.Append(cell);
        }
    }
}