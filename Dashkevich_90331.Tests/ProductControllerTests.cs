using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Dashkevich_90331.Controllers;
using WebLabsV06.DAL.Entities;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebLabsV06.DAL.Data;

namespace Dashkevich_90331.Tests
{
    public class ProductControllerTests
    {
        //private ControllerContext controllerContext = new ControllerContext();
        //private Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();

        //public ProductControllerTests()
        //{
        //    mockHttpContext.Setup(c => c.Request.Headers)
        //        .Returns(new HeaderDictionary());
        //    controllerContext.HttpContext = mockHttpContext.Object;
        //}

        DbContextOptions<ApplicationDbContext> _options;
        public ProductControllerTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testDb")
            .Options;
        }


        //[Theory]
        //[MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        //public void ControllerGetsProperPage(int page, int qty, int id)
        //{
        //    // Arrange 
        //    var controller = GetProductController();
        //    controller._feeds = TestData.GetFeedsList();
        //    // Act
        //    var result = controller.Index(pageNo: page, group: null) as ViewResult;
        //    var model = result?.Model as List<Feed>;
        //    // Assert
        //    Assert.NotNull(model);
        //    Assert.Equal(qty, model.Count);
        //    Assert.Equal(id, model[0].FeedId);
        //}

        [Theory]
        [MemberData(nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange 
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());

            controllerContext.HttpContext = moqHttpContext.Object; 
            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                // создать объект класса контроллера
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };
                // Act
                var result = controller.Index(group: null, pageNo:page) as ViewResult;
                var model = result?.Model as List<Feed>;
                // Assert
                Assert.NotNull(model);
                Assert.Equal(qty, model.Count);
                Assert.Equal(id, model[0].FeedId);
            }
            // удалить базу данных из памяти
            using (var context = new ApplicationDbContext(_options))
            {
                context.Database.EnsureDeleted();
            }
        }


        //[Fact]
        //public void ControllerSelectsGroup()
        //{
        //    // arrange
        //    var controller = GetProductController();
        //    var data = TestData.GetFeedsList();
        //    controller._feeds = data;
        //    var comparer = Comparer<Feed>
        //    .GetComparer((d1, d2) => d1.FeedId.Equals(d2.FeedId) && d1.FeedGroupId.Equals(d2.FeedGroupId));
        //    // act
        //    var result = controller.Index(2) as ViewResult; var model = result.Model as List<Feed>;
        //    // assert
        //    Assert.Equal(1, model.Count);
        //    Assert.Equal(data[4], model[0], comparer);
        //}

        //private ProductController GetProductController()
        //{
        //    return new ProductController()
        //    {
        //        ControllerContext = controllerContext
        //    };
        //}

        [Fact]
        public void ControllerSelectsGroup()
        {
            // arrange
            // Контекст контроллера
            var controllerContext = new ControllerContext();
            // Макет HttpContext
            var moqHttpContext = new Mock<HttpContext>();
            moqHttpContext.Setup(c => c.Request.Headers)
            .Returns(new HeaderDictionary());
            controllerContext.HttpContext = moqHttpContext.Object;
            //заполнить DB данными
            using (var context = new ApplicationDbContext(_options))
            {
                TestData.FillContext(context);
            }
            using (var context = new ApplicationDbContext(_options))
            {
                var controller = new ProductController(context)
                { ControllerContext = controllerContext };

                var comparer = Comparer<Feed>.GetComparer((d1, d2) =>
               d1.FeedId.Equals(d2.FeedId));
                // act
                var result = controller.Index(2) as ViewResult;
                var model = result.Model as List<Feed>;
                // assert
                Assert.Equal(2, model.Count);
                Assert.Equal(context.Feeds
                .ToArrayAsync()
               .GetAwaiter()
               .GetResult()[2], model[0], comparer);
            }
        }
    }  
}
