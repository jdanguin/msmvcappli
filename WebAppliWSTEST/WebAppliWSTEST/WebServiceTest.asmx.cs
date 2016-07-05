using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using System.Numerics;

namespace WebAppliWSTEST
{
    /// <summary>
    /// Summary description for WebServiceTest
    /// </summary>
    [WebService(Namespace = "http://localhost/WebAppliWSTEST/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceTest : System.Web.Services.WebService
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Fibonacci
        // Fibonacci
        /// <summary>
        /// Use fibonacci formula with BigInt number
        /// with accept segmentation (of 1 to 100) else return -1
        /// </summary>
        /// <param name="number">input number</param>
        /// <returns></returns>
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string Fibonacci(int number)
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            try
            {
                Log.Info(string.Format("Request Fibonacci - number: {0}", number));
                BigInteger resultNumber;
                if (number >= 1 && number <= 100)
                {
                    resultNumber = GetFibonacciResult(number);
                }
                else
                {
                    resultNumber = -1;
                }
                Log.Info(string.Format("Response Fibonacci - number {0} - return number {1}", number, resultNumber));
                return resultNumber.ToString();
            }
            catch (Exception ex)
            {
                var message = String.Format("Error Fibonacci Method");
                Log.Error(message, ex);
                return string.Empty; ;
            }
        }

        /// <summary>
        /// Fibonacci formula
        /// </summary>
        /// <param name="n">number</param>
        /// <returns>number</returns>
        public BigInteger GetFibonacciResult(int number)
        {
            BigInteger a = 0;
            BigInteger b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (BigInteger i = 0; i < number; i++)
            {
                BigInteger temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
        #endregion

        #region XMLToJson
        //  Xmltojson
        /// <summary>
        /// Method which convert xml content to json
        /// </summary>
        /// <param name="xml">string</param>
        /// <returns>string (json text)</returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string XmlToJson(string xml)
        {
            try
            {
                Log.Info(string.Format("Request Xmltojson - xml: {0}", xml));
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                if (jsonText == null)
                {
                    var message = String.Format("json not found");
                    Log.Info(string.Format("Response Xmltojson - return json: {0}", message));
                    return message;
                }
                Log.Info(string.Format("Response Xmltojson - return json {0}", jsonText));
                return jsonText;
            }
            catch (Exception ex)
            {
                var message = String.Format("Bad Xml format");
                Log.Error(message, ex);
                return message;
            }
        }
        #endregion
    }
}
