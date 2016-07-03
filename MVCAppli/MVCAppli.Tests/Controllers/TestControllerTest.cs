using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json.Linq;
using MVCAppli.Controllers;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web;
using System.IO;

namespace MVCAppli.Tests.Controllers
{
    /// <summary>
    /// Test Web API TestController 
    /// </summary>
    [TestClass]
    public class TestControllerTest
    {
        #region configuration
        private const string WebApiUrl = "http://localhost/MVCAppli/api/test/";
        private const string xml = "<TRANS><HPAY><lD>'103</lD><STATUS>3</STATUS><EXTRA><lS3DS>0</lS3DS><AUTH>03'1183</AUTH></EXTRA><lNT_MSG/><MLABEL>501767XXXXXX6700</MLABEL><MTOKEN>projectOl</MTOKEN></HPAY></TRANS>";
        private const string badxmlformat = "<foo>hello</bar>";

        /// <summary>
        /// Set Up Context Controller 
        /// </summary>
        /// <param name="controller">TestController</param>
        /// <param name="httpMethod">method Get or Post</param>
        /// <param name="controllerMethod">method in TestController</param>
        /// <returns></returns>
        private TestController SetUpController(TestController controller, string httpMethod, string controllerMethod)
        {
            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri(WebApiUrl + controllerMethod),
                Method = new HttpMethod(httpMethod)
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "test", controllerMethod } });

            return controller;
        }
        #endregion

        #region Test XmlToJson
        /// <summary>
        /// Test Web Service Rest XmlToJson Api Ok
        /// </summary>
        [TestMethod]
        public void XmlToJsonTestServiceOkPost()
        {
            // Arrange
            var url = WebApiUrl + "xmltojson";
            string response = null;

            var URI = new Uri(url);
            //serialize json object to string
            string jsonText = JsonConvert.SerializeObject(xml);

            // Act
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                response = client.UploadString(new Uri(url), "POST", jsonText);
            }

            var json = JsonConvert.DeserializeObject(response);

            // Assert
            Assert.IsNotNull(json);
        }

        /// <summary>
        /// Test Controller method XmlToJson Ok
        /// </summary>
        [TestMethod]
        public void XmlToJsonTestControllerOkPost()
        {
            // Arrange
            TestController controller = new TestController();
            controller = SetUpController(controller, "POST", "xmltojson");

            // Act
            var response = controller.PostXmlToJson(xml);
            var json = JsonConvert.DeserializeObject(((ObjectContent)response.Content).Value as string);

            // Assert
            Assert.IsNotNull(json);
        }

        /// <summary>
        /// Test Web Service Rest Api XmlToJson: case BadXmlFormat
        /// </summary>
        [TestMethod]
        public void XmlToJsonTestServiceBadXmlFormatPost()
        {
            // Arrange
            var url = WebApiUrl + "xmltojson";
            string response = null;
            //serialize json object to string
            string jsonText = JsonConvert.SerializeObject(badxmlformat);

            // Act
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    response = client.UploadString(new Uri(url), "POST", jsonText);
                }
                catch (WebException exception)
                {
                    string responseText = null;

                    if (exception.Response != null)
                    {
                        var responseStream = exception.Response.GetResponseStream();

                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseText = reader.ReadToEnd();
                                response = responseText;
                            }
                        }
                    }
                }
            }
            dynamic json = JObject.Parse(response);
            // Assert
            Assert.AreEqual("Bad Xml format", json.Message.Value);
        }

        /// <summary>
        /// Test Controller method XmlToJson: case BadXmlFormat
        /// </summary>
        [TestMethod]
        public void XmlToJsonTestControllerBadXmlFormatPost()
        {
            // Arrange
            TestController controller = new TestController();
            controller = SetUpController(controller, "POST", "xmltojson");

            // Act
            var response = controller.PostXmlToJson(badxmlformat);
            var error = (HttpError)(((ObjectContent)response.Content).Value);

            // Assert
            Assert.AreEqual("Bad Xml format", error.Message);
        }
        #endregion

        #region Test Fibonacci
        /// <summary>
        /// Test Web Service Rest Api Fibonacci: case 1 Ok
        /// </summary>
        [TestMethod]
        public void FibonacciTestServiceCase1OkGet()
        {
            // Arrange
            var url = WebApiUrl + "fibonacci";
            string response = null;

            // Act
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                response = client.DownloadString(new Uri(url + "/2"));
            }

            var resultNumber = int.Parse(response);

            // Assert
            Assert.AreEqual(1, resultNumber);
        }

        /// <summary>
        /// Test Controller method Fibonacci: case 1 Ok
        /// </summary>
        [TestMethod]
        public void FibonacciTestControllerCasOk1Get()
        {
            // Arrange
            TestController controller = new TestController();
            controller = SetUpController(controller, "GET", "fibonacci/2");

            // Act
            var response = controller.GetFibonacci(2);
            var resultNumber = (int)((ObjectContent)response.Content).Value;

            // Assert
            Assert.IsTrue(((ObjectContent)response.Content).Value is int);
            Assert.AreEqual(1, resultNumber);
        }

        /// <summary>
        /// Test Web Service Rest Api Fibonacci: case 2 Ok
        /// </summary>
        [TestMethod]
        public void FibonacciTestServiceCase2OkGet()
        {
            // Arrange
            var url = WebApiUrl + "fibonacci";
            string response = null;

            // Act
            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                response = client.DownloadString(new Uri(url + "/1000"));
            }

            var resultNumber = int.Parse(response);

            // Assert
            Assert.AreEqual(-1, resultNumber);
        }

        /// <summary>
        /// Test Controller Fibonacci: case 2 Ok
        /// </summary>
        [TestMethod]
        public void FibonacciTestControllerCaseOk2Get()
        {
            // Arrange
            TestController controller = new TestController();
            controller = SetUpController(controller, "GET", "fibonacci/1000");

            // Act
            var response = controller.GetFibonacci(1000);
            var resultNumber = (int)((ObjectContent)response.Content).Value;

            // Assert
            Assert.AreEqual(-1, resultNumber);
        }
        #endregion
    }
}
