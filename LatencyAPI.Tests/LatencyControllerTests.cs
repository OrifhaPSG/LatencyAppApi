using LatencyAPI.Controllers;
using LatencyAPI.Models;
using LatencyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Xunit;

namespace LatencyAPI.Tests
{
    public class LatencyControllerTests
    {
        [Fact]
        public async void SuccessfulPingServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
            config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();

            
            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("region", new StringValues("TestOrigin"));
            dictionary.Add("Region", new StringValues("Region"));
            dictionary.Add("uuid", new StringValues("TEST UUID"));

            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;
       
           

            var res = await controller.Ping();
            Assert.Equal( "TestOrigin", res.Value.OriginRegion);
        }


        [Fact]
        public async void FailOnNoUUIDPingServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
            config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();


            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("region", new StringValues("TestOrigin"));
            dictionary.Add("Region", new StringValues("Region"));
            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;

            var res = await controller.Ping();
            Assert.Null(res.Value);
        }

        [Fact]
        public async void FailOnNoRegionPingServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
            config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();


            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("uuid", new StringValues("TEST UUID"));
            dictionary.Add("Region", new StringValues("Region"));
            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;

            var res = await controller.Ping();
            Assert.Null(res.Value);
        }
        [Fact]
        public async void FailOnNoOriginRegionPingServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<PingLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<PingLog>(It.IsAny<PingLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
          //  config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();


            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("uuid", new StringValues("TEST UUID"));
            dictionary.Add("region", new StringValues("TestOrigin"));
            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;

            var res = await controller.Ping();
            Assert.Null(res.Value);
        }

        [Fact]
        public async void FailCollatorOnNoRegionServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<CollatorLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<CollatorLog>(It.IsAny<CollatorLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
           // config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();
            config.Setup(x => x["PingServiceUrl"]).Returns("https://test.com").Verifiable();


            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("region", new StringValues("TestOrigin"));
            dictionary.Add("Region", new StringValues("Region"));
            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;



            var res = await controller.Collate();
            Assert.Null(res.Value);
            //
        }

        [Fact]
        public async void FailCollatorOnNoURLServiceCallTest()
        {

            var client = new Mock<CosmosClient>();
            var responseMock = new Mock<ItemResponse<CollatorLog>>();
            var container = new Mock<Container>();
            var logger = new Mock<ILogger<LatencyController>>();
            var config = new Mock<IConfiguration>();

            responseMock.Setup(x => x.StatusCode).Returns(System.Net.HttpStatusCode.Created).Verifiable();
            container.Setup(x => x.CreateItemAsync<CollatorLog>(It.IsAny<CollatorLog>(), null, null, It.IsAny<CancellationToken>())).ReturnsAsync(responseMock.Object).Verifiable();
            client.Setup(x => x.GetContainer(It.IsAny<string>(), It.IsAny<string>())).Returns(container.Object).Verifiable();
            config.Setup(x => x["Region"]).Returns("Test Region").Verifiable();
            //config.Setup(x => x["PingServiceUrl"]).Returns("https://test.com").Verifiable();


            var cosmos = new CosmosDbService(client.Object, "db", "ping", "collator");
            var context = new Mock<HttpContext>();
            var request = new Mock<HttpRequest>();
            var dictionary = new Dictionary<string, StringValues>();

            LatencyController controller = new LatencyController(logger.Object, cosmos, config.Object);
            dictionary.Add("region", new StringValues("TestOrigin"));
            dictionary.Add("Region", new StringValues("Region"));
            request.Setup(x => x.Query).Returns(new QueryCollection(dictionary)).Verifiable();
            context.Setup(x => x.Request).Returns(request.Object).Verifiable();
            controller.ControllerContext.HttpContext = context.Object;



            var res = await controller.Collate();
            Assert.Null(res.Value);
            //
        }

    }
}
