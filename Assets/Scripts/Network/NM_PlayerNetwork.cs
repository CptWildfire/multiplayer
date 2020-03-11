using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NM_PlayerNetwork : MonoBehaviour, IPunObservable
{
    [SerializeField] PhotonView myID = null;
    Vector3 localMovement = Vector3.zero;
    Vector3 localRotation = Vector3.zero;
    Vector3 localScale = Vector3.zero;

    private void Start()
    {
        name = myID.ViewID.ToString();
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
    }

    private void Update()
    {
        if(myID.IsMine)
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
        if(stream.IsWriting)
        {
            stream.SendNext(localMovement.x);
            stream.SendNext(localMovement.y);
            stream.SendNext(localMovement.z);

            stream.SendNext(localRotation.x);
            stream.SendNext(localRotation.y);
            stream.SendNext(localRotation.z);

            stream.SendNext(localScale.x);
            stream.SendNext(localScale.y);
            stream.SendNext(localScale.z);

            //stream.SendNext(localColor.r);
            //stream.SendNext(localColor.g);
            //stream.SendNext(localColor.b);
            //stream.SendNext(localColor.a);
        }
        else
        {
            localMovement.x = (float)stream.ReceiveNext();
            localMovement.y = (float)stream.ReceiveNext();
            localMovement.z = (float)stream.ReceiveNext();

            localRotation.x = (float)stream.ReceiveNext();
            localRotation.y = (float)stream.ReceiveNext();
            localRotation.z = (float)stream.ReceiveNext();

            localScale.x = (float)stream.ReceiveNext();
            localScale.y = (float)stream.ReceiveNext();
            localScale.z = (float)stream.ReceiveNext();

            //localColor.r = (float)stream.ReceiveNext();
            //localColor.g = (float)stream.ReceiveNext();
            //localColor.b = (float)stream.ReceiveNext();
            //localColor.a = (float)stream.ReceiveNext();
        }
    }
}
