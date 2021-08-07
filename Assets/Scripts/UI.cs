using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{
    const string SOURCE_URL = "https://github.com/TotallyDomo/totally-snake";

    public static UI Instance;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    GameObject gameOverObject;
    public GameObject GameOverObject => gameOverObject;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        Instance = this;
    }


    public void OnStartClick() => SceneManager.LoadScene(1);
    public void OnSourceClick() => Application.OpenURL(SOURCE_URL);
    public void OnBackToMenuClick() => SceneManager.LoadScene(0);
    public void SetScore(int score) => scoreText.SetText("SCORE: {0}", score);
}
