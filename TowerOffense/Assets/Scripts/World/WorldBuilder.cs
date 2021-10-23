using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public PlayGrid grid;
    public GameObject cellPrefab;
    public GameObject towerPrefab;
    public int worldWidth = 25;
    public int worldHeight = 15;
    public string parentName = "GridParent";

    public void GenerateGrid()
    {
        grid = new PlayGrid(worldWidth, worldHeight);
        var hol = new GameObject(parentName);

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                print("Generating object " + x + z);
                GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation);
                block.GetComponent<Node>().pos = (x, z);
                block.name = $"{x}, {z}";
                grid.nodeGrid[x, z] = block;
                block.transform.SetParent(hol.transform);
                block.transform.localPosition = new Vector3(x, 0, z);
            }
            hol.transform.position = new Vector3(-worldWidth / 2, 0, -worldHeight / 2);
        }
        var tower = GameObject.Instantiate(towerPrefab, new Vector3(-worldWidth/2-1, 2, 0), Quaternion.identity);
    }

}