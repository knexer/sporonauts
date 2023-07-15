using UnityEditor;
using UnityEngine;

public class CircleGeneratorEditor
{
    [MenuItem("Tools/Generate Circle Mesh")]
    public static void GenerateCircle()
    {
        int numSegments = 64;
        float radius = 30.0f;
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(numSegments + 1) * 2];
        Vector2[] uvs = new Vector2[(numSegments + 1) * 2];
        int[] triangles = new int[numSegments * 3];

        float angleStep = 360.0f / numSegments;

        for (int i = 0; i < numSegments + 1; i++)
        {
            float angle = angleStep * i;
            vertices[i * 2] = Vector3.zero;
            vertices[i * 2 + 1] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            uvs[i * 2] = new Vector2(0, angle / 360.0f);
            uvs[i * 2 + 1] = new Vector2(1, angle / 360.0f);
        }

        for (int i = 0; i < numSegments; i++)
        {
            triangles[i * 3] = i * 2;
            triangles[i * 3 + 1] = i * 2 + 1;
            triangles[i * 3 + 2] = (i + 1) * 2 + 1;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        #if UNITY_EDITOR
        // The path where you want to save the mesh
        string path = "Assets/Circle.asset";

        // Create the mesh asset
        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
        #endif
    }
}
