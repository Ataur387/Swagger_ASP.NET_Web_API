using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordsController : Controller
    {
        public static int LineBreak;
        private static List<String> Words = new List<String>{};
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            String Output = "", Answer = "", End = "";
            for (int i = 0; i < Words.Count; i++)
            {
                Output += Words[i];
            }
            for (int i = 0; i < Output.Length; i++)
            {
                if (i % LineBreak == 0 && i != 0)
                {
                    End = "";
                    string Temp = Output.Substring(i - LineBreak, LineBreak);
                    Temp += "\r\n";
                    Answer += Temp;
                }
                End += Output[i];
            }
            if (End.Length > 0) Answer += End;
            return Ok(Answer);
        }

        [HttpPost]
        public async Task<ActionResult<List<WordListModel>>> Add([FromHeader(Name = "page-size")] int PageSize, WordListModel wordList)
        {
            foreach (var i in wordList.Words)
            {
                Words.Add(i);
            }
            LineBreak = PageSize;
            return Created("~api/MyApi", wordList);
        }

    }
}
