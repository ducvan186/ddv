namespace Ex04.StudentManagement.Enums
{
    public enum StudentStatus
    {
        DangHoc = 1,
        TotNghiep = 2,
        BuocNghiHoc = 3
    }

    public static class StudentStatusExtensions
    {
        public static string ToFriendlyString(this StudentStatus status) => status switch
        {
            StudentStatus.DangHoc => "Đang học",
            StudentStatus.TotNghiep => "Tốt nghiệp",
            StudentStatus.BuocNghiHoc => "Buộc nghỉ học",
            _ => "N/A"
        };
    }
}