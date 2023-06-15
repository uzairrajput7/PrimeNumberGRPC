using Grpc.Net.Client;
using RevolvingGamesClient;

Random rand = new Random();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7250");
var client = new PrimeValidator.PrimeValidatorClient(channel);
//Counter to genrate unqiue id for request incrementally for every request
long counter = 1;
//To execute tasks batch wise in 10000
var tasks = new Task[10000];
while (true)
{
    for (int i = 0; i < 10000; i++)
        tasks[i] = sendPrimeRequests(counter++);

    await Task.WhenAll(tasks.Where(i => i != null));
    Thread.Sleep(1000);
}

//Async Method to Send RPC requst for Prime Validation
async Task sendPrimeRequests(long id)
{
    try
    {
        var startTime = DateTime.Now;
        var request = new PrimeNumberRequest { Id = id, Timestamp = DateTime.UnixEpoch.Ticks, Number = rand.NextInt64(1000) };
        var reply = await client.IsPrimeNumberAsync(request);
        Console.WriteLine($"Is {request.Number} a Prime Number: {reply.Message} RequestId: {request.Id} TTR: {DateTime.Now.Subtract(startTime).TotalSeconds}");
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception occurred: " + e.Message);
    }
}
