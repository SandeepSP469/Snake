using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;

    public Vector2 direction = Vector2.right;
    public int initialSize = 4;

    public float speed = 20f;
    public float speedMultiplier = 1f;

    private float nextUpdate;
    public GameObject pauseMenuScreen;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Vector2.down;
            }
        }
        
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        
        if (Time.time < nextUpdate) {
            return;
        }

        
        for (int i = segments.Count - 1; i > 0; i--) {
            segments[i].position = segments[i - 1].position;
        }

        
        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(x, y);
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void ResetState()
    {
        
        direction = Vector2.right;
        transform.position = Vector3.zero;

        
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        
        segments.Clear();
        segments.Add(transform);

        
        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food")) { 
            Grow();
        } else if (other.gameObject.CompareTag("Obstacle")) {
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
        }
    }

    public void PauseGame()
    {
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ScoreController.scoreValue = 0;
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ScoreController.scoreValue = 0;
    }
}
