using System;
using System.Collections.Generic;
using System.Text;
using Ex04.StudentManagement.Enums;
using Ex04.StudentManagement.Helpers;
using Ex04.StudentManagement.Models;
using Ex04.StudentManagement.Services;
using Ex04.StudentManagement.Validators;
using Ex04.StudentManagement.Views;

namespace Ex04.StudentManagement.Managers
{
    public class MenuManager
    {
        private readonly StudentService _service = new();
        private readonly StudentConsoleView _view = new();

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                ShowMenu();
                Console.Write("Mời bạn chọn chức năng (0-13): ");
                string? choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": DisplayAll(); break;
                    case "3": FindById(); break;
                    case "4": FindByName(); break;
                    case "5": UpdateStudent(); break;
                    case "6": DeleteStudent(); break;
                    case "7": SortByName(); break;
                    case "8": SortByGpa(); break;
                    case "9": DisplayHighGpa(); break;
                    case "10": DisplayTopGpa(); break;
                    case "11": DisplayAverageGpa(); break;
                    case "12": GroupByMajor(); break;
                    case "13": GroupByStatus(); break;
                    case "0":
                        running = false;
                        _view.ShowMessage($"Đã thoát ứng dụng. Tổng sinh viên đã khởi tạo: {Student.TotalCreated}");
                        break;
                    default:
                        _view.ShowMessage("Lựa chọn không hợp lệ, vui lòng nhập lại từ 0-13!", isError: true);
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("\n==================================================");
            Console.WriteLine("    QUẢN LÝ SINH VIÊN (C# .NET 8 OOP ARCHITECTURE)");
            Console.WriteLine("==================================================");
            Console.WriteLine("1.  Thêm sinh viên");
            Console.WriteLine("2.  Hiển thị danh sách");
            Console.WriteLine("3.  Tìm sinh viên theo mã");
            Console.WriteLine("4.  Tìm gần đúng theo họ tên");
            Console.WriteLine("5.  Cập nhật sinh viên");
            Console.WriteLine("6.  Xóa sinh viên");
            Console.WriteLine("7.  Sắp xếp theo họ tên (A-Z)");
            Console.WriteLine("8.  Sắp xếp theo điểm trung bình (Giảm dần)");
            Console.WriteLine("9.  Hiển thị sinh viên có điểm từ 8 trở lên");
            Console.WriteLine("10. Hiển thị sinh viên có điểm cao nhất");
            Console.WriteLine("11. Tính điểm trung bình toàn bộ sinh viên");
            Console.WriteLine("12. Thống kê sinh viên theo ngành");
            Console.WriteLine("13. Thống kê sinh viên theo trạng thái");
            Console.WriteLine("0.  Thoát chương trình");
            Console.WriteLine("==================================================");
        }

        private void AddStudent()
        {
            Console.WriteLine("\n--- THÊM SINH VIÊN MỚI ---");
            string id = _view.InputId(_service.GetAll(), isNew: true);
            string fullName = _view.InputFullName();
            DateOnly dob = InputHelper.ReadDate("Nhập ngày sinh (dd/MM/yyyy): ");
            Gender gender = _view.InputGender();
            string? email = _view.InputEmail();
            string? phone = InputHelper.ReadOptionalString("Nhập SĐT (Enter để bỏ qua): ");
            string major = InputHelper.ReadString("Nhập ngành học: ");
            double gpa = _view.InputGpa();
            StudentStatus status = _view.InputStatus();

            var student = new Student(id, fullName, dob, gender, email, phone, major, gpa, status);
            _service.Add(student);
            _view.ShowMessage("Thêm sinh viên thành công!");
        }

        private void DisplayAll()
        {
            Console.WriteLine("\n--- DANH SÁCH SINH VIÊN ---");
            _view.RenderTable(_service.GetAll());
        }

        private void FindById()
        {
            Console.WriteLine("\n--- TÌM SINH VIÊN THEO MÃ ---");
            string id = InputHelper.ReadString("Nhập mã sinh viên cần tìm: ");

            var student = _service.GetById(id);
            if (student != null)
                _view.RenderTable(new List<Student> { student });
            else
                _view.ShowMessage("Không tìm thấy sinh viên.", isError: true);
        }

        private void FindByName()
        {
            Console.WriteLine("\n--- TÌM GẦN ĐÚNG THEO HỌ TÊN ---");
            string keyword = InputHelper.ReadString("Nhập từ khóa tìm kiếm: ");
            _view.RenderTable(_service.SearchByName(keyword));
        }

        private void UpdateStudent()
        {
            Console.WriteLine("\n--- CẬP NHẬT SINH VIÊN ---");
            string id = InputHelper.ReadString("Nhập mã sinh viên cần cập nhật: ");

            var existing = _service.GetById(id);
            if (existing == null)
            {
                _view.ShowMessage("Chỉ được cập nhật khi sinh viên tồn tại!", isError: true);
                return;
            }

            Console.WriteLine($"Cập nhật sinh viên [{existing.FullName}] (Bấm Enter để giữ nguyên cũ):");

            string nameInput = InputHelper.ReadOptionalString($"Họ tên [{existing.FullName}]: ") ?? existing.FullName;
            string majorInput = InputHelper.ReadOptionalString($"Ngành [{existing.Major}]: ") ?? existing.Major;
            string? emailInput = _view.InputEmail() ?? existing.Email;
            string? phoneInput = InputHelper.ReadOptionalString($"SĐT [{existing.PhoneNumber ?? "N/A"}]: ") ?? existing.PhoneNumber;

            Gender genderInput = existing.Gender;
            string? genderStr = InputHelper.ReadOptionalString($"Giới tính ({existing.Gender.ToFriendlyString()}) - Nhập [1]Nam, [2]Nữ, [3]Khác hoặc Enter: ");
            if (int.TryParse(genderStr, out int gVal) && Enum.IsDefined(typeof(Gender), gVal)) genderInput = (Gender)gVal;

            StudentStatus statusInput = existing.Status;
            string? statusStr = InputHelper.ReadOptionalString($"Trạng thái ({existing.Status.ToFriendlyString()}) - Nhập [1]Đang học, [2]Tốt nghiệp, [3]Buộc nghỉ học hoặc Enter: ");
            if (int.TryParse(statusStr, out int sVal) && Enum.IsDefined(typeof(StudentStatus), sVal)) statusInput = (StudentStatus)sVal;

            double gpaInput = existing.Gpa;
            string? gpaStr = InputHelper.ReadOptionalString($"Điểm TB [{existing.Gpa}]: ");
            if (double.TryParse(gpaStr, out double parsedGpa))
            {
                var (isValid, msg) = StudentValidator.ValidateGpa(parsedGpa);
                if (isValid) gpaInput = parsedGpa;
                else _view.ShowMessage($"{msg} -> Giữ nguyên điểm cũ.", isError: true);
            }

            DateOnly dobInput = existing.DateOfBirth;
            string? dobStr = InputHelper.ReadOptionalString($"Ngày sinh [{existing.DateOfBirth:dd/MM/yyyy}]: ");
            if (DateOnly.TryParseExact(dobStr, "dd/MM/yyyy", out DateOnly parsedDob)) dobInput = parsedDob;

            var updatedData = new Student(id, nameInput, dobInput, genderInput, emailInput, phoneInput, majorInput, gpaInput, statusInput);
            _service.Update(id, updatedData);
            _view.ShowMessage("Cập nhật thành công!");
        }

        private void DeleteStudent()
        {
            Console.WriteLine("\n--- XÓA SINH VIÊN ---");
            string id = InputHelper.ReadString("Nhập mã sinh viên cần xóa: ");

            if (!_service.Exists(id))
            {
                _view.ShowMessage("Chỉ được xóa khi sinh viên tồn tại!", isError: true);
                return;
            }

            _service.Delete(id);
            _view.ShowMessage("Xóa sinh viên thành công!");
        }

        private void SortByName()
        {
            Console.WriteLine("\n--- SẮP XẾP THEO HỌ TÊN (A-Z) ---");
            _view.RenderTable(_service.SortByName());
        }

        private void SortByGpa()
        {
            Console.WriteLine("\n--- SẮP XẾP THEO ĐIỂM TRUNG BÌNH (GIẢM DẦN) ---");
            _view.RenderTable(_service.SortByGpaDescending());
        }

        private void DisplayHighGpa()
        {
            Console.WriteLine("\n--- SINH VIÊN CÓ ĐIỂM TRUNG BÌNH >= 8.0 ---");
            _view.RenderTable(_service.GetHighGpaStudents(8.0));
        }

        private void DisplayTopGpa()
        {
            Console.WriteLine("\n--- SINH VIÊN CÓ ĐIỂM CAO NHẤT ---");
            _view.RenderTable(_service.GetTopGpaStudents());
        }

        private void DisplayAverageGpa()
        {
            double avg = _service.GetOverallAverageGpa();
            Console.WriteLine($"\n📊 Điểm trung bình của tất cả sinh viên: {avg:F2}");
        }

        private void GroupByMajor()
        {
            Console.WriteLine("\n--- THỐNG KÊ SINH VIÊN THEO NGÀNH ---");
            foreach (var group in _service.GroupByMajor())
            {
                Console.WriteLine($"\n🎓 Ngành: {group.Key}");
                _view.RenderTable(group);
            }
        }

        private void GroupByStatus()
        {
            Console.WriteLine("\n--- THỐNG KÊ SINH VIÊN THEO TRẠNG THÁI HỌC TẬP ---");
            foreach (var group in _service.GroupByStatus())
            {
                Console.WriteLine($"\n📌 Trạng thái: {group.Key.ToFriendlyString()}");
                _view.RenderTable(group);
            }
        }
    }
}