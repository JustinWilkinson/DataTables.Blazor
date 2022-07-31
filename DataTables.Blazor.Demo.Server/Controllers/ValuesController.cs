using Microsoft.AspNetCore.Mvc;
using DataTables.Blazor.Demo.Server.Controllers;

namespace DataTables.Blazor.Demo.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {
        }

        [HttpGet]
        public object Get(int draw, int start, int length)
        {
            var ien = start + length;

            var range = new List<object>();
            for (var i = start; i < ien; i++)
            {
                range.Add(new
                {
                    Id = i,
                    FirstName = $"FN{i}",
                    LastName = $"LN{i}",
                    Email = $"{i}@mail.com"
                });
            }

            Thread.Sleep(50);

            return new
            {
                draw = draw,
                data = range,
                recordsTotal = 5000000,
                recordsFiltered = 5000000
            };
        }
    }
}
