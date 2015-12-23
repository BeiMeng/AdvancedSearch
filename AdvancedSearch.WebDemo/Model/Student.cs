using System;

namespace AdvancedSearch.WebDemo.Model
{
   public class Student
    {
       public string StuId { get; set; }

       public string StuName { get; set; }

       public int? Nullint { get; set; }

       public DateTime CreateTime { get; set; }

       public DateTime? Birthday { get; set; }

       public string LoveGril { get; set; }

       public virtual StuClass Stuclass { get; set; }

    }
}
