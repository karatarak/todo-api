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
        public BoardColumn[] columns { get; init; }

        public static Board FromData(BoardItemsData boardItems)
        {
            var columns = new List<BoardColumn>();

            foreach (var group in boardItems.items.GroupBy(x => x.status))
            {
                var column = new BoardColumn
                {
                    status = group.Key,
                    items = group.Select(Item.FromData).ToArray()
                };

                columns.Add(column);
            }

            return new Board {
                board_id = boardItems.board.board_id,
                user_id = boardItems.board.owner_id,
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