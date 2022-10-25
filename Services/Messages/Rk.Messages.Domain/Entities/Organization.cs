using System.ComponentModel.DataAnnotations;

namespace Rk.Messages.Domain.Entities
{
    /// <summary>
    /// Организация
    /// </summary>
    public class Organization :BaseEntity
    {
      //  private Organization() { }
        public Organization(long id, string name, string fullName, string ogrn, string inn, string kpp)
        {
            Id = id;
            Name = name;
            FullName = fullName;
            Ogrn = ogrn;
            Inn = inn;
            Kpp = kpp;
        }

        [StringLength(512)]
        public string Name { get; private set; }

        [StringLength(1024)]
        public string FullName { get; private set; }

        [StringLength(20)]
        public string Ogrn { get; private set; }

        [StringLength(12)]
        public string Inn { get; private set; }

        [StringLength(9)]
        public string Kpp { get; private set; }
    }
}
