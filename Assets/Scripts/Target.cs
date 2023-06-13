using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHittable
{
    public void TakeHit(int i)
    {
        Destroy(gameObject);
    }
}
