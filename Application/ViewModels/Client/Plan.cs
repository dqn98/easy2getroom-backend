namespace BE.Application.ViewModels.Client
{
    public class Plan
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public Area Area { get; set; }
        public int Rooms { get; set; }
        public int Baths { get; set; }
        public string Image { get; set; }
    }
}