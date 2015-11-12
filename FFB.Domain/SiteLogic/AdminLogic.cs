using Data.EntityFramework;
using FFB.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFB.Domain.SiteLogic
{
    public class AdminLogic
    {
        public static List<ScheduleTypeModel> GetScheduleTypes()
        {
            using (FFBcontext context = new FFBcontext())
            {
                return (from st in context.Schedule_Types
                        select new ScheduleTypeModel { 
                            Id = st.Id,
                            Name = st.Name
                        }).ToList();
            }
        }

        public static void SavePlayerList(PlayerListModel list)
        {
            using (FFBcontext context = new FFBcontext())
            {

                PlayerLists PlayerList = new PlayerLists();
                PlayerList.ScheduleTypeId = list.Id;
                PlayerList.Week = list.Week;
                PlayerList.Year = list.Year;
                context.PlayerLists.Add(PlayerList);
                
                StreamReader csvreader = new StreamReader(list.File.InputStream);
                
                while (!csvreader.EndOfStream)
                {
                    if (!string.IsNullOrEmpty(csvreader.ReadLine()))
                    {
                        var result = csvreader.ReadLine().Split(',');
                        Players player = new Players();
                        player.Position = result[0].Replace(@"""", String.Empty);
                        player.Name = result[1].Replace(@"""", String.Empty);
                        player.Salary = Convert.ToInt32(result[2]);
                        player.GameInfo = result[3].Replace(@"""", String.Empty);
                        player.AvgPointsPerGame = Convert.ToDecimal(result[4]);
                        player.teamAbbrev = result[5].Replace(@"""", String.Empty);
                        player.PlayerListId = PlayerList.ScheduleTypeId;
                        context.Players.Add(player);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
