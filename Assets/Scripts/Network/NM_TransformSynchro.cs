using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_TransformSynchro : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;
    [SerializeField] bool synchroPosition = false;
    [SerializeField] bool synchroRotation = false;
    [SerializeField] bool synchroScale = false;

    Vector3 localMovement = Vector3.zero;
    Vector3 localRotation = Vector3.zero;
    Vector3 localScale = Vector3.zero;

    private void Update()
    {
        if (myID.IsMine)
        {
            OnLocalMovement();
        }
        else
        {
            OnOnlineMovement();
        }
    }
    void OnLocalMovement()
    {
        localMovement = transform.position;
        localRotation = transform.eulerAngles;
        localScale = transform.localScale;
    }
    void OnOnlineMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, localMovement, Time.deltaTime * 20);
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, localRotation, Time.deltaTime * 20);
        transform.localScale = Vector3.MoveTowards(transform.localScale, localScale, Time.deltaTime * 20);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            SendVector3(stream, localMovement);
            SendVector3(stream, localRotation);
            SendVector3(stream, localScale);
        }
        else
        {
            ReceiveVector3(stream, localMovement);
            ReceiveVector3(stream, localRotation);
            ReceiveVector3(stream, localScale);
        }
    }
    void SendVector3(PhotonStream stream, Vector3 _toSerialize)
    {
        stream.SendNext(_toSerialize.x);
        stream.SendNext(_toSerialize.y);
        stream.SendNext(_toSerialize.z);
    }
    void ReceiveVector3(PhotonStream stream, Vector3 _toReceive)
    {
        _toReceive.x = (float)stream.ReceiveNext();
        _toReceive.y = (float)stream.ReceiveNext();
        _toReceive.z = (float)stream.ReceiveNext();
    }
}
