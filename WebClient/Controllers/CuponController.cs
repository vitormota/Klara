using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Web.Hosting;
using iTextSharp.text.pdf.draw;

namespace WebClient_.Controllers
{
    public class CuponController : Controller
    {
        //
        // GET: /Cupon/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string PrintCupon()
        {
            // Dados para serem introduzidos
            string name = "XXXXXXX XXXXXXXX XXXXXXXXX";
            string description = "XXXXXXXXXX XX XXXXXXXXXX";
            string institution = "XXXXXXXXX XXXXXXXXX XX XXXXXX";
            DateTime date_cupon = new DateTime(2014, 04, 26, 13, 30, 00);
            double price = 25.00;

            // Criar numero aleatorio para nao haver ficheiros repetidos
            Random rm = new Random();
            int randomNumber = rm.Next(0, 1000000000);

            // Criar ficheiro
            string filename = "Health+_" + randomNumber.ToString() + ".pdf";
            string path_folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(path_folder, filename);
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            Document doc = new Document(PageSize.A4);
            doc.SetMargins(iTextSharp.text.Utilities.MillimetersToPoints(30), iTextSharp.text.Utilities.MillimetersToPoints(30), iTextSharp.text.Utilities.MillimetersToPoints(25), iTextSharp.text.Utilities.MillimetersToPoints(25));

            // Criacao do ficheiro pdf
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            PdfContentByte pdf_cb = writer.DirectContent;

            // Criacao da fonte a ser usada
            BaseFont base_font_pdf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font font_pdf = new Font(base_font_pdf, 10);

            // Criacao da linha que vai ser usada
            Chunk line = new Chunk(new LineSeparator((float)1, 50, CMYKColor.BLACK, Element.ALIGN_LEFT, -1));

            // Colocar logo
            Image health_plus_logo = Image.GetInstance(HostingEnvironment.MapPath(VirtualPathUtility.ToAbsolute("~/Content/Images/logo.png")));
            health_plus_logo.ScalePercent(12);
            doc.Add(health_plus_logo);


            // Colocacao do texto no pdf
            Paragraph p1 = new Paragraph("NOME", font_pdf);
            Paragraph p2 = new Paragraph(name, font_pdf);
            p1.SpacingBefore = 80;
            doc.Add(p1);
            doc.Add(p2);

            // Desenho da linha 1
            doc.Add(line);

            Paragraph p3 = new Paragraph("DESCRIÇÃO", font_pdf);
            Paragraph p4 = new Paragraph(description, font_pdf);
            p3.SpacingBefore = 10;
            doc.Add(p3);
            doc.Add(p4);

            // Desenho da linha 2
            doc.Add(line);

            Paragraph p5 = new Paragraph("INSTITUIÇÃO", font_pdf);
            Paragraph p6 = new Paragraph(institution, font_pdf);
            p5.SpacingBefore = 10;
            doc.Add(p5);
            doc.Add(p6);

            // Desenho da linha 3
            doc.Add(line);

            string date_str = date_cupon.Day.ToString() + "-" + date_cupon.Month.ToString() + "-" + date_cupon.Year.ToString() + " | " + date_cupon.Hour.ToString() + "h" + date_cupon.Minute.ToString();
            Paragraph p7 = new Paragraph("DATA", font_pdf);
            Paragraph p8 = new Paragraph(date_str, font_pdf);
            p7.SpacingBefore = 10;
            doc.Add(p7);
            doc.Add(p8);

            // Desenho da linha 4
            doc.Add(line);

            string price_str = price.ToString() + "€";
            Paragraph p9 = new Paragraph("PREÇO", font_pdf);
            Paragraph p10 = new Paragraph(price_str, font_pdf);
            p9.SpacingBefore = 10;
            doc.Add(p9);
            doc.Add(p10);
            doc.Close();

            // Serve para abrir o ficheiro
            ProcessStartInfo sInfo = new ProcessStartInfo(path);
            Process.Start(sInfo);

            return "LOOOL";
        }
	}
}