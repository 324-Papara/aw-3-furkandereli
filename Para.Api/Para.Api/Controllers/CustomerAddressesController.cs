using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Schema;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerAddressResponse>>> GetAllCustomerAddress()
        {
            var operation = new GetAllCustomerAddressQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{customerAdressId}")]
        public async Task<ApiResponse<CustomerAddressResponse>> GetCustomerById([FromRoute] long customerAdressId)
        {
            var operation = new GetCustomerAddressByIdQuery(customerAdressId);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerAddressResponse>> CreateCustomerAddress([FromBody] CustomerAddressRequest request)
        {
            var operation = new CreateCustomerAddressCommand(request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpPut("{customerAddressId}")]
        public async Task<ApiResponse> UpdateCustomerAddress(long customerAddressId, [FromBody] CustomerAddressRequest request)
        {
            var operation = new UpdateCustomerAddressCommand(customerAddressId, request);
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpDelete("{customerAddressId}")]
        public async Task<ApiResponse> DeleteCustomerAddress(long customerAddressId)
        {
            var operation = new DeleteCustomerAddressCommand(customerAddressId);
            var result = await _mediator.Send(operation);
            return result;
        }
    }
}
