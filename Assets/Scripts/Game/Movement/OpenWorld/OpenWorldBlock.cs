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

    [ReadOnly]
    [Tooltip("Indicates that this block was Loaded")]
    public bool Loaded;

    public void Start()
    {
        //make lines to match the size of the block
        Locator.DebugLineRenderer.SetPositions(new Vector3[]
        {
            new Vector3( 0, 0, 0),
            new Vector3( OpenWorldController.Instance.BlockSize, 0, 0),
            new Vector3( OpenWorldController.Instance.BlockSize, 0, OpenWorldController.Instance.BlockSize),
            new Vector3( 0, 0, OpenWorldController.Instance.BlockSize),
            new Vector3( 0, 0, 0)
        });
    }

    public void Refresh()
    {
        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] Refresh");
        Locator.DebugBlockName.text = $"{BlockX},{BlockZ}";
        Loaded = false; //unload if anything was loaded
    }

    public void GenerateIfNotLoaded()
    {
        if( Loaded )
        {
            //skip was generated already
        }
        else
        {
            Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] Loaded");
            Locator.DebugBlockName.color = UnityEngine.Color.green;

            Loaded = true;
        }
    }
}
