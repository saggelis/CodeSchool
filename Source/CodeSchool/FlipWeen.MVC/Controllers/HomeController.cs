using FlipWeen.MVC.Api;
using FlipWeen.MVC.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FlipWeen.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IDataClient _dataClient;
        private readonly ITokenContainer _tokenContainer;

        public HomeController()
        {
            _tokenContainer = new TokenContainer();
            var apiClient = new ApiClient(HttpClientInstance.Instance, _tokenContainer);
            _dataClient = new DataClient(apiClient);
        }
        public HomeController(IDataClient loginClient, ITokenContainer tokenContainer)
        {
            this._dataClient = loginClient;
            this._tokenContainer = tokenContainer;
        }
        public ActionResult Index()
        {
            return View();
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

        public ActionResult HowItWorks()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public async Task<ActionResult> Projects()
        {
            var response = await _dataClient.GetLatestProjects();
         
            return View(response.Data);
        }

        public async Task<ActionResult> Explore()
        {
            var response = await _dataClient.GetProjectCategories();

            
            return View(response.Data);
        }
    }
}