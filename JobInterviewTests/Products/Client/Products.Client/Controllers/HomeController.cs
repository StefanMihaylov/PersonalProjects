namespace Products.Client.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Web.Mvc;
    using System.Xml.Serialization;

    using Newtonsoft.Json;
    using Products.Client.Models;

    public class HomeController : Controller
    {
        public const string Uri = "http://localhost:53871/api/products?userId=";

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult GetByUserId(int userId)
        {
            var products = this.GetProducts(userId);

            if (products.Count > 0)
            {
                this.SaveToXml(products, userId);
            }            

            return this.PartialView("_ResultPartial", products);
        }

        private void SaveToXml(IList<ProductViewModel> products, int userId)
        {
            string xmlFolder = Server.MapPath("~/App_Data");
            string path = string.Format("{0}/UserId_{1}.xml", xmlFolder, userId);

            var serializer = new XmlSerializer(products.GetType(), new XmlRootAttribute("Products"));
            using (var fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, products);
            }
        }

        private IList<ProductViewModel> GetProducts(int userId)
        {
            using (var client = new WebClient())
            {
                string response = client.DownloadString(Uri + userId.ToString());
                return JsonConvert.DeserializeObject<List<ProductViewModel>>(response);
            }
        }
    }
}