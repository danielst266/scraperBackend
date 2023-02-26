using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ST1Controller : ControllerBase
{   
    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        ST1Scraper scraper = new ST1Scraper();
        scraper.fetchData();
        return scraper.jsonData();
    }
}
