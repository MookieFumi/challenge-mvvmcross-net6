using Challenge.Core.ViewModels;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace Challenge.iOS.Views
{
    public partial class ComicsView : MvxViewController<ComicsViewModel>
    {
        public ComicsView() : base(nameof(ComicsView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //var set = this.CreateBindingSet<ComicsView, ComicsViewModel>();
            //set.Bind(TipLabel).To(vm => vm.Tip);
            //set.Bind(SubTotalTextField).To(vm => vm.SubTotal);
            //set.Bind(GenerositySlider).To(vm => vm.Generosity);
            //set.Apply();

            //View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            //{
            //    this.SubTotalTextField.ResignFirstResponder();
            //}));
        }
    }
}