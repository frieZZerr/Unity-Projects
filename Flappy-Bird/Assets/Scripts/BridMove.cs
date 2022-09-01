using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridMove : MonoBehaviour
{
    public GameManager gameManager;
    public float velocity;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ( GameManager.gameStarted && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Time.timeScale != 0 )
        {
            Tap();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
    }

    public void Tap()
    {
        rb.velocity = Vector2.up * velocity;
        FindObjectOfType<AudioManager>().Play("Tap");
    }
}
