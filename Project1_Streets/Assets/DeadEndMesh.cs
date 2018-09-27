using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEndMesh : MonoBehaviour {

    private Vector3[] verts1;
    private int[] tris1;
    private int ntris1 = 0;
    private Mesh mesh1;

    private Vector3[] verts2;
    private int[] tris2;
    private int ntris2 = 0;
    private Mesh mesh2;

    private Vector3[] verts3;
    private int[] tris3;
    private int ntris3 = 0;
    private Mesh mesh3;

    private Vector3[] verts4;
    private int[] tris4;
    private int ntris4 = 0;
    private Mesh mesh4;

    void Awake() {
        mesh1 = new Mesh();
        mesh2 = new Mesh();
        mesh3 = new Mesh();
        mesh4 = new Mesh();

        int num_verts = 24;
        int num_tris = 12;  // need 2 triangles per face

        // -------------------------------- VARIATION 1 (end at top) --------------------------------
        verts1 = new Vector3[num_verts];
        // bottom
        verts1[0] = new Vector3(0.5f, -1, -1);
        verts1[1] = new Vector3(0.5f, -1, 0.5f);
        verts1[2] = new Vector3(-0.5f, -1, 0.5f);
        verts1[3] = new Vector3(-0.5f, -1, -1);
        // top
        verts1[4] = new Vector3(-0.5f, 1, -1);
        verts1[5] = new Vector3(-0.5f, 1, 0.5f);
        verts1[6] = new Vector3(0.5f, 1, 0.5f);
        verts1[7] = new Vector3(0.5f, 1, -1);
        // left
        verts1[8] = new Vector3(-0.5f, 1, 0.5f);
        verts1[9] = new Vector3(-0.5f, 1, -1);
        verts1[10] = new Vector3(-0.5f, -1, -1);
        verts1[11] = new Vector3(-0.5f, -1, 0.5f);
        // back
        verts1[12] = new Vector3(0.5f, 1, 0.5f);
        verts1[13] = new Vector3(-0.5f, 1, 0.5f);
        verts1[14] = new Vector3(-0.5f, -1, 0.5f);
        verts1[15] = new Vector3(0.5f, -1, 0.5f);
        // right
        verts1[16] = new Vector3(0.5f, 1, -1);
        verts1[17] = new Vector3(0.5f, 1, 0.5f);
        verts1[18] = new Vector3(0.5f, -1, 0.5f);
        verts1[19] = new Vector3(0.5f, -1, -1);
        // front
        verts1[20] = new Vector3(-0.5f, 1, -1);
        verts1[21] = new Vector3(0.5f, 1, -1);
        verts1[22] = new Vector3(0.5f, -1, -1);
        verts1[23] = new Vector3(-0.5f, -1, -1);

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

        // -------------------------------- VARIATION 2 (end at right) --------------------------------
        verts2 = new Vector3[num_verts];
        // bottom
        verts2[0] = new Vector3(0.5f, -1, -0.5f);
        verts2[1] = new Vector3(0.5f, -1, 0.5f);
        verts2[2] = new Vector3(-1, -1, 0.5f);
        verts2[3] = new Vector3(-1, -1, -0.5f);
        // top
        verts2[4] = new Vector3(-1, 1, -0.5f);
        verts2[5] = new Vector3(-1, 1, 0.5f);
        verts2[6] = new Vector3(0.5f, 1, 0.5f);
        verts2[7] = new Vector3(0.5f, 1, -0.5f);
        // left
        verts2[8] = new Vector3(-1, 1, 0.5f);
        verts2[9] = new Vector3(-1, 1, -0.5f);
        verts2[10] = new Vector3(-1, -1, -0.5f);
        verts2[11] = new Vector3(-1, -1, 0.5f);
        // back
        verts2[12] = new Vector3(0.5f, 1, 0.5f);
        verts2[13] = new Vector3(-1, 1, 0.5f);
        verts2[14] = new Vector3(-1, -1, 0.5f);
        verts2[15] = new Vector3(0.5f, -1, 0.5f);
        // right
        verts2[16] = new Vector3(0.5f, 1, -0.5f);
        verts2[17] = new Vector3(0.5f, 1, 0.5f);
        verts2[18] = new Vector3(0.5f, -1, 0.5f);
        verts2[19] = new Vector3(0.5f, -1, -0.5f);
        // front
        verts2[20] = new Vector3(-1, 1, -0.5f);
        verts2[21] = new Vector3(0.5f, 1, -0.5f);
        verts2[22] = new Vector3(0.5f, -1, -0.5f);
        verts2[23] = new Vector3(-1, -1, -0.5f);

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

        // -------------------------------- VARIATION 3 (end at bottom) --------------------------------
        verts3 = new Vector3[num_verts];
        // bottom
        verts3[0] = new Vector3(0.5f, -1, -0.5f);
        verts3[1] = new Vector3(0.5f, -1, 1);
        verts3[2] = new Vector3(-0.5f, -1, 1);
        verts3[3] = new Vector3(-0.5f, -1, -0.5f);
        // top
        verts3[4] = new Vector3(-0.5f, 1, -0.5f);
        verts3[5] = new Vector3(-0.5f, 1, 1);
        verts3[6] = new Vector3(0.5f, 1, 1);
        verts3[7] = new Vector3(0.5f, 1, -0.5f);
        // left
        verts3[8] = new Vector3(-0.5f, 1, 1);
        verts3[9] = new Vector3(-0.5f, 1, -0.5f);
        verts3[10] = new Vector3(-0.5f, -1, -0.5f);
        verts3[11] = new Vector3(-0.5f, -1, 1);
        // back
        verts3[12] = new Vector3(0.5f, 1, 1);
        verts3[13] = new Vector3(-0.5f, 1, 1);
        verts3[14] = new Vector3(-0.5f, -1, 1);
        verts3[15] = new Vector3(0.5f, -1, 1);
        // right
        verts3[16] = new Vector3(0.5f, 1, -0.5f);
        verts3[17] = new Vector3(0.5f, 1, 1);
        verts3[18] = new Vector3(0.5f, -1, 1);
        verts3[19] = new Vector3(0.5f, -1, -0.5f);
        // front
        verts3[20] = new Vector3(-0.5f, 1, -0.5f);
        verts3[21] = new Vector3(0.5f, 1, -0.5f);
        verts3[22] = new Vector3(0.5f, -1, -0.5f);
        verts3[23] = new Vector3(-0.5f, -1, -0.5f);

        tris3 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 3);
        MakeQuad(4, 5, 6, 7, 3);
        MakeQuad(8, 9, 10, 11, 3);
        MakeQuad(12, 13, 14, 15, 3);
        MakeQuad(16, 17, 18, 19, 3);
        MakeQuad(20, 21, 22, 23, 3);

        mesh3.vertices = verts3;
        mesh3.triangles = tris3;
        mesh3.RecalculateNormals();

        // -------------------------------- VARIATION 4 (end at left) --------------------------------
        verts4 = new Vector3[num_verts];
        // bottom
        verts4[0] = new Vector3(1, -1, -0.5f);
        verts4[1] = new Vector3(1, -1, 0.5f);
        verts4[2] = new Vector3(-0.5f, -1, 0.5f);
        verts4[3] = new Vector3(-0.5f, -1, -0.5f);
        // top
        verts4[4] = new Vector3(-0.5f, 1, -0.5f);
        verts4[5] = new Vector3(-0.5f, 1, 0.5f);
        verts4[6] = new Vector3(1, 1, 0.5f);
        verts4[7] = new Vector3(1, 1, -0.5f);
        // left
        verts4[8] = new Vector3(-0.5f, 1, 0.5f);
        verts4[9] = new Vector3(-0.5f, 1, -0.5f);
        verts4[10] = new Vector3(-0.5f, -1, -0.5f);
        verts4[11] = new Vector3(-0.5f, -1, 0.5f);
        // back
        verts4[12] = new Vector3(1, 1, 0.5f);
        verts4[13] = new Vector3(-0.5f, 1, 0.5f);
        verts4[14] = new Vector3(-0.5f, -1, 0.5f);
        verts4[15] = new Vector3(1, -1, 0.5f);
        // right
        verts4[16] = new Vector3(1, 1, -0.5f);
        verts4[17] = new Vector3(1, 1, 0.5f);
        verts4[18] = new Vector3(1, -1, 0.5f);
        verts4[19] = new Vector3(1, -1, -0.5f);
        // front
        verts4[20] = new Vector3(-0.5f, 1, -0.5f);
        verts4[21] = new Vector3(1, 1, -0.5f);
        verts4[22] = new Vector3(1, -1, -0.5f);
        verts4[23] = new Vector3(-0.5f, -1, -0.5f);

        tris4 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 4);
        MakeQuad(4, 5, 6, 7, 4);
        MakeQuad(8, 9, 10, 11, 4);
        MakeQuad(12, 13, 14, 15, 4);
        MakeQuad(16, 17, 18, 19, 4);
        MakeQuad(20, 21, 22, 23, 4);

        mesh4.vertices = verts4;
        mesh4.triangles = tris4;
        mesh4.RecalculateNormals();
    }

    public Mesh GetMesh(int variation) {
        if (variation == 1) {
            return mesh1;
        } else if (variation == 2) {
            return mesh2;
        } else if (variation == 3) {
            return mesh3;
        } else {
            return mesh4;
        }
    }

    void MakeTri(int i1, int i2, int i3, int variation) {
        if (variation == 1) {
            int index = ntris1 * 3;
            ntris1++;
            tris1[index] = i1;
            tris1[index + 1] = i2;
            tris1[index + 2] = i3;
        } else if (variation == 2) {
            int index = ntris2 * 3;
            ntris2++;
            tris2[index] = i1;
            tris2[index + 1] = i2;
            tris2[index + 2] = i3;
        } else if (variation == 3) {
            int index = ntris3 * 3;
            ntris3++;
            tris3[index] = i1;
            tris3[index + 1] = i2;
            tris3[index + 2] = i3;
        } else {
            int index = ntris4 * 3;
            ntris4++;
            tris4[index] = i1;
            tris4[index + 1] = i2;
            tris4[index + 2] = i3;
        }

    }

    void MakeQuad(int i1, int i2, int i3, int i4, int variation) {
        MakeTri(i1, i2, i3, variation);
        MakeTri(i1, i3, i4, variation);
    }

    void Update() {
    }
}
