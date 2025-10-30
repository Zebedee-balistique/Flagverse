using UnityEngine;

public enum LocationType
{
    FlagPole,
    None
}

public class LocationIdentifiers : MonoBehaviour
{
    public LocationType Type = LocationType.None;
}
