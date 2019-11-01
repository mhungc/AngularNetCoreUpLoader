using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RestUploadFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
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
        public void Post()
        {
            Request.EnableRewind();

            byte[] streamBytes;

            using (MemoryStream ms = new MemoryStream())
            {
                Request.Body.CopyTo(ms);
                streamBytes = ms.ToArray();
            }

            Request.Body.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(Request.Body))
            {
                var serializer = new JsonSerializer();
                Dictionary<string, object> dict;

                using (var jsonTextReader = new JsonTextReader(reader))
                {
                    dict = serializer.Deserialize<Dictionary<string, object>>(jsonTextReader);
                }

                var nameFile = dict["name"];

            }


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
