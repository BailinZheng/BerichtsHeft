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
        private static List<DateiInfo> Activities { get; set; } = new List<DateiInfo>();
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

        /// <summary>
        /// Liefert alle DateiInfo-Objekte, die im Subject das übergebene Pattern haben
        /// 
        ///  - Bsp: Pattern = "test" ==> "Ein toller test" wird gefunden
        /// 
        /// </summary>
        /// <param name="subjectPattern">Suchmuster; wenn null ==> alle DateiInfo-Objekte zurück geben</param>
        /// <returns></returns>
        public static List<DateiInfo> SearchDateiInfos(string subjectPattern = null)
        {

        }

        /// <summary>
        /// Löscht ein DateiInfo und gibt true zurck, wenn erfolgreich
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string id)
        {
        }

        /// <summary>
        /// Fügt ein DateiInfo-Objekt in die Liste ein
        /// </summary>
        /// <param name="dateiInfo"></param>
        public static void Add(DateiInfo dateiInfo)
        {

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
