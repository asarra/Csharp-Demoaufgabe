using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Grpc.Net.Client;
using DataWorker;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Threading.Channels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860




namespace API_Gateway.Controllers
    {

  

    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly gRPCWorkerClient _gRPCWorkerClient;
        public CustomerController()
        {
            _gRPCWorkerClient = new gRPCWorkerClient();
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<CustomersListResponse> Get()
        {
            return _gRPCWorkerClient.getAllCustomersAsync();
            // return new string[] { "value1", "value2" };
        }

        [HttpGet("{CustomerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ModelResponse> Get([FromRoute(Name = "CustomerId")][Required] int CustomerId)
        {
            return _gRPCWorkerClient.getCustomerAsync(CustomerId);
            // return "value";
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ModelResponse> Post([FromBody] ModelWithoutIDRequest modelWithoutIDRequest)
        {
            return _gRPCWorkerClient.createCustomerAsync(modelWithoutIDRequest);
        }

        [HttpPut("{CustomerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<ModelResponse> Put([FromRoute(Name = "CustomerId")][Required] int CustomerId, [FromBody] ModelRequest modelRequest)
        {
            return _gRPCWorkerClient.updateCustomerAsync(CustomerId, modelRequest);
        }

        [HttpDelete("{CustomerId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task<DeleteCustomerResponse> Delete([FromRoute(Name = "CustomerId")][Required] int CustomerId)
        {
            return _gRPCWorkerClient.deleteCustomerAsync(CustomerId);
        }
    }
}
