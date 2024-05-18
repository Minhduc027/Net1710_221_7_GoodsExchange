using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.business
{
    public interface IGoodsExchangeResult
    {
        int Status { get; set; }
        string? Message { get; set; }
        object? Data { get; set; }
    }

    public class GoodsExchangeResult : IGoodsExchangeResult
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public GoodsExchangeResult()
        {
            Status = -1;
            Message = "Action fail";
        }

        public GoodsExchangeResult(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public GoodsExchangeResult(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
