namespace Ex04.StudentManagement.Enums
{
    public enum Gender
    {
        Nam = 1,
        Nu = 2,
        Khac = 3
    }

    public static class GenderExtensions
    {
        public static string ToFriendlyString(this Gender gender) => gender switch
        {
            Gender.Nam => "Nam",
            Gender.Nu => "Nữ",
            Gender.Khac => "Khác",
            _ => "N/A"
        };
    }
}