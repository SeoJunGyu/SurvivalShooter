using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    int score = 0;

    public void AddScore()
    {
        score += 10;
        scoreText.text = $"Score : {score}";
    }
}
