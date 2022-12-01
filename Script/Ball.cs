using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] float xForce = 3f;
    [SerializeField] float yForce;
    [SerializeField] AudioClip[] ballSound;
    [SerializeField] float randomFactor = 0.2f;



    Vector2 paddleToBallVector;
    bool hasStarted = false;
    Level level;

    // Get Audio Component
    AudioSource myAudiosource;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        level = FindObjectOfType<Level>();
        paddle1 = FindObjectOfType<Paddle>();
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudiosource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        LevelCheck();
    }

    public void LevelCheck()
    {
        yForce = yForce * (1 + (level.currentLevel / 10));
        Debug.Log("Yforce: " + yForce);
        myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, yForce);
    }

    void Update()
    {
        if (hasStarted == false)
        {
            lockTheBall();
            launchTheBall();
        }
        if (hasStarted)
        {
            AdjustBallDirection();
        }
    }

    private void AdjustBallDirection()
    {
        Vector3 dir = myRigidBody2D.velocity.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void launchTheBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xForce, yForce);
        }
    }

    void ShutTheBall(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xForce, yForce);
        }
    }

    private void lockTheBall()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(UnityEngine.Random.Range(0f, randomFactor), UnityEngine.Random.Range(0f, randomFactor));
        // 讓球會彈向不同角度，不會卡住
        if (collision.gameObject.tag == "UpperWall")
        {
            velocityTweak += new Vector2(5, 0);
        }
        if (hasStarted)
        {
            AudioClip clip = ballSound[UnityEngine.Random.Range(0, ballSound.Length)];
            for (var i = 0; i < ballSound.Length; i++)
            {
                clip = ballSound[i];
            }
            myAudiosource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
