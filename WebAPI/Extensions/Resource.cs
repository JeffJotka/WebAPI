using Newtonsoft.Json;
using System.Dynamic;

namespace WebAPI.Extensions
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Resource<T>
    {
        public T Result { get; set; }
        public dynamic Meta { get; set; }

        #region Resource()
        public Resource() : base()
        {
            Meta = new ExpandoObject();
        }

        public Resource(T data) : this()
        {
            Result = data;
        }
        #endregion
    }

}
