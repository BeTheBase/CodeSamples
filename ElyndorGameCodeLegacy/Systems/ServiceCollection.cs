using UnityEngine;

public static class ServiceCollection
{
    public static void ConfigureServices()
    {
        DIContainer.Register<DialogueMemory>(() => new DialogueMemory(), Lifetime.Singleton);
        DIContainer.Register<PlayerStateManager>(() => new PlayerStateManager(), Lifetime.Singleton);
    }
}
