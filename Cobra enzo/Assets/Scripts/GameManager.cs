using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour // Classe responsavel por gerenciar o estado do jogo, como pontuacao e game over.
{
    public Cobra cobra; // Referencia a cobra no jogo.
    public TextMeshProUGUI ScoreText; // Texto para exibir a pontuacao atual.
    public TextMeshProUGUI HighScoreText; // Texto para exibir a maior pontuacao.
    public TextMeshProUGUI gameOverText; // Texto exibido quando o jogo termina.

    private int score = 0; // Pontuacao atual do jogador.
    private int highScore = 0; // Maior pontuacao registrada.
    public TMP_InputField widthInput;// Campo de entrada para a largura da area de jogo.
    public TMP_InputField heightInput;// Campo de entrada para a altura da area de jogo.
    public TMP_InputField speedInput;// Campo de entrada para a velocidade da cobra.
    public GameObject startButton; // Botao de inicio do jogo.
    public GameObject panel; // Painel do menu inicial.

    
    public void StartGame() // Metodo responsavel por iniciar o jogo ao configurar os parametros e esconder o menu.
    {
        float width, height, speed;

        if (pegarvalores(out width, out height, out speed))
        {
            // Configura as dimensões da área de jogo
            cobra.SetGameArea(width, height);
            cobra.colocarvelocidade(speed);

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
    private bool pegarvalores(out float width, out float height, out float speed) // Metodo que tenta obter e validar os valores de entrada do jogador.
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
    }
    void Start() // Metodo chamado no inicio do jogo. Inicializa o estado inicial do HUD.
    {
        gameOverText.gameObject.SetActive(false); // Esconde o texto de "game over".
        ScoreText.gameObject.SetActive(true); // Exibe o texto de  pontuacao.
        HighScoreText.gameObject.SetActive(true); // Exibe o texto de maior pontuacao.
        UpdateScore(0); // Inicia a pontuacao com 0.
    }

    public void UpdateScore(int points) // Metodo responsavel por atualizar a pontuacao do jogador.
    {
        score += points; // Adiciona pontos ao total.
        ScoreText.text = "SCORE: " + score.ToString(); // Atualiza o texto de pontuacao.

        if (score > highScore) // Se a pontuacao atual for maior que a maior pontuacao, atualiza o high score.
        {
            highScore = score;
            HighScoreText.text = "HIGH SCORE: " + highScore.ToString(); // Atualiza o texto de maior pontuacao.
        }
    }

    public void GameOver() // Metodo chamado quando o jogo termina.
    {
        gameOverText.gameObject.SetActive(true);// Exibe o texto de "game over".
    }

    public void Restart() // Metodo responsavel por reiniciar o jogo.
    {
        score = 0; // Reseta a pontuacao.
        UpdateScore(0); // Atualiza a pontuacao exibida.
        cobra.reiniciar(); // Reinicia a cobra.
        gameOverText.gameObject.SetActive(false); // Esconde o texto de "game over".
    }
}
