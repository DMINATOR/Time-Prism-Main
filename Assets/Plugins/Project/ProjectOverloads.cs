using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//SAVE GAME overloads
public partial class SaveGameData : DKAsset
{
    public List<SaveSlotInstance> SaveSlots;
}


[System.Serializable]
public class SaveSlotInstance
{
    //Randomly generated world seed
    public int RandomSeed;

    //Position of the player
    public int BlockX;
    public int BlockZ;

    //Indicates that this save slot is current
    public bool Current;

    //Info
    public DateTimeSerializer Created;
    public DateTimeSerializer Modified;
}
