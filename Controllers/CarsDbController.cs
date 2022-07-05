using BMW_API.Data;
using Microsoft.AspNetCore.Mvc;

namespace BMW_API.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class CarsDbController : ControllerBase
    {
        private readonly ICarAPIRepo _repository;
        public CarsDbController(ICarAPIRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult SeedDb() 
        {
            var parser = new Parser();
            _repository.PostCarListToDb(parser.ParseCarList());
            _repository.SaveChanges();

            return Ok();
        }
    }
}