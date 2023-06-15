using Grpc.Net.Client;
using RevolvingGamesClient;


// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("https://localhost:7250");
var client = new PrimeValidator.PrimeValidatorClient(channel);
var reply = await client.IsPrimeNumberAsync(new PrimeNumberRequest { Id = 1, Timestamp = 12, Number = 3 });
Console.WriteLine("Is Prime Number: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
