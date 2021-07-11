using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    const string SOURCE_URL = "https://github.com/TotallyDomo/totally-snake";

    public static UI Instance;

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
    public void OnOptionsClick() => Debug.LogWarning("Options are not implemented yet");
    public void OnSourceClick() => Application.OpenURL(SOURCE_URL);
    public void OnBackToMenuClick() => SceneManager.LoadScene(0);
}
