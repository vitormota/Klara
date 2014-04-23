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
            // Criar pasta para receber o ficheiro
            string folder_name = "Health+";
            string path_folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folder_name);

            if(!Directory.Exists(path_folder))
            {
                Directory.CreateDirectory(path_folder);
            }

            // Criar numero aleatorio para nao haver ficheiros repetidos
            Random rm = new Random();
            int randomNumber = rm.Next(0, 1000000000);

            // Criar ficheiro
            string filename = randomNumber.ToString() + ".pdf";
            string path = Path.Combine(path_folder, filename);
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            Document doc = new Document(PageSize.A4);

            // Criacao do ficheiro pdf
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            doc.Add(new Paragraph("LOOOOOL"));
            doc.Close();

            // Serve para abrir o ficheiro
            ProcessStartInfo sInfo = new ProcessStartInfo(path);
            Process.Start(sInfo);

            return "LOOOL";
        }
	}
}