using UnityEngine;
using UnityEngine.UI;

public class PanelView : MonoBehaviour
{
    [SerializeField] private GameObject lobbyPanel;
    [SerializeField] private GameObject deckPanel;

    [SerializeField] private Button deckPanelButton;
    [SerializeField] private Button lobbyPanelButton;

    private GameObject currentActivePanel;

    private void Awake()
    {
        deckPanelButton.onClick.AddListener(() => SwitchPanel(deckPanel));
        lobbyPanelButton.onClick.AddListener(() => SwitchPanel(lobbyPanel));

        currentActivePanel = lobbyPanel;
    }

    private void SwitchPanel(GameObject panelToActivate)
    {
        currentActivePanel.SetActive(false);
        currentActivePanel = panelToActivate;
        panelToActivate.gameObject.SetActive(true);
    }
}
