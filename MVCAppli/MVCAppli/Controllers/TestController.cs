using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Xml;

namespace MVCAppli.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        #region fibonacci
        // GET: api/test/fibonacci/{number}
        [Route("fibonacci/{number}")]
        public HttpResponseMessage GetFibonacci(int number)
        {
            try
            {
                Log.Info(string.Format("Request GET api/test/fibonacci/{0}", number));
                int resultNumber;
                if (number >= 1 && number <= 100)
                {
                    resultNumber = GetFibonacciResult(number);
                }
                else
                {
                    resultNumber = -1;
                }
                Log.Info(string.Format("Response GET api/test/fibonacci/{0} - return {1}", number, resultNumber));
                return Request.CreateResponse(HttpStatusCode.OK, resultNumber);
            }
            catch (Exception ex)
            {
                var message = String.Format("Error Fibonacci input");
                var httpError = new HttpError(message);
                Log.Error(message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, httpError);
            }
        }

        public int GetFibonacciResult(int n)
        {
            int a = 0;
            int b = 1;
            // In N steps compute Fibonacci sequence iteratively.
            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
        #endregion

        #region XmlToJson
        // POST: api/Test/xmltojson
        [Route("xmltojson")]
        public HttpResponseMessage PostXmlToJson([FromBody]string xml)
        {
            try
            {
                Log.Info(string.Format("Request POST api/test/xmltojson - Body: ", xml));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string jsonText = JsonConvert.SerializeXmlNode(doc);

                if (jsonText == null)
                {
                    var message = String.Format("json not found");
                    var httpError = new HttpError(message);
                    Log.Info(string.Format("Response POST api/test/xmltojson - return {0}", message));
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, httpError);
                }
                Log.Info(string.Format("Response POST api/test/xmltojson - return {0}", jsonText));
                return Request.CreateResponse(HttpStatusCode.OK, jsonText);
            }
            catch (Exception ex)
            {
                var message = String.Format("Bad Xml format");
                var httpError = new HttpError(message);
                httpError.ExceptionMessage = message;
                Log.Error(message, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, httpError);
            }
        }
        #endregion
    }
}
