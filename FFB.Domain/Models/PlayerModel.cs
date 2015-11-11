using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFB.Domain.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public decimal AvgPointsPerGame { get; set; }
        public string Position { get; set; }
        public string GameInfo { get; set; }
        public string teamAbbrev { get; set; }
    }
}
