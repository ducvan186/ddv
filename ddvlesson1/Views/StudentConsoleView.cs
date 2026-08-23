using System;
using System.Collections.Generic;
using System.Linq;
using Ex04.StudentManagement.Enums;
using Ex04.StudentManagement.Helpers;
using Ex04.StudentManagement.Models;
using Ex04.StudentManagement.Validators;

namespace Ex04.StudentManagement.Views
{
    public class StudentConsoleView
    {
        public void ShowMessage(string message, bool isError = false)
        {
            Console.WriteLine(isError ? $"❌ {message}" : $"✅ {message}");
        }

        public void RenderTable(IEnumerable<Student> students)
        {
            var list = students.ToList();
            if (!list.Any())
            {
                Console.WriteLine("⚠️ Không có sinh viên nào.");
                return;
            }

            Console.WriteLine(new string('-', 125));
            Console.WriteLine($"| {"Mã SV",-10} | {"Họ và Tên",-20} | {"Ngày sinh",-10} | {"Giới tính",-8} | {"Email",-20} | {"Số ĐT",-12} | {"Ngành học",-15} | {"ĐTB",5} | {"Trạng thái",-12} |");
            Console.WriteLine(new string('-', 125));

            foreach (var s in list)
            {
                Console.WriteLine($"| {s.Id,-10} | {s.FullName,-20} | {s.DateOfBirth:dd/MM/yyyy},-10 | {s.Gender.ToFriendlyString(),-8} | {s.Email ?? "N/A",-20} | {s.PhoneNumber ?? "N/A",-12} | {s.Major,-15} | {s.Gpa,5:F2} | {s.Status.ToFriendlyString(),-12} |");
            }

            Console.WriteLine(new string('-', 125));
            Console.WriteLine($"Tổng số: {list.Count} sinh viên.");
        }

        public string InputId(IEnumerable<Student> existingStudents, bool isNew = true)
        {
            while (true)
            {
                string id = InputHelper.ReadString("Nhập mã sinh viên: ");
                var (isValid, message) = StudentValidator.ValidateId(id, existingStudents, isNew);
                if (isValid) return id;
                ShowMessage(message, isError: true);
            }
        }

        public string InputFullName()
        {
            while (true)
            {
                string name = InputHelper.ReadString("Nhập họ tên: ");
                var (isValid, message) = StudentValidator.ValidateFullName(name);
                if (isValid) return name;
                ShowMessage(message, isError: true);
            }
        }

        public double InputGpa()
        {
            while (true)
            {
                double gpa = InputHelper.ReadDouble("Nhập điểm trung bình (0.0 - 10.0): ");
                var (isValid, message) = StudentValidator.ValidateGpa(gpa);
                if (isValid) return gpa;
                ShowMessage(message, isError: true);
            }
        }

        public string? InputEmail()
        {
            while (true)
            {
                string? email = InputHelper.ReadOptionalString("Nhập email (Enter để bỏ qua): ");
                var (isValid, message) = StudentValidator.ValidateEmail(email);
                if (isValid) return email;
                ShowMessage(message, isError: true);
            }
        }

        public Gender InputGender()
        {
            Console.WriteLine("Chọn giới tính: [1] Nam | [2] Nữ | [3] Khác");
            return InputHelper.ReadEnum<Gender>("Mời chọn (1-3): ");
        }

        public StudentStatus InputStatus()
        {
            Console.WriteLine("Chọn trạng thái: [1] Đang học | [2] Tốt nghiệp | [3] Buộc nghỉ học");
            return InputHelper.ReadEnum<StudentStatus>("Mời chọn (1-3): ");
        }
    }
}