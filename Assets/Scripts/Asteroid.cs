using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public event EventHandler OnDestruction;
    public List<Sprite> spriteOptions;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteOptions[UnityEngine.Random.Range(0, spriteOptions.Count)];
        Invoke(nameof(DestroyAsteroid), 25.0f);
    }

    private void DestroyAsteroid()
    {
        OnDestruction.Invoke(this, null);
        Destroy(gameObject);
    }
}
