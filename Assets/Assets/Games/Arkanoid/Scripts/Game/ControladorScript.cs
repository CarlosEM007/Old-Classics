using System;
using System.IO;
using TMPro;
using UnityEngine;

public class ControladorScript : MonoBehaviour
{
    [Header("Pontos")]
    [SerializeField] private TextMeshProUGUI HighScoreText;
    [SerializeField] private TextMeshProUGUI ScoreText;

    private int HighScore;
    private int Score;

    private string HighScoreFilePath = "HighScore.json";

    void Awake()
    {
        Score = 0;

        BuscarRecorde();
    }

    void OnApplicationQuit()
    {
        SalvarHighScore();
    }

    public void AtualizarPontos()
    {
        Score += 100;

        if (Score >= HighScore)
        {
            HighScore = Score;
            HighScoreText.text = HighScore.ToString();
        }

        ScoreText.text = Score.ToString();
    }

    private void BuscarRecorde()
    {
        if (!File.Exists(HighScoreFilePath))
        {
            var json0 = JsonUtility.ToJson(new Score { HighScore = "0" });
            File.WriteAllText(HighScoreFilePath, json0);
        }

        string json = File.ReadAllText(HighScoreFilePath);
        Score score = JsonUtility.FromJson<Score>(json);

        HighScore = Convert.ToInt32(score.HighScore);
        HighScoreText.text = HighScore.ToString();
    }

    private void SalvarHighScore()
    {
        string json = JsonUtility.ToJson(new Score { HighScore = HighScore.ToString() });
        File.WriteAllText(HighScoreFilePath, json);
    }
}

[System.Serializable]
public class Score
{
    public string HighScore;
}
