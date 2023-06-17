using System;
using Challenge.Core.Tests.ViewModels.Base;
using Challenge.Core.ViewModels;
using Challenge.Services.Marvel;
using Challenge.Services.Marvel.Model;
using FluentAssertions;
using Moq;

namespace Challenge.Core.Tests.ViewModels
{
    public class ComicDetailViewModelTests : ViewModelTestBase
    {
        [Fact]
        public void A()
        {
            //Arrange
            var marvelService = new Mock<IComicsService>();
            var sut = new ComicDetailViewModel(NavigationService.Object, DialogsService.Object);
            var url = "https://www.appspace.com/wp-content/uploads/2022/04/hr-tech-appspace";
            //Act
            sut.Prepare(new Comic { Thumbnail = new Thumbnail { Path = new Uri(url), Extension = "png" } });

            //Assert
            sut.Image.Should().Be($"{url}.png");
        }
    }
}