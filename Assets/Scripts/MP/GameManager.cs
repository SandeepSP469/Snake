using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] Movement[] players;
    [SerializeField] UIController canvas;
    [SerializeField] Collectibles collectibles;
    [SerializeField] GameObject pauseMenuScreen;
    int powerUpCooldownTimer = 3;
    int gainerFoodScore = 20;
    int burnerFoodScore = 10;
    int lengthIncreasePerGainer = 1;
    int[] multipliers;
    int[] scores;
    void Start()
    {
        scores = new int[players.Length];
        multipliers = new int[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            scores[i] = 0;
            multipliers[i] = 1;
        }
        canvas.UpdateScore(scores);
        List<Transform> playerBodyTransforms = GetPlayerBodyTransforms();
        StartCoroutine(SpawnFood());
        StartCoroutine(SpawnPowerUp());
    }
    IEnumerator SpawnPowerUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 16));
            collectibles.SpawnPowerUp(GetPlayerBodyTransforms());
        }
    }
    IEnumerator SpawnFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            collectibles.SpawnFood(GetPlayerBodyTransforms());
        }
    }
    List<Transform> GetPlayerBodyTransforms()
    {
        List<Transform> playerBodyTransform = new List<Transform>();
        for (int i = 0; i < players.Length; i++)
        {
            playerBodyTransform.AddRange(players[i].GetBodiesTransform());
        }
        return playerBodyTransform;
    }
    public void FoodConsumed(Player _player, FoodType _foodType)
    {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].player == _player)
                {
                    if (_foodType == FoodType.Gainer || players[i].GetPlayerLength() < 6)
                    {
                        for (int j = 0; j < lengthIncreasePerGainer; j++)
                            players[i].AddBody();
                        scores[i] += gainerFoodScore * multipliers[i];
                    }
                    else if (_foodType == FoodType.Burner || players[i].GetPlayerLength() < 6)
                {
                        for (int j = 0; j < lengthIncreasePerGainer; j++)
                            players[i].RemoveBody();
                        scores[i] -= burnerFoodScore;
                    }
                    break;
                }
            }

        canvas.UpdateScore(scores);
    }
    public void PowerUpConsumed(Player _player, PowerUp _powerUpType)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].player == _player)
            {
                if (_powerUpType == PowerUp.Multiplier)
                {
                    StartCoroutine(DoubleScore(i));
                    break;
                }
                players[i].PowerUps(_powerUpType, powerUpCooldownTimer);
            }
        }
    }
    IEnumerator DoubleScore(int index)
    {
        multipliers[index] = 2;
        yield return new WaitForSeconds(powerUpCooldownTimer);
        multipliers[index] = 1;
    }

    public void GameOver(Player player)
    {
        Time.timeScale = 0;
        canvas.GameOverUI(scores, player);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);

    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);

    }
}
