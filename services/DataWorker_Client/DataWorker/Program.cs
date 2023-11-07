using Grpc.Net.Client;
using DataWorker;

var channel = GrpcChannel.ForAddress("https://localhost:7219");
var client = new Worker.WorkerClient(channel);

ModelResponse createModelResponse = await client.createCustomerAsync(new ModelWithoutIDRequest
{ 
    FirstName = "Mehmet-Ali",
    LastName = "Asar",
    Age = 20,
    Address = "Hankamp 2, Schnathorst"
});
Console.WriteLine("Kunden erstellt: " + createModelResponse);


ModelResponse getModelResponse = await client.getCustomerAsync(new IDRequest{ CustomerId = 2 });
Console.WriteLine("Kunden bekommen: " + getModelResponse.ToString());


CustomersListResponse getAllModelsResponse = await client.getAllCustomersAsync(new EmptyRequest {});
Console.WriteLine("Alle Kunden bekommen: " + getAllModelsResponse.Customers.ToString());


ModelResponse updateModelResponse = await client.updateCustomerAsync(new ModelRequest {
    CustomerId = 2,
    FirstName = "Mehmet-Ali",
    LastName = "Asar",
    Age = 21,
    Address = "Hankamp 2, Schnathorst"
});
Console.WriteLine("Kunden geupdatet: " + updateModelResponse);


DeleteCustomerResponse deletedModelsResponse = await client.deleteCustomerAsync(new IDRequest { CustomerId = 2 });
Console.WriteLine("Kunden entfernt: " + deletedModelsResponse);


Console.ReadKey();