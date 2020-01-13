using System;

namespace Sulmar.Shopping.Domain
{

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRemoved { get; set; }

    }
}
