using System;

namespace XimbleAdventureWorksApi.Models.ApiResponses
{
    public class Response<T>
    {
        public Response() { }

        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }

        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}