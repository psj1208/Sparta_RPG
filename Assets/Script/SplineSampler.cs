using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Splines;

[ExecuteInEditMode()]
public class SplineSampler : MonoBehaviour
{
    [SerializeField] SplineContainer m_splineContainer;
    public SplineContainer SplineContainer { get { return m_splineContainer; } }
    [SerializeField] private float m_width;
    public float Width { get { return m_width; } }

    Vector3 p1;
    Vector3 p2;

    float3 position;
    float3 forward;
    float3 upVector;

    private void Start()
    {
        m_splineContainer = GetComponent<SplineContainer>();
    }
    public void SamplerWidth(float t, out Vector3 p1, out Vector3 p2, bool NotUp = false)
    {
        m_splineContainer.Evaluate(t,out position,out forward,out upVector);
        float3 right = Vector3.Cross(forward, upVector).normalized;
        if (NotUp == true) 
            right = Vector3.Cross(forward,transform.up).normalized;
        p1 = position + (right * m_width);
        p2 = position + (-right * m_width);
    }
}
