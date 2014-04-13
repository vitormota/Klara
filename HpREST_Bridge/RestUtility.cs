using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace HpREST_Bridge
{
    class RestUtility
    {

        public static WebException exception = null;

        /// <summary>
        /// Use this for GET Requests
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            string result = null;
            //TODO: remove try catch clause
            //this is a workaround bacause API when queryied if a user exists on DB (and it doesn't)
            //the api_response will be a 404, causing an WebException
            try
            {
                using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    exception = ex;
                    System.Diagnostics.Debug.WriteLine("Status Code : {0}", ((HttpWebResponse)ex.Response).StatusCode);
                    System.Diagnostics.Debug.WriteLine("Status Description : {0}", ((HttpWebResponse)ex.Response).StatusDescription);
                }
            }
            
            return result;
        }

        /// <summary>
        /// Use this for POST Requests with url encoded data
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <param name="paramVal"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string[] paramName, string[] paramVal)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            // Build a string with all the params, properly encoded.
            // We assume that the arrays paramName and paramVal are
            // of equal length:
            StringBuilder paramz = new StringBuilder();
            for (int i = 0; i < paramName.Length; i++)
            {
                paramz.Append(paramName[i]);
                paramz.Append("=");
                paramz.Append(HttpUtility.UrlEncode(paramVal[i]));
                paramz.Append("&");
            }

            // Encode the parameters as form data:
            byte[] formData = UTF8Encoding.UTF8.GetBytes(paramz.ToString());
            req.ContentLength = formData.Length;

            // Send the request:
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }

            // Pick up the resp:
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }

            return result;
        }

        /// <summary>
        /// Make a JSON request with POST headers
        /// </summary>
        /// <param name="url">url to send to</param>
        /// <param name="data">face_response data object, it can be a string</param>
        /// <returns>The receiver's api_response</returns>
        /// TODO: Should we check face_response consistency to prevent invalid request to API?
        /// ERROR handling
        public static string HttpPostJSON(string url, Object data)
        {
            //create an HTTP request to the URL that we need to invoke
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json; charset=utf-8"; //set the content type to JSON
            request.Method = "POST"; //make an HTTP POST

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                //initiate the request
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }

            // Get the api_response.
            WebResponse response = request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            dynamic result = streamReader.ReadToEnd();
            return result;
    }    }


}
