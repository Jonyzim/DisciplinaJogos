using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        Vector3[] vertices = new Vector3[3]{
            new Vector3(0, 0, 0),
            new Vector3(0, 100, 0),
            new Vector3(100, 100, 0),
        };
        Vector2[] uv = new Vector2[3];
        int [] triangles = new int[3]{
            0, 1, 2
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
