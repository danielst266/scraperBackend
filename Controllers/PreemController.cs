using Microsoft.AspNetCore.Mvc;
using gasStation;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class PreemController : ControllerBase
{   
    private static PreemScraper scraper = new PreemScraper();

    [HttpGet(Name = "[controller]")]
    public string Get()
    {   
        scraper.fetchData();
        return scraper.jsonData();
    }
}
