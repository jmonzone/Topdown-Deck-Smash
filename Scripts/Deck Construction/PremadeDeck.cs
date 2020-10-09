using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DeckData {
    public List<AbilityId> abilities;

    public DeckData(PremadeDeck premadeDeck)
    {
        abilities = premadeDeck.abilities;
    }
}


[CreateAssetMenu(menuName = "ScriptableObjects/PremadeDeck", order = 1)]
public class PremadeDeck : ScriptableObject
{
    public List<AbilityId> abilities;
}