using Microsoft.AspNetCore.Mvc;
using PebriBox.Application.Features.Agents.Commands;
using PebriBox.Application.Features.Agents.Queries;
using PebriBox.Application.Models.Requests;

namespace PebriBox.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class AgentsController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateAgent([FromBody] CreateAgentRequest createAgent)
        {
            var response = await Sender.Send(new CreateAgentCommand
            {
                CreateAgent = createAgent
            });

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAgent([FromBody] UpdateAgentRequest updateAgent)
        {
            var response = await Sender.Send(new UpdateAgentCommand
            {
                UpdateAgent = updateAgent
            });

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAgent(int id)
        {
            var response = await Sender.Send(new DeleteAgentCommand
            {
                AgentId = id
            });

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAgents()
        {
            var response = await Sender.Send(new GetAgentsQuery());

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgent(int id)
        {
            var response = await Sender.Send(new GetAgentQuery { AgentId = id });

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
