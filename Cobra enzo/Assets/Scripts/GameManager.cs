using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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

   
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;
    public TextMeshProUGUI gameOverText;


    private int score = 0;
    private int highScore = 0;

    void Start()
    {

        gameOverTexto.gameObject.SetActive(false);
        recordetexto.gameObject.SetActive(true); 
        melhorrecordetexto.gameObject.SetActive(true); 

        gameOverText.gameObject.SetActive(false);
        ScoreText.gameObject.SetActive(true); 
        HighScoreText.gameObject.SetActive(true); 

        UpdateScore(0);
    }

    public void UpdateScore(int points)
    {
        score += points;

        recordetexto.text = "SCORE: " + score.ToString();

        ScoreText.text = "SCORE: " + score.ToString();


        if (score > highScore)
        {
            highScore = score;

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
            
            Cobra.cobra.colocartamanhodaarea(width, height);
            Cobra.cobra.Colocarvelocidade(speed);

           
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

    
        string[] inputs = { widthInput.text, heightInput.text, speedInput.text };
        float[] values = { width, height, speed };

        for (int i = 0; i < inputs.Length; i++)
        {
            if (!float.TryParse(inputs[i], out values[i]))
            {
                return false;
            }
        }

       
        width = values[0];
        height = values[1];
        speed = values[2];

        return true; 
           
        }

    

   

}
  


