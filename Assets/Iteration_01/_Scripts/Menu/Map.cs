using UnityEngine;

public class Map
{
    public int TargetPoints;
    public string MapName; 
    public AudioType Score;
    public Sprite MapSprite;
    public bool IsMapLocked;
    public int MapIndex;

    public Map(int targetPoints, string mapName, AudioType score, Sprite mapSprite, bool isMapLocked)
    {
        TargetPoints = targetPoints;
        MapName = mapName;
        Score = score;
        MapSprite = mapSprite;
        IsMapLocked = isMapLocked;
    }
}