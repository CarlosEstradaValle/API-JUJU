using System.Data;

namespace API.Context
{
    public class ResultQuery
    {
        public DataTable Table { get; set; }
        public string Result { get; set; }
        public string ResultMessage { get; set; }
    }
}
