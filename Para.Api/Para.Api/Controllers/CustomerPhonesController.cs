using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPhonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerPhonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerPhoneResponse>>> GetAllCustomerPhones()
        {
            var operation = new GetAllCustomerPhoneQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerPhoneId}")]
        public async Task<ApiResponse<CustomerPhoneResponse>> GetCustomerPhoneById([FromRoute] long customerPhoneId)
        {
            var operation = new GetCustomerPhoneByIdQuery(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerPhoneResponse>> CreateCustomerPhone([FromBody] CustomerPhoneRequest request)
        {
            var operation = new CreateCustomerPhoneCommand(request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerPhoneId}")]
        public async Task<ApiResponse> UpdateCustomerPhone(long customerPhoneId, [FromBody] CustomerPhoneRequest request)
        {
            var operation = new UpdateCustomerPhoneCommand(customerPhoneId, request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerPhoneId}")]
        public async Task<ApiResponse> DeleteCustomerPhone(long customerPhoneId)
        {
            var operation = new DeleteCustomerPhoneCommand(customerPhoneId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
