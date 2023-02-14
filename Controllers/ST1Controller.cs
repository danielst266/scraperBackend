using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ST1Controller : ControllerBase
{   
    private static ST1Scraper scraper = new ST1Scraper();
    

    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        scraper.fetchData();
        return scraper.jsonData();
    }
}
