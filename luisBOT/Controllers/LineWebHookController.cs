using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace isRock.Template
{
    public class LineWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/LineBotWebHook")]
        [HttpPost]
        public IActionResult POST()
        {
            var AdminUserId = "U58ea9d974ce2db7a480a8b43f8526862";

            try
            {
                //設定ChannelAccessToken
                this.ChannelAccessToken = "b4pmhV2LAWOxYIASTKIhyte9tZV+Q7/StPErV6dLLCY4ZTRSgqRX7iAxsncpVEstPaHmjwP9H1p7usfEyCf1PLWyTS63qhFVOpzNYsmb+ViIhQlmn28iDtRmdUJnUTlDGJZfENmmnbBHjKFx8GvEDQdB04t89/1O/w1cDnyilFU=";
                //取得Line Event
                var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                var responseMsg = "";
                //準備回覆訊息
                if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
                {
                    var LuisRet = CallLUIS(LineEvent.message.text);
                    // var id = LineEvent.source.userId;
                    // var userInfo = this.GetUserInfo(id);
                    responseMsg = $"判斷目的 : {LuisRet.prediction.topIntent}";
                    // responseMsg = $"{userInfo.displayName} ({id}) 說了:{LineEvent.message.text}";
                }
                else if (LineEvent.type.ToLower() == "message")
                    responseMsg = $"收到 event : {LineEvent.type} type: {LineEvent.message.type} ";
                else
                    responseMsg = $"收到 event : {LineEvent.type} ";
                //回覆訊息
                this.ReplyMessage(LineEvent.replyToken, responseMsg);
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //回覆訊息
                this.PushMessage(AdminUserId, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }


        public dynamic CallLUIS(string message)
        {
            string endpoint = "https://westus.api.cognitive.microsoft.com/luis/prediction/v3.0/apps/fe27cad5-17bd-4bcb-bff4-9946c03c6ac0/slots/production/predict?subscription-key=d48defe10b0945ccb73dcd6f1f061f19&verbose=true&show-all-intents=true&log=true&query=";

            //call http get
            var client = new HttpClient();
            var endpointUri = endpoint + message;
            var response = client.GetAsync(endpointUri).Result;
            //get JSON
            var JSON = response.Content.ReadAsStringAsync().Result;
            //反序列化 JSON to Object
            var Result = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(JSON);
            return Result;
        }
    }
}
