using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using DataTransformation.Funtion.Models;
using Newtonsoft.Json.Linq;

namespace DataTransformation.Funtion
{
    /// <summary>
    /// Transform.
    /// </summary>
    public static class FunctionTransform
    {
        /// <summary>
        /// Transforms the XML to JSON.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>JSON as string.</returns>
        /// <exception cref="ArgumentNullException">req.</exception>
        [FunctionName(nameof(TransformXmlToJson))]
        public static async Task<IActionResult> TransformXmlToJson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(TransformXmlToJson)} triggered.");
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            string result = string.Empty;
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(req.Body);
                string requestBody = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                result = Transformer.TransformXmlToJson<Person>(requestBody);

            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return new BadRequestResult();
            }
            finally
            {
                streamReader?.Dispose();
            }

            req.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            return new OkObjectResult(JObject.Parse(result));
        }

        /// <summary>
        /// Transforms the JSON to XML.
        /// </summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>XML as string.</returns>
        /// <exception cref="ArgumentNullException">req.</exception>
        [FunctionName(nameof(TransformJsonToXml))]
        public static async Task<IActionResult> TransformJsonToXml(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"{nameof(TransformJsonToXml)} triggered.");
            if (req == null)
            {
                throw new ArgumentNullException(nameof(req));
            }

            string result = string.Empty;
            StreamReader streamReader = null;
            try
            {
                streamReader = new StreamReader(req.Body);
                string requestBody = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                result = Transformer.TransformJsonToXml<Person>(requestBody);

            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return new BadRequestResult();
            }
            finally
            {
                streamReader?.Dispose();
            }

            return new ContentResult() { Content = result, ContentType = "application/xml", StatusCode = 200 };
        }
    }
}
