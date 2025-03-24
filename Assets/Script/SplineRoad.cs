using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode()]
public class SplineRoad : MonoBehaviour
{
    private SplineSampler m_splineSampler;
    [SerializeField] private int resolution;
    [SerializeField] private bool EffectByUpVector;
    MeshFilter m_meshFilter;

    List<Vector3> m_vertsP1;
    List<Vector3> m_vertsP2;

    void Start()
    {
        m_splineSampler = GetComponent<SplineSampler>();
        m_meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        GetVerts();
        BuildMesh();
    }

    void GetVerts()
    {
        m_vertsP1 = new List<Vector3>();
        m_vertsP2 = new List<Vector3>();

        float step = 1f / (float)resolution;
        for (int i = 0; i <= resolution; i++)
        {
            float t = step * i;
            Math.Round(t, 4);
            m_splineSampler.SamplerWidth(t, out Vector3 p1, out Vector3 p2, EffectByUpVector);
            m_vertsP1.Add(p1);
            m_vertsP2.Add(p2);
        }
    }

    private void BuildMesh()
    {
        Mesh m = new Mesh();
        List<Vector3> verts = new List<Vector3>();
        List<int> tris = new List<int>();

        int length = m_vertsP2.Count;

        for (int i = 0; i < length; i++)
        {

            // ���� �ε��� i�� ���� �ε��� (��ȯ�ϵ���)
            int nextIndex = (i + 1) % length;

            // ���� �簢���� �����ϴ� �� ���� ����
            Vector3 p1 = transform.InverseTransformPoint(m_vertsP1[i]);
            Vector3 p2 = transform.InverseTransformPoint(m_vertsP2[i]);
            Vector3 p3 = transform.InverseTransformPoint(m_vertsP1[nextIndex]);
            Vector3 p4 = transform.InverseTransformPoint(m_vertsP2[nextIndex]);

            // ���� �߰�
            int offset = verts.Count; // ���� �߰��� ���� ���� ������� ������ ���

            verts.Add(p1);
            verts.Add(p2);
            if (nextIndex != 0)
            {
                verts.Add(p3);
                verts.Add(p4);
            }

            tris.Add(offset + 0);
            if (nextIndex != 0)
            {
                tris.Add(offset + 2);
                tris.Add(offset + 3);
                tris.Add(offset + 3);
            }
            tris.Add(offset + 1);
            tris.Add(offset + 0);
        }

        m.SetVertices(verts);
        m.SetTriangles(tris, 0);

        m_meshFilter.mesh = m;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_vertsP1.Count; i++)
        {
            Handles.SphereHandleCap(0, m_vertsP1[i], Quaternion.identity, 1f, EventType.Repaint);
            Handles.SphereHandleCap(0, m_vertsP2[i], Quaternion.identity, 1f, EventType.Repaint);
            Gizmos.DrawLine(m_vertsP1[i], m_vertsP2[i]);
        }
    }
}
