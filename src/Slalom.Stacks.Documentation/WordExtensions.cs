using System;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Slalom.Stacks.Documentation
{
    public static class WordExtensions
    {
      
        public static void AddNewStyle(this StyleDefinitionsPart styleDefinitionsPart, string styleid, string stylename)
        {
            // Get access to the root element of the styles part.
            var styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            var style = new Style
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            var styleName1 = new StyleName { Val = stylename };
            var basedOn1 = new BasedOn { Val = "Normal" };
            var nextParagraphStyle1 = new NextParagraphStyle { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            var styleRunProperties1 = new StyleRunProperties();
            var bold1 = new Bold();
            var color1 = new Color { ThemeColor = ThemeColorValues.Accent2 };
            var font1 = new RunFonts { Ascii = "Lucida Console" };
            var italic1 = new Italic();
            // Specify a 12 point size.
            var fontSize1 = new FontSize { Val = "24" };
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            styleRunProperties1.Append(italic1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public static StyleDefinitionsPart AddStylesPartToPackage(this WordprocessingDocument doc)
        {
            StyleDefinitionsPart part;
            part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();
            root.Save(part);
            return part;
        }

        public static string GetStyleIdFromStyleName(this WordprocessingDocument doc, string styleName)
        {
            var stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                                      .Where(s => s.Val.Value.Equals(styleName, StringComparison.OrdinalIgnoreCase) &&
                                                  (((Style)s.Parent).Type == StyleValues.Paragraph))
                                      .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }

        public static bool IsStyleIdInDocument(this WordprocessingDocument doc, string styleid)
        {
            // Get access to the Styles element for this document.
            var s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            var n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            var style = s.Elements<Style>()
                         .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                         .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }
    }
}