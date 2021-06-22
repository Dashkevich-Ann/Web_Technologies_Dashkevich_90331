using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLabsV06.DAL.Entities;

namespace Dashkevich_90331.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        /// <summary>
        /// Вес суммарный корзины
        /// </summary>
        public double Weight
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Feed.Weight);
            }
        }

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="feed">добавляемый объект</param>
        public virtual void AddToCart(Feed feed)
        {
            // если объект есть в корзине
            // то увеличить количество
            if (Items.ContainsKey(feed.FeedId))
                Items[feed.FeedId].Quantity++;
            // иначе - добавить объект в корзину
            else
                Items.Add(feed.FeedId, new CartItem
                {
                    Feed = feed,
                    Quantity = 1
                });
        }

        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="id">id удаляемого объекта</param>
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
    /// <summary>
    /// Клас описывает одну позицию в корзине
    /// </summary>
    public class CartItem
    {
        public Feed Feed { get; set; }
        public int Quantity { get; set; }
    }
}

