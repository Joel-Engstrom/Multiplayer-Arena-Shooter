using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using TMPro;

public class NetworkBrain : NetworkManager
{
    public TMP_InputField MatchName;
    public TMP_InputField MaxPlayers;

    private int MaxPlayersInt;

    public void StartHosting()
    {
        StartMatchMaker();
        if (MatchName != null && MaxPlayers != null)
        {
            if (int.TryParse(MaxPlayers.text, out MaxPlayersInt))
            {
                MaxPlayersInt = int.Parse(MaxPlayers.text);
            }
            else
            {
                MaxPlayersInt = 4;
            }
        }
        matchMaker.CreateMatch(MatchName.text, (uint)MaxPlayersInt, true, "", "", "", 0, 0, OnMatchCreated);
    }

    private void OnMatchCreated(bool success, string extendedInfo, MatchInfo responseData)
    {
        base.StartHost(responseData);
    }

    private void Awake()
    {
        InvokeRepeating("RefreshMatches", 0f, 3f);
    }

    private void RefreshMatches()
    {
        if (matchMaker == null)
            StartMatchMaker();

        matchMaker.ListMatches(0, 10, "", false, 0, 0, HandleListMatchesComplete);
    }

    private void HandleListMatchesComplete(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        AvailableMatchesList.HandleNewMatchList(matchList);
    }

    public void JoinMatch(MatchInfoSnapshot match)
    {
        if (matchMaker == null)
            StartMatchMaker();

        matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, HandleJoinedMatch);
    }

    private void HandleJoinedMatch(bool success, string extendedInfo, MatchInfo responseData)
    {
        StartClient(responseData);
    }
}