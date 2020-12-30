using System;
using Microsoft.AspNetCore.Mvc;
using ExpensesSplitter.WebApi.Repositories;
using ExpensesSplitter.WebApi.Models;

namespace ExpensesSplitter.WebApi.Controllers
{
    [Route("api/settlements/{settlementId}/users")]
    [ApiController]
    public class SettlementUsersController : ControllerBase
    {
        private readonly ISettlementUsersRepository _settlementUsersRepository;

        public SettlementUsersController(ISettlementUsersRepository settlementUsersRepository)
        {
            _settlementUsersRepository = settlementUsersRepository;
        }

        [HttpGet("")]
        public ActionResult GetAll(string settlementId)
        {
            return Ok(_settlementUsersRepository.GetSettlementUsers(settlementId));
        }

        [HttpGet("{userId}")]
        public ActionResult GetOne(string settlementId, Guid userId)
        {
            return Ok(_settlementUsersRepository.GetSettlementUser(settlementId, userId));
        }
        
        [HttpPost("")]
        public ActionResult Post(string settlementId, [FromBody] NewSettlementUser settlementUser)
        {
            var id = _settlementUsersRepository.CreateSettlementUser(settlementId, settlementUser);
            return Ok(id);
        }

        [HttpDelete("{settlementUserId}")]
        public ActionResult Delete(string settlementId, Guid settlementUserId)
        {
            _settlementUsersRepository.DeleteSettlementUser(settlementId, settlementUserId);
            return Ok();
        }

        [HttpPut("{settlementUserId}")]
        public ActionResult Post(string settlementId, Guid settlementUserId, [FromBody] NewSettlementUser settlementUser)
        {
            _settlementUsersRepository.UpdateSettlementUser(settlementId, settlementUserId, settlementUser);
            return Ok();
        }
    }
}