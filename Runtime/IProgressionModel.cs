namespace Gameframe.Progressions
{
    public interface IProgressionModel<T> where T : struct
    {
        int Count { get; }
        T Get(int index);
    }
}

