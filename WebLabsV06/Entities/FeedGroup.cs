using System;
using System.Collections.Generic;
using System.Text;

namespace WebLabsV06.DAL.Entities
{
    public class FeedGroup
    {
        public int FeedGroupId { get; set; }
        public string GroupName { get; set; }
        public List<Feed> Feeds { get; set; }
    }
}
