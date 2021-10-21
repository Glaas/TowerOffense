using UnityEngine;

public class Spawn : MonoBehaviour
{
    private PlayGrid grid;

    public int scale = 1;

    [ContextMenu("CreateWorld")]
    public void CreateWorld()
    {
        grid = GetComponent<PlayGrid>();
        grid.gridObject = new GameObject[grid.worldWidth, grid.worldHeight];
        if (GameObject.Find("hol"))
        {
            Debug.Log("Console cleared manually");
            DestroyWorld();
            Debug.Log("You forgot to destroy the old world buddy, but dw i gotchu");

        }

        var hol = new GameObject("hol");

        for (int x = 0; x < grid.worldWidth; x++)
        {
            for (int z = 0; z < grid.worldHeight; z++)
            {
                GameObject block = GameObject.Instantiate(grid.blockPrefab, Vector3.zero, grid.blockPrefab.transform.rotation) as GameObject;
                block.name = $"{x}, {z}";
                grid.gridObject[x, z] = block;
                block.transform.SetParent(hol.transform);
                block.transform.localScale = new Vector3(scale, scale, scale);
                block.transform.localPosition = new Vector3(x * scale, 0, z * scale);
            }
            hol.transform.position = new Vector3(-grid.worldWidth - scale, 0, -grid.worldHeight - scale);
        }

    }
    [ContextMenu("CreateWorld")]
    public void DestroyWorld()
    {
        grid.gridObject = new GameObject[grid.worldWidth, grid.worldHeight];
        var hol = GameObject.Find("hol");
        DestroyImmediate(hol);
    }
}