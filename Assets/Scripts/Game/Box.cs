using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using UnityEngine;


public class Box : NetworkBehaviour
{
    [field: SyncVar]
    public string Tag { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody == null)
            return; 
        if (collision.rigidbody.TryGetComponent(out Player player))
        {
            Tag = player.Tag; 
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerRemove()
    {
        ObserversRemove();
    }
    [ObserversRpc(BufferLast = true)]   
    public void ObserversRemove()
    {
        gameObject.SetActive(false);    
    }
}
