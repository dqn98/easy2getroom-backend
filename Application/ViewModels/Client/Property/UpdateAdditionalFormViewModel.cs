namespace BE.Application.ViewModels.Client.Property
{
    public class UpdateAdditionalFormViewModel
    {
        public int YearBuild { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Garages { get; set; }
        public int[] Features { get; set; }
    }
}