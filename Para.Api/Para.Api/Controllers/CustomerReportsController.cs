using MediatR;
using Microsoft.AspNetCore.Mvc;
using Para.Bussiness.Cqrs;

namespace Para.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Reports")]
        public async Task<IActionResult> GetCustomerReports()
        {
            var operation = new GetCustomersReport();
            var result = await _mediator.Send(operation);
            return Ok(result);
        }
    }
}
