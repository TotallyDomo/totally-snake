using UnityEngine;

[RequireComponent(typeof(Clock))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    int score = 0;

    Clock clock;
    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
        MapGrid.OnFoodTaken += UpdateScore;
        clock = GetComponent<Clock>();
    }

    public void TriggerGameOver()
    {
        Clock.PauseClock();
        UI.Instance.SetScore(score);
        UI.Instance.GameOverObject.SetActive(true);
        // TODO: add score to local leaderboard
    }

    void UpdateScore() => score += Mathf.FloorToInt(5 / clock.ClockSpeed);

    private void OnDestroy()
    {
        MapGrid.OnFoodTaken -= UpdateScore;
    }
}
