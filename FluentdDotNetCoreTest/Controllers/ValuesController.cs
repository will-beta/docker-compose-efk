using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FluentdDotNetCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get([FromServices]ILogger<ValuesController> logger)
        {
            logger.LogInformation($"hey out info");
            logger.LogWarning($"hey out warn");
            logger.LogError($"hey in error");
            using (var scope = logger.BeginScope($"scope{DateTimeOffset.Now.Second}"))
            {
                logger.LogInformation($"hey in info");
                logger.LogWarning($"hey in warn");
                logger.LogError($"hey in error");
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
