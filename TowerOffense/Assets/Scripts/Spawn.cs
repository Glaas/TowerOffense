using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject block1;

    public GameObject[,] blocks;

    public int worldWidth = 10;
    public int worldHeight = 10;
    public int scale = 1;

    [ContextMenu("CreateWorld")]
    public void CreateWorld()
    {
        blocks = new GameObject[worldWidth, worldHeight];
        if (GameObject.Find("hol"))
        {
            ClearLog();
            Debug.Log("Console cleared manually");
            DestroyWorld();
            Debug.Log("You forgot to destroy the old world buddy, but dw i gotchu");

        }

        var hol = new GameObject("hol");

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = GameObject.Instantiate(block1, Vector3.zero, block1.transform.rotation) as GameObject;
                block.name = $"{x}, {z}";
                blocks[x, z] = block;
                block.transform.SetParent(hol.transform);
                block.transform.localScale = new Vector3(scale, scale, scale);
                block.transform.localPosition = new Vector3(x * scale, 0, z * scale);
            }
            hol.transform.position = new Vector3(-worldWidth - scale, 0, -worldHeight - scale);
        }
        foreach (GameObject block in blocks) Debug.Log(block);

    }
    [ContextMenu("CreateWorld")]
    public void DestroyWorld()
    {
        blocks = new GameObject[worldWidth, worldHeight];
        var hol = GameObject.Find("hol");
        DestroyImmediate(hol);
    }
    public void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}