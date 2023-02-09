using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace WebAPI.Helper
{
    public class Pager
    {
        private int pageIndex;
        private int pageSize;
        private string? sort;
        private string? order;
        private int totalRows;


        #region PageIndex
        /// <summary>
        /// Page index
        /// </summary>
        [FromQuery(Name = "index")]
        [JsonProperty(PropertyName = "index")]
        public virtual int PageIndex
        {
            get
            {
                if (pageIndex < TotalPages)
                {
                    return pageIndex;
                }
                else if (TotalPages > 0)
                {
                    return TotalPages;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                pageIndex = value > 0 ? value : 1;
            }
        }
        #endregion

        #region PageSize
        [FromQuery(Name = "size")]
        [JsonProperty(PropertyName = "size")]
        public virtual int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > 0 ? value : 1;
            }
        }
        #endregion

        #region TotalPages
        [BindNever]
        public virtual int TotalPages
        {
            get
            {
                if (PageSize > 0)
                    return Convert.ToInt32(Math.Ceiling((double)TotalRows / PageSize));
                else
                    return 0;
            }
        }
        #endregion

        #region TotalRows
        [BindNever]
        public virtual int TotalRows
        {
            get
            {
                return totalRows;
            }
            set
            {
                totalRows = value > 0 ? value : 0;
            }
        }
        #endregion

        #region Order
        [FromQuery(Name = "order")]
        [JsonProperty(PropertyName = "order")]
        public virtual string Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value?.ToUpper() == "ASC" ? "ASC" : "DESC";
            }
        }
        #endregion

        #region Sort
        [FromQuery(Name = "sort")]
        [JsonProperty(PropertyName = "sort")]
        public virtual string Sort
        {
            get
            {
                return sort;
            }
            set
            {
                sort = value;
            }
        }
        #endregion

        #region Offset
        /// <summary>
        /// Page offset
        /// </summary>
        [BindNever]
        public virtual int Offset
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
        #endregion

        #region Pager()
        public Pager() : this(1, 20, "Id", "ASC")
        {
        }

        public Pager(Pager pager)
        {
            totalRows = pager.totalRows;
            pageIndex = pager.pageIndex;
            pageSize = pager.pageSize;
            sort = pager.sort;
            order = pager.order;
        }

        public Pager(int pageIndex, int pageSize = 20, string sort = "Id", string order = "ASC")
        {
            TotalRows = Int32.MaxValue;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Sort = sort;
            Order = order;
        }
#endregion
    }

}
