using Grpc.Net.Client;
using RevolvingGamesClient;

Random rand = new Random();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7250");
var client = new PrimeValidator.PrimeValidatorClient(channel);

var tasks = new Task[100000];
for (int i = 0; i < 10000; i++)
    tasks[i] = sendPrimeRequests(i);

await Task.WhenAll(tasks.Where(i => i != null));


Console.WriteLine("Press any key to exit...");
Console.ReadKey();

async Task sendPrimeRequests(long id)
{
    var startTime = DateTime.Now;
    var request = new PrimeNumberRequest { Id = id, Timestamp = DateTime.UnixEpoch.Ticks, Number = rand.NextInt64(1000) };
    var reply = await client.IsPrimeNumberAsync(request);
    Console.WriteLine($"Is {request.Number} a Prime Number: {reply.Message} RequestId: {request.Id} TTR: {DateTime.Now.Subtract(startTime).TotalSeconds}");
}
