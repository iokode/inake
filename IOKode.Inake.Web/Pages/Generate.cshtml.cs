using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IOKode.Inake.Web.Pages
{
    public class GeneratePage : PageModel
    {
        private readonly WordGenerator _Generator;

        [FromQuery]
        public int Words { get; set; } = 1;

        [FromQuery]
        public int Letters { get; set; } = 8;
        
        public IEnumerable<string> GeneratedWords { get; private set; }

        public GeneratePage(WordGenerator generator)
        {
            _Generator = generator;
        }
        
        public void OnGet()
        {
            GeneratedWords = new List<string>();
            for (int i = 0; i < Words; i++)
            {
                var generatedWordsAsList = (List<string>)GeneratedWords;
                string word = _Generator.GenerateWord(Letters);
                generatedWordsAsList.Add(word);
            }
        }

        public IActionResult OnPost()
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }
    }
}