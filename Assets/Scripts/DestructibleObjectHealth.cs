using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class DestructibleObjectHealth : NetworkBehaviour
{
    [SyncVar]
    public int health = 1; // Realmente por ahora todo va a tener esta vida

    [Server]
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}

