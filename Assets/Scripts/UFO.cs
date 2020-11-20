using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public event EventHandler OnAsteroidHit;

    public int health = 100;

    void Update()
    {
        if(Input.GetKey(KeyCode.W) && transform.position.y < 6)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            health -= 5;
            OnAsteroidHit.Invoke(this, null);
        }
    }
}
