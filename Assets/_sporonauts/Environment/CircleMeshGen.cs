using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[RequireComponent(typeof(Planet), typeof(CircleCollider2D))]
[ExecuteInEditMode]
public class CircleGenerator : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private int numSegments = 64;
    [SerializeField] private Color color = Color.white;

    private void OnValidate()
    {
        EditorApplication.delayCall += GenerateCircle;
    }

    private void GenerateCircle()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[numSegments + 1];
        int[] triangles = new int[numSegments * 3];

        float angleStep = 360.0f / numSegments;
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = angleStep * i;
            vertices[i] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;
        }

        for (int i = 0; i < numSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 2) % (numSegments + 1);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        #if UNITY_EDITOR
        // The path where you want to save the mesh
        string path = "Assets/Circle.asset";

        // Create the mesh asset
        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        #endif
    }
}
