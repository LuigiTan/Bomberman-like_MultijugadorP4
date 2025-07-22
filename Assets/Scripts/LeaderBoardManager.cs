using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using TMPro;

public class LeaderBoardManager : NetworkBehaviour
{
    public static LeaderBoardManager Instance;

    [SerializeField] private Transform leaderboardParent;
    [SerializeField] private GameObject entryPrefab;

    private List<PlayerStats> players = new List<PlayerStats>();

    void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(PlayerStats player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    public void UpdateLeaderboard()
    {
        if (!isServer) return;

        // De mayor kills a menor
        players = players.OrderByDescending(p => p.kills).ToList();

        RpcUpdateLeaderboard(players.Select(p => $"{p.name} - {p.kills} Kills").ToArray());
    }

    [ClientRpc]
    void RpcUpdateLeaderboard(string[] lines)
    {
        // Elimina anteriores
        foreach (Transform child in leaderboardParent)
            Destroy(child.gameObject);

        foreach (string line in lines)
        {
            GameObject entry = Instantiate(entryPrefab, leaderboardParent);
            entry.GetComponent<TMP_Text>().text = line;
        }
    }
}
