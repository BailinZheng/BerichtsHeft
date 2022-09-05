using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerichtsHeft.Shared
{
    public class DateiInfo
    {   
        public static List<DateiInfo> Activities { get; private set; } = new List<DateiInfo>();
        public DateTime DateOfReport { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Only confirmed users can play!")]
        public string AbgabeType { get; set; }
        public string HauptText { get; set; }
        public int DateBlock { get; set; }
        public int Dauertmin { get; set; }
        public string WochenTag { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }

    }
}
