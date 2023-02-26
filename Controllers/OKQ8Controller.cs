using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class OKQ8Controller : ControllerBase
{   
    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        OKQ8Scraper scraper = new OKQ8Scraper();
        scraper.fetchData();
        return scraper.jsonData();
    }
}
