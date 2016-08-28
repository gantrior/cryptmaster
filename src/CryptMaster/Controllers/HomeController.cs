namespace CryptMaster.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Main controller which returns index.html
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Main index.html route (returns SPA app)
        /// </summary>
        /// <returns>SPA application response</returns>
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
