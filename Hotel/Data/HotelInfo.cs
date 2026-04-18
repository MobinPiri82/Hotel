namespace Hotel.Data
{
    public class HotelInfo
    {
        public int id{ get; set; }
        public string Name{ get; set; }
        public string Address{ get; set; }
        public double Rate{ get; set; }
        public int CountryId { get; set; }
        public counrty Country{ get; set; }
        public List<Personel> personels { get; set; }
        //public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}



