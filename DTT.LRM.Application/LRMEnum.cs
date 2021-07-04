using DTT.LRM.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM
{
    public class LRMEnum
    {
        public enum BookStatus
        {
            UnUsed = 0, //Không sử dụng
            Using = 1, //Đang sử dụng
            Lost = -1, //Bị mất
            Liquidated = -2 //Đã thanh lý
        }
    }
}
