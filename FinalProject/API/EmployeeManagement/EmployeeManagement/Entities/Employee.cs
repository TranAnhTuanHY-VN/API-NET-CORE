namespace EmployeeManagement.Entities
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Guid PositionID { get; set; }
        public Guid DepartmentID { get; set; }

    }
}
