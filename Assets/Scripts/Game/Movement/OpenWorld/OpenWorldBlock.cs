using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldBlock : MonoBehaviour
{
    public OpenWorldBlockLocator Locator;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockX;

    [ReadOnly]
    [Tooltip("Current block position in the universe")]
    public long BlockZ;

    public void Start()
    {
        //make lines to match the size of the block
        Locator.LineRenderer.SetPositions(new Vector3[]
        {
            new Vector3( OpenWorldController.Instance.HalfBlockSize, 0, -OpenWorldController.Instance.HalfBlockSize),
            new Vector3( OpenWorldController.Instance.HalfBlockSize, 0, OpenWorldController.Instance.HalfBlockSize),
            new Vector3( -OpenWorldController.Instance.HalfBlockSize, 0, OpenWorldController.Instance.HalfBlockSize),
            new Vector3( -OpenWorldController.Instance.HalfBlockSize, 0, -OpenWorldController.Instance.HalfBlockSize),
            new Vector3( OpenWorldController.Instance.HalfBlockSize, 0, -OpenWorldController.Instance.HalfBlockSize)
        });
    }
}
