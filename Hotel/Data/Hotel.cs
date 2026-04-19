namespace Hotel.Data
{
    public class Hotel
    {
        public int id{ get; set; }
        public string Name{ get; set; }
        public string Address{ get; set; }
        public double Rate{ get; set; }
        public int CountryId { get; set; }
        public Counrty country{ get; set; }
        public IList<Personel> personels { get; set; }
        //public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}



