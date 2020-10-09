using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PremadeDeck defaultDeck;

    public int numComputers;

    public List<AbilityId> Abilities { get; private set; } = new List<AbilityId>();

    public event Action OnAbilitiesChanged;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var jsonAbilities = PlayerPrefs.GetString("PreferredDeck", JsonUtility.ToJson(new DeckData(defaultDeck)));
        Abilities = JsonUtility.FromJson<DeckData>(jsonAbilities).abilities;
    }

    public void SetAbilities(DeckData deck)
    {
        Abilities = deck.abilities;
        PlayerPrefs.SetString("PreferredDeck", JsonUtility.ToJson(deck));
        OnAbilitiesChanged?.Invoke();
    }

    //private void InitAbilities()
    //{
    //    for (int i = 0; i < 24; i++)
    //    {
    //        var rand = Random.Range(0f, 1f);

    //        if (rand > .90f) Abilities.Add(AbilityId.Dash);
    //        else if (rand > .50f) Abilities.Add(AbilityId.Dark_Pulse);
    //        else Abilities.Add(AbilityId.Arrow);
    //    }
    //}
}
