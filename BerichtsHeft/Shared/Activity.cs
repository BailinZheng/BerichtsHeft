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
        public DateTime DateOfReport { get; set; } = DateTime.Now;
        public string AbgabeType { get; set; }
        public string HauptText { get; set; }
        public int DateBlock { get; set; }
        public int DauertMin { get; set; }
        public string WochenTag { get; set; }
        public string Name { get; set; }
        public string Fach { get; set; }
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public string IDs { get; private set; }

    }
}
