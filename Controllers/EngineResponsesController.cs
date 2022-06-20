using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;


using Horizon.XmlRpc.Core;
using WebApplication2.Utils;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/engineStatus")]
    [ApiController]
    public class EngineResponsesController : ControllerBase
    {

        //private readonly EngineContext _context;

        // GET: api/engineStatus/list
        [HttpGet]
        [Route("list")]
        public  async Task<ActionResult<string>> GetEngineStatusList()
        {
            //엔진서버 읽어오기

            string FILEPATH = Path.Combine(Environment.CurrentDirectory, "engineInfo");

            //string result;
            if (!System.IO.File.Exists(FILEPATH))
            {
               
            }

            string[] engineUrls = await System.IO.File.ReadAllLinesAsync(FILEPATH);

            StringBuilder stringBuilder = new StringBuilder();

            //한줄씩 ip와 port 추출하기
            foreach(string url in engineUrls)
            {
                

                string PATTERN = @".+?://(?<address>[0-9.]+):(?<port>[0-9]+)";
                Regex regex = new Regex(PATTERN);
                Match match = regex.Match(url);
                if (match.Success)
                {
                    //추출한 값 저장하기
                    string address = match.Result("${address}");
                    string port = match.Result("${port}");
                    Debug.WriteLine("address==>" + address+",port==>"+port);
                    //xml문서 만들기
                    XmlRpcStruct xmlRpcStuct = new XmlRpcStruct();
                    xmlRpcStuct.Add("text", "apple");

                    //번역서버상태
                    Boolean translateResult=false;
                    try
                    {
                        //서버에 요청하기
                        XmlRpcAPI xmlRpcAPI = new XmlRpcAPI();
                        XmlRpcStruct res = (XmlRpcStruct)xmlRpcAPI.translateValue(xmlRpcStuct, url);
                        string resultWord = (string)res["text"];

                        translateResult = resultWord.Contains("사과");
                    }
                    catch(Exception e) {
                        Debug.WriteLine(e.ToString());
                        translateResult = false;
                    }
                    finally
                    {
                        
                        //예시: engine_status{ip="localhost",port="22"} 1                  

                        string urlResult = String.Format(@"engine_status {{ip=""{0}"",port=""{1}""}}  {2}", address, port, translateResult ? "1" : "0");          
                        stringBuilder.AppendLine(urlResult);
                    }

                    
                }
            }

            


            // return await _context.engineResponses.ToListAsync();



            return stringBuilder.ToString();
        }



        #region 번역서버에서 상태값 가져오기
        private void getEngineStatusLog()
        {

        }
        #endregion
    }
}


