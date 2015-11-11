using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFB.Domain.Models
{
    public class ScheduleModel
    {
        public int Id { get; set; }
        public int ScheduleTypeId { get; set; }
        public string ScheduleTypeName { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public string Note { get; set; }
    }
}
