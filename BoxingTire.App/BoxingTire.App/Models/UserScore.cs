using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.Models
{
    public class UserScore
    {
        public Guid Id { get; set; }
        public Guid UserAccount_Id { get; set; }
        public int ChallengeCategory_Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public DateTime Date { get; set; }
        public int PunchCount { get; set; }
        public int Force { get; set; }
        public int Speed { get; set; }
    }
}
