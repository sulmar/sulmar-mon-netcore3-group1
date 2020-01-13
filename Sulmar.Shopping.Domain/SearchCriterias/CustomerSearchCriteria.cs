using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Domain.SearchCriterias
{
    public class CustomerSearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsRemoved { get; set; }

    }
}
