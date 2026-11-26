namespace Common.Quiz.Commands
{
    public interface ICommandHandler<T>
    {
        Task HandleAsync(IEnumerable<T> items);
    }
}
