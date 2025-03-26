using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;
    GameManager gameManager;
    [SerializeField] private List<GameObject> enemyPrefabs;
    private Dictionary<string, GameObject> enemyPrefabDic;
    [SerializeField] private List<GameObject> activeEnemys;

    [Header("Spawn Info")]
    [SerializeField] private Transform curSpawnTrasform;
    [SerializeField] private Collider spawnCollider;
    [SerializeField] private Transform enemyParent;
    [SerializeField] float spawnCornerOffset;
    [SerializeField] float spawnOffsetY;


    [Header("Stage Info")]
    [SerializeField] float timeBetweenWaves;
    bool enemySpawnComplite = false;

    [Header("Player Info")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool isIn;
    public Transform CurSpawnTransform { get { return curSpawnTrasform; } }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        enemyPrefabDic = new Dictionary<string, GameObject>();
        foreach (GameObject prefab in enemyPrefabs)
        {
            enemyPrefabDic.Add(prefab.name, prefab);
        }
    }

    private void Update()
    {
        isInPlayer();
    }
    public void SpawnRandomEnemy(string prefabName = null)
    {
        Debug.Log("스폰 시도");
        if (enemyPrefabDic.Count == 0 || curSpawnTrasform == null)
        {
            Debug.Log("스폰 실패");
            return;
        }
        GameObject prefab = enemyPrefabDic[prefabName];
        curSpawnTrasform.TryGetComponent<Collider>(out spawnCollider);
        if (spawnCollider == null)
        {
            Debug.Log("콜라이더가 없음");
            return;
        }
        float posX = Random.Range(spawnCollider.bounds.min.x + spawnCornerOffset, spawnCollider.bounds.max.x - spawnCornerOffset);
        float posZ = Random.Range(spawnCollider.bounds.min.z + spawnCornerOffset, spawnCollider.bounds.max.z - spawnCornerOffset);
        GameObject curEnemy = Instantiate(prefab, new Vector3(posX, spawnOffsetY, posZ), Quaternion.identity, enemyParent);
        activeEnemys.Add(curEnemy);
    }

    public void SetSpawnTransform(Transform trans)
    {
        curSpawnTrasform = trans;
    }

    public void RemoveInList(GameObject input)
    {
        activeEnemys.Remove(input);
    }

    public void ClearEnemy()
    {
        foreach(var enemy in activeEnemys)
        {
            Destroy(enemy);
        }
    }

    public void StartStage(WaveData waveData)
    {
        if (waveRoutine != null)
            StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(SpawnStart(waveData));
    }

    private IEnumerator SpawnStart(WaveData waveData)
    {
        enemySpawnComplite = false;
        gameManager.MapManager.MakeNextStage();

        for (int i = 0; i < waveData.monsters.Length; i++)
        {
            yield return null;

            MonsterSpawnData monsterSpawnData = waveData.monsters[i];
            for (int j = 0; j < monsterSpawnData.spawnCount; j++)
            {
                SpawnRandomEnemy(monsterSpawnData.monsterType);
            }
        }

        while (activeEnemys.Count > 0)
        {
            yield return null;
        }

        gameManager.StartNextWave();

        enemySpawnComplite = true;
    }

    void isInPlayer()
    {
        if (CurSpawnTransform == null || spawnCollider == null)
            return;
        Vector3 half = new Vector3(spawnCollider.bounds.max.x - spawnCollider.bounds.min.x, 6f, spawnCollider.bounds.max.z - spawnCollider.bounds.min.z) / 2;
        Collider[] cols = Physics.OverlapBox(CurSpawnTransform.position, half, quaternion.identity, playerLayer);
        if (cols.Length > 0 && isIn == false)
        {
            isIn = true;
            Collider firstHit = cols[0];
            foreach (var enemy in activeEnemys)
            {
                enemy.GetComponent<EnemyAI>().SetTarget(firstHit.transform);
            }
        }
        else if (cols.Length <= 0)
            isIn = false;
    }

    private void OnDrawGizmos()
    {
        if (CurSpawnTransform == null || spawnCollider == null)
            return;
        Vector3 size = new Vector3(spawnCollider.bounds.max.x - spawnCollider.bounds.min.x, 6f, spawnCollider.bounds.max.z - spawnCollider.bounds.min.z);
        Gizmos.DrawWireCube(CurSpawnTransform.position, size);
    }
}
