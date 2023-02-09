using Newtonsoft.Json;
using System.Dynamic;

namespace WebAPI.Extensions
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Pagination<T>
    {
        public int TotalRows { get; set; }
        public IEnumerable<T> Items { get; set; }
        public dynamic Meta { get; set; }

        #region Pagination()
        public Pagination() : base()
        {
            TotalRows = 0;
            Meta = new ExpandoObject();
        }

        public Pagination(IEnumerable<T> items, int totalRows) : this()
        {
            TotalRows = totalRows;
            Items = items;
        }
        #endregion
    }
}
