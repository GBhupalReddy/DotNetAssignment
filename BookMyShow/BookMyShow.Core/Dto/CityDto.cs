namespace BookMyShow.Core.Dto
{
    public class CityDto
    {

        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;


    }
}
