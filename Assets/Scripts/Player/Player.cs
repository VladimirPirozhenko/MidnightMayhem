using System.Collections;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;

[RequireComponent(typeof(PlayerStatistics))]  
public class Player : NetworkBehaviour
{
    #region PlayerComponents

    public PlayerStatistics PlayerStatistics { get; private set; }
    public IInputReader InputReader { get; private set; }

    #endregion

    #region SyncVars

    [field: SyncVar] public string userName { get; private set; }
    [field: SyncVar] public string Tag { get; private set; }

    #endregion
    private void Awake()
    {
        PlayerStatistics = GetComponent<PlayerStatistics>();
        //inputReader = new JoystickInputReader(joystick);
        InputReader = new KeyboardInputReader();
    }
    public override void OnStartServer()
    {
        base.OnStartServer();
        Tag = this.Owner.ClientId.ToString();
        GameSession.Instance.ServerRegisterPlayer(Tag, this);
    }
    public override void OnStopServer()
    {
        base.OnStopServer();
       // GameSession.Instance.RemoveOwnership();
        GameSession.Instance.ServerUnregisterPlayer(Tag);
       // GameSession.Instance.UnregisterPlayer(Tag);
       // GameSession.Instance.RemovePlayerCardRpc(Tag);
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner)
            gameObject.GetComponent<Player>().enabled = false; 
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
       // GameSession.Instance.UnregisterPlayerRpc(Tag);
    }
}
