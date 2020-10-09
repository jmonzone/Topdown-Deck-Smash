using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public static class PlayerPrefsKeys
{
    public const string NICKNAME_KEY = "nickname";
}

public class JoinRoom : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        if (!PhotonNetwork.IsConnected) PhotonNetwork.ConnectUsingSettings();
    }

    public void SetNickname(string nickname)
    {
        PhotonNetwork.NickName = nickname;
        PlayerPrefs.SetString(PlayerPrefsKeys.NICKNAME_KEY, nickname);
    }

    public void Join()
    {

        RoomOptions options = new RoomOptions
        {
            IsOpen = true,
            IsVisible = true,
            EmptyRoomTtl = 0,
            PublishUserId = true,
        };

        PhotonNetwork.JoinOrCreateRoom("Test", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(1);
    }
}
