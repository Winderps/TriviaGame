using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TriviaNight.Models;

namespace TriviaNight.Controllers
{
    public class GameController : Controller
    {
        private readonly triviaDataContext _context;

        public GameController(triviaDataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> CreateGame()
        {
            return View();
        }

        [HttpPost]
        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> CreateGame(string name, int maxPlayers)
        {
            _context.TriviaGame.Add(new TriviaGame()
            {
                Name = name,
                MaxPlayers = (byte)maxPlayers,
                Host = (ViewData[Global.UserDataKey] as User).Id
            });
            await _context.SaveChangesAsync();
            TriviaGame myGame = await _context.TriviaGame
                .Include(x => x.HostNavigation)
                .FirstAsync(
                x => x.Name.Equals(name) 
                && x.MaxPlayers.Equals(maxPlayers) 
                && x.HostNavigation.Token.Equals(ViewData[Global.UserDataKey]
                ));
            return RedirectToAction("EditGame", new { id = myGame.Id });
        }

        [HttpGet]
        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> EditGame([FromQuery]int id = -1)
        {
            if (id == -1)
            {
                return RedirectToAction("CreateGame");
            }
            TriviaGame game = await _context.TriviaGame.FirstAsync(x => x.Id == id);
            if (game.Host != ((User)ViewData[Global.UserDataKey]).Id)
            {
                return Forbid();
            }
            return View(game);
        }

        [HttpPost]
        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<JsonResult> AddQuestion(string prompt, bool multipleChoice, int numberOfAnswers = 1, params string[] answers)
        {
            return Json(new
            {
                title = prompt,
                isMultipleChoice = multipleChoice,
                answerCount = numberOfAnswers,
                firstAnswer = answers[0]
            });
        }
        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> MyTrivia()
        {
            return View();
        }

        [ServiceFilter(typeof(RequireLoginAttribute))]
        public async Task<IActionResult> CreateQuestion()
        {
            return View();
        }
    }
}
