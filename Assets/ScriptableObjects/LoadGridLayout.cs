using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LoadGridLayout : ScriptableObject
{
    [SerializeField]
    public int[,] grid = new int[5, 6];

    [SerializeField]
    public int maxFriends = 0;

    [SerializeField]
    public int tetrisCompleted = 0;
}
