using System;

namespace Ex04.StudentManagement.Helpers
{
    public static class InputHelper
    {
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrWhiteSpace(input)) return input;
                Console.WriteLine("❌ Giá trị không được để rỗng.");
            }
        }

        public static string? ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }

        public static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out double value))
                    return value;
                Console.WriteLine("❌ Vui lòng nhập số thực hợp lệ.");
            }
        }

        public static DateOnly ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateOnly.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", out DateOnly date))
                    return date;
                Console.WriteLine("❌ ĐỊnh dạng ngày sai (Ví dụ chuẩn: 15/08/2003).");
            }
        }

        public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, Enum
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int choice) && Enum.IsDefined(typeof(TEnum), choice))
                {
                    return (TEnum)(object)choice;
                }
                Console.WriteLine("❌ Lựa chọn không hợp lệ, vui lòng chọn lại!");
            }
        }
    }
}