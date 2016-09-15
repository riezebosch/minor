namespace TddDemo
{
    interface IAdd<in T>
    {
        void Add(T item);
    }
}