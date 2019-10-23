using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.Models
{
    public class UserScore
    {
        Guid Id { get; set; }
        int UserAccount_Id { get; set; }
        int ChallengeCategory_Id { get; set; }
        DateTime StartTime { get; set; }
        DateTime StopTime { get; set; }
        DateTime Date { get; set; }
        int PunchCount { get; set; }
        int Force { get; set; }
        int Speed { get; set; }
    }
}
