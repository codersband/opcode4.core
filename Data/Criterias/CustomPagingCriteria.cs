using System;
using System.Runtime.Serialization;

namespace opcode4.core.Data.Criterias
{
    [Serializable]
    [DataContract]
    public class CustomPagingCriteria : ICriteria
    {
        private int _page;
        [DataMember]
        public int Page { set { _page = value < 1 ? 1 : value; } get { return  _page > _totalPages ? 1 : _page; } }

        private int _order;
        [DataMember]
        public int Order
        {
            set
            {
                _order = (value != 1 && value != -1) ? _order = 1 : value;
            }
            get { return _order; }
        }

        private int _pageSize;
        [DataMember]
        public int PageSize {
            set {
                _pageSize = value < 15 ? 15 : value;
            }
            get { return _pageSize; }
        }

        private int _totalPages;
        [DataMember]
        public int TotalPages { set{} get { return _totalPages; } }

        [DataMember]
        public double TotalRecords { 
            set{_totalPages = (int)Math.Ceiling(value / PageSize);}
            get { return _totalPages * PageSize; }
        }

        public CustomPagingCriteria()
        {
            Page = 1;
        }

        public int StartIndex
        {
            get
            {
                if (Page < 2)
                    return 0;
                return (Page - 1)*PageSize;
            }
        }

        public string PageQueryString(string query, SqlEngineName dbEngine = SqlEngineName.Oracle)
        {
            var end = Page * PageSize;
            var start = end - PageSize;

            switch (dbEngine)
            {
                case SqlEngineName.MySql:
                    return $"{query} LIMIT {start}, {end}";
                case SqlEngineName.Oracle:
                default:
                    return
                        // $"SELECT * FROM (SELECT a.*, ROWNUM r FROM ({query}) as a WHERE ROWNUM<={end}) as b WHERE r>{start}";
                        $"SELECT * FROM (SELECT a.*, ROWNUM r FROM ({query}) a WHERE ROWNUM<={end}) b WHERE r>{start}";
            }
        }

        public enum SqlEngineName
        {
            Oracle = 0,
            MySql
        }
    }
}
