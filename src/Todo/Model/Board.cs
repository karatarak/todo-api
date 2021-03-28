using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;

namespace Todo.Model
{ 
    public record Board
    {
        public Guid user_id { get; init; }
        public Guid board_id { get; init; }
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
                user_id = userId,
                board_id = boardId,
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