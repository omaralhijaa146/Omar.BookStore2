using Omar.BookStore2.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Omar.BookStore2.Books
{
    public class Book:AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public BookType Type { get; private set; }
        public DateTime PublishDate { get; private set; }
        public float Price { get; private set; }


        private Book()
        {
            
        }

        public Book(Guid id,string name,BookType type,DateTime publishDate,float price):base(id)
        {
            SetId();
            SetName(name);
            SetType(type);
            SetPublishDate(publishDate);
            SetPrice(price);
        }

        private void SetId()
        {
            Check.NotDefaultOrNull<Guid>(Id, nameof(Id));
        }

        private void SetName(string name) {
            Name= Check.NotNullOrWhiteSpace(name, nameof(name));
        }

        private void SetType(BookType type) {

            Type=CustomCheck.AssignableTo<BookType>(type, nameof(type));

        }

        private void SetPublishDate(DateTime publishDate) {
           PublishDate = Check.NotDefaultOrNull<DateTime>(publishDate, nameof(publishDate));
        }

        private void SetPrice(float price) {
            
            Price = Check.Positive(price, nameof(price));
        }   

    }

    
}
