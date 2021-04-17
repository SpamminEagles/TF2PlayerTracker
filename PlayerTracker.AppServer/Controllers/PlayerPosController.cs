using Microsoft.AspNetCore.Mvc;
using PlayerTracker.AppServer.Model;
using PlayerTracker.AppServer.Services;

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
        public ActionResult Post([FromBody] PlayerPosModel value)
        {
            if (value != null && value.Validate())
            {
                var collection = dbContext.PluginDb.GetCollection<PlayerPosModel>(PlayerPosModel.COLLECTION);
                _ = collection.InsertOneAsync(value);

                return Ok();
            }
            else
            {
                // Unprocessable Entity
                return StatusCode(422);
            }
        }

    }
}
