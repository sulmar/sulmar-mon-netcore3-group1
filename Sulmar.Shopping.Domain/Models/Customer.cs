using System;
using System.Diagnostics;

namespace Sulmar.Shopping.Domain
{

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public bool IsRemoved { get; set; }
        public string PostCode { get; set; }

        public string Email { get; set; }
        public string City { get; set; }

    }
}
