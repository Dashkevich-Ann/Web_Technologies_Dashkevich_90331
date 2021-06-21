using System;
using System.Collections.Generic;
using System.Text;

namespace WebLabsV06.DAL.Entities
{
    public class Feed
    {
        public int FeedId { get; set; }
        public string FeedName { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public string Image { get; set; }

        // Навигационные свойства
        /// <summary>
        /// группа блюд (например, супы, напитки и т.д.)
        /// </summary>
        public int FeedGroupId { get; set; }
        public FeedGroup Group { get; set; }
    }
}
