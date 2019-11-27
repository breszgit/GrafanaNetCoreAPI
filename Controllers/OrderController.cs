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
    public class OrderController : ControllerBase
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
            Series.Add("ALL");
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
                    var results = new {target = "MaterailNo", 
                                        datapoints = GetOrderData()
                                        };
                    object[] ITEMS = new object[1];
                    ITEMS[0] = results;

                    _result = JsonConvert.SerializeObject(ITEMS);
                }
            }

            return this.Ok(_result);
        }

        [HttpPost("AddOrder")]
        public ActionResult<string> AddNewOrder(string MaterialNo)
        {
            DB.AddNewRobotOrder(MaterialNo);
            return "Mat:"+MaterialNo;
        }

        private object[] GetOrderData(){

            object [] result = null;
            //DB.AddNewRobotOrder("TEST");

            var DATA = DB.GetRobotOrder();
            result = new object[DATA.Count];
            int index = 0;
            foreach(var item in DATA){
                result[index] = new object[2] {item.MaterialNo, item.UnixTimeStamp};
                index++;
            }

            return result;

        }
    }
}
