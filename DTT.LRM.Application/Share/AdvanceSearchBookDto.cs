using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT.LRM.Share
{
    public class AdvanceSearchBookDto
    {
        public int BookCategoryId { get; set; }
        public string Author { get; set; }
        public int PublisherId { get; set; }
        public int ReleaseYear { get; set; }
    }
}
