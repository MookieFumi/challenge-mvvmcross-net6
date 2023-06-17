using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Core.Tests.ViewModels.Base;
using Challenge.Core.ViewModels;
using Challenge.Services.Marvel;
using Challenge.Services.Marvel.Model;
using FluentAssertions;
using Moq;
using MvvmCross.ViewModels;

namespace Challenge.Core.Tests.ViewModels
{
    public class ComicsViewModelTests: ViewModelTestBase
    {        
        [Fact]
        public void Comics_Property_Should_Be_Empty_When_ViewModel_Is_Constructed()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();

            //Act
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            
            //Assert
            sut.Comics.Should().BeEmpty();
        }
        
        [Fact]
        public async Task Should_Call_IMarvelService_GetComics_Method_ViewModel_Is_Constructed()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            marvelService.Setup(m => m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MarvelResult<Comic>());
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            
            //Act
            await sut.Initialize();
            
            //Assert
            marvelService.Verify(m=> m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
        
        [Fact]
        public async Task Should_Assign_No_Comics_With_An_Empty_Response()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            marvelService.Setup(m => m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MarvelResult<Comic>());
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            
            //Act
            await sut.Initialize();
            
            //Assert
            sut.Comics.Should().BeEmpty();
        }
        
        [Fact]
        public async Task Should_Assign_Comics_With_Not_Empty_Response()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            marvelService.Setup(m => m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(GetNotEmptyResponse());
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            
            //Act
            await sut.Initialize();
            
            //Assert
            sut.Comics.Should().HaveCount(1);
            sut.Comics.Count.Should().Be(1);
        }

        [Fact]
        public async Task NavigateToDetailCommand_Should_Navigate_To_ComicDetailViewModel()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            marvelService.Setup(m => m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(GetNotEmptyResponse());
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            await sut.Initialize();

            //Act
            sut.NavigateToDetailCommand.Execute(new Comic());

            //Assert
            NavigationService.Verify(m => m.Navigate<ComicDetailViewModel, Comic>(It.IsAny<Comic>(), It.IsAny<IMvxBundle>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SearchComicCommand_Should_Send_Filter_As_Expected()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            marvelService.Setup(m => m.GetComics(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(GetNotEmptyResponse());
            var sut = new ComicsViewModel(NavigationService.Object, DialogsService.Object, marvelService.Object);
            await sut.Initialize();
            sut.Filter = "Ant-Man";

            //Act
            sut.SearchComicCommand.Execute(null);

            //Assert
            marvelService.Verify(p => p.GetComics(It.Is<string>(p => p == sut.Filter), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        private static MarvelResult<Comic> GetNotEmptyResponse()
        {
            return new MarvelResult<Comic>
            {
                Data = new Data<Comic>
                {
                    Count = 1,
                    Limit = 50,
                    Offset = 0,
                    Results = new List<Comic>
                    {
                        new Comic
                        {
                            Title = nameof(Comic.Title)
                        }
                    }
                }
            };
        }
    }
}