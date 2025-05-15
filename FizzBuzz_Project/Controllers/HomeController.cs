using System.Diagnostics;
using FizzBuzz_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FizzBuzzModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Result", new { number = model.Number });
        }



        
       

        public IActionResult Result(int number, int page = 1)
        {
            const int PageSize = 20;
            var results = new List<string>();

            bool isWednesday = (DateTime.Today.DayOfWeek == DayOfWeek.Wednesday);
            string fizz = isWednesday ? "Wizz" : "Fizz";
            string buzz = isWednesday ? "Wuzz" : "Buzz";

            for (int i = 1; i <= number; i++)
            {
                if (i % 15 == 0) results.Add($"{fizz} {buzz}");
                else if (i % 3 == 0) results.Add(fizz);
                else if (i % 5 == 0) results.Add(buzz);
                else results.Add(i.ToString());
            }

            var pagedResults = results
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.Number = number;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)results.Count / PageSize);
            ViewBag.Results = pagedResults;

            return View(model: number);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
