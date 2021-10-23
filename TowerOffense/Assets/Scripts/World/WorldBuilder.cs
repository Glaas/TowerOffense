using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public PlayGrid grid;
    public GameObject cellPrefab;
    public GameObject towerPrefab;
    public GameObject edgePrefab;
    public int worldWidth = 25;
    public int worldHeight = 15;
    public string parentName = "GridParent";

    public void GenerateGrid()
    {
        grid = new PlayGrid(worldWidth, worldHeight);
        var hol = new GameObject(parentName);
        var subVector = new Vector3(-worldWidth / 2, 0, -worldHeight / 2);

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
                block.GetComponent<Node>().pos = (x, z);
                block.name = $"{x}, {z}";
                grid.nodeGrid[x, z] = block;
                block.transform.SetParent(hol.transform);
                block.transform.localPosition = new Vector3(x, 0, z);
            }
            hol.transform.position = subVector;
        }
        var tower = GameObject.Instantiate(towerPrefab, new Vector3(-worldWidth / 2 - 1, 2, 0), Quaternion.identity);
        grid.InitializeNodesComponentsInGrid();
        grid.GenerateEdge();


        foreach (var edge in grid.edges)
        {
            var n = GameObject.Instantiate(edgePrefab, subVector + new Vector3(0, 0, 0), Quaternion.identity, hol.transform);
            Vector2 res = (edge.cellA + edge.cellB) / 2;
            n.transform.localPosition = new Vector3(res.x, 1, res.y);
            n.name = $"{edge.cellA} - {edge.cellB}";
        }
    }

}