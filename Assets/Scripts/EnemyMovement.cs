using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    [SerializeField] GameObject sliderr;
    [SerializeField] Transform sliderHealth;
    GameObject localSlider;
    Vector2 health = new Vector2(1f, 1f);
    Player player;

    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        sliderHealth.localScale = health;
        localSlider = Instantiate(sliderr, sliderPosition(), Quaternion.identity);
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Health();

        localSlider.GetComponent<Rigidbody2D>().velocity = myRigidBody.velocity;

        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private Vector2 sliderPosition()
    {
        Vector2 sliderPosition = new Vector2(myRigidBody.position.x, myRigidBody.position.y + 1f);
        return sliderPosition;
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        health = new Vector2(health.x - 0.3f, health.y);
        player.rigidbod.velocity = player.deathKick;
    }*/


    public void Health()
    {
        sliderHealth.localScale = health;
        if (sliderHealth.localScale.x < 0)
        {
            Destroy(localSlider);
            Destroy(gameObject);
        }
    }
}
