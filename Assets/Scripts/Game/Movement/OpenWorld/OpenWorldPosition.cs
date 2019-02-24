using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldPosition : MonoBehaviour
{
    [ReadOnly]
    public int BLOCK_SIZE;

    public void Start()
    {
        BLOCK_SIZE = SettingsController.Instance.GetValue<int>(SettingsConstants.Name.BLOCK_SIZE);
    }

    public void Translate(Vector3 vector, Transform transform)
    {
        this.transform.Translate(vector, transform);
    }
}
