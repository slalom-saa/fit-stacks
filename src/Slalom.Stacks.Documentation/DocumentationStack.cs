using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Services;
using Slalom.Stacks.Services.Registry;
using Slalom.Stacks.Text;

namespace Slalom.Stacks.Documentation
{
    public class DocumentStack : Stack
    {
        public DocumentStack(params object[] markers) : base(markers)
        {
        }

        public void WriteToConsole()
        {
            //var services = this.GetServices();
            //var path = @"C:\source\Stacks\Core\src\Slalom.Stacks.Documentation\output.docx";
            //using (var package = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document))
            //{
            //    // Add a new main document part. 
            //    package.AddMainDocumentPart();
            //    package.MainDocumentPart.Document =
            //           new Document(new Body());

            //    foreach (var endPoint in services.Hosts.SelectMany(e => e.Services).SelectMany(e => e.EndPoints).OrderBy(e => e.Path))
            //    {
            //        var paragraph = package.MainDocumentPart.Document.Body.AppendChild(new Paragraph());

            //        // Create the Document DOM. 
            //        var run = new Run(
            //            new DocumentFormat.OpenXml.Wordprocessing.Text(endPoint.ServiceType));

            //        var properties = new RunProperties(
            //            new RunFonts()
            //            {
            //                Ascii = "Arial"
            //            });

            //        run.PrependChild(properties);
            //        paragraph.AppendChild(run);
            //    }
            //    package.MainDocumentPart.Document.Save();
            //}

            //return;


            var services = this.GetServices();
            var path = @"C:\source\Stacks\Core\src\Slalom.Stacks.Documentation\output.docx";
            using (var document = new WordDocument(path))
            {
                foreach (var service in services.Hosts.SelectMany(e => e.Services).OrderBy(e => e.Path))
                {
                    if (service.Path.StartsWith("_"))
                    {
                        continue;
                    }

                    foreach (var endPoint in service.EndPoints)
                    {
                        var name = service.Name.ToTitle();
                        if (endPoint.Version > 1)
                        {
                            name += " (version " + endPoint.Version + ")";
                        }
                        document.Append(name, "Heading 2");
                        document.Append("v" + endPoint.Version + "/" + service.Path, "Endpoint Path");

                        if (endPoint.Summary != null)
                        {
                            document.Append(service.EndPoints.First().Summary);
                        }
                        document.Append("Parameters", "Heading 3");

                        document.Append(endPoint.RequestProperties);

                        //document.NewTable();
                        //document.AppendRows("Name", "Type", "Description", "Validation");
                        ////document.Append("Input", "Heading3");
                        //foreach (var property in endPoint.RequestProperties)
                        //{
                        //    document.AppendRows(property.Name, Type.GetType(property.Type).Name, property.Summary, property.Validation);
                        //}
                        document.Append("Rules", "Heading 3");
                        var table = document.AppendTable(1500, 8500);
                        table.AppendRow("Type", "Summary");
                        foreach (var property in endPoint.RequestProperties)
                        {
                            if (property.Validation != null)
                            {
                                table.AppendRow("Input", property.Validation);
                            }
                        }
                        foreach (var rule in endPoint.Rules)
                        {
                            table.AppendRow(rule.RuleType.ToString(), rule.Comments?.Summary);
                        }
                    }
                }
                document.Save();
                document.Open();
            }
        }
    }
}
