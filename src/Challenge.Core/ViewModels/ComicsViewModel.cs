using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Challenge.Core.Services;
using Challenge.Core.ViewModels.Base;
using Challenge.Services.Marvel;
using Challenge.Services.Marvel.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Challenge.Core.ViewModels
{
    public class ComicsViewModel : ViewModelBase
    {
        private readonly IComicsService _comicsService;

        private MvxAsyncCommand<Comic> _navigateToDetailCommand;
        private MvxAsyncCommand _searchComicCommand;
        private MvxObservableCollection<Comic> _comics;
        private string _filter;

        public ComicsViewModel(IMvxNavigationService navigationService, IUserDialogs dialogsService, IComicsService comicsService): base(navigationService, dialogsService)
        {
            _comicsService = comicsService;

            Comics = new MvxObservableCollection<Comic>();
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            await LoadComics();
        }
        
        public MvxObservableCollection<Comic> Comics
        {
            get => _comics;
            set
            {
                _comics = value;
                RaisePropertyChanged(() => Comics);
            }
        }

        public string Filter
        {
            get => _filter;
            set
            {
                _filter = value;
                RaisePropertyChanged(() => Filter);
            }
        }
        
        public MvxAsyncCommand<Comic> NavigateToDetailCommand
        {
            get
            {
                return _navigateToDetailCommand ??= new MvxAsyncCommand<Comic>(comic => NavigationService.Navigate<ComicDetailViewModel, Comic>(comic));
            }
        }

        public ICommand SearchComicCommand
        {
            get
            {
                return _searchComicCommand = _searchComicCommand ??
                    new MvxAsyncCommand(()=> LoadComics());
            }
        }

        private async Task LoadComics()
        {
            ShowLoading();

            Comics.Clear();
            var response = await _comicsService.GetComics(Filter, 50);
            Comics.AddRange(response.Data.Results);

            HideLoading();
        }
    }
}
