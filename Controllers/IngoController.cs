using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class IngoController : ControllerBase
{   
  
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        IngoScraper scraper = new IngoScraper();
        scraper.fetchData();
        return scraper.jsonData();
    }
}
