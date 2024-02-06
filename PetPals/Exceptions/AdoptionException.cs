using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals.Exceptions
{
    internal class AdoptionException : Exception
    {
        public AdoptionException() { }
       public AdoptionException(string message) : base(message) { }
    }
}
