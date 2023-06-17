using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Challenge.Services.Tests.Builders
{
    public static class HttpClientHandlerBuilder
    {
        public static Mock<HttpClientHandler> WithResponse(HttpResponseMessage response)
        {
            var handler = new Mock<HttpClientHandler>();

            handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            return handler;
        }
    }
}

