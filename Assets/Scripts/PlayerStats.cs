using System.Collections.Generic;
using UnityEngine;
using Mirror;



public class PlayerStats : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnKillsChanged))] public int kills = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();
        LeaderBoardManager.Instance?.RegisterPlayer(this);
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
