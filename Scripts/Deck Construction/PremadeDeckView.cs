using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremadeDeckView : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<PremadeDeck> premadeDecks;


    private void Awake()
    {
        for(int i = 0; i < 3; i++)
        {
            var temp = i;
            buttons[i].onClick.AddListener(() => ChangeToPremadeDeck(temp));
        }
    }

    private void ChangeToPremadeDeck(int i)
    {
        GameManager.Instance.SetAbilities(new DeckData(premadeDecks[i]));
    }
}
