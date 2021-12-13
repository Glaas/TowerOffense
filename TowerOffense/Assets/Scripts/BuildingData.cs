using UnityEngine;

public enum BuildingType
{
    BaseTurret,
    ModifiedTurret,
    Other
}
[CreateAssetMenu(fileName = "New ActionData", menuName = "ActionData")]
public class BuildingData : ScriptableObject
{
    public string buildingName;
    public BuildingType buildingToBuild = BuildingType.BaseTurret;
    public int cost;
    public string description;
    public GameObject buildingPrefab;
}
