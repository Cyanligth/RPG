using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReacter : MonoBehaviour, IListenable
{
    public void Listen(Transform transform)
    {
        gameObject.transform.LookAt(transform.position);
    }
}
