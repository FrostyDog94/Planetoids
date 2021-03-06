using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
public class SerializeMesh : MonoBehaviour
{
    [HideInInspector] [SerializeField] Vector2[] uv;
    [HideInInspector] [SerializeField] Vector3[] verticies;
    [HideInInspector] [SerializeField] int[] triangles;
    [HideInInspector] [SerializeField] bool serialized = false;
    // Use this for initialization

    void Awake()
    {
        if (serialized)
        {
            GetComponent<MeshFilter>().mesh = Rebuild();
        }
    }

    void Start()
    {
        if (serialized) return;

        Serialize();
    }

    public void Serialize()
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var collider = GetComponent<MeshCollider>();

        uv = mesh.uv;
        verticies = mesh.vertices;
        triangles = mesh.triangles;
        collider.sharedMesh = mesh;

        serialized = true;
    }

    public Mesh Rebuild()
    {
        Mesh mesh = new Mesh();
        MeshCollider collider = new MeshCollider();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.uv = uv;
        collider.sharedMesh = mesh;


        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SerializeMesh))]
class SerializeMeshEditor : Editor
{
    SerializeMesh obj;

    void OnSceneGUI()
    {
        obj = (SerializeMesh)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Rebuild"))
        {
            if (obj)
            {
                obj.gameObject.GetComponent<MeshFilter>().mesh = obj.Rebuild();
            }
        }

        if (GUILayout.Button("Serialize"))
        {
            if (obj)
            {
                obj.Serialize();
            }
        }
    }
}

#endif



