using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoseColider : MonoBehaviour
{
    GameStatus status;
    Paddle paddle;
    private void Start()
    {
        status = FindObjectOfType<GameStatus>();
        paddle = FindObjectOfType<Paddle>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        status.BallDestroyed();
        paddle.CreatANewBall();
    }

}
