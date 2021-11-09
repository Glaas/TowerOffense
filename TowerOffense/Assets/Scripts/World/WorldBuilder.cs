using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using DG.Tweening;

public class WorldBuilder : MonoBehaviour
{
    public PlayGrid grid;
    public GameObject cellPrefab;
    public GameObject towerPrefab;
    public GameObject edgePrefab;

    private GameObject gridParent;

    public int worldWidth = 25;
    public int worldHeight = 15;
    string gridParentName = "GridParent";


    public void GeneratingWorld() => StartCoroutine(nameof(WorldGenerationSequence));
    IEnumerator WorldGenerationSequence()
    {
        StartCoroutine(nameof(CreateParents));
        yield return StartCoroutine(nameof(InitializeGrid));
        yield return StartCoroutine(nameof(GenerateFrontOfTower));
        yield return StartCoroutine(nameof(GenerateTower));
        GameObject.Find("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
        yield return new WaitForSeconds(.2f);
        GlobalStateManager.Instance.gameState = GlobalStateManager.GameState.PLAYER_PREPARATION;
        GlobalStateManager.Instance.IterateGameState();
    }
    IEnumerator CreateParents()
    {
        gridParent = new GameObject(gridParentName);
        gridParent.transform.SetParent(GameObject.Find("--Grid--").transform);
        gridParent.transform.localScale *= 3;
        yield return null;
    }
    IEnumerator InitializeGrid()
    {
        grid = new PlayGrid(worldWidth, worldHeight);

        var subVector = new Vector3(-worldWidth / 2, 0, -worldHeight / 2);
        gridParent.transform.position = subVector;

        for (int x = 0; x < worldWidth; x++)
        {
            for (int z = 0; z < worldHeight; z++)
            {
                GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation, gridParent.transform);
                block.GetComponentInChildren<Node>().pos = (x, z);
                block.name = $"{x}, {z}";
                grid.nodeGrid[x, z] = block;
                block.transform.localPosition = new Vector3(x, 20, z);
                block.transform.DOLocalMoveY(0, Random.Range(1f, 1.5f));
            }
        }
        yield return new WaitForSeconds(2);
    }
    IEnumerator GenerateFrontOfTower()
    {
        for (int i = 3; i < 6; i++)
        {
            GameObject block = GameObject.Instantiate(cellPrefab, Vector3.zero, cellPrefab.transform.rotation, gridParent.transform);
            block.GetComponentInChildren<Node>().pos = (-1, i);
            block.name = $"-1, {i}";
            block.transform.localPosition = new Vector3(-1, 20, i);
            block.transform.DOLocalMoveY(0, Random.Range(1f, .7f));
            block.GetComponentInChildren<Node>().canBeOutlined = false;
            var col = block.GetComponentsInChildren<Collider>();
            foreach (var item in col) Destroy(item);
        }
        yield return new WaitForEndOfFrame();
    }
    IEnumerator GenerateTower()
    {
        var tower = GameObject.Instantiate(towerPrefab, Vector3.zero, towerPrefab.transform.rotation, GameObject.Find("--Tower--").transform);
        tower.transform.position = new Vector3(-16.5f, 20, 8);
        tower.transform.DOLocalMoveY(0, Random.Range(1f, .5f));

        tower.name = $"Tower";
        grid.InitializeNodesComponentsInGrid();
        yield return new WaitForSeconds(1.3f);
    }
}