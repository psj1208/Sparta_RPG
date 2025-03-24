using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    [SerializeField] Transform connectPos;
    public Transform ConnectPos { get { return connectPos; } }
}
