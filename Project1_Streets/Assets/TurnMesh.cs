using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnMesh : MonoBehaviour {

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

        int num_verts = 40;
        int num_tris = 20;  // need 2 triangles per face

        // -------------------------------- VARIATION 1 (left and bottom) --------------------------------
        verts1 = new Vector3[num_verts];

        // bottom long
        verts1[0] = new Vector3(0.5f, -1, -1);
        verts1[1] = new Vector3(0.5f, -1, 0.5f);
        verts1[2] = new Vector3(-0.5f, -1, 0.5f);
        verts1[3] = new Vector3(-0.5f, -1, -1);
        // bottom left nubbin
        verts1[4] = new Vector3(-0.5f, -1, -0.5f);
        verts1[5] = new Vector3(-0.5f, -1, 0.5f);
        verts1[6] = new Vector3(-1, -1, 0.5f);
        verts1[7] = new Vector3(-1, -1, -0.5f);
        // top long
        verts1[8] = new Vector3(-0.5f, 1, -1);
        verts1[9] = new Vector3(-0.5f, 1, 0.5f);
        verts1[10] = new Vector3(0.5f, 1, 0.5f);
        verts1[11] = new Vector3(0.5f, 1, -1);
        // top left nubbin
        verts1[12] = new Vector3(-1, 1, -0.5f);
        verts1[13] = new Vector3(-1, 1, 0.5f);
        verts1[14] = new Vector3(-0.5f, 1, 0.5f);
        verts1[15] = new Vector3(-0.5f, 1, -0.5f);
        // left (far)
        verts1[16] = new Vector3(-1, 1, 0.5f);
        verts1[17] = new Vector3(-1, 1, -0.5f);
        verts1[18] = new Vector3(-1, -1, -0.5f);
        verts1[19] = new Vector3(-1, -1, 0.5f);
        // left (near)
        verts1[20] = new Vector3(-0.5f, 1, -0.5f);
        verts1[21] = new Vector3(-0.5f, 1, -1);
        verts1[22] = new Vector3(-0.5f, -1, -1);
        verts1[23] = new Vector3(-0.5f, -1, -0.5f);
        // back
        verts1[24] = new Vector3(0.5f, 1, 0.5f);
        verts1[25] = new Vector3(-1, 1, 0.5f);
        verts1[26] = new Vector3(-1, -1, 0.5f);
        verts1[27] = new Vector3(0.5f, -1, 0.5f);
        // right
        verts1[28] = new Vector3(0.5f, 1, -1);
        verts1[29] = new Vector3(0.5f, 1, 0.5f);
        verts1[30] = new Vector3(0.5f, -1, 0.5f);
        verts1[31] = new Vector3(0.5f, -1, -1);
        // front (left)
        verts1[32] = new Vector3(-1, 1, -0.5f);
        verts1[33] = new Vector3(-0.5f, 1, -0.5f);
        verts1[34] = new Vector3(-0.5f, -1, -0.5f);
        verts1[35] = new Vector3(-1, -1, -0.5f);
        // front (right)
        verts1[36] = new Vector3(-0.5f, 1, -1);
        verts1[37] = new Vector3(0.5f, 1, -1);
        verts1[38] = new Vector3(0.5f, -1, -1);
        verts1[39] = new Vector3(-0.5f, -1, -1);

        tris1 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 1);
        MakeQuad(4, 5, 6, 7, 1);
        MakeQuad(8, 9, 10, 11, 1);
        MakeQuad(12, 13, 14, 15, 1);
        MakeQuad(16, 17, 18, 19, 1);
        MakeQuad(20, 21, 22, 23, 1);
        MakeQuad(24, 25, 26, 27, 1);
        MakeQuad(28, 29, 30, 31, 1);
        MakeQuad(32, 33, 34, 35, 1);
        MakeQuad(36, 37, 38, 39, 1);

        mesh1.vertices = verts1;
        mesh1.triangles = tris1;
        mesh1.RecalculateNormals();

        // -------------------------------- VARIATION 2 (left and top) --------------------------------
        verts2 = new Vector3[num_verts];

        // bottom long
        verts2[0] = new Vector3(0.5f, -1, -0.5f);
        verts2[1] = new Vector3(0.5f, -1, 1);
        verts2[2] = new Vector3(-0.5f, -1, 1);
        verts2[3] = new Vector3(-0.5f, -1, -0.5f);
        // bottom left nubbin
        verts2[4] = new Vector3(-0.5f, -1, -0.5f);
        verts2[5] = new Vector3(-0.5f, -1, 0.5f);
        verts2[6] = new Vector3(-1, -1, 0.5f);
        verts2[7] = new Vector3(-1, -1, -0.5f);
        // top long
        verts2[8] = new Vector3(-0.5f, 1, -0.5f);
        verts2[9] = new Vector3(-0.5f, 1, 1);
        verts2[10] = new Vector3(0.5f, 1, 1);
        verts2[11] = new Vector3(0.5f, 1, -0.5f);
        // top left nubbin
        verts2[12] = new Vector3(-1, 1, -0.5f);
        verts2[13] = new Vector3(-1, 1, 0.5f);
        verts2[14] = new Vector3(-0.5f, 1, 0.5f);
        verts2[15] = new Vector3(-0.5f, 1, -0.5f);
        // left (far)
        verts2[16] = new Vector3(-0.5f, 1, 1);
        verts2[17] = new Vector3(-0.5f, 1, 0.5f);
        verts2[18] = new Vector3(-0.5f, -1, 0.5f);
        verts2[19] = new Vector3(-0.5f, -1, 1);
        // left (near)
        verts2[20] = new Vector3(-1, 1, 0.5f);
        verts2[21] = new Vector3(-1, 1, -0.5f);
        verts2[22] = new Vector3(-1, -1, -0.5f);
        verts2[23] = new Vector3(-1, -1, 0.5f);
        // back (left)
        verts2[24] = new Vector3(-0.5f, 1, 0.5f);
        verts2[25] = new Vector3(-1, 1, 0.5f);
        verts2[26] = new Vector3(-1, -1, 0.5f);
        verts2[27] = new Vector3(-0.5f, -1, 0.5f);
        // back (right)
        verts2[28] = new Vector3(0.5f, 1, 1);
        verts2[29] = new Vector3(-0.5f, 1, 1);
        verts2[30] = new Vector3(-0.5f, -1, 1);
        verts2[31] = new Vector3(0.5f, -1, 1);
        // right
        verts2[32] = new Vector3(0.5f, 1, -0.5f);
        verts2[33] = new Vector3(0.5f, 1, 1);
        verts2[34] = new Vector3(0.5f, -1, 1);
        verts2[35] = new Vector3(0.5f, -1, -0.5f);
        // front
        verts2[36] = new Vector3(-1, 1, -0.5f);
        verts2[37] = new Vector3(0.5f, 1, -0.5f);
        verts2[38] = new Vector3(0.5f, -1, -0.5f);
        verts2[39] = new Vector3(-1, -1, -0.5f);

        tris2 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 2);
        MakeQuad(4, 5, 6, 7, 2);
        MakeQuad(8, 9, 10, 11, 2);
        MakeQuad(12, 13, 14, 15, 2);
        MakeQuad(16, 17, 18, 19, 2);
        MakeQuad(20, 21, 22, 23, 2);
        MakeQuad(24, 25, 26, 27, 2);
        MakeQuad(28, 29, 30, 31, 2);
        MakeQuad(32, 33, 34, 35, 2);
        MakeQuad(36, 37, 38, 39, 2);

        mesh2.vertices = verts2;
        mesh2.triangles = tris2;
        mesh2.RecalculateNormals();

        // -------------------------------- VARIATION 3 (right and top) --------------------------------
        verts3 = new Vector3[num_verts];

        // bottom long
        verts3[0] = new Vector3(0.5f, -1, -0.5f);
        verts3[1] = new Vector3(0.5f, -1, 1);
        verts3[2] = new Vector3(-0.5f, -1, 1);
        verts3[3] = new Vector3(-0.5f, -1, -0.5f);
        // bottom right nubbin
        verts3[4] = new Vector3(1, -1, -0.5f);
        verts3[5] = new Vector3(1, -1, 0.5f);
        verts3[6] = new Vector3(0.5f, -1, 0.5f);
        verts3[7] = new Vector3(0.5f, -1, -0.5f);
        // top long
        verts3[8] = new Vector3(-0.5f, 1, -0.5f);
        verts3[9] = new Vector3(-0.5f, 1, 1);
        verts3[10] = new Vector3(0.5f, 1, 1);
        verts3[11] = new Vector3(0.5f, 1, -0.5f);
        // top right nubbin
        verts3[12] = new Vector3(0.5f, 1, -0.5f);
        verts3[13] = new Vector3(0.5f, 1, 0.5f);
        verts3[14] = new Vector3(1, 1, 0.5f);
        verts3[15] = new Vector3(1, 1, -0.5f);
        // left
        verts3[16] = new Vector3(-0.5f, 1, 1);
        verts3[17] = new Vector3(-0.5f, 1, -0.5f);
        verts3[18] = new Vector3(-0.5f, -1, -0.5f);
        verts3[19] = new Vector3(-0.5f, -1, 1);
        // back (left)
        verts3[20] = new Vector3(0.5f, 1, 1);
        verts3[21] = new Vector3(-0.5f, 1, 1);
        verts3[22] = new Vector3(-0.5f, -1, 1);
        verts3[23] = new Vector3(0.5f, -1, 1);
        // back (right)
        verts3[24] = new Vector3(1, 1, 0.5f);
        verts3[25] = new Vector3(0.5f, 1, 0.5f);
        verts3[26] = new Vector3(0.5f, -1, 0.5f);
        verts3[27] = new Vector3(1, -1, 0.5f);
        // right (far)
        verts3[28] = new Vector3(0.5f, 1, 0.5f);
        verts3[29] = new Vector3(0.5f, 1, 1);
        verts3[30] = new Vector3(0.5f, -1, 1);
        verts3[31] = new Vector3(0.5f, -1, 0.5f);
        // right (near)
        verts3[32] = new Vector3(1, 1, -0.5f);
        verts3[33] = new Vector3(1, 1, 0.5f);
        verts3[34] = new Vector3(1, -1, 0.5f);
        verts3[35] = new Vector3(1, -1, -0.5f);
        // front
        verts3[36] = new Vector3(-0.5f, 1, -0.5f);
        verts3[37] = new Vector3(1, 1, -0.5f);
        verts3[38] = new Vector3(1, -1, -0.5f);
        verts3[39] = new Vector3(-0.5f, -1, -0.5f);

        tris3 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 3);
        MakeQuad(4, 5, 6, 7, 3);
        MakeQuad(8, 9, 10, 11, 3);
        MakeQuad(12, 13, 14, 15, 3);
        MakeQuad(16, 17, 18, 19, 3);
        MakeQuad(20, 21, 22, 23, 3);
        MakeQuad(24, 25, 26, 27, 3);
        MakeQuad(28, 29, 30, 31, 3);
        MakeQuad(32, 33, 34, 35, 3);
        MakeQuad(36, 37, 38, 39, 3);

        mesh3.vertices = verts3;
        mesh3.triangles = tris3;
        mesh3.RecalculateNormals();

        // -------------------------------- VARIATION 4 (right and bottom) --------------------------------
        verts4 = new Vector3[num_verts];

        // bottom long
        verts4[0] = new Vector3(0.5f, -1, -1);
        verts4[1] = new Vector3(0.5f, -1, 0.5f);
        verts4[2] = new Vector3(-0.5f, -1, 0.5f);
        verts4[3] = new Vector3(-0.5f, -1, -1);
        // bottom right nubbin
        verts4[4] = new Vector3(1, -1, -0.5f);
        verts4[5] = new Vector3(1, -1, 0.5f);
        verts4[6] = new Vector3(0.5f, -1, 0.5f);
        verts4[7] = new Vector3(0.5f, -1, -0.5f);
        // top long
        verts4[8] = new Vector3(-0.5f, 1, -1);
        verts4[9] = new Vector3(-0.5f, 1, 0.5f);
        verts4[10] = new Vector3(0.5f, 1, 0.5f);
        verts4[11] = new Vector3(0.5f, 1, -1);
        // top right nubbin
        verts4[12] = new Vector3(0.5f, 1, -0.5f);
        verts4[13] = new Vector3(0.5f, 1, 0.5f);
        verts4[14] = new Vector3(1, 1, 0.5f);
        verts4[15] = new Vector3(1, 1, -0.5f);
        // left
        verts4[16] = new Vector3(-0.5f, 1, 0.5f);
        verts4[17] = new Vector3(-0.5f, 1, -1);
        verts4[18] = new Vector3(-0.5f, -1, -1);
        verts4[19] = new Vector3(-0.5f, -1, 0.5f);
        // back
        verts4[20] = new Vector3(1, 1, 0.5f);
        verts4[21] = new Vector3(-0.5f, 1, 0.5f);
        verts4[22] = new Vector3(-0.5f, -1, 0.5f);
        verts4[23] = new Vector3(1, -1, 0.5f);
        // right (far)
        verts4[24] = new Vector3(1, 1, -0.5f);
        verts4[25] = new Vector3(1, 1, 0.5f);
        verts4[26] = new Vector3(1, -1, 0.5f);
        verts4[27] = new Vector3(1, -1, -0.5f);
        // right (near)
        verts4[28] = new Vector3(0.5f, 1, -1);
        verts4[29] = new Vector3(0.5f, 1, -0.5f);
        verts4[30] = new Vector3(0.5f, -1, -0.5f);
        verts4[31] = new Vector3(0.5f, -1, -1);
        // front (left)
        verts4[32] = new Vector3(-0.5f, 1, -1);
        verts4[33] = new Vector3(0.5f, 1, -1);
        verts4[34] = new Vector3(0.5f, -1, -1);
        verts4[35] = new Vector3(-0.5f, -1, -1);
        // front (right)
        verts4[36] = new Vector3(0.5f, 1, -0.5f);
        verts4[37] = new Vector3(1, 1, -0.5f);
        verts4[38] = new Vector3(1, -1, -0.5f);
        verts4[39] = new Vector3(0.5f, -1, -0.5f);

        tris4 = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3, 4);
        MakeQuad(4, 5, 6, 7, 4);
        MakeQuad(8, 9, 10, 11, 4);
        MakeQuad(12, 13, 14, 15, 4);
        MakeQuad(16, 17, 18, 19, 4);
        MakeQuad(20, 21, 22, 23, 4);
        MakeQuad(24, 25, 26, 27, 4);
        MakeQuad(28, 29, 30, 31, 4);
        MakeQuad(32, 33, 34, 35, 4);
        MakeQuad(36, 37, 38, 39, 4);

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
