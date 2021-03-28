using System;

namespace Todo.Data
{
    public class ItemData
    {
        public BinaryId item_id { get; set; }
        public BinaryId owner_id { get; set; }
        public BinaryId board_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime created_date { get; set; }
        public DateTime? due_date { get; set; }
    }
}
