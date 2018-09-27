using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour {

    private Vector3[] verts;  // the vertices of the mesh
    private int[] tris;       // the triangles of the mesh (triplets of integer references to vertices)
    private int ntris = 0;    // the number of triangles that have been created so far
    private Mesh mesh;

    void Awake() {
        mesh = new Mesh();
        int num_verts = 24;
        verts = new Vector3[num_verts];
        // bottom counterclockwise
        verts[0] = new Vector3(1, -1, -1);
        verts[1] = new Vector3(1, -1, 1);
        verts[2] = new Vector3(-1, -1, 1);
        verts[3] = new Vector3(-1, -1, -1);
        // top clockwise
        verts[4] = new Vector3(-1, 1, -1);
        verts[5] = new Vector3(-1, 1, 1);
        verts[6] = new Vector3(1, 1, 1);
        verts[7] = new Vector3(1, 1, -1);
        // left clockwise
        verts[8] = new Vector3(-1, 1, 1);
        verts[9] = new Vector3(-1, 1, -1);
        verts[10] = new Vector3(-1, -1, -1);
        verts[11] = new Vector3(-1, -1, 1);
        // back counterclockwise
        verts[12] = new Vector3(1, 1, 1);
        verts[13] = new Vector3(-1, 1, 1);
        verts[14] = new Vector3(-1, -1, 1);
        verts[15] = new Vector3(1, -1, 1);
        // right clockwise
        verts[16] = new Vector3(1, 1, -1);
        verts[17] = new Vector3(1, 1, 1);
        verts[18] = new Vector3(1, -1, 1);
        verts[19] = new Vector3(1, -1, -1);
        // front clockwise
        verts[20] = new Vector3(-1, 1, -1);
        verts[21] = new Vector3(1, 1, -1);
        verts[22] = new Vector3(1, -1, -1);
        verts[23] = new Vector3(-1, -1, -1);
        // squares that make up the cube faces

        int num_tris = 12;  // need 2 triangles per face
        tris = new int[num_tris * 3];  // need 3 vertices per triangle

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);
        MakeQuad(16, 17, 18, 19);
        MakeQuad(20, 21, 22, 23);


        // save the vertices and triangles in the mesh object
        mesh.vertices = verts;
        mesh.triangles = tris;

        mesh.RecalculateNormals();  // automatically calculate the vertex normals
    }

    public Mesh GetMesh() {
        return mesh;
    }

    // make a triangle from three vertex indices (clockwise order)
    void MakeTri(int i1, int i2, int i3) {
        int index = ntris * 3;  // figure out the base index for storing triangle indices
        ntris++;

        tris[index] = i1;
        tris[index + 1] = i2;
        tris[index + 2] = i3;
    }

    // make a quadrilateral from four vertex indices (clockwise order)
    void MakeQuad(int i1, int i2, int i3, int i4) {
        MakeTri(i1, i2, i3);
        MakeTri(i1, i3, i4);
	}

    // Update is called once per frame
    void Update() {

	}
}
