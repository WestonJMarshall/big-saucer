using System;
using UnityEngine;

public class Cow : MonoBehaviour
{
    public event EventHandler OnDestruction;

    public bool captured = false;
    private void Start()
    {
        captured = false;
        Invoke(nameof(DestroyCow), 22.0f);
    }

    private void DestroyCow()
    {
        if (!captured)
        {
            OnDestruction.Invoke(this, null);
            Destroy(gameObject);
        }
        else
        {
            Invoke(nameof(DestroyCow), 5.0f);
        }
    }
}
