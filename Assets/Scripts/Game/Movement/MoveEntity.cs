using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEntity : MonoBehaviour
{
    public float Force;

    public InputButton ButtonMoveHorizontal;
    public InputButton ButtonMoveVertical;

    public Camera Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(ButtonMoveHorizontal.KeyName))
        {
            float horizontal = Input.GetAxis(ButtonMoveHorizontal.KeyName) * Force * Time.deltaTime;

            transform.Translate(Vector3.right * horizontal, Camera.transform);
        }

        if (Input.GetButton(ButtonMoveVertical.KeyName))
        {
            float vertical = Input.GetAxis(ButtonMoveVertical.KeyName) * Force * Time.deltaTime;

            transform.Translate(Vector3.up * vertical, Camera.transform);
        }
        //else - skip any other key
    }
}
