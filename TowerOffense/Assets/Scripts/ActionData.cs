using UnityEngine;

public enum BuildingType
{
    BaseTurret,
    ModifiedTurret,
    Other
}
public class ActionData : MonoBehaviour
{
    public BuildingType buildingToBuild = BuildingType.BaseTurret;
    public string actionName;
    public int cost;
    public string description;
}
