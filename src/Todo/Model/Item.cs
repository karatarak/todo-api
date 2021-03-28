using System;
using Todo.Data;

namespace Todo.Model
{   
    public record Item
    {
        public string item_id { get; init; }
        public string owner_id { get; init; }
        public string board_id { get; init; }
        public string title { get; init; }
        public string description { get; init; }
        public string status { get; init; }
        public DateTime created_date { get; init; }
        public DateTime? due_date { get; init; }

        public static Item FromData(ItemData data)
        {
            return new Item {
                item_id = data.item_id.ToString(),
                owner_id = data.owner_id.ToString(),
                board_id = data.board_id.ToString(),
                title = data.title,
                description = data.description,
                status = data.status,
                created_date = data.created_date,
                due_date = data.due_date,
            };
        }
    }

    public record ItemCollection
    {
        public Item[] items { get; init; }
    };
}