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
        public string ID { get; private set; } = Guid.NewGuid().ToString();
        public static List<DateiInfo> Activities { get; private set; } = new List<DateiInfo>();
        public static DateiInfo GetDateiInfo(string ID)
        {
            foreach(DateiInfo dateiinfo in Activities)
            {
                if (dateiinfo.ID == ID)
                {
                    return dateiinfo;
                }
            }
            return null;
        }
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
