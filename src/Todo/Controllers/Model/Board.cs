using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Data;

namespace Todo.Controllers.Model
{ 
    public record Board
    {
        public Guid user_id { get; init; }
        public Guid board_id { get; init; }
        public string title { get; init; }
        public DateTime created_date { get; init; }
        public Column[] columns { get; init; }

        public static Board FromData(BoardData board)
        {
            var columns = new List<Column>();

            foreach (var group in board.items.GroupBy(x => x.status))
            {
                var column = new Column
                {
                    status = group.Key,
                    items = group.Select(Item.FromData).ToArray()
                };

                columns.Add(column);
            }

            return new Board {
                board_id = board.board_id,
                user_id = board.owner_id,
                title = board.title,
                created_date = board.created_date,
                columns = columns.ToArray()
            };
        }
    }

    public record Column
    {
        public string status { get; init; }
        public Item[] items { get; init; }
    }

    public record BoardCollection
    {
        public Board[] boards { get; init; }
    }
}