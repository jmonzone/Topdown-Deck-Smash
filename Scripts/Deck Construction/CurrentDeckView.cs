using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDeckView : MonoBehaviour
{
    [SerializeField] private Transform cardViewParent;

    private List<Image> cardImages = new List<Image>();

    private void Start()
    {
        cardViewParent.GetComponentsInChildren(includeInactive: true, cardImages);
        UpdateCurrentDeckView();
        GameManager.Instance.OnAbilitiesChanged += UpdateCurrentDeckView;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnAbilitiesChanged -= UpdateCurrentDeckView;
    }

    private void UpdateCurrentDeckView()
    {
        Debug.Log("Updating current deck view.");
        int i = 0;
        foreach (var ability in GameManager.Instance.Abilities)
        {
            var sprite = Resources.Load<Sprite>("Ability Sprites/" + ability.ToString());
            cardImages[i].sprite = sprite;
            i++;
        }
    }
}
