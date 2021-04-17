using Microsoft.AspNetCore.Mvc;
using PlayerTracker.AppServer.Model;
using PlayerTracker.AppServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlayerTracker.AppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerPosController : ControllerBase
    {

        private MongoDbContext dbContext;

        public PlayerPosController(MongoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // POST api/<PlayerPosController>
        [HttpPost]
        public void Post([FromBody] PlayerPosModel value)
        {
            var collection = dbContext.PluginDb.GetCollection<PlayerPosModel>(PlayerPosModel.COLLECTION);
            collection.InsertOne(value);
        }

    }
}
