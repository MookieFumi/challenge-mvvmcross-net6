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
        private const string ApiPrivateKey = "20aed5b965e7641cde0af5570c199d75ac3ff485";
        private const string ApiPublicKey = "b3f61a5518e29a79066583c1989557e2";

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