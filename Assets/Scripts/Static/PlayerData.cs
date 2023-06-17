public static class PlayerData
{
    public static int CurrentLevel { get; private set; } = 0;

    public static void SetData(int currentLevel)
    {
        CurrentLevel = currentLevel;
    }
}