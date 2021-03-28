using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;

namespace Todo.Model
{ 
    public record Board
    {
        public string user_id { get; init; }
        public string board_id { get; init; }
        public BoardColumn[] columns { get; init; }

        public static Board FromData(Guid userId, Guid boardId, IEnumerable<ItemData> items)
        {
            var columns = new List<BoardColumn>();

            foreach (var group in items.GroupBy(x => x.status))
            {
                var column = new BoardColumn
                {
                    status = group.Key,
                    items = group.Select(Item.FromData).ToArray()
                };

                columns.Add(column);
            }

            return new Board {
                user_id = userId.ToString("N"),
                board_id = boardId.ToString("N"),
                columns = columns.ToArray()
            };
        }
    }

    public record BoardColumn
    {
        public string status { get; init; }
        public Item[] items { get; init; }
    }
}