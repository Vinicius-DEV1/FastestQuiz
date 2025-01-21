using Microsoft.AspNetCore.Mvc;

namespace FastestQuiz.Controllers
{
    public class FirebaseController : Controller
    {
        private readonly IConfiguration _configuration;

        public FirebaseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("/api/firebase-config")]
        public IActionResult GetFirebaseConfig()
        {
            var firebaseConfig = _configuration.GetSection("Firebase").Get<Dictionary<string, string>>();
            return Json(firebaseConfig);
        }
        
    }
}
