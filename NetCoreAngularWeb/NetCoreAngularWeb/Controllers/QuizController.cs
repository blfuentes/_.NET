using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreAngularWeb.Data;
using NetCoreAngularWeb.ViewModels;
using Newtonsoft.Json;

namespace NetCoreAngularWeb.Controllers
{

    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        #region Private Fields
        private ApplicationDbContext DbContext;
        #endregion

        #region Constructor
        public QuizController(ApplicationDbContext context)
        {
            // Instantiate the ApplicationDbContext through DI
            DbContext = context;
        }
        #endregion Constructor

        #region RESTful conventions methods 
        /// <summary> 
        /// GET: api/quiz/{}id 
        /// Retrieves the Quiz with the given {id} 
        /// </summary> 
        /// <param name="id">The ID of an existing Quiz</param> 
        /// <returns>the Quiz with the given {id}</returns> 
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = DbContext.Quizzes.Where(_i => _i.Id == id).FirstOrDefault();

            return new JsonResult(quiz.Adapt<QuizViewModel>(),
                new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }

        /// <summary> 
        /// Adds a new Quiz to the Database 
        /// </summary> 
        /// <param name="m">The QuizViewModel containing the data to insert</param> 
        [HttpPut]
        public IActionResult Put(QuizViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Edit the Quiz with the given {id} 
        /// </summary> 
        /// <param name="m">The QuizViewModel containing the data to update</param> 
        [HttpPost]
        public IActionResult Post(QuizViewModel m)
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Deletes the Quiz with the given {id} from the Database 
        /// </summary> 
        /// <param name="id">The ID of an existing Quiz</param> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Attribute-based routing methods
        /// <summary> 
        /// GET: api/quiz/latest 
        /// Retrieves the {num} latest Quizzes 
        /// </summary> 
        /// <param name="num">the number of quizzes to retrieve</param> 
        /// <returns>the {num} latest Quizzes</returns>
        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(_q => _q.CreatedDate)
                .Take(num)
                .ToArray();
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/quiz/ByTitle
        /// Retrieves the {nunm} Quizzas sorted by Title (A to Z)
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} Quizzes sorted by Title</returns>
        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(_q => _q.Title)
                .Take(num)
                .ToArray();
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }

        /// <summary>
        /// GET: api/quiz/mostViewed
        /// Retrieves the {num} random Quizzes
        /// </summary>
        /// <param name="num">the number of quizzes to retrieve</param>
        /// <returns>{num} random Quizzes</returns>
        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10)
        {
            var latest = DbContext.Quizzes
                .OrderByDescending(_q => Guid.NewGuid())
                .Take(num)
                .ToArray();
            return new JsonResult(
                latest.Adapt<QuizViewModel[]>(),
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
        }
        #endregion
    }
}