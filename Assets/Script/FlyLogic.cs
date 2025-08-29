using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLogic : MonoBehaviour
{
    public float flyForce;
    private Rigidbody2D rb2d;
    public GameOver game;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FLY();
    }

    void FLY()
    {
        if (Input.GetMouseButton(0))
        {
            rb2d.linearVelocity = Vector2.up * flyForce;
        }

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            AudioManager.Instance?.PlayFlap();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance?.PlayHit();
        game.GamerOverActive();
    }
}
