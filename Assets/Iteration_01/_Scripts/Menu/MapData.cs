using UnityEngine;

[CreateAssetMenu(fileName = "Map_01", menuName = "Data/Maps/Map_01", order = 1)]
public class MapData : ScriptableObject
{
    public int TargetPoints;
    public string MapName; 
    public AudioType Score;
}