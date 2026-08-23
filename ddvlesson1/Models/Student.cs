using Ex04.StudentManagement.Enums;

namespace Ex04.StudentManagement.Models
{
    public class Student
    {
        public static int TotalCreated { get; private set; } = 0;

        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Major { get; set; } = string.Empty;
        public double Gpa { get; set; }
        public StudentStatus Status { get; set; } = StudentStatus.DangHoc;

        public Student()
        {
            TotalCreated++;
        }

        public Student(string id, string fullName, DateOnly dateOfBirth, Gender gender,
                       string? email, string? phoneNumber, string major, double gpa, StudentStatus status)
        {
            Id = id;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Major = major;
            Gpa = gpa;
            Status = status;
            TotalCreated++;
        }
    }
}