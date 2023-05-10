using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BlogPost.Application.Abstactions
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}
