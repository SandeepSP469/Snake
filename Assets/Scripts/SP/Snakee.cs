using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snakee : MonoBehaviour
{
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;

    public Vector2 direction = Vector2.right;
    public int initialSize = 4;

    public float speed = 20f;
    public float speedMultiplier = 1f;

    private float nextUpdate;
    public GameObject pauseMenuScreen;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {

        if(direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = Vector2.down;
            }
        } 
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
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


        Movement();
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

    public void Movement()
    {
        float x = Mathf.Round(transform.position.x) + direction.x;
        {
            if (x > maxX)
            {
                x = minX;
            }
            if (x < minX)
            {
                x = maxX;
            }
        }
        float y = Mathf.Round(transform.position.y) + direction.y;
        {

            if (y > maxY)
            {
                y = minY;
            }
            if (y < minY)
            {
                y = maxY;
            }
        }
        transform.position = new Vector2(x, y);
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food")) { 
            Grow();
        } else if (other.gameObject.CompareTag("Obstacle")) {
            PlayerManagerr.isGameOver = true;
            gameObject.SetActive(false);
       // } else if (other.gameObject.CompareTag("Player")){
         //   PlayerManagerr.isGameOver = true;
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
        ScoreControllerr.scoreValue = 0;
    }

    public void ResumeGame()
    {
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        ScoreControllerr.scoreValue = 0;
    }

    public void Pickupkey()
    {
        Debug.Log("Key picked up");
        ScoreControllerr.scoreValue += 10;
    }
}
