using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class TankHealth : NetworkBehaviour
{
    [SyncVar]
    public int health = 3;
    [SyncVar(hook = nameof(OnHealthChanged))] public int currentHealth;


    public void TakeDamage(int amount, GameObject attacker)
    {
        if (currentHealth <= 0) return;
        if (!isServer) return;

        health -= amount;
        Debug.Log("It actually took damage");
        if (health <= 0)
        {
            if(attacker != null)
            {
                PlayerStats killerStats = attacker.GetComponent<PlayerStats>();
                if (killerStats != null)
                {
                    killerStats.AddKill();
                }
            }
            // Aqui va un posible respawn. (todavia hay que hacer eso)
            Debug.Log($"{gameObject.name} murió.");
            NetworkServer.Destroy(gameObject);
        }
    }

    void OnHealthChanged(int oldValue, int newValue) 
    { 
        //Aqui va la update a la UI de vida que definitivamente voy a ahcer despues
    }
}
