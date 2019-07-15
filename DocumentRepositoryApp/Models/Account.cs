using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;

namespace DocumentRepositoryApp.Models
{
    public class Account
    {
        public virtual int Id { get; set; }

        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual ISet<Document> Documents { get; set; }

        public Account()
        {
            Documents = new HashSet<Document>();
        }

        public override string ToString()
        {
            return Login;
        }
    }


    public class UserMap : ClassMapping<Account>
    {
        public UserMap()
        {
            Id(i => i.Id, j => j.Generator(Generators.Identity));

            Property(i => i.Login, j => 
            {
                j.NotNullable(true);
                j.Unique(true);
            });

            Property(i => i.Password, j =>
            {
                j.NotNullable(true);
            });

            Set(i => i.Documents, j => 
            {
                j.Key(k => k.Column("AuthorId"));
                j.Cascade(Cascade.All);
                j.Inverse(true);
            },
            k => k.OneToMany());
        }
    }
}