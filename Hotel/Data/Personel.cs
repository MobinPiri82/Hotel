namespace Hotel.Data
{
    public class Personel  
    {
        public int id { get; set; }
        public int Name { get; set; }
        public int HotelId { get; set; }
        public Hotel? WorkingHotel { get; set; } 
        public int CountryId { get; set; }
        public Counrty Nationality { get; set; }
        
    }
}
