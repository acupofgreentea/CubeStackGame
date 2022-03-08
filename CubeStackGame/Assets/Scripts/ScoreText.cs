using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private int score;
    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
