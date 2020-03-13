using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;

    private void Start()
    {
        //name = myID.ViewID.ToString();
        name = myID.Owner.NickName;
        if (myID.IsMine) GetComponent<MeshRenderer>().material.SetColor("_BaseColor", Color.red);
        //GetComponent<PhotonView>().RPC("SetColor", RpcTarget.OthersBuffered, 0);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
}
