using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerDetailsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerDetailResponse>>> GetAllCustomerDetails()
        {
            var operation = new GetAllCustomerDetailQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerDetailId}")]
        public async Task<ApiResponse<CustomerDetailResponse>> GetCustomerDetailById([FromRoute] long customerDetailId)
        {
            var operation = new GetCustomerDetailByIdQuery(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerDetailResponse>> CreateCustomerDetail([FromBody] CustomerDetailRequest request)
        {
            var operation = new CreateCustomerDetailCommand(request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerDetailId}")]
        public async Task<ApiResponse> UpdateCustomerDetail(long customerDetailId, [FromBody] CustomerDetailRequest request)
        {
            var operation = new UpdateCustomerDetailCommand(customerDetailId, request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerDetailId}")]
        public async Task<ApiResponse> DeleteCustomerDetail(long customerDetailId)
        {
            var operation = new DeleteCustomerDetailCommand(customerDetailId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
