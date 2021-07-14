using System.Collections.Generic;

namespace DyShop.Data.Entities.Enums
{
    public enum OrderStatus
    {
        Created,
        Accepted,
        Complete,
        Canceled,
    }

    public static class OrderStatusList
    {
        public static readonly Dictionary<OrderStatus, string> Statuses = new Dictionary<OrderStatus, string>()
        {
            {OrderStatus.Created, "Vytvořena"},
            {OrderStatus.Accepted, "Přijata"},
            {OrderStatus.Complete, "Dokončena"},
            {OrderStatus.Canceled, "Stornována"},
        };

        public static string StatusTitle(this OrderStatus status)
        {
            return Statuses[status];
        }
    }
}