using System;
using System.Collections.Generic;
using System.Text;
using WebLabsV06.DAL.Data;
using WebLabsV06.DAL.Entities;

namespace Dashkevich_90331.Tests
{
    public class TestData
    {
        public static List<Feed> GetFeedsList()
        {
            return new List<Feed>
             {
                 new Feed{ FeedId=1, FeedGroupId=1},
                 new Feed{ FeedId=2, FeedGroupId=1},
                 new Feed{ FeedId=3, FeedGroupId=3},
                 new Feed{ FeedId=4, FeedGroupId=6},
                 new Feed{ FeedId=5, FeedGroupId=2}
             };
        }
        public static IEnumerable<object[]> Params()
        {
            // 1-я страница, кол. объектов 3, id первого объекта 1
            yield return new object[] { 1, 3, 1 };
            // 2-я страница, кол. объектов 2, id первого объекта 4
            yield return new object[] { 2, 2, 4 };
        }

        public static void FillContext(ApplicationDbContext context)
        {
            context.FeedGroups.Add(new FeedGroup { GroupName = "fake group" });
            context.AddRange(new List<Feed>
        {
            new Feed{ FeedId=1, FeedGroupId=1},
            new Feed{ FeedId=2, FeedGroupId=1},
            new Feed{ FeedId=3, FeedGroupId=3},
            new Feed{ FeedId=4, FeedGroupId=6},
            new Feed{ FeedId=5, FeedGroupId=2}
        });
            context.SaveChanges();
        }
    }
}
