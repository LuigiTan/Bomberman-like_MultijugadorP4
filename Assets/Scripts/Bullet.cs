using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class Bullet : NetworkBehaviour
{
    [SyncVar]
    public int bulletlife = 1; // Lo voy a usar en un futuro para que se puedan hacer balas penetradoras

    [SyncVar]
    public GameObject owner;

    void OnTriggerEnter(Collider other)
    {
        if (!isServer) return;

        if (other.CompareTag("Indestructible"))
        {
            Debug.Log("Pego con una pared del limite, bye bala");
            NetworkServer.Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Destructible"))
        {
            Debug.Log("Pego con una destructible, se debera dañar");
            DestructibleObjectHealth destructible = other.GetComponent<DestructibleObjectHealth>();
            if (destructible != null)
                destructible.TakeDamage(1);

            bulletlife--;
        }

        if (other.CompareTag("Player"))
        {
            TankHealth health = other.GetComponent<TankHealth>();
            Debug.Log("Pego con un jugador");
            if (health != null)
            {
                Debug.Log("Y la vida no fue nula");
                health.TakeDamage(1, owner);
            }

            bulletlife--;
        }

        if (bulletlife <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
    [Server]
    public void SetOwner(GameObject shooter)
    {
        owner = shooter;
    }
}
