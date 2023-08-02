using Microsoft.JSInterop;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;
using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using BlazorApp5.Client.Helpers;

namespace BlazorApp5.Client.Pdf
{
    public class report
    {
        private int _pagenumber;

        public void Generate(IJSRuntime js, int pagenumber, string filename = "report.pdf")
        {
            _pagenumber = pagenumber;

            js.InvokeVoidAsync("jsDownloadFile",
                                filename,
                                ReportPDF()
                                );
        }

        public void OpenToIframe(IJSRuntime js, int pagenumber, string idiFrame)
        {
            _pagenumber = pagenumber;

            js.InvokeVoidAsync("jsOpenToIframe",
                                idiFrame,
                                Convert.ToBase64String(ReportPDF())
                                );
        }
        public void OpenNewTab(IJSRuntime js, int pagenumber, string filename = "report.pdf")
        {
            _pagenumber = pagenumber;

            js.InvokeVoidAsync("jsOpenIntoNewTab",
                                filename,
                                Convert.ToBase64String(ReportPDF())
                                );
        }


        private byte[] ReportPDF()
        {
            var memoryStream = new MemoryStream();

            // Marge in centimeter, then I convert with .ToDpi()
            float margeLeft = 1.5f;
            float margeRight = 1.5f;
            float margeTop = 1.0f;
            float margeBottom = 1.0f;

            iTextSharp.text.Document pdf = new iTextSharp.text.Document(
                                    PageSize.A4,
                                    margeLeft.ToDpi(),
                                    margeRight.ToDpi(),
                                    margeTop.ToDpi(),
                                    margeBottom.ToDpi()
                                   );

            pdf.AddTitle("Gustavo Ziegler");
            pdf.AddAuthor("Alejandro Pons");
            pdf.AddCreationDate();
            pdf.AddKeywords("blazor");
            pdf.AddSubject("Gregor Duarte Martinez");

            PdfWriter writer = PdfWriter.GetInstance(pdf, memoryStream);

            //HEADER and FOOTER
            var fontStyle = FontFactory.GetFont("Arial", 16, BaseColor.White);
            var labelHeader = new Chunk("Messi", fontStyle);
            HeaderFooter header = new HeaderFooter(new Phrase(labelHeader), false)
            {
                BackgroundColor = new BaseColor(133, 76, 199),
                Alignment = Element.ALIGN_CENTER,
                Border = iTextSharp.text.Rectangle.NO_BORDER
            };
            //header.Border = Rectangle.NO_BORDER;
            pdf.Header = header;


            var labelFooter = new Chunk("Page", fontStyle);
            HeaderFooter footer = new HeaderFooter(new Phrase(labelFooter), true)
            {
                Border = iTextSharp.text.Rectangle.NO_BORDER,
                Alignment = Element.ALIGN_RIGHT
            };
            pdf.Footer = footer;

            pdf.Open();


            if (_pagenumber == 1)
                Page1.PageText(pdf);

            pdf.Close();

            return memoryStream.ToArray();
        }
    }
}
