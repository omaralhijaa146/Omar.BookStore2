using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Omar.BookStore2.Authors
{
    public class AuthorAlreadyExistsException:BusinessException
    {

        public AuthorAlreadyExistsException(string name):base(BookStore2DomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }

    }
}
