using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Repositories;

namespace MVCDemo.Controllers
{
    public class CastController : Controller
    {
        public IMovieRepo Repo { get; set; }


        //(dependency injection, based on Startup.ConfigureServices
        public CastController(IMovieRepo repo)
        {
            Repo = repo;
        }


        //We have two basic types of routing in ASp.NET
        //Routing is how ASP.NET decides, based on the URL (and the HTTP method)
        //Wich controller to construct and wich action method call

        //(1) convention routing - defined globally in Startup.cs
        //(2) Attribute routing - defined with attributes on controllers or actions methods

        //route parameter name dafined in my route will wind up here

        [Route("MoviesWithActor/{name}", Name = "cast_attr")]
        public IActionResult Index(string name)
        {
            var movies = Repo.GetAllByCastMember(name).ToList();
            //a ToList here enables me to debug the LINQ stuff since
            //The iteration happens in my code and not the View rendering code
            return View(movies);
        }
    }
}