using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class PlayerStats : NetworkBehaviour
{
    //Con el objetivo de hacer el unico power up que va a haber, este scrip va a ser modificiado. Voy a marcar con comentarios todo lo extra para que en
    //caso de que todo truene, todavia pueda quitarlo y hechar para atras
    //Pensandolo bien voy a hacerlo toda una seccion 

    #region Modify Bullet Life for PowerUp

    [SyncVar]
    public int BaseBulletLife = 1;

    private int extraShotsRemaining = 0;

    //Para modificarse en un posible power up mas poderoso (no creo)
    public int ExtraPenetrationAmmount = 1;

    public int GetCurrentBulletLife()
    {
        // Si tiene power-up activo, suma +1
        return BaseBulletLife + (extraShotsRemaining > 0 ? ExtraPenetrationAmmount : 0);
    }


    public void UseBullet()
    {
        if (extraShotsRemaining > 0)
        {
            extraShotsRemaining--;
            if (extraShotsRemaining == 0)
            {
                Debug.Log("Se acabo el efecto del power-up."); //Mas bien, aqui deberia decir, "no hay powerup en efecto", pero es por mientras.
            }
        }
    }

    // Llamado por el power-up
    public void ActivateTemporaryPowerup(int extraShots)
    {
        extraShotsRemaining = extraShots;
        Debug.Log($"Power-up activado por {extraShots} disparos.");
    }


    #endregion



    [SyncVar(hook = nameof(OnKillsChanged))] public int kills = 0;

    public override void OnStartServer()
    {
        LeaderBoardManager.Instance?.RegisterPlayer(this);
        LeaderBoardManager.Instance?.UpdateLeaderboard();
        base.OnStartServer();
        
    }
    public void AddKill()
    {
        kills++;
        Debug.Log($"{gameObject.name} tiene {kills} kills.");
        LeaderBoardManager.Instance?.UpdateLeaderboard(); // Deberia actualizar a todos.
    }


    void OnKillsChanged(int oldVal, int newVal)
    {
        // La idea era dar algun feedback por la kill pero por ahorita la leaderboard es suficiente
    }
}
