using System.Collections.Generic;
using HPChallenge.Models;
using System.Web.Http;
using System.Net.Http;

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

        public HttpResponseMessage PostPlayer(string player)
        {
            if (ModelState.IsValid)
            {
                //insert in DB
                HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.Created, player);
                response.Headers.Location = new System.Uri(Url.Link("DefaultApi", new { id = player }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest,ModelState);
            }
        }

        //// POST: api/Game
        public void Post([FromBody]string value)
        {
            if (ModelState.IsValid)
            {
                //insert in DB
                HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.Created, value);
                response.Headers.Location = new System.Uri(Url.Link("DefaultApi", new { id = value }));
            }
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
