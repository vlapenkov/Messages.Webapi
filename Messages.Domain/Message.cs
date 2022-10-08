using System;
using System.ComponentModel.DataAnnotations;

namespace Messages.Domain
{
    public class Message : MessageType
    {

        [Key]
        public int Id
        {
            get; set;
        }
        public int SenderId
        {
            get; set;
        }

        public int ReceiverId
        {
            get; set;
        }

        public DateTime Created
        {
            get; set;
        }

        public bool IsRead
        {
            get; set;
        }


    }
}