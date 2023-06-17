using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Services.Marvel;
using Challenge.Services.Marvel.Exceptions;
using Challenge.Services.Marvel.Model;
using Challenge.Services.Tests.Builders;
using FluentAssertions;
using Moq;
using Moq.Protected;

namespace Challenge.Services.Tests.Marvel
{
    public class ComicsServiceTests
    {
        private Mock<IMarvelAuthenticatorService> _authenticatorService;

        public ComicsServiceTests()
        {
            _authenticatorService = new Mock<IMarvelAuthenticatorService>();
            _authenticatorService.Setup(m => m.GetAuthInfo()).Returns("apikey=yyy&hash=6f75a7f8cdf90cad9811541684b23330&ts=1687076130");
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.Unauthorized)]
        public async Task GetComics_Should_Throw_MarvelException_If_Api_Call_Call_Is_Not_Success(HttpStatusCode httpStatusCode)
        {
            //Arrange
            var handler = HttpClientHandlerBuilder.WithResponse(new HttpResponseMessage
            {
                StatusCode = httpStatusCode
            });
            var sut = new ComicsService(handler.Object, _authenticatorService.Object);

            //Act
            var action = new Func<Task<MarvelResult<Comic>>>(() => sut.GetComics(string.Empty));

            //Assert
            await action.Should().ThrowExactlyAsync<MarvelException>();
        }

        [Fact]
        public async Task GetComics_Should_Returns_An_Empty_Results_Collection_If_There_IS_No_Datay()
        {
            //Arrange
            var handler = HttpClientHandlerBuilder.WithResponse(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            });
            var sut = new ComicsService(handler.Object, _authenticatorService.Object);

            //Act
            var result = await sut.GetComics(string.Empty);

            //Assert
            result.Data.Results.Should().BeEmpty();
        }

        [Fact]
        public async Task GetComics_Should_Returns_Expected_Data()
        {
            //Arrange
            var handler = HttpClientHandlerBuilder.WithResponse(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(GetNotEmptyApiResponse())
            });
            var sut = new ComicsService(handler.Object, _authenticatorService.Object);

            //Act
            var result = await sut.GetComics(string.Empty);

            //Assert
            result.Code.Should().Be(200);
            result.Status.Should().Be("Ok");
            result.AttributionText.Should().Be("Data provided by Marvel. © 2023 MARVEL");
            result.Data.Results.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetComics_Should_Include_TitleStartsWith_In_QueryString()
        {
            //Arrange
            var handler = HttpClientHandlerBuilder.WithResponse(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(GetNotEmptyApiResponse())
            });
            var sut = new ComicsService(handler.Object, _authenticatorService.Object);
            const string filter = "Ant";

            //Act
            var result = await sut.GetComics(filter);

            //Assert
            handler.Protected().Verify(
                           "SendAsync",
                           Times.Exactly(1),
                           ItExpr.Is<HttpRequestMessage>(request =>
                              request.Method == HttpMethod.Get
                              && request.RequestUri.ToString().Contains($"titleStartsWith={filter}")
                           ),
                           ItExpr.IsAny<CancellationToken>()
                        );
        }

        private static string GetNotEmptyApiResponse ()
        {
            return @"
                {
                    ""code"": 200,
                    ""status"": ""Ok"",
                    ""copyright"": ""© 2023 MARVEL"",
                    ""attributionText"": ""Data provided by Marvel. © 2023 MARVEL"",
                    ""attributionHTML"": ""<a href=\""http://marvel.com\"">Data provided by Marvel. © 2023 MARVEL</a>"",
                    ""etag"": ""561ed8424b8a2d55836142e6685378eee8e127b1"",
                    ""data"": {
                        ""offset"": 0,
                        ""limit"": 50,
                        ""total"": 56263,
                        ""count"": 50,
                        ""results"": [
                            {
                                ""id"": 82967,
                                ""digitalId"": 0,
                                ""title"": ""Marvel Previews (2017)"",
                                ""issueNumber"": 0,
                                ""variantDescription"": """",
                                ""description"": """",
                                ""modified"": ""2019-11-07T08:46:15-0500"",
                                ""isbn"": """",
                                ""upc"": ""75960608839302811"",
                                ""diamondCode"": """",
                                ""ean"": """",
                                ""issn"": """",
                                ""format"": """",
                                ""pageCount"": 112,
                                ""textObjects"": [],
                                ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/82967"",
                                ""urls"": [
                                    {
                                        ""type"": ""detail"",
                                        ""url"": ""http://marvel.com/comics/issue/82967/marvel_previews_2017?utm_campaign=apiRef&utm_source=b3f61a5518e29a79066583c1989557e2""
                                    }
                                ],
                                ""series"": {
                                    ""resourceURI"": ""http://gateway.marvel.com/v1/public/series/23665"",
                                    ""name"": ""Marvel Previews (2017 - Present)""
                                },
                                ""variants"": [
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/82965"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/82970"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/82969"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/74697"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/72736"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/75668"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/65364"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/65158"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/65028"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/75662"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/74320"",
                                        ""name"": ""Marvel Previews (2017)""
                                    },
                                    {
                                        ""resourceURI"": ""http://gateway.marvel.com/v1/public/comics/73776"",
                                        ""name"": ""Marvel Previews (2017)""
                                    }
                                ],
                                ""collections"": [],
                                ""collectedIssues"": [],
                                ""dates"": [
                                    {
                                        ""type"": ""onsaleDate"",
                                        ""date"": ""2099-10-30T00:00:00-0500""
                                    },
                                    {
                                        ""type"": ""focDate"",
                                        ""date"": ""2019-10-07T00:00:00-0400""
                                    }
                                ],
                                ""prices"": [
                                    {
                                        ""type"": ""printPrice"",
                                        ""price"": 0
                                    }
                                ],
                                ""thumbnail"": {
                                    ""path"": ""http://i.annihil.us/u/prod/marvel/i/mg/b/40/image_not_available"",
                                    ""extension"": ""jpg""
                                },
                                ""images"": [],
                                ""creators"": {
                                    ""available"": 1,
                                    ""collectionURI"": ""http://gateway.marvel.com/v1/public/comics/82967/creators"",
                                    ""items"": [
                                        {
                                            ""resourceURI"": ""http://gateway.marvel.com/v1/public/creators/10021"",
                                            ""name"": ""Jim Nausedas"",
                                            ""role"": ""editor""
                                        }
                                    ],
                                    ""returned"": 1
                                },
                                ""characters"": {
                                    ""available"": 0,
                                    ""collectionURI"": ""http://gateway.marvel.com/v1/public/comics/82967/characters"",
                                    ""items"": [],
                                    ""returned"": 0
                                },
                                ""stories"": {
                                    ""available"": 2,
                                    ""collectionURI"": ""http://gateway.marvel.com/v1/public/comics/82967/stories"",
                                    ""items"": [
                                        {
                                            ""resourceURI"": ""http://gateway.marvel.com/v1/public/stories/183698"",
                                            ""name"": ""cover from Marvel Previews (2017)"",
                                            ""type"": ""cover""
                                        },
                                        {
                                            ""resourceURI"": ""http://gateway.marvel.com/v1/public/stories/183699"",
                                            ""name"": ""story from Marvel Previews (2017)"",
                                            ""type"": ""interiorStory""
                                        }
                                    ],
                                    ""returned"": 2
                                },
                                ""events"": {
                                    ""available"": 0,
                                    ""collectionURI"": ""http://gateway.marvel.com/v1/public/comics/82967/events"",
                                    ""items"": [],
                                    ""returned"": 0
                                }
                            }
                        ]
                    }
                }
                   ";
        }
    }
}