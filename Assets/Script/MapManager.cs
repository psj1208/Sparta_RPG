using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject navPrefab;
    NavMeshSurface navMeshSurface;
    [SerializeField] GameObject mapPrefab_Straight;
    MapInfo curMap;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        curMap = GetComponentInChildren<MapInfo>();
        navMeshSurface = Instantiate(navPrefab).GetComponent<NavMeshSurface>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(SurfaceBuild), 0, 0.5f);
        Debug.Log(SceneData.stage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            gameManager.StageStart();
    }

    public void MakeNextStage()
    {
        if (curMap == null)
        {
            Debug.Log("맵이 비었습니다.");
            return;
        }
        Transform connect = curMap.ConnectPos;
        curMap = Instantiate(mapPrefab_Straight, connect.position, connect.rotation, transform).GetComponentInChildren<MapInfo>();
        gameManager.EnemyManager.SetSpawnTransform(curMap.SpawnObject);
    }

    public void SurfaceBuild()
    {
        navMeshSurface.BuildNavMesh();
    }
}
