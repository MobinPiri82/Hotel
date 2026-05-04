using System.ComponentModel.DataAnnotations;

namespace Hotel.Data
{
    public class Counrty
    {
        //[Required]
        //public int Pass{ get; set; }
        //[Compare(nameof(Pass),ErrorMessage ="")]
        //public int ConfirmedPass{ get; set; }
        [Key]
        public int CountryId{ get; set; }
        public  string Name{ get; set; }
        public string Shortname{ get; set; }
        public IList<HotelInfo> Hotels { get; set; }
        public IList<Personel> personels{ get; set; }

        //public virtual IList<HotelInfo> hotesls { get; set; } = new List<HotelInfo>();
        //public virtual IList<Personel> Personels { get; set; } = new List<Personel>();
        
    }
}



