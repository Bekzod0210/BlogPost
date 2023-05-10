using BlogPost.Application.UseCases.Admin.Commands;
using BlogPost.Application.UseCases.Auth.Commands;
using BlogPost.Application.UseCases.User.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XAct.Security;

namespace BlogPost.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Admin/Login")]
        public async Task<IActionResult> AdminLogin(LoginCommand command)   
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }

        [HttpPost("User/Register")]
        public async Task<IActionResult> UserRegister(UserRegisterCommand command)
        {
            var number = await _mediator.Send(command);
            return Ok(number);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminActions")]
        public async Task<IActionResult> UserDelete([FromRoute] int id)
        {
            var num = await _mediator.Send(new UserDeleteByIdCommand() { Id = id});
            return Ok(num);
        }
    }
}
