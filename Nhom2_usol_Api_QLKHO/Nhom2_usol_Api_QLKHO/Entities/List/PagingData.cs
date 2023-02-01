using System.Collections.Generic;

namespace Nhom2_usol_Api_QLKHO.Entities.List
{
    public class PagingData<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public long count { get; set; }
    }
}
