using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data",menuName = "LevelChunk")]
public class LevelChunck : ScriptableObject
{
    public enum Direction
    {
        North,South,East,West
    }
    public Vector2 chunckSize = new Vector2(10f, 10f);
    public GameObject[] levelChunk;
    public Direction entryDirection;
    public Direction exitDirection;

}
