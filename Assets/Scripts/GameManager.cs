using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int vidas = 3;

    public TMP_Text scoreText;
    public TMP_Text vidasText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int puntos)
    {
        score += puntos;
        UpdateUI();
    }

    public void RestarVida()
    {
        vidas--;
        UpdateUI();

        if (vidas <= 0)
        {
            GameOver();
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        vidasText.text = "Vidas: " + vidas;
    }

    void GameOver()
    {
        // Lógica de Game Over 
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}