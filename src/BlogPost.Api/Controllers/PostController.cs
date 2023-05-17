using BlogPost.Application.UseCases.User.Commands;
using BlogPost.Application.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePostCommand command)
        {
            var responce = await _mediator.Send(command);

            return Ok(responce);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdatePostCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _mediator.Send(new GetAllPostQuery());

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _mediator.Send(new GetPostById() { Id = id });

            return Ok(post);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePostCommand() { Id = id });

            return Ok();
        }
    }
}
