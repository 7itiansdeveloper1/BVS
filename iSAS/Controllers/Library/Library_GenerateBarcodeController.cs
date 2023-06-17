using BarcodeLib;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_GenerateBarcodeController : Controller
    {
        private ILibrary_GenerateBarcodeRepo _genbarcodeRepo;
        public Library_GenerateBarcodeController(ILibrary_GenerateBarcodeRepo genbarcodeRepo)
        {
            _genbarcodeRepo = genbarcodeRepo;
        }

        // GET: Library_GenerateBarcode
        public ActionResult GenerateBarcode_FilterPage()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GenerateBarcode_PrintPage(string fromaccno, string toaccno)
        {
            int barcodeWidth = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultWidth_BarCode"]);
            List<string> accNoList = _genbarcodeRepo.GetAccNoList(fromaccno, toaccno);
            List<string> barcodeList = new List<string>();
            for (int i = 0; i < accNoList.Count; i++)
                barcodeList.Add("data:image/png;base64," + GenBarcode(accNoList[i]));
            ViewBag.barcode = barcodeList;
            return View();
        }

        public string GenBarcode(string data)
        {
            int barcodeWidth = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultWidth_BarCode"]);
            Barcode barcode = new Barcode()
            {
                IncludeLabel = true,
                Alignment = AlignmentPositions.CENTER,
                Width = barcodeWidth,
                Height = 100,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };
            using (Image image = barcode.Encode(TYPE.CODE39, data))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, ImageFormat.Jpeg);
                    byte[] imageBytes = m.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
        #region Code 39
        //[HttpPost]
        //public ActionResult GenerateBarcode_PrintPage(string fromaccno, string toaccno)
        //{
        //    int barcodeWidth = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultWidth_BarCode"]);
        //    List<string> accNoList = _genbarcodeRepo.GetAccNoList(fromaccno, toaccno);
        //    List<string> barcodeList = new List<string>();
        //    for (int i = 0; i < accNoList.Count; i++)
        //    {
        //        using (Bitmap bitMap = new Bitmap(accNoList[i].Length * barcodeWidth, 80)) //40 Default
        //        {
        //            using (Graphics graphics = Graphics.FromImage(bitMap))
        //            {
        //                Font oFont = new Font("IDAutomationHC39M", 16);
        //                PointF point = new PointF(2f, 2f);
        //                SolidBrush blackBrush = new SolidBrush(Color.Black);
        //                SolidBrush whiteBrush = new SolidBrush(Color.White);
        //                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
        //                graphics.DrawString("*" + accNoList[i] + "*", oFont, blackBrush, point);
        //            }
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //                barcodeList.Add("data:image/png;base64," + Convert.ToBase64String(ms.ToArray()));
        //            }
        //        }
        //    }
        //    ViewBag.barcode = barcodeList;
        //    return View();
        //}
        #endregion
    }
}