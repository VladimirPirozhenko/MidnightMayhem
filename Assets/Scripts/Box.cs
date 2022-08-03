using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using UnityEngine;


public class Box : NetworkBehaviour
{
    [field: SyncVar]
    public string Tag { get; private set; }

    //[field: SyncVar]
    //public Player Player {  get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null)
            return; 
        if (collision.rigidbody.TryGetComponent(out Player player))
        {
            //this.Player = player;
            //ConnectionOwner = player.Owner;
            Tag = player.Owner.ClientId.ToString(); 
            //Debug.Log("Owner of " + this.ToString() + "  " + ConnectionOwner.ClientId);
        }
    }
}
