using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourWayMesh : MonoBehaviour {

    private Vector3[] verts;
    private int[] tris;
    private int ntris = 0;
    private Mesh mesh;

    void Awake() {
        mesh = new Mesh();

        int num_verts = 72;
        int num_tris = 36;  // need 2 triangles per face
        
        verts = new Vector3[num_verts];
        // bottom long
        verts[0] = new Vector3(0.5f, -1, -1);
        verts[1] = new Vector3(0.5f, -1, 1);
        verts[2] = new Vector3(-0.5f, -1, 1);
        verts[3] = new Vector3(-0.5f, -1, -1);
        // bottom left nubbin
        verts[4] = new Vector3(-0.5f, -1, -0.5f);
        verts[5] = new Vector3(-0.5f, -1, 0.5f);
        verts[6] = new Vector3(-1, -1, 0.5f);
        verts[7] = new Vector3(-1, -1, -0.5f);
        // bottom right nubbin
        verts[8] = new Vector3(1, -1, -0.5f);
        verts[9] = new Vector3(1, -1, 0.5f);
        verts[10] = new Vector3(0.5f, -1, 0.5f);
        verts[11] = new Vector3(0.5f, -1, -0.5f);
        // top long
        verts[12] = new Vector3(-0.5f, 1, -1);
        verts[13] = new Vector3(-0.5f, 1, 1);
        verts[14] = new Vector3(0.5f, 1, 1);
        verts[15] = new Vector3(0.5f, 1, -1);
        // top left nubbin
        verts[16] = new Vector3(-1, 1, -0.5f);
        verts[17] = new Vector3(-1, 1, 0.5f);
        verts[18] = new Vector3(-0.5f, 1, 0.5f);
        verts[19] = new Vector3(-0.5f, 1, -0.5f);
        // top right nubbin
        verts[20] = new Vector3(0.5f, 1, -0.5f);
        verts[21] = new Vector3(0.5f, 1, 0.5f);
        verts[22] = new Vector3(1, 1, 0.5f);
        verts[23] = new Vector3(1, 1, -0.5f);

        // left (far)
        verts[24] = new Vector3(-0.5f, 1, 1);
        verts[25] = new Vector3(-0.5f, 1, 0.5f);
        verts[26] = new Vector3(-0.5f, -1, 0.5f);
        verts[27] = new Vector3(-0.5f, -1, 1);
        // left (mid)
        verts[28] = new Vector3(-1, 1, 0.5f);
        verts[29] = new Vector3(-1, 1, -0.5f);
        verts[30] = new Vector3(-1, -1, -0.5f);
        verts[31] = new Vector3(-1, -1, 0.5f);
        // left (near)
        verts[32] = new Vector3(-0.5f, 1, -0.5f);
        verts[33] = new Vector3(-0.5f, 1, -1);
        verts[34] = new Vector3(-0.5f, -1, -1);
        verts[35] = new Vector3(-0.5f, -1, -0.5f);

        // back (left)
        verts[36] = new Vector3(-0.5f, 1, 0.5f);
        verts[37] = new Vector3(-1, 1, 0.5f);
        verts[38] = new Vector3(-1, -1, 0.5f);
        verts[39] = new Vector3(-0.5f, -1, 0.5f);
        // back (mid)
        verts[40] = new Vector3(0.5f, 1, 1);
        verts[41] = new Vector3(-0.5f, 1, 1);
        verts[42] = new Vector3(-0.5f, -1, 1);
        verts[43] = new Vector3(0.5f, -1, 1);
        // back (right)
        verts[44] = new Vector3(1, 1, 0.5f);
        verts[45] = new Vector3(0.5f, 1, 0.5f);
        verts[46] = new Vector3(0.5f, -1, 0.5f);
        verts[47] = new Vector3(1, -1, 0.5f);

        // right (far)
        verts[48] = new Vector3(0.5f, 1, 0.5f);
        verts[49] = new Vector3(0.5f, 1, 1);
        verts[50] = new Vector3(0.5f, -1, 1);
        verts[51] = new Vector3(0.5f, -1, 0.5f);
        // right (mid)
        verts[52] = new Vector3(1, 1, -0.5f);
        verts[53] = new Vector3(1, 1, 0.5f);
        verts[54] = new Vector3(1, -1, 0.5f);
        verts[55] = new Vector3(1, -1, -0.5f);
        // right (near)
        verts[56] = new Vector3(0.5f, 1, -1);
        verts[57] = new Vector3(0.5f, 1, -0.5f);
        verts[58] = new Vector3(0.5f, -1, -0.5f);
        verts[59] = new Vector3(0.5f, -1, -1);

        // front (left)
        verts[60] = new Vector3(-1, 1, -0.5f);
        verts[61] = new Vector3(-0.5f, 1, -0.5f);
        verts[62] = new Vector3(-0.5f, -1, -0.5f);
        verts[63] = new Vector3(-1, -1, -0.5f);
        // front (mid)
        verts[64] = new Vector3(-0.5f, 1, -1);
        verts[65] = new Vector3(0.5f, 1, -1);
        verts[66] = new Vector3(0.5f, -1, -1);
        verts[67] = new Vector3(-0.5f, -1, -1);
        // front (right)
        verts[68] = new Vector3(0.5f, 1, -0.5f);
        verts[69] = new Vector3(1, 1, -0.5f);
        verts[70] = new Vector3(1, -1, -0.5f);
        verts[71] = new Vector3(0.5f, -1, -0.5f);

        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);
        MakeQuad(16, 17, 18, 19);
        MakeQuad(20, 21, 22, 23);
        MakeQuad(24, 25, 26, 27);
        MakeQuad(28, 29, 30, 31);
        MakeQuad(32, 33, 34, 35);
        MakeQuad(36, 37, 38, 39);
        MakeQuad(40, 41, 42, 43);
        MakeQuad(44, 45, 46, 47);
        MakeQuad(48, 49, 50, 51);
        MakeQuad(52, 53, 54, 55);
        MakeQuad(56, 57, 58, 59);
        MakeQuad(60, 61, 62, 63);
        MakeQuad(64, 65, 66, 67);
        MakeQuad(68, 69, 70, 71);

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
    }

    public Mesh GetMesh() {
        return mesh;
    }

    void MakeTri(int i1, int i2, int i3) {
        int index = ntris * 3;
        ntris++;
        tris[index] = i1;
        tris[index + 1] = i2;
        tris[index + 2] = i3;

    }

    void MakeQuad(int i1, int i2, int i3, int i4) {
        MakeTri(i1, i2, i3);
        MakeTri(i1, i3, i4);
    }

    void Update() {
    }
}
