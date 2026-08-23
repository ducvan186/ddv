using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ex04.StudentManagement.Models;

namespace Ex04.StudentManagement.Validators
{
    public static class StudentValidator
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public static (bool IsValid, string Message) ValidateId(string id, IEnumerable<Student> existingStudents, bool isNew = true)
        {
            if (string.IsNullOrWhiteSpace(id))
                return (false, "Mã sinh viên không được để rỗng.");

            if (isNew && existingStudents.Any(s => s.Id.Equals(id.Trim(), StringComparison.OrdinalIgnoreCase)))
                return (false, "Mã sinh viên đã tồn tại.");

            return (true, string.Empty);
        }

        public static (bool IsValid, string Message) ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return (false, "Họ tên sinh viên không được để rỗng.");

            return (true, string.Empty);
        }

        public static (bool IsValid, string Message) ValidateGpa(double gpa)
        {
            if (gpa < 0.0 || gpa > 10.0)
                return (false, "Điểm trung bình phải nằm trong khoảng 0.0 đến 10.0.");

            return (true, string.Empty);
        }

        public static (bool IsValid, string Message) ValidateEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return (true, string.Empty);

            if (!EmailRegex.IsMatch(email.Trim()))
                return (false, "Email không đúng định dạng (VD: name@domain.com).");

            return (true, string.Empty);
        }
    }
}