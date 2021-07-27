namespace UniversityApp.DTOs
{
    public class StudentSearchParametersDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }


        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
