using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uvMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*  theMesh = transform.GetComponent<MeshFilter>().mesh;
        //theUVs = new Vector2[theMesh.uv.Length];
        theUVs = theMesh.uv;
        Debug.Log(theMesh, gameObject);*/
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uvs = new Vector2[vertices.Length];

        for (int i = 0; i < uvs.Length; i++)
        {
            //Debug.Log(vertices[i].x+" "+vertices[i].y+" "+ vertices[i].z);
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        }
        mesh.uv = uvs;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
