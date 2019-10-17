using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.Domain.Entities
{
  public  class UserAccount
    {
        Guid Id { get; set; }
        string Name  { get; set; }
        string EmailAddress { get; set; }
        string Password { get; set; }
        bool IsEnable { get; set; }
      int Role { get; set; }
        DateTime UpdatedOn{ get; set; }

    }
}
