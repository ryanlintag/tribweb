using Commons.Mediator;

namespace Application.Schools
{
    public sealed record GetDivisionsRequest : IRequest<List<DivisionModel>>;
    public sealed class GetDivisionsRequestHandler : IRequestHandler<GetDivisionsRequest, List<DivisionModel>>
    {
        public Task<List<DivisionModel>> Handle(GetDivisionsRequest request, CancellationToken cancellationToken)
        {
            // Simulate fetching data from a database or other source
            var divisions = new List<DivisionModel>
            {
                new DivisionModel(),
                new DivisionModel()
            };
            return Task.FromResult(divisions);
        }
    }
    public sealed class DivisionModel
    {

    }
}
