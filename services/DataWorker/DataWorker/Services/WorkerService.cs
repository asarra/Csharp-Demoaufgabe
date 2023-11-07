using Grpc.Core;
using DataWorker;
using DataWorker.Models;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;

namespace DataWorker.Services
{
    public class WorkerService : Worker.WorkerBase
    {
        private readonly CustomerContext _customerContext;
        private readonly ILogger<WorkerService> _logger;
        public WorkerService(ILogger<WorkerService> logger, CustomerContext customerContext)
        {
            _logger = logger;
            _customerContext = customerContext;
        }


        public override async Task<ModelResponse> getCustomer(IDRequest idRequest, ServerCallContext context)
        {
            _logger.LogInformation("Kunde mit der ID finden: {request.CustomerId}", idRequest.CustomerId);
            var customer = await _customerContext.Customers.FindAsync(idRequest.CustomerId);

            if (customer == null)
            {
                _logger.LogWarning("Kunde mit der ID nicht gefunden: {request.CustomerId}", idRequest.CustomerId);
                throw new RpcException(new Status(StatusCode.NotFound, "Kunde nicht gefunden"));
            }

            _logger.LogInformation("Kunde mit der ID gefunden: {request.CustomerId}", idRequest.CustomerId);

            return new ModelResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                Address = customer.Address
            };
        }


        public override async Task<CustomersListResponse> getAllCustomers(EmptyRequest emptyRequest, ServerCallContext context)
        {
            _logger.LogInformation("Kundenliste wird zurückgeliefert");
            var customers = await _customerContext.Customers.ToListAsync();

            var customerModels = new List<ModelResponse>();
            foreach (var customer in customers)
            {
                var response = new ModelResponse
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Age = customer.Age,
                    Address = customer.Address
                };

                customerModels.Add(response);
            }

            if (customerModels.Count == 0)
            {
                _logger.LogInformation("Kundenliste ist leer");
            }

            _logger.LogInformation("Kundenliste zurückgeliefert");
            return new CustomersListResponse { Customers = { customerModels } };
        }


        public override async Task<ModelResponse> createCustomer(ModelWithoutIDRequest modelWithoutIDRequest, ServerCallContext context)
        {
            _logger.LogInformation("Kunde wird erstellt");

            var isValid = Validator.ValidatingCreateData(modelWithoutIDRequest);
            if (!isValid)
            {
                _logger.LogInformation("Kundendaten sind nicht gültig");
                throw new RpcException(new Status ( StatusCode.InvalidArgument, "Kundendaten nicht gültig"));
            }

            var customer = new Customer
            {
                FirstName = modelWithoutIDRequest.FirstName,
                LastName = modelWithoutIDRequest.LastName,
                Age = modelWithoutIDRequest.Age,
                Address = modelWithoutIDRequest.Address
            };

            _customerContext.Customers.Add(customer);
            await _customerContext.SaveChangesAsync();

            _logger.LogInformation("Kunde mit der ID wurde erstellt: {request.CustomerId}", customer.CustomerId);

            return new ModelResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                Address = customer.Address
            };
        }


        public override async Task<DeleteCustomerResponse> deleteCustomer(IDRequest idRequest, ServerCallContext context)
        {
            _logger.LogInformation("Kunde mit der ID wird gelöscht: {request.CustomerId}", idRequest.CustomerId);
            var customer = await _customerContext.Customers.FindAsync(idRequest.CustomerId);

            if (customer == null)
            {
                _logger.LogWarning("Kunde mit der ID konnte nicht gefunden werden: {request.CustomerId}", idRequest.CustomerId);
                throw new RpcException(new Status(StatusCode.NotFound, "Kunde nicht gefunden"));
            }

            _customerContext.Customers.Remove(customer);
            await _customerContext.SaveChangesAsync();

            _logger.LogInformation("Kunde mit der ID wurde gelöscht: {request.CustomerId}", idRequest.CustomerId);

            return new DeleteCustomerResponse { Message = "Kunde erfolgreich gelöscht" };
        }


        public override async Task<ModelResponse> updateCustomer(ModelRequest modelRequest, ServerCallContext context)
        {
            _logger.LogInformation("Kunde mit der ID wird geupdatet: {request.CustomerId}", modelRequest.CustomerId);

            var isValid = Validator.ValidatingUpdateData(modelRequest);
            if (!isValid)
            {
                _logger.LogInformation("Kundendaten des Kunden mit der ID {request.CustomerId} sind nicht gültig", modelRequest.CustomerId);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Kundendaten nicht gültig"));
            }

            var customer = await _customerContext.Customers.FindAsync(modelRequest.CustomerId);

            if (customer == null)
            {
                _logger.LogWarning("Kunde mit der ID wurde nicht gefunden: {request.CustomerId}", modelRequest.CustomerId);
                throw new RpcException(new Status(StatusCode.NotFound, "Kunde nicht gefunden"));
            }

            customer.FirstName = modelRequest.FirstName;
            customer.LastName = modelRequest.LastName;
            customer.Age = modelRequest.Age;
            customer.Address = modelRequest.Address;

            _customerContext.Update(customer);
            await _customerContext.SaveChangesAsync();

            _logger.LogInformation("Kunde mit der ID wurde geupdatet: {request.CustomerId}", modelRequest.CustomerId);

            return new ModelResponse
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                Address = customer.Address
            };
        }
    }
}