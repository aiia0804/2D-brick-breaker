using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Paddle : MonoBehaviour
{
    //configuration paramaters
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 18f;
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float Speed = 10f;
    [SerializeField] GameObject BallPrefab;

    //cached references
    Ball theBall;
    GameStatus theGameStatus;
    float ballStartPosY;
    Quaternion ballRotation;


    void Start()
    {
        theBall = FindObjectOfType<Ball>();
        theGameStatus = FindObjectOfType<GameStatus>();
        ballStartPosY = theBall.transform.position.y;
        ballRotation = theBall.transform.rotation;
    }

    void Update()
    {
        float positoin = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        Vector2 paddlePosit = new Vector2(transform.position.x, transform.position.y);
        paddlePosit.x = Mathf.Clamp(GetXpos(), minX, maxX);

        transform.position = paddlePosit;
    }

    // For debug purose
    private float GetXpos()
    {
        if (theGameStatus.IsAutoPlayEnabled())
        {
            return theBall.transform.position.x;
        }

        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }

    public void CreatANewBall()
    {
        if (theGameStatus.ballQty <= 0) { return; }
        var ballX = this.transform.position.x;
        var ballPOs = new Vector2(ballX, ballStartPosY);
        Instantiate(BallPrefab, ballPOs, ballRotation);
    }
}
