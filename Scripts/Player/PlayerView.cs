using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : PhotonObject
{
    #region References
    [SerializeField] private SpriteRenderer colorToChange;

    
    private readonly Color[] colors = new Color[4]
    {
        Color.yellow,
        Color.green,
        Color.blue,
        Color.red
    };
    #endregion

    #region Monobehaviour Callbacks
    private void Awake()
    {
        Deck = GetComponent<AbilityDeck>();
        colorToChange.color = colors[photonView.CreatorActorNr - 1];
    }

    
    #endregion

    #region Player Interactions
    public void Init(List<AbilityId> abilityTypes, int lives)
    {
        Deck.Init(abilityTypes);
    }
    #endregion

    #region Properties
    public string Nickname { get; set; }
    public AbilityDeck Deck { get; private set; }
    
    #endregion

}
