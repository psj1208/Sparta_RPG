using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemyManager EnemyManager;
    public MapManager MapManager;

    public PlayerAI player;

    [Header("스테이지 관련")]
    StageInfo stageInfo;
    [SerializeField] int curWave;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        EnemyManager.Init(this);
        MapManager.Init(this);
    }


    private void Start()
    {
        GameStart();
    }

    void GameStart()
    {
        StageStart();
    }

    public void StageStart()
    {
        stageInfo = GetStageInfo(SceneData.stage);
        if (stageInfo == null)
        {
            Debug.Log("스테이지 정보가 없습니다.");
            return;
        }
        EnemyManager.StartStage(stageInfo.waves[curWave]);
    }

    public void StartNextWave()
    {
        curWave++;
        if (curWave >= stageInfo.waves.Length)
        {
            CompleteStage();
            return;
        }
        EnemyManager.StartStage(stageInfo.waves[curWave]);
    }

    public void CompleteStage()
    {
        curWave = 0;
        //게임 완료
        SceneManager.LoadScene("MainScene");
    }

    private StageInfo GetStageInfo(int stageKey)
    {
        foreach (var stage in StageData.Stages)
        {
            if (stage.stageKey == stageKey) return stage;
        }
        return null;
    }
}
