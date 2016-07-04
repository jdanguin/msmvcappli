using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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
            long resultNumber = service.Fibonacci(0);

            //Assert
            Assert.AreEqual(-1, resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber1()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            long resultNumber = service.Fibonacci(1);

            //Assert
            Assert.AreEqual(1, resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber2()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            long resultNumber = service.Fibonacci(2);

            //Assert
            Assert.AreEqual(1, resultNumber);
        }

        [TestMethod]
        public void TestWSFibonacciNumber75()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            long resultNumber = service.Fibonacci(75);

            //Assert
            Assert.AreEqual(2111485077978050, resultNumber);

        }

        [TestMethod]
        public void TestWSFibonacciNumber100()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            long resultNumber = service.Fibonacci(100);

            //Assert
            Assert.AreEqual(3736710778780434371, resultNumber);

        }

        [TestMethod]
        public void TestWSFibonacciNumber1000()
        {
            // Arrange
            var service = new WebAppliWSTESTReference.WebServiceTestSoapClient();

            //Act
            long resultNumber = service.Fibonacci(1000);

            //Assert
            Assert.AreEqual(-1, resultNumber);
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
