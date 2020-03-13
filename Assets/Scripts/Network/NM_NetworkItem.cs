using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_NetworkItem : NM_Synchronize
{

    protected virtual void Update()
    {
        if (myID.IsMine)
            OnLocal();
        else
            OnOnline();
    }

    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    protected override void OnLocal(){}
    protected override void OnOnline(){}
}
