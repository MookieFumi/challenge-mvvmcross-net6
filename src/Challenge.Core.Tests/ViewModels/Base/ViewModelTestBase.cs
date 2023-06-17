using Acr.UserDialogs;
using Challenge.Core.Tests.Dispatchers;
using Moq;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using MvvmCross.Views;

namespace Challenge.Core.Tests.ViewModels.Base
{
    public abstract class ViewModelTestBase : MvxIoCSupportingTest
    {
        protected readonly Mock<IMvxNavigationService> NavigationService;
        protected readonly Mock<IUserDialogs> DialogsService;

        protected ViewModelTestBase()
        {
            NavigationService = new Mock<IMvxNavigationService>();
            DialogsService = new Mock<IUserDialogs>();

            base.Setup();
        }
        
        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();
            MockMvxViewDispatcher mockMvxViewDispatcher = new MockMvxViewDispatcher();
            Ioc.RegisterSingleton<IMvxViewDispatcher>(mockMvxViewDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(mockMvxViewDispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadAsyncDispatcher>(mockMvxViewDispatcher);
        }
    }
}