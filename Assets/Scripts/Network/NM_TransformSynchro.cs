using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_TransformSynchro : NM_NetworkItem
{
    [SerializeField] bool synchroPosition = false;
    [SerializeField] bool synchroRotation = false;
    [SerializeField] bool synchroScale = false;

    Vector3 localMovement = Vector3.zero;
    Vector3 localRotation = Vector3.zero;
    Vector3 localScale = Vector3.zero;


    protected override void OnLocal()
    {
        if (synchroPosition) localMovement = transform.position;
        if (synchroRotation) localRotation = transform.eulerAngles;
        if (synchroScale) localScale = transform.localScale;
    }
    protected override void OnOnline()
    {
        if (synchroPosition) transform.position = Vector3.MoveTowards(transform.position, localMovement, Time.deltaTime * 20);
        if (synchroRotation) transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, localRotation, Time.deltaTime * 200);
        if (synchroScale) transform.localScale = Vector3.MoveTowards(transform.localScale, localScale, Time.deltaTime * 20);
    }
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (synchroPosition)
            {
                stream.SendNext(localMovement.x);
                stream.SendNext(localMovement.y);
                stream.SendNext(localMovement.z);
            }

            if (synchroRotation)
            {
                stream.SendNext(localRotation.x);
                stream.SendNext(localRotation.y);
                stream.SendNext(localRotation.z);
            }

            if (synchroScale)
            {
                stream.SendNext(localScale.x);
                stream.SendNext(localScale.y);
                stream.SendNext(localScale.z);
            }

        }
        else
        {
            if (synchroPosition)
            {
                localMovement.x = (float)stream.ReceiveNext();
                localMovement.y = (float)stream.ReceiveNext();
                localMovement.z = (float)stream.ReceiveNext();
            }

            if (synchroRotation)
            {
                localRotation.x = (float)stream.ReceiveNext();
                localRotation.y = (float)stream.ReceiveNext();
                localRotation.z = (float)stream.ReceiveNext();
            }

            if (synchroScale)
            {
                localScale.x = (float)stream.ReceiveNext();
                localScale.y = (float)stream.ReceiveNext();
                localScale.z = (float)stream.ReceiveNext();
            }
        }
    }
}
