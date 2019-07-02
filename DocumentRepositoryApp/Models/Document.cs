using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace DocumentRepositoryApp.Models
{
    public class Document
    {
        public virtual int Id { get; set; }

        public virtual string FileName { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual Account Author { get; set; }

        public virtual string FilePath { get; set; }



        public override bool Equals(object obj)
        {
            return obj is Document document &&
                   Id == document.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }

    public class DocumentMap : ClassMapping<Document>
    {
        public DocumentMap()
        {
            Id(i => i.Id, j => j.Generator(Generators.Identity));

            Property(i => i.FileName, j =>
            {
                j.NotNullable(true);
            });

            Property(i => i.Date, j => {
                j.NotNullable(true);
                j.Generated(PropertyGeneration.Insert);
                j.Column(k => k.Default("GETDATE()"));
            });

            ManyToOne(i => i.Author, j => 
            {
                j.Cascade(Cascade.All);
                j.NotNullable(true);
                j.Column("AuthorId");
            });

            Property(i => i.FilePath, j => j.NotNullable(true));
        }
    }
}