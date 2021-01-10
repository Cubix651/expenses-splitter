using System;
using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Repositories;
using ExpensesSplitter.WebApi.Models;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/settlements/{settlementId}/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _transactionsRepository;

        public TransactionsController(ITransactionsRepository transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }

        [HttpGet("")]
        public ActionResult GetAll(string settlementId)
        {
            return Ok(_transactionsRepository.GetTransactions(settlementId));
        }

        [HttpGet("{transactionId}")]
        public ActionResult GetOne(string settlementId, Guid transactionId)
        {
            return Ok(_transactionsRepository.GetTransaction(settlementId, transactionId));
        }

        [HttpPost("")]
        public ActionResult Post(string settlementId, [FromBody] NewTransaction transaction)
        {
            var id = _transactionsRepository.CreateTransaction(settlementId, transaction);
            return Ok(id);
        }

        [HttpDelete("{transactionId}")]
        public ActionResult Delete(string settlementId, Guid transactionId)
        {
            _transactionsRepository.DeleteTransaction(settlementId, transactionId);
            return Ok();
        }

        [HttpPut("{transactionId}")]
        public ActionResult Post(string settlementId, Guid transactionId, [FromBody] NewTransaction transaction)
        {
            _transactionsRepository.UpdateTransaction(settlementId, transactionId, transaction);
            return Ok();
        }
    }
}