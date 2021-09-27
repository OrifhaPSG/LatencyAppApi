using LatencyAPI.Models;
using LatencyAPI.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.Services.Location;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LatencyAPI.Tests
{
    public class CosmosDbServiceTests
    {
        [Fact]
        public void SuccessfullyCreateCollatorLogTest()
        {
            var container = new Mock<Container>();
            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<CollatorLog>>();

            //Setups
            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<CollatorLog>(It.IsAny<CollatorLog>(),null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
            

            ICosmosDbService cosmos = new CosmosDbService(client.Object, It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>());

            var resp = cosmos.CreateCollatorLog(It.IsAny<double>(), It.IsAny<String>(), It.IsAny<DateTime>(), It.IsAny<String>());
            container.Verify();
            client.Verify();

            // Assert
            Assert.IsType<Task<CollatorLog>>(resp);
            Assert.NotNull(resp.Result);

        }

        [Fact]
        public void FailCreateCollatorLogTest()
        {
            var container = new Mock<Container>();
            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<CollatorLog>>();

            //Setups
            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.BadRequest).Verifiable();
            container.Setup(x => x.CreateItemAsync<CollatorLog>(It.IsAny<CollatorLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();


            ICosmosDbService cosmos = new CosmosDbService(client.Object, It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>());

            var resp = cosmos.CreateCollatorLog(It.IsAny<double>(), It.IsAny<String>(), It.IsAny<DateTime>(), It.IsAny<String>());

            container.Verify();
            client.Verify();

            //Assert
            Assert.Null(resp.Result);

        }

        [Fact]
        public void CreatePingLogTest()
        {
            var container = new Mock<Container>();
            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();

            //Setups
            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();


            ICosmosDbService cosmos = new CosmosDbService(client.Object, It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>());

            var resp = cosmos.CreatePingLog(It.IsAny<string>(), It.IsAny<String>(), It.IsAny<string>());
            container.Verify();
            client.Verify();

            // Assert
            Assert.IsType<Task<PingLog>>(resp);
            Assert.NotNull(resp.Result);

        }
        [Fact]
        public void FailCreatePingLogTest()
        {
            var container = new Mock<Container>();
            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();

            //Setups
            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.BadRequest).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();


            ICosmosDbService cosmos = new CosmosDbService(client.Object, It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>());

            var resp = cosmos.CreatePingLog(It.IsAny<string>(), It.IsAny<String>(), It.IsAny<string>());
            container.Verify();
            client.Verify();

            // Assert
            Assert.IsType<Task<PingLog>>(resp);
            Assert.Null(resp.Result);

        }
    }
}
