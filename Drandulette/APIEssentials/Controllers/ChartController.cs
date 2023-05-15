using Drandulette.APIEssentials.Data.Models;
using Drandulette.Controllers.Data;
using Microsoft.AspNetCore.Mvc;

namespace Drandulette.APIEssentials.Controllers
{
    [Route("chart/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        DranduletteContext dbConnector;

        public ChartController(DranduletteContext context) => dbConnector = context;

        [HttpGet(Name = "GetChart")]
        public IEnumerable<ChartTuple> Get(int type)
        {
            if (type == 1) return dbConnector.Announcement
                                             .GroupBy(x => x.brand)
                                             .Select(x => new ChartTuple
                                             {
                                                 lable = x.Key,
                                                 count = x.Count()
                                             });
            else if (type == 2) return dbConnector.Announcement
                                                  .GroupBy(x => x.year)
                                                  .Select(x => new ChartTuple
                                                  {
                                                      lable = x.Key.ToString(),
                                                      count = x.Average(x => x.price)
                                                  });

            return Enumerable.Empty<ChartTuple>();
        }
    }
}
