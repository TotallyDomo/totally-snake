using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }

    public void TriggerGameOver()
    {
        Clock.PauseClock();
        UI.Instance.GameOverObject.SetActive(true);
        // TODO: add score to local leaderboard
    }
}
