using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking.Match;

public class JoinButton : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(MatchInfoSnapshot match)
    {
        text.text = match.name;
    }
}
