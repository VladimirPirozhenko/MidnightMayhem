using FishNet;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Prediction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : NetworkBehaviour
{
    #region Data
    public struct MoveData
    {
        public float Horizontal;
        public float Vertical;
        public MoveData(float horizontal, float vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
    }
    public struct ReconcileData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public Vector3 AngularVelocity;
        public ReconcileData(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
        }
    }
    #endregion

    #region Serialized
    [SerializeField]
    private float speed = 15f;
    #endregion

    #region Private
    private Rigidbody rb;
    private Camera playerCamera;
    #endregion

    private IInputReader inputReader;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        InstanceFinder.TimeManager.OnTick += TimeManager_OnTick;
        InstanceFinder.TimeManager.OnPostTick += TimeManager_OnPostTick;
        //inputReader = new JoystickInputReader(joystick);
        inputReader = new KeyboardInputReader();
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        playerCamera = Camera.main;
    }
    private void OnDestroy()
    {
        if (InstanceFinder.TimeManager != null)
        {
            InstanceFinder.TimeManager.OnTick -= TimeManager_OnTick;
            InstanceFinder.TimeManager.OnPostTick -= TimeManager_OnPostTick;
        }
    }
    private void TimeManager_OnTick()
    {
        if (base.IsOwner)
        {
            Reconciliation(default, false);
            MoveData data;
            GatherInputs(out data);
            Move(data, false);   
        }
        if (base.IsServer)
        {
            Move(default, true);
        }
    }


    private void TimeManager_OnPostTick()
    {
        if (base.IsServer)
        {
            ReconcileData rd = new ReconcileData(transform.position, transform.rotation, rb.velocity, rb.angularVelocity);
            Reconciliation(rd, true);
        }
    }

    private void GatherInputs(out MoveData md)
    {
        md = default;
        inputReader.ReadInputs();
        float horizontal = inputReader.Horizontal;
        float vertical = inputReader.Vertical;
        if (horizontal == 0f && vertical == 0f)
            return;
        Vector3 movement = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        md = new MoveData(movement.x, movement.z);
    }

    [Replicate]
    private void Move(MoveData md, bool asServer, bool replaying = false)
    {
        Vector3 forces = new Vector3(md.Horizontal, Physics.gravity.y, md.Vertical) * speed;// * (float)base.TimeManager.TickDelta;
        rb.AddForce(forces);
    }

    [Reconcile]
    private void Reconciliation(ReconcileData rd, bool asServer)
    {
        transform.position = rd.Position;
        transform.rotation = rd.Rotation;
        rb.velocity = rd.Velocity;
        rb.angularVelocity = rd.AngularVelocity;
       // Debug.Log("ReconciledPos: " + transform.position);
    }
}
