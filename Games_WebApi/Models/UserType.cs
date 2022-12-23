using System;

namespace Games_WebApi.Models
{
    public class UserType
    {
        public int ID { get; set; }
        public String TypeName { get; set; }
        public bool Read { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool EditAll { get; set; }
        public bool Delete { get; set; }
        public bool DeleteAll { get; set; }
        public bool RezTable { get; set; }

        public bool EmergTable { get; set; }

        public bool UserTable { get; set; }

    }
}
