using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class OKQ8Controller : ControllerBase
{   
    private static OKQ8Scraper scraper = new OKQ8Scraper();


    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        
        scraper.fetchData();
        return scraper.jsonData();
    }
}
