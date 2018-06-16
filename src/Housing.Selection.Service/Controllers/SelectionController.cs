using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Housing.Selection.Service.Controllers
{
  [Route("api/[controller]")]
  public class SelectionController : BaseController
  {
    public SelectionController(ILoggerFactory loggerFactory) : base(loggerFactory) {}
    
    public async Task<IActionResult> Get()
    {
      return await Task.Run(() => Ok());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      return await Task.Run(() => Ok());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]object value)
    {
      return await Task.Run(() => Ok());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody]object value)
    {
      return await Task.Run(() => Ok());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      return await Task.Run(() => Ok());
    }
  }
}