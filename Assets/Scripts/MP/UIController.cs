using UnityEngine;
using TMPro;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField] TMP_Text[] scores;
    [SerializeField] TMP_Text[] gameOverScore;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseScreenUI;
    [SerializeField] TMP_Text winnerText;
    CanvasGroup[] P1_powers;
    CanvasGroup[] P2_powers;
    public void UpdateScore(int[] _scores)
    {
        for (int i = 0; i < _scores.Length; i++)
        {
            scores[i].text = _scores[i].ToString();
        }
    }
    public void GameOverUI(int[] _scores, Player player)
    {
        int maxScore = 0;
        int playerid = 0;
        for (int i = 0; i < _scores.Length; i++)
        {
            gameOverScore[i].text = _scores[i].ToString();
            if (_scores[i] > maxScore)
            {
                maxScore = _scores[i];
                playerid = i + 1;
            }
        }
        if (_scores.Length > 1)
        {
            if (player == Player.Player1)
                winnerText.text = "Player " + 2 + " is the winner!";
            else
                winnerText.text = "Player " + 1 + " is the winner!";
            winnerText.gameObject.SetActive(true);
        }
        gameOverUI.SetActive(true);
    }
    public void PauseScreenUI()
    {
        if (pauseScreenUI.activeInHierarchy)
            pauseScreenUI.SetActive(false);
        else
            pauseScreenUI.SetActive(true);
    }
    IEnumerator PowerUpEnabled(CanvasGroup canvas, int timer)
    {
        canvas.alpha = 1f;
        yield return new WaitForSeconds(timer);
        Debug.Log("Timer over");
        canvas.alpha = 0.3f;
    }
}
