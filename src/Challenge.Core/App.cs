using Challenge.Core.Services;
using Challenge.Core.ViewModels;
using Challenge.Services;
using Challenge.Services.Marvel;
using MvvmCross;
using MvvmCross.ViewModels;

namespace Challenge.Core
{
    public class App : MvxApplication
    {
        private const string ApiPrivateKey = "xxx";
        private const string ApiPublicKey = "yyy";

        public override void Initialize()
        {
            RegisterServices();
            RegisterAppStart<ComicsViewModel>();
        }

        private static void RegisterServices()
        {
            Mvx.IoCProvider.RegisterType<IDateTimeService, DateTimeService>();
            Mvx.IoCProvider.RegisterSingleton<IMarvelAuthenticatorService>(new MarvelAuthenticatorService(Mvx.IoCProvider.Resolve<IDateTimeService>(), ApiPrivateKey, ApiPublicKey));
            Mvx.IoCProvider.RegisterType<IComicsService>(() => new ComicsService(null, Mvx.IoCProvider.Resolve<IMarvelAuthenticatorService>()));
        }
    }
}