using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject block1;

    public int worldWidth = 10;
    public int worldHeight = 10;
    public int scale = 1;

    public GameObject parent;

    [ContextMenu("Cubes")]
    void CreateWorld()
    {
        parent = GameObject.Find("World");
        var hol = new GameObject("hol");

        for (int x = 0; x < worldWidth; x++)
        {

            for (int z = 0; z < worldHeight; z++)
            {

                GameObject block = GameObject.Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                block.transform.SetParent(hol.transform);
                block.transform.localScale = new Vector3(scale, scale, scale);
                block.transform.localPosition = new Vector3(x * scale, 0, z * scale);
            }
            hol.transform.position = new Vector3(-worldWidth-scale, 0, -worldHeight-scale);
        }
    }
}