using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.Domain.Entities
{
  public  class ChallengeCategory
    {
       public int Id { get; set; }
        public string  Name { get; set; }

        public string ImgSrc { get; set; }
        public bool IsEnable { get; set; }
    }
}
