using System;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Component[] disablerExceptions;

    public void KillThisObject()
    {
        Destroy(gameObject);
    }
}
