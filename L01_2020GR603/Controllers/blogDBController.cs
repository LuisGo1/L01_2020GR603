using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020GR603.models;
using Microsoft.EntityFrameworkCore;




namespace L01_2020GR603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class blogDBController : ControllerBase
    {
        private readonly usuarios _blogDB;

        public blogDBController(usuarios blogDB)
        {
            _blogDB = blogDB;
        }
        [HttpGet]
        [Route("GetAll")]

    }
}
