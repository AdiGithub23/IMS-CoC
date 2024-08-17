using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class TransferRequest
    {
        public int TransferRequestId { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsRejected { get; set; }

        // Optional: Navigation properties
        //public IdentityUser FromUser { get; set; }
        //public IdentityUser ToUser { get; set; }
        public CartItem CartItem { get; set; }
    }

}

