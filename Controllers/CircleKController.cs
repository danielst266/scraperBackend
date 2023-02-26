using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CircleKController : ControllerBase
{   
   
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        CircleKScraper scraper = new CircleKScraper();
        scraper.fetchData();
        return scraper.jsonData();
    }
}
