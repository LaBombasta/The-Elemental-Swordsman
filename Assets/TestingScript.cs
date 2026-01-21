using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    //private PlayerStats playerStats;
    protected Animator animator;
    protected Rigidbody2D rb;
    private Vector2 movement;

    private void Start()
    {
        //playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        Debug.Log(movement);
        //rb.velocity = movement * playerStats.MoveSpeed;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        if (movement != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }
    }
}
