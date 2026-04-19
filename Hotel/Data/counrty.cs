namespace Hotel.Data
{
    public class Counrty
    {
        public int CountryId{ get; set; }
        public string Name{ get; set; }
        public string Shortname{ get; set; }
        public IList<Hotel> Hotels { get; set; }
        public IList<Personel> personels{ get; set; }

        //public virtual ICollection<HotelInfo> hotesls { get; set; } 
    }
}



