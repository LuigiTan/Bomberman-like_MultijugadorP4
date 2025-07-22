using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class TankHealth : NetworkBehaviour
{
    [SyncVar]
    public int maxHealth = 3;
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int currentHealth;

    private TankRespawnManager respawnManager;

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
        respawnManager = GetComponent<TankRespawnManager>();
    }
    public void TakeDamage(int amount, GameObject attacker)
    {
        if (!isServer || currentHealth <= 0)
        {
            Debug.Log("La vida no fue menos que 0");
            Debug.Log(" O, El server esta dando pedos");
            return;
        }

        currentHealth -= amount;
        Debug.Log("It actually took damage");


        if (currentHealth <= 0)
        {
            if (attacker != null)
            {
                PlayerStats killerStats = attacker.GetComponent<PlayerStats>();
                if (killerStats != null)
                {
                    killerStats.AddKill();
                }
            }
            // Aqui va un posible respawn. (todavia hay que hacer eso) <- Ya no 
            Debug.Log($"{gameObject.name} murió.");
            //NetworkServer.Destroy(gameObject);

            // En vez de destruir. Checo si hay un manager y uso la funcion para iniciar el respawn
            respawnManager?.StartRespawn();
        }
    }

    void OnHealthChanged(int oldValue, int newValue)
    {
        //Aqui va la update a la UI de vida que definitivamente voy a ahcer despues
    }

    [Server]
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
    }
}
