using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Omar.BookStore2.Authors
{
    public class Author:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string ShortBio { get; set; }

        private Author() { }

        internal Author(
            Guid id,
            string name,
            DateTime birthDate,
            string? shortBio=null
            ):base(id) {

            Check.NotDefaultOrNull<Guid>(id,nameof(id));
            SetName(name);
            SetBirthDate(birthDate);
            ShortBio = shortBio;

        }

        private void SetBirthDate(DateTime birthDate) {

            BirthDate = Check.NotDefaultOrNull<DateTime>(birthDate,nameof(birthDate));

        }

        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name,nameof(name),maxLength: AuthorConsts.MaxNameLength);
        }

        internal Author ChangeName(string name) {

            SetName(name);
            return this;
        
        }
    }
}
