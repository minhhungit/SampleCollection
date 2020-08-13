using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ConsoleLogger
{
    public class SampleService : ISampleService
    {
        private readonly ILogger<SampleService> _log;
        private readonly IConfiguration _config;

        public SampleService(ILogger<SampleService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void DoSomething()
        {
            var countdownStartValue = _config.GetValue<int>("CountdownStartValue");
            var countdownTick = _config.GetValue<int>("CountdownTick");

            for (int i = countdownStartValue; i >= 0; i -= countdownTick)
            {
                if (i % 10 == 0)
                {
                    _log.LogError($"Current value {i.ToString().PadLeft(countdownStartValue.ToString().Length, ' ')}");
                }
                else
                {
                    _log.LogWarning($"Current value {i.ToString().PadLeft(countdownStartValue.ToString().Length, ' ')}");
                }
            }
        }
    }
}
