using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public event EventHandler<Cow> OnCowHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Cow>() != null && !collision.gameObject.GetComponent<Cow>().captured)
        {
            OnCowHit.Invoke(this, collision.gameObject.GetComponent<Cow>());
        }
    }
}
