using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoosterr : MonoBehaviour
{
    public GameObject[] newPower;


    //private void Start()
    //{
      //  StartCoroutine(SpawnAfterTime());
    ///}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("Player"))
        //{
        //  ScoreController.scoreValue += 5;
        // Destroy(this.gameObject);
        //StartCoroutine(SpawnAfterTime());
        //
        //}
        if (collision.gameObject.GetComponent<Snakee>() != null)
        {
            Snakee snake = collision.gameObject.GetComponent<Snakee>();
            snake.Pickupkey();
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnAfterTime()
    {
        yield return new WaitForSeconds(10);
        GameObject nb = Instantiate(newPower[Random.Range(0, newPower.Length)], this.transform) as GameObject;
        nb.transform.localPosition = new Vector3(Random.Range(-21, 21),12, (0));
    }
}
