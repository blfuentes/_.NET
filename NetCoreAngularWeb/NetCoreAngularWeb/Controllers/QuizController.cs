using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetCoreAngularWeb.Data;
using NetCoreAngularWeb.Data.Models;
using NetCoreAngularWeb.ViewModels;
using Newtonsoft.Json;

namespace NetCoreAngularWeb.Controllers
{
    public class QuizController : BaseApiController
    {
        #region Constructor
        public QuizController(ApplicationDbContext context,
                                RoleManager<IdentityRole> roleManager,
                                UserManager<ApplicationUser> userManager,
                                IConfiguration configuration)
            : base(context, roleManager, userManager, configuration) { }
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

            // handle requests asking for non-existing quizzes
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID {id} has not been found"
                });
            }

            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary> 
        /// Adds a new Quiz to the Database 
        /// </summary> 
        /// <param name="model">The QuizViewModel containing the data to insert</param> 
        [HttpPut]
        public IActionResult Put([FromBody]QuizViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            // handle the insert (without object-mapping)
            var quiz = new Quiz();

            // properties taken from the request
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            // properties set from server-side
            quiz.CreatedDate = DateTime.Now;
            quiz.LastModifiedDate = quiz.CreatedDate;

            // Set a temporary author using the Admin user's userId
            // as user login isn't supported yet: we'll change this later on.
            quiz.UserId = DbContext.Users.Where(_u => _u.UserName == "Admin").FirstOrDefault().Id;

            // add the new quiz
            DbContext.Quizzes.Add(quiz);
            // persist the changes into the database
            DbContext.SaveChanges();

            // return the newly-created Quiz to the client.
            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary> 
        /// Edit the Quiz with the given {id} 
        /// </summary> 
        /// <param name="model">The QuizViewModel containing the data to update</param> 
        [HttpPost]
        public IActionResult Post([FromBody]QuizViewModel model)
        {
            // return a generic HTTP Status 500 (Server Error)
            // if the client payload is invalid.
            if (model == null) return new StatusCodeResult(500);

            // retrieve the quiz to edit
            var quiz = DbContext.Quizzes.Where(_q => _q.Id == model.Id).FirstOrDefault();

            // handle request asking for non-existing quizzes
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID {model.Id} has not been found"
                });
            }

            // handle the update (without object-mapping)
            // by manually assigning the properties
            // we want to accept from the request
            quiz.Title = model.Title;
            quiz.Description = model.Description;
            quiz.Text = model.Text;
            quiz.Notes = model.Notes;

            // properties set from the server-side
            quiz.LastModifiedDate = quiz.CreatedDate;

            // persist the changes into the database.
            DbContext.SaveChanges();

            // return the updated Quiz to the client.
            return new JsonResult(quiz.Adapt<QuizViewModel>(), JsonSettings);
        }

        /// <summary> 
        /// Deletes the Quiz with the given {id} from the Database 
        /// </summary> 
        /// <param name="id">The ID of an existing Quiz</param> 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // retrieve the quiz from the Database
            var quiz = DbContext.Quizzes.Where(i => i.Id == id)
                .FirstOrDefault();

            // handle requests asking for non-existing quizzes
            if (quiz == null)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID {id} has not been found"
                });
            }

            // remove the quiz from the DbContext.
            DbContext.Quizzes.Remove(quiz);
            // persist the changes into the Database.
            DbContext.SaveChanges();

            // return an HTTP Status 200 (OK).
            return new OkResult();
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
                latest.Adapt<QuizViewModel[]>(), JsonSettings);
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
                latest.Adapt<QuizViewModel[]>(), JsonSettings);
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
                latest.Adapt<QuizViewModel[]>(), JsonSettings);
        }
        #endregion
    }
}