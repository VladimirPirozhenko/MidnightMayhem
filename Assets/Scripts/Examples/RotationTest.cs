using System.Collections;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    Quaternion currentState;
    private void Start()
    {
        //store initial state
        currentState = Quaternion.identity;
    }

    private void Update()
    {
        //draw axes tripod in viewport for demo
        Debug.DrawRay(Vector3.zero, currentState * Vector3.up, Color.green);
        Debug.DrawRay(Vector3.zero, currentState * Vector3.right, Color.red);
        Debug.DrawRay(Vector3.zero, currentState * Vector3.forward, Color.blue);
    }

    private void OnGUI()
    {
        RotateOnGlobalAxis();
        RotateOnLocalAxis();
        CompareCurrentPositionWithInitialState();
    }

    private void RotateOnGlobalAxis()
    {
        GUILayout.BeginArea(new Rect(100, 100, 200, 100));
        if (GUILayout.Button("Rotate around global X"))
        {
            currentState = Quaternion.Euler(90, 0, 0) * currentState;
        }
        if (GUILayout.Button("Rotate around global Y"))
        {
            currentState = Quaternion.Euler(0, 90, 0) * currentState;
        }
        if (GUILayout.Button("Rotate around global Z"))
        {
            currentState = Quaternion.Euler(0, 0, 90) * currentState;
        }
        GUILayout.EndArea();
    }

    private void RotateOnLocalAxis()
    {
        GUILayout.BeginArea(new Rect(350, 100, 200, 100));
        if (GUILayout.Button("Rotate around local X"))
        {
            currentState = currentState * Quaternion.Euler(90, 0, 0);
        }
        if (GUILayout.Button("Rotate around local Y"))
        {
            currentState = currentState * Quaternion.Euler(0, 90, 0);
        }
        if (GUILayout.Button("Rotate around local Z"))
        {
            currentState = currentState * Quaternion.Euler(0, 0, 90);
        }
        GUILayout.EndArea();
    }

    private void CompareCurrentPositionWithInitialState()
    {
        GUILayout.BeginArea(new Rect(225, 200, 200, 20));
        if (GUILayout.Button("Check State"))
        {
            Vector3 currentUp = currentState * Vector3.up;
            if (Vector3.Dot(currentUp, Vector3.up) > 0.9f)
            {
                Debug.Log("Current State has up pointing up");
            }
            else
            {
                Debug.Log("Current State has up pointing somewhere else");
            }
        }
        GUILayout.EndArea();
    }
}