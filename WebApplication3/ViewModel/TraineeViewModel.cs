namespace WebApplication3.ViewModels
{
    public class TraineeViewModel
    {
        public string TraineeName { get; set; }
        public string TraineeImage { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public string Advertisement { get; set; }
        public string Color { get; set; }
        public List<string> Branches { get; set; }
        public int Temp { get; set; }
    }
}