using Acr.UserDialogs;
using Challenge.Core.ViewModels.Base;
using Challenge.Services.Marvel.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Challenge.Core.ViewModels
{
    public class ComicDetailViewModel : ViewModelBase<Comic>
    {
        private string _image;

        public ComicDetailViewModel(IMvxNavigationService navigationService, IUserDialogs dialogsService) : base(navigationService, dialogsService)
        {
        }

        public Comic Comic { get; private set; }
        
        public override void Prepare(Comic comic)
        {
            Comic = comic;

            Image = $"{comic.Thumbnail.Path}.{comic.Thumbnail.Extension}";
        }

        public string Image
        {
            get => _image;
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }
    }
}
