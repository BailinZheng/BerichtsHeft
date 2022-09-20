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
    public class Activity
    {   

        private static List<Activity> Activities { get; set; } = new List<Activity>();
        public static Activity GetDateiInfo(string ID)
        {
            foreach (Activity dateiInfo in Activities)
            {
                if (dateiInfo.ID == ID)
                {
                    return dateiInfo;
                }
            }
            return null;
        }
        public static List<Activity> SearchDateiInfos(string subjectPattern = null)
        {
            List<Activity> list = new List<Activity>();
            foreach (Activity dateiItem in Activities)
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

        public static bool Delete(Activity id)
        {
            if (Activity.Activities.Remove(id))
            {
                return true;
            };
            return false;
        }

        public static void Add(Activity dateiInfo)
        {
            Activity.Activities.Add(dateiInfo);
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
        public string IDs { get; private set; } 

    }
}
