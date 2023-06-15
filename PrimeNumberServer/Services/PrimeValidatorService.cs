using Grpc.Core;

namespace PrimeNumberServer.Services
{
    public class PrimeValidatorService : PrimeValidator.PrimeValidatorBase
    {
        private readonly ILogger<PrimeValidatorService> _logger;
        public static SortedSet<long> primes = new SortedSet<long>();
        public static long counter = 0;
        static readonly object _object = new object();

        public PrimeValidatorService(ILogger<PrimeValidatorService> logger)
        {
            _logger = logger;
        }

        public override Task<IsPrimeNumberReply> IsPrimeNumber(PrimeNumberRequest request, ServerCallContext context)
        {
            try
            {
                //Validating the request number to be prime
                var response = isPrimeNumber(request.Number);
                //Locking the variables for maintaing multitasking operations
                lock (_object)
                {
                    counter++;
                    if (response)
                        if (!primes.Contains(request.Number))
                            primes.Add(request.Number);

                }
                return Task.FromResult(new IsPrimeNumberReply
                {
                    Message = response ? "True" : "false"
                });
            }
            catch(Exception ex)
            {
                return Task.FromResult(new IsPrimeNumberReply { Message = ex.Message });
            }
        }

        //Function to validate a number is a prime or not
        //Works by taking square root of the number and finding the divisor of that number
        //If the square root number doesnt have a divisor from 3 to it then number is also prime
        private bool isPrimeNumber(long number)
        {
            if (primes.Contains(number)) return true;
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var maxCheck = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= maxCheck; i++)
                if (number % i == 0)
                    return false;

            return true;
        }

        //Printing the primes list in an async task
        public static Task printPrimesList()
        {
            while (true)
            {
                var _p = primes.TakeLast(10).ToArray(); //Taking last 10 highest primes in max sorted 
                foreach (var p in _p)
                {
                    Console.Write($"{p},");
                }
                Console.WriteLine($"Total Requests: {counter}");
                Console.WriteLine("++++++++++++++++++++++++++++++");
                Thread.Sleep(1000);
            }
        }
    }
}
