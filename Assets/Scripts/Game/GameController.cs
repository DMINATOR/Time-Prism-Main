using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(GameControllerLocator))]
public class GameController : MonoBehaviour
{
    SaveGameData _saveData;

    [Header("Constants")]
    [ReadOnly]
    [Tooltip("Logging source")]
    public static string LOG_SOURCE = "GameController";


    //Public instance to game controller
    public static GameController Instance = null;


    //Exposed

    [Header("Locator")]

    [Tooltip("Locator")]
    public GameControllerLocator Locator;

    [Tooltip("Current Save Instance")]
    public SaveSlotInstance CurrentSaveInstance;

    private void LoadGameData()
    {
        _saveData = SaveGameController.Instance.Load();

        CurrentSaveInstance = _saveData.SaveSlots.Where(x => x.Current == true).SingleOrDefault();

        if( CurrentSaveInstance == null )
        {
            CurrentSaveInstance = new SaveSlotInstance();
            CurrentSaveInstance.Current = true;
            _saveData.SaveSlots.Add(CurrentSaveInstance);
        }

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Loaded");
    }

    //Debug

    [Header("Debug")]

    [Tooltip("Shows control objects")]
    public bool DebugShowControlObjects;

    [Tooltip("For how long to show the game objects")]
    public float DebugShowControlObjectsTime = 10f;


    private void SaveGameData()
    {
        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saving");

        SaveGameController.Instance.Save(_saveData);

        Log.Instance.Info(GameController.LOG_SOURCE, $"Game Data Saved");
    }

    private void Awake()
    {
        //Create instance
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadGameData();

        StartGame();
    }

    private void StartGame()
    {
        OpenWorldController.Instance.SetUniverseCenter(CurrentSaveInstance.BlockX, CurrentSaveInstance.BlockZ);
    }
}
