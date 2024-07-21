using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record GetCustomersReport() : IRequest<ApiResponse<List<CustomerReport>>>;