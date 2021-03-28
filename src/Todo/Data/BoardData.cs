using System;

namespace Todo.Data
{
    public class BoardData
    {
        public Guid board_id { get; set; }
        public Guid owner_id { get; set; }
        public string title { get; set; }
        public DateTime created_date { get; set; }
    }

    public class BoardItemsData
    {
        public BoardData board { get; set; }
        public ItemData[] items { get; set; }
    }
}
