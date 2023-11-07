using Grpc.Net.Client;
using System.Threading.Channels;

namespace DataWorker
{
    public class gRPCWorkerClient
    {
        private Worker.WorkerClient _workerClient;

        public gRPCWorkerClient()
        {
            GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7219");
            _workerClient = new Worker.WorkerClient(channel);
        }

        public async Task<ModelResponse> createCustomerAsync(ModelWithoutIDRequest modelWithoutIDRequest)
        {
            ModelResponse createModelResponse = await _workerClient.createCustomerAsync(new ModelWithoutIDRequest
            {
                FirstName = modelWithoutIDRequest.FirstName,
                LastName = modelWithoutIDRequest.LastName,
                Age = modelWithoutIDRequest.Age,
                Address = modelWithoutIDRequest.Address
            });
            return createModelResponse;
            // Console.WriteLine("Kunden erstellt: " + createModelResponse);
        } 

        public async Task<ModelResponse> getCustomerAsync(int customerId)
        {
            ModelResponse getModelResponse = await _workerClient.getCustomerAsync(new IDRequest { CustomerId = customerId });
            return getModelResponse;
            // Console.WriteLine("Kunden bekommen: " + getModelResponse.ToString());
        }

        public async Task<CustomersListResponse> getAllCustomersAsync()
        {
            CustomersListResponse getAllModelsResponse = await _workerClient.getAllCustomersAsync(new EmptyRequest { });
            return getAllModelsResponse;
            // Console.WriteLine("Alle Kunden bekommen: " + getAllModelsResponse.Customers.ToString());
        }



        public async Task<ModelResponse> updateCustomerAsync(int customerid, ModelRequest modelRequest)
        {
            ModelResponse updateModelResponse = await _workerClient.updateCustomerAsync(new ModelRequest
            {
                CustomerId = customerid,
                FirstName = modelRequest.FirstName,
                LastName = modelRequest.LastName,
                Age = modelRequest.Age,
                Address = modelRequest.Address
            });
            return updateModelResponse;
            // Console.WriteLine("Kunden geupdatet: " + updateModelResponse);
        }


        public async Task<DeleteCustomerResponse> deleteCustomerAsync(int customerid)
        {
            DeleteCustomerResponse deletedModelsResponse = await _workerClient.deleteCustomerAsync(new IDRequest { CustomerId = customerid });
            return deletedModelsResponse;
            // Console.WriteLine("Kunden entfernt: " + deletedModelsResponse);
        }
    }
}
