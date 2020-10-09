using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityDeck : MonoBehaviour
{
    public List<PlayerAbility> abilities = new List<PlayerAbility>();
    private int abilityIndex;

    public List<PlayerAbility> storedAbilities = new List<PlayerAbility>();
    public Dictionary<string, PlayerAbility> comboAbilities = new Dictionary<string, PlayerAbility>();

    public PlayerAbility CurrentAbility => abilityIndex < abilities.Count ? abilities[abilityIndex] : null;
    public PlayerAbility GetAbility(int i) => abilityIndex + i < abilities.Count ? abilities[abilityIndex + i] : null;

    public event Action OnCurrentAbilityChanged;
    public event Action OnStoredAbilitiesChanged;

    public void Init(List<AbilityId> abilityTypes)
    {
        var player = GetComponent<PlayerView>();
        var abilityFactory = new AbilityFactory(player);

        var abilityDict = new Dictionary<AbilityId, PlayerAbility>();

        foreach (var type in abilityTypes)
        {
            if (abilityDict.ContainsKey(type)) abilities.Add(abilityDict[type]);
            else
            {
                var ability = abilityFactory.CreateAbility(type);
                abilities.Add(ability);
                abilityDict.Add(type, ability);
            }
            
        }

        comboAbilities = abilityFactory.GetComboAbilities(abilityTypes);
    }

    public bool Attack()
    {
        if (CurrentAbility == null) return false;
        
        CurrentAbility.Cast();
        abilityIndex++;
        OnCurrentAbilityChanged?.Invoke();

        return true;
    }

    public void Skip()
    {
        abilityIndex++;
        OnCurrentAbilityChanged?.Invoke();
    }


    public void StoreAbility()
    {
        if (storedAbilities.Count >= 3 || CurrentAbility == null) return;

        storedAbilities.Add(CurrentAbility);
        abilityIndex++;

        OnCurrentAbilityChanged?.Invoke();
        OnStoredAbilitiesChanged?.Invoke();
    }

    public void ReleaseStoredAbilities(Action onComplete)
    {
        if (storedAbilities.Count == 0) return;

        if (storedAbilities.Count == 1)
        {
            storedAbilities[0].Cast();
            storedAbilities = new List<PlayerAbility>();
            OnStoredAbilitiesChanged?.Invoke();
            return;
        }

        var sortedAbilities = storedAbilities.OrderBy(x => (int)(x.Id)).ToList();

        if (sortedAbilities.Count == 3 && sortedAbilities[1].Id == sortedAbilities[2].Id) sortedAbilities.Reverse();

        var comboString = string.Empty;
        foreach (var ability in sortedAbilities) comboString += ability.Id.ToString() + "-";
        comboString = comboString.Remove(comboString.Length - 1, 1);

        var semiComboString = string.Empty;
        for (int i = 0; i < 2; i++) semiComboString += sortedAbilities[i].Id.ToString() + "-";
        semiComboString = semiComboString.Remove(semiComboString.Length - 1, 1);

        if (comboAbilities.ContainsKey(comboString))
        {
            comboAbilities[comboString].Cast();
            storedAbilities = new List<PlayerAbility>();
            OnStoredAbilitiesChanged?.Invoke();
            onComplete();
        }
        else if (comboAbilities.ContainsKey(semiComboString)) StartCoroutine(ReleaseSemiStoredAbilities(comboAbilities[semiComboString], sortedAbilities[2], onComplete));
        else StartCoroutine(ReleaseStoredAbilityUpdate(onComplete));
    }

    public void Reload()
    {
        abilityIndex = 0;
        OnCurrentAbilityChanged?.Invoke();
    }

    //public void ReleaseStoredAbilities(Action onComplete)
    //{
    //    var sortedAbilities = storedAbilities.OrderBy(x => (int)(x.Id)).ToList();
    //    if (sortedAbilities.Count == 3 && sortedAbilities[1].Id == sortedAbilities[2].Id) sortedAbilities.Reverse();

    //    var comboString = string.Empty;
    //    foreach (var ability in sortedAbilities) comboString += ability.Id.ToString() + "-";
    //    comboString = comboString.Remove(comboString.Length - 1, 1);

    //    if (comboAbilities.ContainsKey(comboString))
    //    {
    //        comboAbilities[comboString].Cast();
    //        storedAbilities = new List<PlayerAbility>();
    //        OnStoredAbilitiesChanged?.Invoke();
    //        onComplete();
    //    }
    //    else
    //    {
    //        StartCoroutine(ReleaseStoredAbilityUpdate(onComplete));
    //    }
    //}

    private IEnumerator ReleaseStoredAbilityUpdate(Action onComplete)
    {
        var temp = storedAbilities.Count;
        for(int i = 0; i < temp; i++)
        {
            storedAbilities[0].Cast();
            storedAbilities.Remove(storedAbilities[0]);
            OnStoredAbilitiesChanged?.Invoke();
            yield return new WaitForSeconds(0.75f);
        }
        onComplete();
    }

    private IEnumerator ReleaseSemiStoredAbilities(PlayerAbility combo, PlayerAbility basic, Action onComplete)
    {
        basic.Cast();
        yield return new WaitForSeconds(0.5f);
        combo.Cast();

        storedAbilities = new List<PlayerAbility>();
        OnStoredAbilitiesChanged?.Invoke();
        onComplete();
    }
}
