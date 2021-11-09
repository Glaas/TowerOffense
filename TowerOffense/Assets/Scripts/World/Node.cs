using System.Collections.Generic;
using System;
using UnityEngine;
using EPOOutline;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections;
using TowerOffense;
using NaughtyAttributes;

public class Node : MonoBehaviour, ISelectable
{
    public Outlinable outline;
    public PlayGrid playgrid;
    public Selection selection;

    [ColorUsage(false, true)]
    public Color baseColor;
    [ColorUsage(false, true)]
    public Color offSetColor;

    public List<GameObject> possibleBuildings;
    public GameObject currentBuilding;
    public Transform buildingLocation;

    public int buildingPrice = 30; //FIXME: This should be a variable in the building itself. and more modular for more buildings

    public (int, int) pos;
    public bool canBeOutlined = true;
    public bool isSelected { get; set; }

    private void Awake()
    {

        selection = FindObjectOfType<Selection>();
        playgrid = FindObjectOfType<WorldBuilder>().grid;
        baseColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
        buildingLocation = transform.Find("BuildingLocation");
    }
    private void Start()
    {
        if (GetComponent<Outlinable>())
        {
            outline = GetComponent<Outlinable>();
            outline.enabled = false;
        }
    }

    public void OnSelect()
    {
        switch (selection.toolType)
        {
            case Selection.TOOL_TYPE.BUILD_WALLS:
                isSelected = true;
                transform.DOMoveY(transform.position.y + 3f, .2f);
                GetComponent<MeshRenderer>().material.DOColor(offSetColor, "_Color", .2f);
                ExcludeItselfFromNavmesh(true);

                break;
            case Selection.TOOL_TYPE.PLACE_BUILDINGS:
                foreach (var n in playgrid.Square(pos))
                {
                    if (playgrid.nodeGrid[n.Item1, n.Item2].GetComponentInChildren<Node>().currentBuilding != null)
                    {
                        UiHandler.instance.SetInfo("Too close to another structure");
                        GlobalSoundManager.instance.PlayError();
                        return;
                    }
                }
                if (GlobalDataHandler.instance.currentPlayerCoins < buildingPrice)
                {
                    UiHandler.instance.SetInfo("You can't afford that");
                    GlobalSoundManager.instance.PlayError();
                    return;
                }
                BuildBuilding();
                GlobalDataHandler.instance.SubtractMoney(buildingPrice);
                UiHandler.instance.UpdateCoins();
                UiHandler.instance.SetInfo("Structure was built successfully");
                GameObject.Find("PlaceTurretSFX").GetComponent<AudioSource>().Play();
                break;
            default: throw new NotImplementedException();
        }

    }
    public void OnDeselect()
    {
        switch (selection.toolType)
        {
            case Selection.TOOL_TYPE.BUILD_WALLS:
                isSelected = false;
                transform.DOMoveY(transform.position.y + -3f, .2f);
                GetComponent<MeshRenderer>().material.DOColor(baseColor, "_Color", .2f);
                ExcludeItselfFromNavmesh(false);

                break;
            case Selection.TOOL_TYPE.PLACE_BUILDINGS:
                break;
            default: throw new NotImplementedException();
        }

    }

    [Button("Build building")]
    void BuildBuilding()
    {
        currentBuilding = Instantiate(possibleBuildings[0], buildingLocation.position, possibleBuildings[0].transform.rotation, transform);
        ExcludeItselfFromNavmesh(true);

    }

    void ExcludeItselfFromNavmesh(bool becomeObstacle)
    {
        if (becomeObstacle)
        {
            gameObject.AddComponent<NavMeshObstacle>();
            GetComponent<NavMeshObstacle>().carving = true;
            GetComponent<NavMeshObstacle>().carveOnlyStationary = false;
            GetComponent<NavMeshObstacle>().size = Vector3.one * 1.5f;
        }
        else Destroy(gameObject.GetComponent<NavMeshObstacle>());

    }


}
