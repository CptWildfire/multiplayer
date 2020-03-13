using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public abstract class NM_Synchronize : MonoBehaviour, IPunObservable
{
    [SerializeField] protected PhotonView myID;

    protected abstract void OnLocal();
    protected abstract void OnOnline();
    public abstract void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info);
}
