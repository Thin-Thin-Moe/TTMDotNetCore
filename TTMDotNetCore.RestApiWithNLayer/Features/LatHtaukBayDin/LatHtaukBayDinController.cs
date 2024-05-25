using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TTMDotNetCore.RestApiWithNLayer.Features.LatHtaukBayDin
{
    [Route("api/[controller]")]
    [ApiController]
    public class LatHtaukBayDinController : ControllerBase
    {
        private async Task<LatHtaukBayDin> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("LatHtaukBayDin.json");
            var model = JsonConvert.DeserializeObject<LatHtaukBayDin>(jsonStr);
            return model;
        }

        [HttpGet("questions")]
        public async Task<IActionResult> Questions()
        {
            var model = await GetDataAsync();
            return Ok(model.questions);
        }

        [HttpGet]
        public async Task<IActionResult> NumberList()
        {
            var model = await GetDataAsync();
            return Ok(model.numberList);
        }

        [HttpGet("{questionNo}/{no}")]
        public async Task<IActionResult> Anaswer(int questionNo, int no)
        {
            var model = await GetDataAsync();
            var answer = model.answers.FirstOrDefault(x => x.questionNo == questionNo && x.answerNo == no);
            return Ok(answer);
        }

        static string ToNumber(string num)
        {
            num = num.Replace("၁", "1");
            num = num.Replace("၂", "2");
            num = num.Replace("၃", "3");
            num = num.Replace("၄", "4");
            num = num.Replace("၅", "5");
            num = num.Replace("၆", "6");
            num = num.Replace("၇", "7");
            num = num.Replace("၈", "8");
            num = num.Replace("၉", "9");
            return num;
        }

        public class LatHtaukBayDin
        {
            public Question[] questions { get; set; }
            public Answer[] answers { get; set; }
            public string[] numberList { get; set; }
        }

        public class Question
        {
            public int questionNo { get; set; }
            public string questionName { get; set; }
        }

        public class Answer
        {
            public int questionNo { get; set; }
            public int answerNo { get; set; }
            public string answerResult { get; set; }
        }
    }
}
