using FFB.Domain.Models;
using FFB.Domain.SiteLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FFB.Controllers
{
    public class AdminAPIController : ApiController
    {
        [HttpGet]
        public List<ScheduleTypeModel> GetScheduleTypes()
        {
            var result = AdminLogic.GetScheduleTypes();
            return result;
        }
    }
}
