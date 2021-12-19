using gChartRazor.NW;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace gChartRazor.Pages;

public class ChartDataModel : PageModel {
   private readonly ILogger<ChartDataModel> _logger;
   private readonly NorthwindContext _northwindContext;

   public ChartDataModel(ILogger<ChartDataModel> logger, NorthwindContext northwindContext) {
     _logger = logger;
     _northwindContext = northwindContext;
   }

   public async Task<JsonResult> OnGet() {
     var query = await _northwindContext.Products
     .Include(c => c.Category)
     .GroupBy(p => p.Category!.CategoryName)
     .Select(g => new
     {
         Name = g.Key,
         Count = g.Count()
     })
     .OrderByDescending(cp => cp.Count)
     .ToListAsync();

     return new JsonResult(query);
   }
}