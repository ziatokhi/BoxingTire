using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.Domain.Entities
{
  public  class UserAccount
    {
      public  int Id { get; set; }
        public string Name  { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool IsEnable { get; set; }
        public int Role { get; set; }
        public DateTime UpdatedOn{ get; set; }

        // one time login for user
        public bool IsLogin { get; set; }

    }
}
