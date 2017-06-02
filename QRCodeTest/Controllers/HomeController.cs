using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QRCode_Test.NET4._6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("QRCode");
        }

        public ActionResult Home()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult QRCode(string text = null, int size = 20)
        {
            text = text ?? Guid.NewGuid().ToString();

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new Base64QRCode(qrCodeData))
                    {
                        ViewBag.ImageAsBase64 = qrCode.GetGraphic(size);
                    }

                    using (var qrCode = new SvgQRCode(qrCodeData))
                    {
                        ViewBag.ImageAsSvg = qrCode.GetGraphic(20);
                    }
                }
            }
            
            return View();
        }
    }
}