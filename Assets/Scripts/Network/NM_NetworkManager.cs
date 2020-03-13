using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string version = "2.0";
    //
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        RoomOptions _roomSettings = new RoomOptions()
        {
            MaxPlayers = 7
        };
        PhotonNetwork.JoinOrCreateRoom("O3D", _roomSettings, new TypedLobby("O3D", LobbyType.Default));
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined room O3D");
        PhotonView _id = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity).GetPhotonView();
        _id.name = _id.ViewID.ToString();
    }
    //
    private void Connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.AppVersion = version;
        PhotonNetwork.ConnectUsingSettings();
    }
    //
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }
    private void OnGUI()
    {
        GUILayout.Box(PhotonNetwork.NetworkClientState.ToString());
        GUILayout.Box(PhotonNetwork.IsMasterClient.ToString());
        GUILayout.Box(PhotonNetwork.CurrentRoom?.Name);
        GUILayout.Box($"{PhotonNetwork.CurrentRoom?.PlayerCount}/{PhotonNetwork.CurrentRoom?.MaxPlayers}");
        if(GUILayout.Button("Join"))
            Connect();
    }
}
