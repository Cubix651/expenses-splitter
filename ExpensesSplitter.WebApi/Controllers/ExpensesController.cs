using System;
using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Repositories;
using ExpensesSplitter.WebApi.Models;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/settlements/{settlementId}/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesRepository _expensesRepository;

        public ExpensesController(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        [HttpGet("")]
        public ActionResult GetAll(string settlementId)
        {
            return Ok(_expensesRepository.GetExpenses(settlementId));
        }

        [HttpGet("{expenseId}")]
        public ActionResult GetOne(string settlementId, Guid expenseId)
        {
            return Ok(_expensesRepository.GetExpense(settlementId, expenseId));
        }

        [HttpPost("")]
        public ActionResult Post(string settlementId, [FromBody] NewExpense expense)
        {
            var id = _expensesRepository.CreateExpense(settlementId, expense);
            return Ok(id);
        }

        [HttpDelete("{expenseId}")]
        public ActionResult Delete(string settlementId, Guid expenseId)
        {
            _expensesRepository.DeleteExpense(settlementId, expenseId);
            return Ok();
        }

        [HttpPut("{expenseId}")]
        public ActionResult Post(string settlementId, Guid expenseId, [FromBody] NewExpense expense)
        {
            _expensesRepository.UpdateExpense(settlementId, expenseId, expense);
            return Ok();
        }
    }
}