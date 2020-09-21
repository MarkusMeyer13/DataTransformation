using DataTransformation.Funtion;
using DataTransformation.Funtion.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace DataTransformation.Tests
{
    /// <summary>
    /// TransformerTests.
    /// </summary>
    [TestClass]
    public class TransformerTests
    {
        /// <summary>
        /// Transforms the XML to JSON test.
        /// </summary>
        [TestMethod]
        public void TransformXmlToJsonTest()
        {
            string xml = File.ReadAllText("./person.xml");
            var result = Transformer.TransformXmlToJson<Person>(xml);
            Assert.AreNotEqual(string.Empty, result);
            Assert.IsTrue(result.Contains("Person", StringComparison.InvariantCulture));
            Assert.IsTrue(result.Contains("FirstName", StringComparison.InvariantCulture));
        }

        /// <summary>
        /// Transforms the JSON to XML test.
        /// </summary>
        [TestMethod]
        public void TransformJsonToXmlTest()
        {
            string json = File.ReadAllText("./person.json");
            var result = Transformer.TransformJsonToXml<Person>(json);
            Assert.AreNotEqual(string.Empty, result);
            Assert.IsTrue(result.Contains("firstname", StringComparison.InvariantCulture));
        }

    }
}
