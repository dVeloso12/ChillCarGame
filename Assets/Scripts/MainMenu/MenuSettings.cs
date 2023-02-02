using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuData", menuName = "ScriptableObjects/MenuData", order = 1)]
public class MenuSettings : ScriptableObject
{
   public int carIndex;
   public float FovValue;
   public bool isFullscreen;
    public int QualityIndex;
    public Vector2 Resolution;
    public float EffectVolume;
    public float MusicVolume;
    public string filePath;

}
