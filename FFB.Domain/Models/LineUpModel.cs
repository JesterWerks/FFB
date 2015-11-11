using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFB.Domain.Models
{
    public class LineUpModel
    {
        public int Id { get; set; }
        public PlayerModel QB { get; set; }
        public PlayerModel RB1 { get; set; }
        public PlayerModel RB2 { get; set; }
        public PlayerModel WR1 { get; set; }
        public PlayerModel WR2 { get; set; }
        public PlayerModel WR3 { get; set; }
        public PlayerModel TE { get; set; }
        public PlayerModel FLEX { get; set; }
        public PlayerModel DST { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal ProjectedFFP { get; set; }
    }
}
