namespace RengieExServices.Core
{
    public class SingletonLazy
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<SingletonLazy> _instance = new(() => new SingletonLazy());

        private SingletonLazy() { }

        public static SingletonLazy Instance => _instance.Value;
    }
}
