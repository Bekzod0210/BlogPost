using BlogPost.Application.UseCases.User.Commands;
using BlogPost.Application.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPost.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateBlogCommand command)
        {
            var num = await _mediator.Send(command);

            return Ok(num);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateBlogCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _mediator.Send(new GetAllBlogQuery());

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var blog = await _mediator.Send(new GetBlogByIdQuery { Id = id });

            return Ok(blog);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _mediator.Send(new DeleteBlogCommand { Id = id });

            return Ok();
        }
    }
}
