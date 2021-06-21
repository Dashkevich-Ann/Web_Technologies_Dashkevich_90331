using Dashkevich_90331.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WebLabsV06.DAL.Entities;
using Xunit;

namespace Dashkevich_90331.Tests
{
    public class ListViewModelTests
    {
        [Fact]
        public void ListViewModelCountsPages()
        {
            // Act
            var model = ListViewModel<Feed>.GetModel(TestData.GetFeedsList(), 1, 3);
            // Assert
            Assert.Equal(2, model.TotalPages);
        }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params), MemberType = typeof(TestData))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Feed>.GetModel(TestData.GetFeedsList(), page, 3);
            // Assert
            Assert.Equal(qty, model.Count);
        }
        [Theory]
        [MemberData(memberName: nameof(TestData.Params),
       MemberType = typeof(TestData))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Feed>
           .GetModel(TestData.GetFeedsList(), page, 3);
            // Assert
            Assert.Equal(id, model[0].FeedId);
        }
    }
}
