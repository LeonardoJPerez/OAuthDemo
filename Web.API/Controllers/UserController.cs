using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Swashbuckle.SwaggerGen.Annotations;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        /// <summary>
        /// This method returns an array of values.
        /// </summary>
        /// <returns>Array of String Values.</returns>
        // GET api/user
        [HttpGet]
        [Authorize]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IActionResult> Get()
        {
            var user = User as ClaimsPrincipal;

            dynamic response = new ExpandoObject();
            response.Email = user?.FindFirst(c => c.Type == "sub")?.Value;
            response.Name = user?.FindFirst(c => c.Type == "name")?.Value;

            return Ok(response);
        }

        /// <summary>
        /// This method returns an array of values. Secured and requires the private scope.
        /// </summary>
        /// <returns>Array of String Values.</returns>
        // GET api/user
        [HttpGet]
        [Route("private")]
        [Authorize("private")]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(string))]
        public async Task<IActionResult> Private()
        {
            var user = User as ClaimsPrincipal;

            dynamic response = new ExpandoObject();
            response.Email = user?.FindFirst(c => c.Type == "sub")?.Value;
            response.Name = user?.FindFirst(c => c.Type == "name")?.Value;

            return Ok(response);
        }

        // GET api/user/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}