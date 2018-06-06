using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactInformation.ReadModel.Shared
{
    public class CIRestProperties
    {
        public string Controller { get; set; }
        public string Method { get; set; }
        public List<CIRestParameter> Parameters { get; set; }
        public RestSharp.Method MethodType { get; set; }
        public object ReturnType { get; set; }
    }

    public class CIRestParameter
    {
        public string key { get; set; }
        public object value { get; set; }
    }

    public class CIRestService
    {
        public static IRestResponse<T> ExecuteRestRequest<T>(CIRestProperties properties, IRestClient client) where T : new()
        {
            IRestRequest request = new RestRequest(properties.Controller + "/" + properties.Method, properties.MethodType);

            if (properties.Parameters != null)
            {
                foreach (CIRestParameter parameter in properties.Parameters)
                {
                    if (properties.MethodType == Method.POST || properties.MethodType == Method.PUT)

                        request.AddParameter(parameter.key, JsonConvert.SerializeObject(parameter.value), ParameterType.RequestBody);

                    else

                        request.AddParameter(parameter.key, JsonConvert.SerializeObject(parameter.value));

                }
            }

            // easily add HTTP Headers
            request.AddHeader("Content-Type", "application/json");

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            return client.Execute<T>(request);
        }
    }

    public enum Status
    {
        Active,
        Inactive
    }
}