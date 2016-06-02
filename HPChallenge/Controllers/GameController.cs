using System.Collections.Generic;
using HPChallenge.Models;
using System.Web.Http;

namespace HPChallenge.Controllers
{
    public class GameController : ApiController
    {
        // GET: api/Game
        public IEnumerable<string> Get()
        {
            return new string[] { "valueAA1", "valueBB2" };
        }

        // GET: api/Game/5
        public List<Player> Get(int id)
        {
            ScoreBoard sb = new ScoreBoard();
            return sb.GetTopPlayers(id);
        }

        // POST: api/Game
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Game/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Game/5
        public void Delete(int id)
        {
        }
    }
}
