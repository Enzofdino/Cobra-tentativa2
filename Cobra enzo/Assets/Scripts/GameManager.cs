using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
<<<<<<< HEAD
    #region singleton
    public static Cobra cobra;
    public GameManager instance;
    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public TMP_InputField speedInput;
    public GameObject startButton; // Referência ao botão de iniciar
    public GameObject panel; // Referência ao painel do menu

    private void Awake()
    {
       instance = this;
    }
    #endregion
    public TextMeshProUGUI recordetexto;
    public TextMeshProUGUI melhorrecordetexto;
    public TextMeshProUGUI gameOverTexto;
=======
    public Cobra cobra;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;
>>>>>>> 76d0feb368aff55e530ffb4a9da007192eae708b

    private int score = 0;
    private int highScore = 0;

    void Start()
    {
<<<<<<< HEAD
        gameOverTexto.gameObject.SetActive(false);
        recordetexto.gameObject.SetActive(true); // Certifique-se de que o scoreText está ativo
        melhorrecordetexto.gameObject.SetActive(true); // Certifique-se de que o highScoreText está ativo
=======
        gameOverText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true); // Certifique-se de que o scoreText está ativo
        HighScoreText.gameObject.SetActive(true); // Certifique-se de que o highScoreText está ativo
>>>>>>> 76d0feb368aff55e530ffb4a9da007192eae708b
        UpdateScore(0);
    }

    public void UpdateScore(int points)
    {
        score += points;
<<<<<<< HEAD
        recordetexto.text = "SCORE: " + score.ToString();
=======
        ScoreText.text = "SCORE: " + score.ToString();
>>>>>>> 76d0feb368aff55e530ffb4a9da007192eae708b

        if (score > highScore)
        {
            highScore = score;
<<<<<<< HEAD
            melhorrecordetexto.text = "HIGH SCORE: " + highScore.ToString();
        }
    }

    public void GameOver()
    {
        gameOverTexto.gameObject.SetActive(true);
    }

    public void Restart()
    {
        score = 0;
        UpdateScore(0);
        Cobra.Restart();
        gameOverTexto.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        float width, height, speed;

        if (TryGetInputValues(out width, out height, out speed))
        {
            // Configura as dimensões da área de jogo
            Cobra.cobra.colocartamanhodaarea(width, height);
            Cobra.cobra.Colocarvelocidade(speed);

            // Desativa o painel, os InputFields e o StartButton
            panel.SetActive(false);
            widthInput.gameObject.SetActive(false);
            heightInput.gameObject.SetActive(false);
            speedInput.gameObject.SetActive(false);
            startButton.SetActive(false);
        }
        else
        {
            Debug.LogError("Por favor, insira valores válidos.");
        }
    }

    private bool TryGetInputValues(out float width, out float height, out float speed)
    {
        width = height = speed = 0;

        // Valida cada entrada
        string[] inputs = { widthInput.text, heightInput.text, speedInput.text };
        float[] values = { width, height, speed };

        for (int i = 0; i < inputs.Length; i++)
        {
            if (!float.TryParse(inputs[i], out values[i]))
            {
                return false; // Retorna falso se qualquer entrada for inválida
            }
        }

        // Atribui os valores de volta às variáveis de saída
        width = values[0];
        height = values[1];
        speed = values[2];

        return true; // Retorna verdadeiro se todos os valores forem válidos
=======
            HighScoreText.text = "HIGH SCORE: " + highScore.ToString();
        }
>>>>>>> 76d0feb368aff55e530ffb4a9da007192eae708b
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        score = 0;
        UpdateScore(0);
        Cobra.Restart();
        gameOverText.gameObject.SetActive(false);
    }

}
  


