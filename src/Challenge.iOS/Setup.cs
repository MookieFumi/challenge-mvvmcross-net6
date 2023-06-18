using MvvmCross.Platforms.Ios.Core;
using Challenge.Core;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;

namespace Challenge.iOS
{
    public class Setup : MvxIosSetup<App>
    {
        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.NSLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}
