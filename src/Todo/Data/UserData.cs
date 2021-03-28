using System;

namespace Todo.Data
{
    public class UserData
    {
        public Guid user_id { get; set; }
        public Guid user_name { get; set; }
        public DateTime created_date { get; set; }
    }
}
