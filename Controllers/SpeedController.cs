using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace PalletizerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeedController : ControllerBase
    {
        LocalLiteDB DB = new LocalLiteDB();

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "API Work ^_^";
        }

        [HttpPost("search")]
        public ActionResult<string> SeriesList()
        {
            string result = "";
            List<string> Series = new List<string>();
            Series.Add("Speed");
            result = JsonConvert.SerializeObject(Series);
            return result;
        }

        // [HttpGet("query")]
        [HttpPost("query")]
        public async Task<IActionResult> GetData()
        {
            string _result = "";

            string requestBody = await new StreamReader(Request.Body).ReadToEndAsync();
            GrafanaRequestModel Body = JsonConvert.DeserializeObject<GrafanaRequestModel>(requestBody);
            if(Body != null)
            {
                if(Body.targets[0].type == "table")
                {
                    string series = null;
                    var tables = new object[1];
                    tables[0] = new { columns = new object [1]{ new {text = "Speed"} }, 
                                        rows = new object[1] { new int[1] { 80 } }, 
                                        type = "table",
                                        refId = "A" };
                    var A = new {refId = "A", series, tables};
                    var results = new {A};
                    object[] ITEMS = new object[1];
                    ITEMS[0] = new {results};


                    _result = JsonConvert.SerializeObject(ITEMS);
                }
                else{
                    // Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    // var results = new {target = "Speed", 
                    //                     datapoints = new object[1] { new Int32[2] {80,unixTimestamp}}
                    //                     };
                    var results = new {target = "Speed", 
                                        datapoints = GetSpeedData()
                                        };
                    object[] ITEMS = new object[1];
                    ITEMS[0] = results;

                    _result = JsonConvert.SerializeObject(ITEMS);
                }
            }

            return this.Ok(_result);
        }

        private object[] GetSpeedData(){

            object [] result = null;
            
            DB.AddNewRobotSpeed();
            DB.AddNewRobotSpeed();
            DB.AddNewRobotSpeed();
            DB.AddNewRobotSpeed();
            DB.AddNewRobotSpeed();

            var DATA = DB.GetRobotSpeed();
            result = new object[DATA.Count];
            int index = 0;
            foreach(var item in DATA){
                result[index] = new Int32[2] {item.Speed, item.UnixTimeStamp};
                index++;
            }

            return result;

        }
    }
}
