using FastestQuiz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace FastestQuiz.Controllers
{
    public class QuizController : Controller
    {
        // GET: QuizController
        public ActionResult Index()
        {
            return View();
        }

        private readonly FirebaseService _firebaseService;

        public QuizController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }



        // GET: QuizController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            // Simule a busca no bd
            var quiz = await _firebaseService.GetQuizByIdAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);  // Passando o quiz para a view
        }

        // GET: QuizController/Create
        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Search()
        {
            var Quizzes = await _firebaseService.ListQuizzesAsync();
            return View(Quizzes);
        }

        // POST: QuizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection formCollection)
        {
            try
            {
                await _firebaseService.AddQuizzesAsync(formCollection);
                return RedirectToAction(nameof(Search));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuizController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuizController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
