using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CircleKController : ControllerBase
{   
    private static CircleKScraper scraper = new CircleKScraper();


    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        scraper.fetchData();
        return scraper.jsonData();
    }
}
