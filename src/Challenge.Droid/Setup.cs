using Acr.UserDialogs;
using Challenge.Core;
using Microsoft.Extensions.Logging;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace Challenge.UI.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            iocProvider.RegisterType<IUserDialogs>(() => UserDialogs.Instance);
            base.InitializeLastChance(iocProvider);
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AndroidLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }
    }
}
