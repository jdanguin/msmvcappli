using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using WebAppliWSTEST.Tests.WebAppliWSTESTReference;
using System.Numerics;

namespace WebAppliWSTEST.Tests
{
    [TestClass]
    public class WsTestUnitTest
    {
        private const string xml = "<TRANS><HPAY><lD>'103</lD><STATUS>3</STATUS><EXTRA><lS3DS>0</lS3DS><AUTH>03'1183</AUTH></EXTRA><lNT_MSG/><MLABEL>501767XXXXXX6700</MLABEL><MTOKEN>projectOl</MTOKEN></HPAY></TRANS>";
        private const string badxmlformat = "<foo>hello</bar>";

        #region Fibonacci Test
        [TestMethod]
        public void TestWSFibonacciNumber0()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            var response = service.Fibonacci(0);
            var resultNumber = BigInteger.Parse(response.ToString());
            //Assert
            Assert.AreEqual(BigInteger.Parse("-1"), resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber1()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            var response = service.Fibonacci(1);
            var resultNumber = BigInteger.Parse(response.ToString());
            //Assert
            Assert.AreEqual(BigInteger.Parse("1"), resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber75()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();
            //Act
            var response = service.Fibonacci(75);
            var resultNumber = BigInteger.Parse(response.ToString());
            //Assert
            Assert.AreEqual(BigInteger.Parse("2111485077978050"), resultNumber);

        }

        [TestMethod]
        public void TestWSFibonacciNumber100()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();
            //Act
            var response = service.Fibonacci(100);
            var resultNumber = BigInteger.Parse(response.ToString());
            //Assert
            Assert.AreEqual(BigInteger.Parse("354224848179261915075"), resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber1000()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            var response = service.Fibonacci(1000);
            var resultNumber = BigInteger.Parse(response.ToString());
            //Assert
            Assert.AreEqual(BigInteger.Parse("-1"), resultNumber);
        }
        #endregion

        #region XmlToJson Test
        [TestMethod]
        public void TestWSXmltojsonOk()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            string responseJson = service.XmlToJson(xml);
            var json = JsonConvert.DeserializeObject(responseJson);

            //Assert
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void TestWSXmltojsonBadXmlFormat()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            string responseJson = service.XmlToJson(badxmlformat);

            //Assert
            Assert.AreEqual("Bad Xml format", responseJson);
        }

        #endregion
    }
}
