using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MuzGenreBot.Models;
using Telegram.Bot.Types;

namespace MuzGenreBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuzGenreBotController : ControllerBase
    {
        private static List<InfoList> UserInfoList = new List<InfoList>
        {
            new InfoList
            {
                ID = 1,
                Favourites = "Blues"
                
            },
            new InfoList
            {
                ID = 2,
                Favourites = "Jazz"
                
            },


        };



        [HttpGet]
        public IEnumerable<InfoList> Get() // до публ api
        {
            return UserInfoList;
        }

        [HttpGet("f&{genre}")]
        public string GetG(string genre)
        {
            return PublicApi.Program1.Wiki(genre);
        }

        [HttpGet("randomgenre")]
        public string GetRG()
        {
            return PublicApi.Program.RandomGenre();
        }

        [HttpGet("randomstory")]
        public string GetRS()
        {
            return PublicApi.Program.RandomStory();
        }

        [HttpGet("sp&{find}")]
        public string GetSP(string find)
        {
            return PublicApi.Program.SpotifyApi(find);
        }

        [HttpGet("{id}")]
        public InfoList Get(int id)
        {
            return UserInfoList.FirstOrDefault(gi => gi.ID == id);
        }




        //[HttpPost]
        //public ActionResult Post([FromBody] InfoList UserInfo)
        //{
        //    if (UserInfo.Any(g => g.ID == genreInfo.ID))
        //    {
        //        return this.StatusCode((int)HttpStatusCode.Conflict);
        //    }

        //    UserInfo.Add(UserInfo);
        //    return this.Ok();

        //}

        //[HttpPut("{id}")]
        //public ActionResult Put(int id, [FromBody]GenreInfo genreInfo)
        //{
        //    var index = GenreInfoList.FindIndex(w => w.ID == genreInfo.ID);
        //    if (index < 0)
        //    {
        //        return this.StatusCode((int)HttpStatusCode.NotFound);
        //    }

        //    GenreInfoList.RemoveAt(index);
        //    GenreInfoList.Add(genreInfo);

        //    return this.Ok();
        //}

        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    var index = GenreInfoList.FindIndex(w => w.ID == id);
        //    if (index < 0)
        //    {
        //        return this.StatusCode((int)HttpStatusCode.NotFound);
        //    }

        //    GenreInfoList.RemoveAt(index);

        //    return this.Ok();
        //}

        //[HttpPatch]
        //public ActionResult Patch(int id, [FromBody]GenreInfo genreInfo)
        //{
        //    var index = GenreInfoList.FindIndex(w => w.ID == id);
        //    if (index < 0)
        //    {
        //        return this.StatusCode((int)HttpStatusCode.NotFound);
        //    }

        //    if (genreInfo.GenreName != null)
        //    {
        //        GenreInfoList.ElementAt(index).GenreName = genreInfo.GenreName;
        //    }

        //    return this.Ok();
        //}
    }
    [Route("api/message/update")]
    public class MessageController : Controller
    {
        // GET api/values
        

        // POST api/values
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Update update)
        {
            if (update == null) return Ok();

            var commands = Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient);
                    break;
                }
            }
            return Ok();
        }
    }
}
