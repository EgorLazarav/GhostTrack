using UnityEngine;

public class PlayerScoreHandler : MonoBehaviour
{
    public int KillScore { get; private set; } = 1500;
    public int KillStreakScore { get; private set; } = 700;
    public int TimeScore { get; private set; } = 500;
    public int AccuracyScore { get; private set; } = 1200;
    public int TotalScore { get; private set; } = 3900;
}
