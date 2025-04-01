namespace DialogueSystem.Core
{
    public interface IPlayerState
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        bool Check(string key, string op, object value);
    }
}