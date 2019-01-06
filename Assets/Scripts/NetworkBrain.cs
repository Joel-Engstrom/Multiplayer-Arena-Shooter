using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkBrain : NetworkManager
{
    public void StartHosting()
    {
        NetworkServer.Reset();
        base.StartHost();
    }

    private void OnEnable()
    {
        RefreshMatches();
    }

    private void RefreshMatches()
    {
        if (matchMaker == null)
        {
            StartMatchMaker();
        }
        matchMaker.ListMatches(0, 10, "", true, 0, 0, HandleListMatchesComplete);
    }

    private void HandleListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        AvailableMatchesList.HandleNewMatchList(matchList);
    }
}