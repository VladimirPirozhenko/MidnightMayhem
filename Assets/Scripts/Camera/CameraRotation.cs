using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]  
public class CameraRotation : MonoBehaviour
{
    private Touch touch;
    private Camera cam;
    public Transform target;
    private float width;
    private float height;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }
    void Start()
    {
        cam = GetComponent<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            float delta = Input.GetAxis("Mouse X") * 10;
            transform.RotateAround(target.position, Vector3.up, delta);
            //Vector2 pos = Input.mousePosition;
            //pos.x = (pos.x - width) / width;
            //pos.y = (pos.y - height) / height;
            cam.transform.position = new Vector3(10, 10, 0.0f);
        }
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 pos = touch.position;
                pos.x = (pos.x - width) / width;
                pos.y = (pos.y - height) / height;
                cam.transform.position = new Vector3(-pos.x, pos.y, 0.0f);
            }

            //if (Input.touchCount == 2)
            //{
            //    touch = Input.GetTouch(1);

            //    if (touch.phase == TouchPhase.Began)
            //    {
            //        // Halve the size of the cube.
            //        transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            //    }

            //    if (touch.phase == TouchPhase.Ended)
            //    {
            //        // Restore the regular size of the cube.
            //        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //    }
            //}
        }
    }
}
