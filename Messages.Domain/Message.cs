using System;
using System.ComponentModel.DataAnnotations;

namespace Messages.Domain
{
    public class Message : MessageType
    {

        [Key]
        public int Id {
            get; set;
        }
        public int SenderId {
            get; set;
        }

        public int ReceiverId {
            get; set;
        }

        public DateTime Created {
            get; set;
        }

        public bool IsRead {
            get; set;
        }

        public double Test1 {
            get; set;
        }

        public void DoSomething()
        {
            int x = 1; if (x == 1) { x = 1; } else { }
        }

        public void DoSomething1()
        {
            int? x = 1;
            try { } catch { }

            if (x == null)
            {
                for (int i = 0 ; i < 10 ; i++) { }
            }

            try { } catch { }
        }







    }
}