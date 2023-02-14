using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class IngoController : ControllerBase
{   
    private static IngoScraper scraper = new IngoScraper();


    
    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        scraper.fetchData();
        return scraper.jsonData();
    }
}
