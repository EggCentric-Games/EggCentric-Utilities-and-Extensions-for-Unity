using UnityEngine;

namespace EggCentric.Geometry.MeshGeneration
{
    //GPT Generated
    public static class MeshGenerationUtil
    {
        public static Mesh GenerateCylinderMesh(Vector3 normal, float height, float radius, int precision = 16)
        {
            Vector3[] vertices = PointPlacementUtil.GetCylinder(normal, height, radius, precision);

            int resolution = vertices.Length / 2;
            if (resolution < 3 || vertices.Length != resolution * 2)
            {
                Debug.LogError("Invalid cylinder vertex data.");
                return null;
            }

            Mesh mesh = new Mesh();
            mesh.name = "GeneratedCylinder";

            int[] triangles = new int[resolution * 12]; // 2 triangles per side + 1 cap (top + bottom)

            int triIndex = 0;

            // Side triangles
            for (int i = 0; i < resolution; i++)
            {
                int next = (i + 1) % resolution;

                // bottom[i], bottom[next], top[i]
                triangles[triIndex++] = i;
                triangles[triIndex++] = next;
                triangles[triIndex++] = i + resolution;

                // top[i], bottom[next], top[next]
                triangles[triIndex++] = i + resolution;
                triangles[triIndex++] = next;
                triangles[triIndex++] = next + resolution;
            }

            // Bottom cap
            for (int i = 1; i < resolution - 1; i++)
            {
                triangles[triIndex++] = 0;
                triangles[triIndex++] = i + 1;
                triangles[triIndex++] = i;
            }

            // Top cap
            for (int i = 1; i < resolution - 1; i++)
            {
                triangles[triIndex++] = resolution;
                triangles[triIndex++] = resolution + i;
                triangles[triIndex++] = resolution + i + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals(); // Optional for visuals
            mesh.RecalculateBounds();

            return mesh;
        }
    }
}