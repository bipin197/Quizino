using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.Controllers
{
    public class QuizController : Controller
    {
        private readonly CosmoDBRepository<Quiz> _quizRepository;
        public QuizController()
        {
            _quizRepository = CosmoDBRepository<Quiz>.GetInstance();
        }

        // GET: QuizController/Details/5
        [HttpGet]
        public Quiz Get(int id)
        {
            return _quizRepository.GetItem(x => x.Id == id);
        }

        //// GET: QuizController/Create
        //[HttpPost]
        //public HttpResponse Create()
        //{
            
        //}

        //[HttpPut]
        //// GET: QuizController/Edit/5
        //public ActionResult Edit(int id)
        //{
            
        //}



        //// GET: QuizController/Delete/5
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
            
        //}
    }
}
