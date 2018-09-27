using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMesh : MonoBehaviour {

    private Vector3[] verts1;
    private int[] tris1;
    private int ntris1 = 0;
    private Mesh mesh1;
    
    private Vector3[] verts2;
    private int[] tris2;
    private int ntris2 = 0;
    private Mesh mesh2;

    void Awake() {
        mesh1 = new Mesh();
        mesh2 = new Mesh();

        int num_verts = 24;
        int num_tris = 12;  // need 2 triangles per face

        // -------------------------------- VARIATION 1 (horizontal) --------------------------------
        verts1 = new Vector3[num_verts];
        // bottom counterclockwise
        verts1[0] = new Vector3(1, -1, -0.5f);
        verts1[1] = new Vector3(1, -1, 0.5f);
        verts1[2] = new Vector3(-1, -1, 0.5f);
        verts1[3] = new Vector3(-1, -1, -0.5f);
        // top clockwise
        verts1[4] = new Vector3(-1, 1, -0.5f);
        verts1[5] = new Vector3(-1, 1, 0.5f);
        verts1[6] = new Vector3(1, 1, 0.5f);
        verts1[7] = new Vector3(1, 1, -0.5f);
        // left clockwise
        verts1[8] = new Vector3(-1, 1, 0.5f);
        verts1[9] = new Vector3(-1, 1, -0.5f);
        verts1[10] = new Vector3(-1, -1, -0.5f);
        verts1[11] = new Vector3(-1, -1, 0.5f);
        // back counterclockwise
        verts1[12] = new Vector3(1, 1, 0.5f);
        verts1[13] = new Vector3(-1, 1, 0.5f);
        verts1[14] = new Vector3(-1, -1, 0.5f);
        verts1[15] = new Vector3(1, -1, 0.5f);
        // right clockwise
        verts1[16] = new Vector3(1, 1, -0.5f);
        verts1[17] = new Vector3(1, 1, 0.5f);
        verts1[18] = new Vector3(1, -1, 0.5f);
        verts1[19] = new Vector3(1, -1, -0.5f);
        // front clockwise
        verts1[20] = new Vector3(-1, 1, -0.5f);
        verts1[21] = new Vector3(1, 1, -0.5f);
        verts1[22] = new Vector3(1, -1, -0.5f);
        verts1[23] = new Vector3(-1, -1, -0.5f);

        tris1 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 1);
        MakeQuad(4, 5, 6, 7, 1);
        MakeQuad(8, 9, 10, 11, 1);
        MakeQuad(12, 13, 14, 15, 1);
        MakeQuad(16, 17, 18, 19, 1);
        MakeQuad(20, 21, 22, 23, 1);

        mesh1.vertices = verts1;
        mesh1.triangles = tris1;
        mesh1.RecalculateNormals();

        // -------------------------------- VARIATION 2 (vertical) --------------------------------
        verts2 = new Vector3[num_verts];
        // bottom counterclockwise
        verts2[0] = new Vector3(0.5f, -1, -1);
        verts2[1] = new Vector3(0.5f, -1, 1);
        verts2[2] = new Vector3(-0.5f, -1, 1);
        verts2[3] = new Vector3(-0.5f, -1, -1);
        // top clockwise
        verts2[4] = new Vector3(-0.5f, 1, -1);
        verts2[5] = new Vector3(-0.5f, 1, 1);
        verts2[6] = new Vector3(0.5f, 1, 1);
        verts2[7] = new Vector3(0.5f, 1, -1);
        // left clockwise
        verts2[8] = new Vector3(-0.5f, 1, 1);
        verts2[9] = new Vector3(-0.5f, 1, -1);
        verts2[10] = new Vector3(-0.5f, -1, -1);
        verts2[11] = new Vector3(-0.5f, -1, 1);
        // back counterclockwise
        verts2[12] = new Vector3(0.5f, 1, 1);
        verts2[13] = new Vector3(-0.5f, 1, 1);
        verts2[14] = new Vector3(-0.5f, -1, 1);
        verts2[15] = new Vector3(0.5f, -1, 1);
        // right clockwise
        verts2[16] = new Vector3(0.5f, 1, -1);
        verts2[17] = new Vector3(0.5f, 1, 1);
        verts2[18] = new Vector3(0.5f, -1, 1);
        verts2[19] = new Vector3(0.5f, -1, -1);
        // front clockwise
        verts2[20] = new Vector3(-0.5f, 1, -1);
        verts2[21] = new Vector3(0.5f, 1, -1);
        verts2[22] = new Vector3(0.5f, -1, -1);
        verts2[23] = new Vector3(-0.5f, -1, -1);

        tris2 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 2);
        MakeQuad(4, 5, 6, 7, 2);
        MakeQuad(8, 9, 10, 11, 2);
        MakeQuad(12, 13, 14, 15, 2);
        MakeQuad(16, 17, 18, 19, 2);
        MakeQuad(20, 21, 22, 23, 2);

        mesh2.vertices = verts2;
        mesh2.triangles = tris2;
        mesh2.RecalculateNormals();
    }

    public Mesh GetMesh(int variation) {
        if (variation == 1) {
            return mesh1;
        } else {
            return mesh2;
        }
    }
    
    void MakeTri(int i1, int i2, int i3, int variation) {
        if (variation == 1) {
            int index = ntris1 * 3;
            ntris1++;
            tris1[index] = i1;
            tris1[index + 1] = i2;
            tris1[index + 2] = i3;
        } else {
            int index = ntris2 * 3;
            ntris2++;
            tris2[index] = i1;
            tris2[index + 1] = i2;
            tris2[index + 2] = i3;
        }
        
    }

    void MakeQuad(int i1, int i2, int i3, int i4, int variation) {
        MakeTri(i1, i2, i3, variation);
        MakeTri(i1, i3, i4, variation);
    }

    void Update() {
    }
}
