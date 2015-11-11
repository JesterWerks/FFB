using FFB.Domain.SiteLogic;
using FFB.Domain.Models;
using FFB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FFB.Controllers
{
    public class HomeAPIController : ApiController
    {
        [HttpGet]
        public List<ScheduleModel> GetSchedules()
        {
            var result = HomeLogic.GetSchedules();
            return result;
        }
        
        [HttpGet]
        public List<PlayerModel> GetAllPlayers()
        {
            var result = HomeLogic.GetAllPlayers();
            return result;
        }

        public List<LineUpModel> GenerateLineUps(List<PlayerModel> playerList)
        {
            var result = HomeLogic.GenerateLineUps(playerList);
            return result;
        }
    }
}
