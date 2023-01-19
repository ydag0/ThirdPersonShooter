using UnityEngine;
using System;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]
public class Player : Character
{
    public static event Action PlayerDeath;
    private bool fallCheck;
    private float fallWaitTime = 2.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetPlayerHealth();

        speed = GameManager.Instance.gameSettings.speed;
        turninngSpeed = GameManager.Instance.gameSettings.turningSpeed;
        jumpForce = GameManager.Instance.gameSettings.jumpForce;
    }
    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    private void Update()
    {
        if (health <= 0 && dead == false)
        {
            InvokeDead();
        }
        //Check for fall
        if (transform.position.y < 0 && fallCheck == false)
            StartCoroutine(CheckForFall(fallWaitTime));
    }
    public bool CanJump()
    {
        return CheckForGrounded();
    }
    public void SetPlayerHealth()
    {
        SetHealth();
    }
    private IEnumerator CheckForFall(float waitTime)
    {
        fallCheck = true;
        yield return new WaitForSeconds(waitTime);
        if (transform.position.y < -.1f)
        {
            InvokeDead();
        }
        fallCheck = false;
    }
    private void InvokeDead()
    {
        OnPlayerDeath();
        PlayerDeath?.Invoke();
    }
}
