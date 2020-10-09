using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JoinRoomView : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    [SerializeField] private InputField nicknameInput;
    [SerializeField] private Button singleplayerButton;
    [SerializeField] private Button multiplayerButton;

    private void Awake()
    {
        nicknameInput.text = PlayerPrefs.GetString(PlayerPrefsKeys.NICKNAME_KEY, "Player " + Random.Range(100,1000));

        var joinRoom = GetComponent<JoinRoom>();
        joinRoom.SetNickname(nicknameInput.text);
        nicknameInput.onValueChanged.AddListener((value) => joinRoom.SetNickname(value));
        singleplayerButton.onClick.AddListener(() =>
        {
            GameManager.Instance.numComputers = 1;
            joinRoom.Join();
        });
        multiplayerButton.onClick.AddListener(() =>
        {
            GameManager.Instance.numComputers = 0;
            joinRoom.Join();
        });
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        singleplayerButton.interactable = multiplayerButton.interactable = true;
    }
}
