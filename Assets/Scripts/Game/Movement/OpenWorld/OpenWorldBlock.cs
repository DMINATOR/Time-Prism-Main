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

    [Tooltip("Cache of currently created projectiles active or inactive")]
    public GameObjectCache Projectiles;

    public long BlockDeltaX
    {
        get
        {
            return BlockX - OpenWorldController.Instance.BlockCenterX;
        }
    }


    public long BlockDeltaZ
    {
        get
        {
            return BlockZ - OpenWorldController.Instance.BlockCenterZ;
        }
    }

    [ReadOnly]
    [Tooltip("Indicates that this block was Loaded")]
    public bool Loaded;

    [Header("Settings")]

    [ReadOnly]
    [Tooltip("Settings to load PROJECTILES_LIMIT value")]
    public SettingsConstants.Name PROJECTILES_LIMIT_SETTING_NAME = SettingsConstants.Name.PROJECTILES_LIMIT;

    [Header("Loaded Settings")]

    [ReadOnly]
    [Tooltip("Limit of number of projectiles")]
    public int ProjectilesLimitSize;

    public void Start()
    {
        ProjectilesLimitSize = SettingsController.Instance.GetValue<int>(PROJECTILES_LIMIT_SETTING_NAME);

        Projectiles = new GameObjectCache(Locator.ProjectilesContainer, ProjectilesLimitSize);

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

    public void Refresh(long BlockX, long BlockZ)
    {
        //correct block positions
        this.BlockX = BlockX;
        this.BlockZ = BlockZ;

        //disable projectiles
        Projectiles.SetActive(false);

        gameObject.transform.position = new Vector3(OpenWorldController.Instance.BlockSize * BlockDeltaX, 0, OpenWorldController.Instance.BlockSize * BlockDeltaZ);

        Log.Instance.Info(OpenWorldController.LOG_SOURCE, $"Block [{BlockX}, {BlockZ}] D[{BlockDeltaX},{BlockDeltaZ}] Refresh");
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
