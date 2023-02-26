using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class PreemController : ControllerBase
{   
    
    [HttpGet(Name = "[controller]")]
    public string Get()

    {   PreemScraper scraper = new PreemScraper();
        scraper.fetchData();
        return scraper.jsonData();
    }
}
