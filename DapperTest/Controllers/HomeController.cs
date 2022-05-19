using Dapper;
using DapperTest.Models;
using DapperTest.Models.Validator;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DapperTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        TodoItemsValidator customerValidator = new TodoItemsValidator();

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("localhost")))
                {
                    conn.Open();
                    List<TodoItems> todoItems = conn.Query<TodoItems>("select * from TodoItems").ToList();
                    return Ok(todoItems);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItems todoItems)
        {
            if (!customerValidator.Validate(todoItems).IsValid) {
                return BadRequest(customerValidator.Validate(todoItems).Errors);
            }
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("localhost")))
                {
                    conn.Open();
                    conn.Execute("insert into TodoItems(Title,Description,Status) values (@Title,@Description,@Status)", todoItems);
                    return Ok(todoItems);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            

           
        }



    }
}