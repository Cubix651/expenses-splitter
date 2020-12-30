using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Providers;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/settlements/{settlementId}/balances")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesProvider _balancesProvider;

        public BalancesController(IBalancesProvider balancesProvider)
        {
            _balancesProvider = balancesProvider;
        }

        [HttpGet("")]
        public ActionResult GetAll(string settlementId)
        {
            return Ok(_balancesProvider.GetBalances(settlementId));
        }
    }
}