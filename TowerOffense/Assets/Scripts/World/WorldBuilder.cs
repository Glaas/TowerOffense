using UnityEngine;
using UnityEngine.AI;

public class WorldBuilder : MonoBehaviour
{
    public PlayGrid grid;
    public GameObject cellPrefab;
    public GameObject towerPrefab;
    public GameObject edgePrefab;
    public int worldWidth = 25;
    public int worldHeight = 15;
    string gridParentName = "GridParent";


    private void Awake()
    {
        GenerateGrid();
        GameObject.Find("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void Start()
    {

    }

    public void GenerateGrid()
    {
        if (GameObject.Find(gridParentName))
        {
            Destroy(GameObject.Find(gridParentName));
        }
        grid = new PlayGrid(worldWidth, worldHeight);
        var GridParent = new GameObject(gridParentName);
        var subVector = new Vector3(-worldWidth / 2, 0, -worldHeight / 2);

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
                block.GetComponentInChildren<Node>().pos = (x, z);
                block.name = $"{x}, {z}";
                grid.nodeGrid[x, z] = block;
                block.transform.SetParent(GridParent.transform);
                block.transform.localPosition = new Vector3(x, 0, z);
            }
            GridParent.transform.position = subVector;
        }

        for (int i = 3; i < 6; i++)
        {
            GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
            block.GetComponentInChildren<Node>().pos = (-1, i);
            block.name = $"-1, {i}";
            block.transform.SetParent(GridParent.transform);
            block.transform.localPosition = new Vector3(-1, 0, i);




        }
        var tower = GameObject.Instantiate(towerPrefab, new Vector3(-worldWidth / 2 - 1 - 1, .25f, 0), Quaternion.identity);
        tower.name = $"Tower";
        tower.transform.SetParent(GridParent.transform);
        GridParent.transform.localScale *= 3;
        grid.InitializeNodesComponentsInGrid();


    }

}