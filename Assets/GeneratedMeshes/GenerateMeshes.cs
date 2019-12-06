using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class GenerateMeshes : MonoBehaviour
{
    [MenuItem("Assets/Generate A Bunch of Meshes")]
    public static void GenerateSomeMeshes()
    {
        System.IO.Directory.CreateDirectory("Assets/GeneratedMeshes");

        {
            var m = GenIcosahedron(
                "",
                new[]
                {
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, 0),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 1),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 2, 1),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 1),
                }, false, false);
            AssetDatabase.CreateAsset(m, $"Assets/GeneratedMeshes/{m.name}.asset");
        }
        {
            var m = GenIcosahedron(
                "-F16PosNorTan-F32Color",
                new[]
                {
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float16, 4, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Normal, VertexAttributeFormat.Float16, 4, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Tangent, VertexAttributeFormat.Float16, 4, 0),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 0),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord1, VertexAttributeFormat.Float32, 2, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.Float32, 4, 0),
                }, false, false);
            AssetDatabase.CreateAsset(m, $"Assets/GeneratedMeshes/{m.name}.asset");
        }
        {
            var m = GenIcosahedron(
                "-MultiSubMeshes",
                new[]
                {
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 1),
                }, true, false);
            AssetDatabase.CreateAsset(m, $"Assets/GeneratedMeshes/{m.name}.asset");
        }
        {
            var m = GenIcosahedron(
                "-Lines",
                new[]
                {
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, 0),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 1),
                }, false, true);
            AssetDatabase.CreateAsset(m, $"Assets/GeneratedMeshes/{m.name}.asset");
        }
    }

    static Mesh GenIcosahedron(string name, VertexAttributeDescriptor[] attribs, bool multiSubmeshes, bool linesTopology)
    {
        var m = new Mesh();
        m.name = "Ico" + name;
        var t = (1f + Mathf.Sqrt(5f)) / 2f;
        var verts = new[]
        {
            new Vector3(-1,+t,0),
            new Vector3(+1,+t,0),
            new Vector3(-1,-t,0),
            new Vector3(+1,-t,0),
            
            new Vector3(0,-1,+t),
            new Vector3(0,+1,+t),
            new Vector3(0,-1,-t),
            new Vector3(0,+1,-t),
            
            new Vector3(+t,0,-1),
            new Vector3(+t,0,+1),
            new Vector3(-t,0,-1),
            new Vector3(-t,0,+1),
        };
        var indices = new[] {
            0, 11, 5,
            0, 5, 1,
            0, 1, 7,
            0, 7, 10,
            0, 10, 11,
            1, 5, 9,
            5, 11, 4,
            11, 10, 2,
            10, 7, 6,
            7, 1, 8,
            3, 9, 4,
            3, 4, 2,
            3, 2, 6,
            3, 6, 8,
            3, 8, 9,
            4, 9, 5,
            2, 4, 11,
            6, 2, 10,
            8, 6, 7,
            9, 8, 1,
        };
        m.vertices = verts;
        m.triangles = indices;

        if (multiSubmeshes)
        {
            m.subMeshCount = 4;
            m.SetTriangles(indices.Skip(0).Take(15).ToArray(), 0);
            m.SetTriangles(indices.Skip(15).Take(15).ToArray(), 1);
            m.SetTriangles(indices.Skip(30).Take(15).ToArray(), 2);
            m.SetTriangles(indices.Skip(45).Take(15).ToArray(), 3);
        }
        if (attribs.Any(e => e.attribute == VertexAttribute.TexCoord0))
            m.uv = verts.Select(v => new Vector2(v.x*0.5f, v.y*0.5f)).ToArray();
        if (attribs.Any(e => e.attribute == VertexAttribute.TexCoord1))
            m.uv2 = verts.Select(v => new Vector2(v.x*0.3f, v.z*0.3f)).ToArray();
        m.RecalculateNormals();
        m.RecalculateTangents();
        if (attribs.Any(e => e.attribute == VertexAttribute.Color))
            m.colors = m.normals.Select(v => new Color(-v.x*0.5f+0.5f, Mathf.Cos(v.y*7f)*0.5f+0.5f, Mathf.Sin(v.z*5f)*0.5f+0.5f, 0.5f)).ToArray();
        
        m.SetVertexBufferParams(m.vertexCount, attribs);

        if (linesTopology)
        {
            var lines = new List<int>();
            for (var i = 0; i < indices.Length; i += 3)
            {
                var i0 = indices[i + 0];
                var i1 = indices[i + 1];
                var i2 = indices[i + 2];
                lines.AddRange(new[]{i0,i1, i1,i2, i2,i0});
            }
            m.SetIndices(lines, MeshTopology.Lines, 0);
        }
        return m;
    }
}
