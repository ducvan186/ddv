using System;
using System.Collections.Generic;
using System.Linq;
using Ex04.StudentManagement.Enums;
using Ex04.StudentManagement.Models;

namespace Ex04.StudentManagement.Services
{
    public class StudentService
    {
        private readonly List<Student> _students = new();

        public StudentService()
        {
            // Seed Data mẫu
            _students.Add(new Student("SV001", "Nguyen Van A", new DateOnly(2003, 5, 12), Gender.Nam, "a.nguyen@gmail.com", "0901234567", "CNTT", 8.5, StudentStatus.DangHoc));
            _students.Add(new Student("SV002", "Tran Thi B", new DateOnly(2002, 8, 20), Gender.Nu, "b.tran@gmail.com", "0912345678", "Kinh tế", 7.2, StudentStatus.DangHoc));
            _students.Add(new Student("SV003", "Le Van C", new DateOnly(2001, 11, 3), Gender.Nam, null, null, "CNTT", 9.1, StudentStatus.TotNghiep));
            _students.Add(new Student("SV004", "Pham Thi D", new DateOnly(2003, 1, 15), Gender.Nu, "d.pham@gmail.com", "0934567890", "Ngoại ngữ", 6.8, StudentStatus.BuocNghiHoc));
        }

        public IReadOnlyList<Student> GetAll() => _students.AsReadOnly();

        public bool Exists(string id) => _students.Any(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        public Student? GetById(string id) => _students.FirstOrDefault(s => s.Id.Equals(id, StringComparison.OrdinalIgnoreCase));

        public bool Add(Student student)
        {
            if (Exists(student.Id)) return false;
            _students.Add(student);
            return true;
        }

        public bool Update(string id, Student updatedData)
        {
            var student = GetById(id);
            if (student == null) return false;

            student.FullName = updatedData.FullName;
            student.DateOfBirth = updatedData.DateOfBirth;
            student.Gender = updatedData.Gender;
            student.Email = updatedData.Email;
            student.PhoneNumber = updatedData.PhoneNumber;
            student.Major = updatedData.Major;
            student.Gpa = updatedData.Gpa;
            student.Status = updatedData.Status;

            return true;
        }

        public bool Delete(string id)
        {
            var student = GetById(id);
            if (student == null) return false;

            _students.Remove(student);
            return true;
        }

        public List<Student> SearchByName(string keyword) =>
            _students.Where(s => s.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Student> SortByName() => _students.OrderBy(s => s.FullName).ToList();

        public List<Student> SortByGpaDescending() => _students.OrderByDescending(s => s.Gpa).ToList();

        public List<Student> GetHighGpaStudents(double minGpa = 8.0) => _students.Where(s => s.Gpa >= minGpa).ToList();

        public List<Student> GetTopGpaStudents()
        {
            if (!_students.Any()) return new List<Student>();
            double maxGpa = _students.Max(s => s.Gpa);
            return _students.Where(s => s.Gpa == maxGpa).ToList();
        }

        public double GetOverallAverageGpa() => _students.Any() ? _students.Average(s => s.Gpa) : 0.0;

        public IEnumerable<IGrouping<string, Student>> GroupByMajor() => _students.GroupBy(s => s.Major);

        public IEnumerable<IGrouping<StudentStatus, Student>> GroupByStatus() => _students.GroupBy(s => s.Status);
    }
}