using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Mirror;



public class TankRespawnManager : NetworkBehaviour
{
    public float respawnDelay = 3f;

    private Vector3 initialSpawnPoint;
    private Quaternion initialRotation;
    private TankHealth health;

    private PlayerTanksController movement;
    private TankShooting shooting;
    private Rigidbody rb;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        // Guardamos posición inicial desde el punto de vista del cliente con autoridad
        initialSpawnPoint = transform.position;
        initialRotation = transform.rotation;
    }
    private void Awake()
    {
        health = GetComponent<TankHealth>();
        

        movement = GetComponent<PlayerTanksController>();
        shooting = GetComponent<TankShooting>();
        rb = GetComponent<Rigidbody>();
    }

    [Server]
    public void StartRespawn()
    {
        RpcHandleDeathVisuals(false); // Apagar visuales (que no existen xd) en los clientes
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        // Desactivar movimiento y colisiones
        RpcSetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        // Restaurar estado a la posision donde spawneo para empezar
        transform.position = initialSpawnPoint;
        transform.rotation = initialRotation;
        health.RestoreFullHealth();

        // Reactivar visuals y colisiones
        RpcSetActive(true);
        RpcHandleDeathVisuals(true);
    }

    [ClientRpc]
    void RpcSetActive(bool value)
    {
        // Aqui se apagan las fisicas, input, renderers, colliders, etc. etc.
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = value;

        foreach (var rend in GetComponentsInChildren<Renderer>())
            rend.enabled = value;

        // Fisica
        //if (rb != null && value)//En teoria esto deberia evitar bugs, ojala no los genere 
        //{
        //    rb.linearVelocity = Vector3.zero;
        //    rb.angularVelocity = Vector3.zero;
        //}
        //No funciono, miren, ni siquiera tengo con quier testearlo porque soy un solo chango, si salen errores de este tipo me lavo las manos al respecto.
        if (rb != null) rb.isKinematic = !value;

        // Movimiento y disparo
        if (movement != null) movement.enabled = value;
        if (shooting != null) shooting.enabled = value;
    }

    [ClientRpc]
    void RpcHandleDeathVisuals(bool isAlive)
    {
        // Aqui se mostrarian efectos de muerte o respawn (explosion, flash, etc)
        // La neta no lo voy a hacer ahorita nmms 
       
    }
}
