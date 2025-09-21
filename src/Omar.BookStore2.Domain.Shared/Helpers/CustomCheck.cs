using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omar.BookStore2.Helpers
{
    public static class CustomCheck
    {
        public static TEnum AssignableTo<TEnum>(TEnum value, string name) where TEnum : Enum { 
        
            if(!Enum.IsDefined(typeof(TEnum),value))
                throw new ArgumentException($"The {name} is not valid {typeof(TEnum).Name} type");
            return value;

        }
    }
}
