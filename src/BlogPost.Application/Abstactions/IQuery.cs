using MediatR;

namespace BlogPost.Application.Abstactions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
