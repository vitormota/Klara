using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

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

            // Criacao do ficheiro pdf
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            PdfContentByte pdf_cb = writer.DirectContent;

            // Criacao da fonte a ser usada
            BaseFont base_font_pdf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            Font font_pdf = new Font(base_font_pdf, 10);

            // Desenhar linhas
            float left = iTextSharp.text.Utilities.MillimetersToPoints((float)12.7);
            float delta = (float)17.7;

            // Colocar linhas
            for (int i = 0; i < 4; i++)
            {
                float top = iTextSharp.text.Utilities.MillimetersToPoints(269 - (i * delta));

                pdf_cb.MoveTo(left, top);
                pdf_cb.LineTo(doc.PageSize.Width / 2, top);
                pdf_cb.Stroke();
            }

            // Colocacao do texto no pdf
            Paragraph p1 = new Paragraph("NOME", font_pdf);
            Paragraph p2 = new Paragraph(name, font_pdf);
            p2.SpacingAfter = 20;
            doc.Add(p1);
            doc.Add(p2);

            Paragraph p3 = new Paragraph("DESCRIÇÃO", font_pdf);
            Paragraph p4 = new Paragraph(description, font_pdf);
            p4.SpacingAfter = 20;
            doc.Add(p3);
            doc.Add(p4);

            Paragraph p5 = new Paragraph("INSTITUIÇÃO", font_pdf);
            Paragraph p6 = new Paragraph(institution, font_pdf);
            p6.SpacingAfter = 20;
            doc.Add(p5);
            doc.Add(p6);

            string date_str = date_cupon.Day.ToString() + "-" + date_cupon.Month.ToString() + "-" + date_cupon.Year.ToString() + " | " + date_cupon.Hour.ToString() + "h" + date_cupon.Minute.ToString();
            Paragraph p7 = new Paragraph("DATA", font_pdf);
            Paragraph p8 = new Paragraph(date_str, font_pdf);
            p8.SpacingAfter = 20;
            doc.Add(p7);
            doc.Add(p8);

            string price_str = price.ToString() + "€";
            Paragraph p9 = new Paragraph("PREÇO", font_pdf);
            Paragraph p10 = new Paragraph(price_str, font_pdf);
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