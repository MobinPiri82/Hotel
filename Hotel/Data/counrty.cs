namespace Hotel.Data
{
    public class counrty
    {
        public int CountryId{ get; set; }
        public string Name{ get; set; }
        public string Shortname{ get; set; }
        public IList<HotelInfo> Hotels { get; set; }
        public IList<Personel> personels{ get; set; }

        //public virtual ICollection<HotelInfo> hotesls { get; set; } 
    }
}



