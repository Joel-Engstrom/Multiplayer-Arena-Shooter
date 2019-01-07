using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking.Match;

public class JoinButton : MonoBehaviour
{
    private TextMeshProUGUI text;
    private MatchInfoSnapshot match;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(MatchInfoSnapshot match, Transform panelTransform)
    {
        this.match = match;
        text.text = match.name;
        transform.SetParent(panelTransform);
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
    }

    public void JoinMatch()
    {
        FindObjectOfType<NetworkBrain>().JoinMatch(match);
    }
}
