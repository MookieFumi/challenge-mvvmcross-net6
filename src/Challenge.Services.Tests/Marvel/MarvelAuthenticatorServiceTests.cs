using System;
using Challenge.Services.Marvel;
using FluentAssertions;
using Moq;

namespace Challenge.Services.Tests.Marvel
{
    public class MarvelAuthenticatorServiceTests
    {
        [Fact]
        public void GetAuthInfo_Should_Return_Expected_Value()
        {
            //Arrange
            var dateTimeService = new Mock<IDateTimeService>();
            dateTimeService.Setup(m => m.UtcNow()).Returns(new DateTime(2023, 06, 18, 08, 15, 30));
            var sut = new MarvelAuthenticatorService(dateTimeService.Object, "xxx", "yyy");

            //Act
            var result = sut.GetAuthInfo();

            //Assert
            result.Should().Be("apikey=yyy&hash=6f75a7f8cdf90cad9811541684b23330&ts=1687076130");
        }
    }
}