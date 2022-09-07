using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BerichtsHeft.Shared
{
    public class DateiInfo
    {
        private static List<DateiInfo> Activities { get; set; } = new List<DateiInfo>();
        public static DateiInfo GetDateiInfo(string ID)
        {
            foreach (DateiInfo dateiInfo in Activities)
            {
                if (dateiInfo.ID == ID)
                {
                    return dateiInfo;
                }
            }
            return null;
        }
        public static List<DateiInfo> SearchDateiInfos(string subjectPattern = null)
        {
            List<DateiInfo> list = new List<DateiInfo>();
            foreach (DateiInfo dateiItem in Activities)
            {
                if (subjectPattern == null)
                {
                    list.Add(dateiItem);
                }
                else
                {
                    if (dateiItem.Subject.Contains(subjectPattern, StringComparison.OrdinalIgnoreCase))
                    {
                        list.Add(dateiItem);
                    }
                }
            }
            return list;
        }

        public static bool Delete(DateiInfo id)
        {
            if (DateiInfo.Activities.Remove(id))
            {
                return true;
            };
            return false;
        }

        public static void Add(DateiInfo dateiInfo)
        {
            DateiInfo.Activities.Add(dateiInfo);
        }

        public DateTime DateOfReport { get; set; } = DateTime.Now;
        public string AbgabeType { get; set; }
        public string HauptText { get; set; }
        public int DateBlock { get; set; }
        public int Dauertmin { get; set; }
        public string WochenTag { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string ID { get; private set; } = Guid.NewGuid().ToString();

    }
}
