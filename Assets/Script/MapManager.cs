using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject mapPrefab_Straight;
    MapInfo curMap;

    private void Start()
    {
        curMap = GetComponentInChildren<MapInfo>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MakeNextStage();
    }

    public void MakeNextStage()
    {
        curMap = Instantiate(mapPrefab_Straight, curMap.ConnectPos.position, Quaternion.identity, transform).GetComponentInChildren<MapInfo>();
    }
}
