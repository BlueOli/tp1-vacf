using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DifficultyManagerSO : ScriptableObject
{
    [SerializeField]
    public int maxFriendNumber;

    public int minFriendGroup;
    public int maxFriendGroup;

    DifficultyManagerSO() 
    {
        maxFriendNumber = 8;
        minFriendGroup = 2;
        maxFriendGroup = 4;
    }
}
