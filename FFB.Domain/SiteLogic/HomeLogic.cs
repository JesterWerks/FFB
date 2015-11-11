using Data.EntityFramework;
using FFB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFB.Domain.SiteLogic
{
    public class HomeLogic
    {
        public static List<ScheduleModel> GetSchedules()
        {
            using (FFBcontext context = new FFBcontext())
            {
                return (from s in context.Schedules
                        join st in context.Schedule_Types on s.Schedule_Type_Id equals st.Id
                        select new ScheduleModel { 
                            Id = s.Id,
                            Note = s.Note,
                            ScheduleTypeId = s.Schedule_Type_Id,
                            ScheduleTypeName = st.Name,
                            Week = s.Week,
                            Year = s.Year
                        }).ToList();
            }
        }
        
        public static List<PlayerModel> GetAllPlayers()
        {
            using (FFBcontext context = new FFBcontext())
            {
                return (from p in context.Players
                        select new PlayerModel { 
                            Id = p.Id,
                            AvgPointsPerGame = p.AvgPointsPerGame,
                            GameInfo = p.GameInfo,
                            Name = p.Name,
                            Position = p.Position,
                            Salary = p.Salary,
                            teamAbbrev = p.teamAbbrev
                        }).ToList();
            }
        }

        public static List<LineUpModel> GenerateLineUps(List<PlayerModel> playerList)
        {
            List<LineUpModel> lineUps = new List<LineUpModel>();

            List<PlayerModel> QBList = new List<PlayerModel>();
            List<PlayerModel> RBList = new List<PlayerModel>();
            List<PlayerModel> WRList = new List<PlayerModel>();
            List<PlayerModel> TEList = new List<PlayerModel>();
            List<PlayerModel> FLEXList = new List<PlayerModel>();
            List<PlayerModel> DSTList = new List<PlayerModel>();

            string qb = "QB";
            string rb = "RB";
            string wr = "WR";
            string te = "TE";
            string dst = "DST";

            foreach (PlayerModel pm in playerList){
                
                if (pm.Position.CompareTo(qb) == 0) { QBList.Add(pm); }
                if (pm.Position.CompareTo(rb) == 0) { RBList.Add(pm); FLEXList.Add(pm); }
                if (pm.Position.CompareTo(wr) == 0) { WRList.Add(pm); FLEXList.Add(pm); }
                if (pm.Position.CompareTo(te) == 0) { TEList.Add(pm); FLEXList.Add(pm); }
                if (pm.Position.CompareTo(dst) == 0) { DSTList.Add(pm); }
            }

            foreach (PlayerModel QB in QBList)
            {
                List<PlayerModel> tempFlexList = new List<PlayerModel>();
                tempFlexList.AddRange(FLEXList);

                foreach (PlayerModel RB1 in RBList)
                {
                    tempFlexList.RemoveAll(x => x.Id == RB1.Id);

                    foreach (PlayerModel RB2 in RBList)
                    {
                        if (RB2.Id == RB1.Id) { break; }
                        tempFlexList.RemoveAll(x => x.Id == RB2.Id);

                        foreach (PlayerModel WR1 in WRList)
                        {
                            tempFlexList.RemoveAll(x => x.Id == WR1.Id);

                            foreach (PlayerModel WR2 in WRList)
                            {
                                if (WR2.Id == WR1.Id) { break; }
                                tempFlexList.RemoveAll(x => x.Id == WR2.Id);

                                foreach (PlayerModel WR3 in WRList)
                                {
                                    if (WR3.Id == WR1.Id || WR3.Id == WR2.Id) { break; }
                                    tempFlexList.RemoveAll(x => x.Id == WR3.Id);

                                    foreach (PlayerModel TE in TEList)
                                    {
                                        tempFlexList.RemoveAll(x => x.Id == TE.Id);

                                        foreach (PlayerModel DST in DSTList)
                                        {

                                            foreach (PlayerModel FLEX in tempFlexList)
                                            {
                
                                                LineUpModel lu = new LineUpModel();
                                                lu.QB = QB;
                                                lu.RB1 = RB1;
                                                lu.RB2 = RB2;
                                                lu.WR1 = WR1;
                                                lu.WR2 = WR2;
                                                lu.WR3 = WR3;
                                                lu.TE = TE;
                                                lu.FLEX = FLEX;
                                                lu.DST = DST;
                                                lu.TotalSalary = QB.Salary + RB1.Salary + RB2.Salary + WR1.Salary + WR2.Salary + WR3.Salary + TE.Salary + FLEX.Salary + DST.Salary;
                                                if (lu.TotalSalary <= 50000)
                                                {
                                                    lineUps.Add(lu);
                                                }

                                            } // end FLEX

                                        } // end DST

                                    } // end TE

                                } // end WR3

                            } // end WR2

                        } // end WR1

                    } // end RB2

                } // end RB1                                

            } // end QB


            return lineUps;
        }
    }
}
