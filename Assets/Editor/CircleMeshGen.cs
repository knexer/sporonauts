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

        Vector3[] vertices = new Vector3[numSegments + 2];
        Vector2[] uvs = new Vector2[numSegments + 2];
        int[] triangles = new int[numSegments * 3];

        float angleStep = 360.0f / numSegments;

        vertices[0] = Vector3.zero;
        uvs[0] = new Vector2(0, 0);

        for (int i = 0; i <= numSegments; i++)
        {
            float angle = angleStep * i;
            vertices[i + 1] = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * radius;

            uvs[i + 1] = new Vector2(1, angle * Mathf.Deg2Rad);
        }

        for (int i = 0; i < numSegments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            if (i != numSegments - 1)
            {
                triangles[i * 3 + 2] = i + 2;
            }
            else
            {
                triangles[i * 3 + 2] = 1; // Close the loop for the last slice
            }
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
