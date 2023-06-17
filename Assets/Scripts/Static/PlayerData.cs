public static class PlayerData
{
    public static int CurrentLevel { get; private set; } = 0;

    public static void TrySetCurrentLevel(int currentLevel)
    {
        if (currentLevel < CurrentLevel)
            return;

        CurrentLevel = currentLevel;
    }

    public static void SetCurrentLevel(int currentLevel)
    {
        CurrentLevel = currentLevel;
    }
}