using Bank.Commun.Domain.Dto;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bank.Api.Pipelines
{
    public class ValidateCommand<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is Validatable validate)) return await next();

            validate.Validate();
            if (!validate.Invalid) return await next();

            var response = new Result();
            foreach (var notification in validate.Notifications)
                response.AddValidation(notification.Message);
            return response as TResponse;
        }
    }
}