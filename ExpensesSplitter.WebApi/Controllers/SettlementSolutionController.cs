using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Providers;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/settlements/{settlementId}/solution")]
    [ApiController]
    public class SettlementSolutionController : ControllerBase
    {
        private readonly ISettlementSolutionProvider _settlementSolutionProvider;

        public SettlementSolutionController(ISettlementSolutionProvider settlementSolutionProvider)
        {
            _settlementSolutionProvider = settlementSolutionProvider;
        }

        [HttpGet("")]
        public ActionResult GetAll(string settlementId)
        {
            return Ok(_settlementSolutionProvider.GetSolutionTransactions(settlementId));
        }
    }
}