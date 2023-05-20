namespace Domain.Interfaces
{
    public interface IQuestion : IEntityBase
    {
        string Text { get; set; }
        string OptionA { get; set; }
        string OptionB { get; set; }
        string OptionC { get; set; }
        string OptionD { get; set; }
        string ApplicableCategories { get; set; }
        int Answer { get; set; }
    }
}
