using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExpensesSplitter.WebApi.Database;
using ExpensesSplitter.WebApi.Database.Models;

namespace ExpensesSplitter.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ExpensesSplitterContext _expensesSplitterContext;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ExpensesSplitterContext expensesSplitterContext)
        {
            _logger = logger;
            _expensesSplitterContext = expensesSplitterContext;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _expensesSplitterContext.Settlements
                .Select(s => $"{s.Id} - {s.Name} - {s.Description}");
        }

        [HttpPost]
        public async Task Post()
        {
            _logger.LogInformation("Creating new settlement");
            _expensesSplitterContext.Settlements.Add(new Settlement
            {
                Id = "moje-id",
                Name = "Nowe rozliczenie",
                Description = "To jest przykladowe rozliczenie"
            });
            await _expensesSplitterContext.SaveChangesAsync();
        }
    }
}
