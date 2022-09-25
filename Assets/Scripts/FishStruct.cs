using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FishObject", order = 1)]
public class FishStruct : ScriptableObject
{
    public string fishName; // name of the fish
    public uint value; // value of the fish
}

