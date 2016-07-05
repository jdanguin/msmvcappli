using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Numerics;
using System.Reflection;
using System.Web.Http;
using System.Xml;

namespace MVCAppli.Controllers
{
    /// <summary>
    /// Methods WS API regrouped in test controller
    /// </summary>
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region Fibonacci
        // GET: api/test/fibonacci/{number}
        /// <summary>
        /// Use fibonacci formula 
        /// with accept segmentation (1 to 100) else return -1
        /// </summary>
        /// <param name="number">input number</param>
        /// <returns></returns>
        [Route("fibonacci/{number}")]
        public HttpResponseMessage GetFibonacci(int number)
        {
            try
            {
                Log.Info(string.Format("Request GET api/test/fibonacci/{0}", number));
                BigInteger resultNumber;
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
            for (int i = 0; i < number; i++)
            {
                BigInteger temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
        #endregion

        #region XmlToJson
        // POST: api/Test/xmltojson
        /// <summary>
        /// Method which convert xml content to json
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
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
