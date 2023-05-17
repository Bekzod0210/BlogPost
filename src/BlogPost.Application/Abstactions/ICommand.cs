using MediatR;

namespace BlogPost.Application.Abstactions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
