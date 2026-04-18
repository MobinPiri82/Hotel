namespace Hotel.Data
{
    public class Personel  
    {
        public int id { get; set; }
        public int Name { get; set; }
        public int HotelId { get; set; }
        public HotelInfo? WorkingHotel { get; set; } 
        public int CountryId { get; set; }
        public counrty Nationality { get; set; }
        
    }
}
