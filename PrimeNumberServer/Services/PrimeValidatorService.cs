using Grpc.Core;

namespace PrimeNumberServer.Services
{
    public class PrimeValidatorService : PrimeValidator.PrimeValidatorBase
    {
        private readonly ILogger<PrimeValidatorService> _logger;
        public static SortedList<long, bool> primes = new SortedList<long, bool>();

        public PrimeValidatorService(ILogger<PrimeValidatorService> logger)
        {
            _logger = logger;
        }

        public override Task<IsPrimeNumberReply> IsPrimeNumber(PrimeNumberRequest request, ServerCallContext context)
        {
            var response = isPrimeNumber(request.Number);
            return Task.FromResult(new IsPrimeNumberReply
            {
                Message = response ? "True" : "false"
            });
        }

        private bool isPrimeNumber(long number)
        {
            if (primes.ContainsKey(number)) return true;
            if (number < 2) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var maxCheck = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= maxCheck; i++)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
