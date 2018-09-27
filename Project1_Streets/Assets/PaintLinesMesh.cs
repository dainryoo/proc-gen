using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintLinesMesh : MonoBehaviour {

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

    private Vector3[] verts5;
    private int[] tris5;
    private int ntris5 = 0;
    private Mesh mesh5;

    private Vector3[] verts6;
    private int[] tris6;
    private int ntris6 = 0;
    private Mesh mesh6;

    private Vector3[] verts7;
    private int[] tris7;
    private int ntris7 = 0;
    private Mesh mesh7;

    private Vector3[] verts8;
    private int[] tris8;
    private int ntris8 = 0;
    private Mesh mesh8;

    private Vector3[] verts9;
    private int[] tris9;
    private int ntris9 = 0;
    private Mesh mesh9;

    private Vector3[] verts10;
    private int[] tris10;
    private int ntris10 = 0;
    private Mesh mesh10;

    private Vector3[] verts11;
    private int[] tris11;
    private int ntris11 = 0;
    private Mesh mesh11;

    private Vector3[] verts12;
    private int[] tris12;
    private int ntris12 = 0;
    private Mesh mesh12;

    private Vector3[] verts13;
    private int[] tris13;
    private int ntris13 = 0;
    private Mesh mesh13;

    private Vector3[] verts14;
    private int[] tris14;
    private int ntris14 = 0;
    private Mesh mesh14;

    private Vector3[] verts15;
    private int[] tris15;
    private int ntris15 = 0;
    private Mesh mesh15;


    void Awake() {
        mesh1 = new Mesh();
        mesh2 = new Mesh();
        mesh3 = new Mesh();
        mesh4 = new Mesh();
        mesh5 = new Mesh();
        mesh6 = new Mesh();
        mesh7 = new Mesh();
        mesh8 = new Mesh();
        mesh9 = new Mesh();
        mesh10 = new Mesh();
        mesh11 = new Mesh();
        mesh12 = new Mesh();
        mesh13 = new Mesh();
        mesh14 = new Mesh();
        mesh15 = new Mesh();



        // Straight (horizontal)
        int num_verts = 16;
        int num_tris = 8;
        verts1 = new Vector3[num_verts];
        verts1[0] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts1[1] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts1[2] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts1[3] = new Vector3(-0.25f, 1.1f, -0.05f);
        verts1[4] = new Vector3(0.25f, 1.1f, -0.05f);
        verts1[5] = new Vector3(0.25f, 1.1f, 0.05f);
        verts1[6] = new Vector3(0.75f, 1.1f, 0.05f);
        verts1[7] = new Vector3(0.75f, 1.1f, -0.05f);
        verts1[8] = new Vector3(-1, 1.1f, 0.4f);
        verts1[9] = new Vector3(-1, 1.1f, 0.45f);
        verts1[10] = new Vector3(1, 1.1f, 0.45f);
        verts1[11] = new Vector3(1, 1.1f, 0.4f);
        verts1[12] = new Vector3(-1, 1.1f, -0.45f);
        verts1[13] = new Vector3(-1, 1.1f, -0.4f);
        verts1[14] = new Vector3(1, 1.1f, -0.4f);
        verts1[15] = new Vector3(1, 1.1f, -0.45f);
        tris1 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 1);
        MakeQuad(4, 5, 6, 7, 1);
        MakeQuad(8, 9, 10, 11, 1);
        MakeQuad(12, 13, 14, 15, 1);
        mesh1.vertices = verts1;
        mesh1.triangles = tris1;
        mesh1.RecalculateNormals();

        // Straight (vertical)
        verts2 = new Vector3[num_verts];
        verts2[0] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts2[1] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts2[2] = new Vector3(0.05f, 1.1f, 0.75f);
        verts2[3] = new Vector3(0.05f, 1.1f, 0.25f);
        verts2[4] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts2[5] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts2[6] = new Vector3(0.05f, 1.1f, -0.25f);
        verts2[7] = new Vector3(0.05f, 1.1f, -0.75f);
        verts2[8] = new Vector3(-0.45f, 1.1f, -1);
        verts2[9] = new Vector3(-0.45f, 1.1f, 1);
        verts2[10] = new Vector3(-0.4f, 1.1f, 1);
        verts2[11] = new Vector3(-0.4f, 1.1f, -1);
        verts2[12] = new Vector3(0.4f, 1.1f, -1);
        verts2[13] = new Vector3(0.4f, 1.1f, 1);
        verts2[14] = new Vector3(0.45f, 1.1f, 1);
        verts2[15] = new Vector3(0.45f, 1.1f, -1);
        tris2 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 2);
        MakeQuad(4, 5, 6, 7, 2);
        MakeQuad(8, 9, 10, 11, 2);
        MakeQuad(12, 13, 14, 15, 2);
        mesh2.vertices = verts2;
        mesh2.triangles = tris2;
        mesh2.RecalculateNormals();

        // Turn (left and bottom)
        num_verts = 24;
        num_tris = 12;
        verts3 = new Vector3[num_verts];
        verts3[0] = new Vector3(-1, 1.1f, 0.4f);
        verts3[1] = new Vector3(-1, 1.1f, 0.45f);
        verts3[2] = new Vector3(0.4f, 1.1f, 0.45f);
        verts3[3] = new Vector3(0.4f, 1.1f, 0.4f);
        verts3[4] = new Vector3(0.4f, 1.1f, -1);
        verts3[5] = new Vector3(0.4f, 1.1f, 0.45f);
        verts3[6] = new Vector3(0.45f, 1.1f, 0.45f);
        verts3[7] = new Vector3(0.45f, 1.1f, -1);
        verts3[8] = new Vector3(-1, 1.1f, -0.45f);
        verts3[9] = new Vector3(-1, 1.1f, -0.4f);
        verts3[10] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts3[11] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts3[12] = new Vector3(-0.45f, 1.1f, -1);
        verts3[13] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts3[14] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts3[15] = new Vector3(-0.4f, 1.1f, -1);
        verts3[16] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts3[17] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts3[18] = new Vector3(0.05f, 1.1f, -0.25f);
        verts3[19] = new Vector3(0.05f, 1.1f, -0.75f);
        verts3[20] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts3[21] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts3[22] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts3[23] = new Vector3(-0.25f, 1.1f, -0.05f);
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

        // Turn (left and top)
        verts4 = new Vector3[num_verts];
        verts4[0] = new Vector3(-1, 1.1f, 0.4f);
        verts4[1] = new Vector3(-1, 1.1f, 0.45f);
        verts4[2] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts4[3] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts4[4] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts4[5] = new Vector3(-0.45f, 1.1f, 1);
        verts4[6] = new Vector3(-0.4f, 1.1f, 1);
        verts4[7] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts4[8] = new Vector3(-1, 1.1f, -0.45f);
        verts4[9] = new Vector3(-1, 1.1f, -0.4f);
        verts4[10] = new Vector3(0.4f, 1.1f, -0.4f);
        verts4[11] = new Vector3(0.4f, 1.1f, -0.45f);
        verts4[12] = new Vector3(0.4f, 1.1f, -0.45f);
        verts4[13] = new Vector3(0.4f, 1.1f, 1);
        verts4[14] = new Vector3(0.45f, 1.1f, 1);
        verts4[15] = new Vector3(0.45f, 1.1f, -0.45f);
        verts4[16] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts4[17] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts4[18] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts4[19] = new Vector3(-0.25f, 1.1f, -0.05f);
        verts4[20] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts4[21] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts4[22] = new Vector3(0.05f, 1.1f, 0.75f);
        verts4[23] = new Vector3(0.05f, 1.1f, 0.25f);
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

        // Turn (right and top)
        verts5 = new Vector3[num_verts];
        verts5[0] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts5[1] = new Vector3(-0.45f, 1.1f, 1);
        verts5[2] = new Vector3(-0.4f, 1.1f, 1);
        verts5[3] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts5[4] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts5[5] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts5[6] = new Vector3(1, 1.1f, -0.4f);
        verts5[7] = new Vector3(1, 1.1f, -0.45f);
        verts5[8] = new Vector3(0.4f, 1.1f, 0.4f);
        verts5[9] = new Vector3(0.4f, 1.1f, 1);
        verts5[10] = new Vector3(0.45f, 1.1f, 1);
        verts5[11] = new Vector3(0.45f, 1.1f, 0.4f);
        verts5[12] = new Vector3(0.45f, 1.1f, 0.4f);
        verts5[13] = new Vector3(0.45f, 1.1f, 0.45f);
        verts5[14] = new Vector3(1, 1.1f, 0.45f);
        verts5[15] = new Vector3(1, 1.1f, 0.4f);
        verts5[16] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts5[17] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts5[18] = new Vector3(0.05f, 1.1f, 0.75f);
        verts5[19] = new Vector3(0.05f, 1.1f, 0.25f);
        verts5[20] = new Vector3(0.25f, 1.1f, -0.05f);
        verts5[21] = new Vector3(0.25f, 1.1f, 0.05f);
        verts5[22] = new Vector3(0.75f, 1.1f, 0.05f);
        verts5[23] = new Vector3(0.75f, 1.1f, -0.05f);
        tris5 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 5);
        MakeQuad(4, 5, 6, 7, 5);
        MakeQuad(8, 9, 10, 11, 5);
        MakeQuad(12, 13, 14, 15, 5);
        MakeQuad(16, 17, 18, 19, 5);
        MakeQuad(20, 21, 22, 23, 5);
        mesh5.vertices = verts5;
        mesh5.triangles = tris5;
        mesh5.RecalculateNormals();

        // Turn (right and bottom)
        verts6 = new Vector3[num_verts];
        verts6[0] = new Vector3(-0.45f, 1.1f, -1);
        verts6[1] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts6[2] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts6[3] = new Vector3(-0.4f, 1.1f, -1);
        verts6[4] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts6[5] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts6[6] = new Vector3(1, 1.1f, 0.45f);
        verts6[7] = new Vector3(1, 1.1f, 0.4f);
        verts6[8] = new Vector3(0.4f, 1.1f, -1);
        verts6[9] = new Vector3(0.4f, 1.1f, -0.4f);
        verts6[10] = new Vector3(0.45f, 1.1f, -0.4f);
        verts6[11] = new Vector3(0.45f, 1.1f, -1);
        verts6[12] = new Vector3(0.45f, 1.1f, -0.45f);
        verts6[13] = new Vector3(0.45f, 1.1f, -0.4f);
        verts6[14] = new Vector3(1, 1.1f, -0.4f);
        verts6[15] = new Vector3(1, 1.1f, -0.45f);
        verts6[16] = new Vector3(0.25f, 1.1f, -0.05f);
        verts6[17] = new Vector3(0.25f, 1.1f, 0.05f);
        verts6[18] = new Vector3(0.75f, 1.1f, 0.05f);
        verts6[19] = new Vector3(0.75f, 1.1f, -0.05f);
        verts6[20] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts6[21] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts6[22] = new Vector3(0.05f, 1.1f, -0.25f);
        verts6[23] = new Vector3(0.05f, 1.1f, -0.75f);
        tris6 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 6);
        MakeQuad(4, 5, 6, 7, 6);
        MakeQuad(8, 9, 10, 11, 6);
        MakeQuad(12, 13, 14, 15, 6);
        MakeQuad(16, 17, 18, 19, 6);
        MakeQuad(20, 21, 22, 23, 6);
        mesh6.vertices = verts6;
        mesh6.triangles = tris6;
        mesh6.RecalculateNormals();

        // Four way
        num_verts = 64;
        num_tris = 32;
        verts7 = new Vector3[num_verts];
        verts7[0] = new Vector3(-1, 1.1f, 0.4f);
        verts7[1] = new Vector3(-1, 1.1f, 0.45f);
        verts7[2] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts7[3] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts7[4] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts7[5] = new Vector3(-0.45f, 1.1f, 1);
        verts7[6] = new Vector3(-0.4f, 1.1f, 1);
        verts7[7] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts7[8] = new Vector3(0.4f, 1.1f, 0.4f);
        verts7[9] = new Vector3(0.4f, 1.1f, 1);
        verts7[10] = new Vector3(0.45f, 1.1f, 1);
        verts7[11] = new Vector3(0.45f, 1.1f, 0.4f);
        verts7[12] = new Vector3(0.45f, 1.1f, 0.4f);
        verts7[13] = new Vector3(0.45f, 1.1f, 0.45f);
        verts7[14] = new Vector3(1, 1.1f, 0.45f);
        verts7[15] = new Vector3(1, 1.1f, 0.4f);
        verts7[16] = new Vector3(-1, 1.1f, -0.45f);
        verts7[17] = new Vector3(-1, 1.1f, -0.4f);
        verts7[18] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts7[19] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts7[20] = new Vector3(-0.45f, 1.1f, -1);
        verts7[21] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts7[22] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts7[23] = new Vector3(-0.4f, 1.1f, -1);
        verts7[24] = new Vector3(0.4f, 1.1f, -1);
        verts7[25] = new Vector3(0.4f, 1.1f, -0.4f);
        verts7[26] = new Vector3(0.45f, 1.1f, -0.4f);
        verts7[27] = new Vector3(0.45f, 1.1f, -1);
        verts7[28] = new Vector3(0.45f, 1.1f, -0.45f);
        verts7[29] = new Vector3(0.45f, 1.1f, -0.4f);
        verts7[30] = new Vector3(1, 1.1f, -0.4f);
        verts7[31] = new Vector3(1, 1.1f, -0.45f);
        verts7[32] = new Vector3(-0.4f, 1.1f, 0.5f);
        verts7[33] = new Vector3(-0.4f, 1.1f, 0.55f);
        verts7[34] = new Vector3(0.4f, 1.1f, 0.55f);
        verts7[35] = new Vector3(0.4f, 1.1f, 0.5f);
        verts7[36] = new Vector3(-0.4f, 1.1f, 0.75f);
        verts7[37] = new Vector3(-0.4f, 1.1f, 0.8f);
        verts7[38] = new Vector3(0.4f, 1.1f, 0.8f);
        verts7[39] = new Vector3(0.4f, 1.1f, 0.75f);
        // Upper crosswalk
        verts7[32] = new Vector3(-0.4f, 1.1f, 0.5f);
        verts7[33] = new Vector3(-0.4f, 1.1f, 0.55f);
        verts7[34] = new Vector3(0.4f, 1.1f, 0.55f);
        verts7[35] = new Vector3(0.4f, 1.1f, 0.5f);
        verts7[36] = new Vector3(-0.4f, 1.1f, 0.75f);
        verts7[37] = new Vector3(-0.4f, 1.1f, 0.8f);
        verts7[38] = new Vector3(0.4f, 1.1f, 0.8f);
        verts7[39] = new Vector3(0.4f, 1.1f, 0.75f);
        // Left crosswalk
        verts7[40] = new Vector3(-0.8f, 1.1f, -0.4f);
        verts7[41] = new Vector3(-0.8f, 1.1f, 0.4f);
        verts7[42] = new Vector3(-0.75f, 1.1f, 0.4f);
        verts7[43] = new Vector3(-0.75f, 1.1f, -0.4f);
        verts7[44] = new Vector3(-0.55f, 1.1f, -0.4f);
        verts7[45] = new Vector3(-0.55f, 1.1f, 0.4f);
        verts7[46] = new Vector3(-0.5f, 1.1f, 0.4f);
        verts7[47] = new Vector3(-0.5f, 1.1f, -0.4f);
        // Right crosswalk
        verts7[48] = new Vector3(0.5f, 1.1f, -0.4f);
        verts7[49] = new Vector3(0.5f, 1.1f, 0.4f);
        verts7[50] = new Vector3(0.55f, 1.1f, 0.4f);
        verts7[51] = new Vector3(0.55f, 1.1f, -0.4f);
        verts7[52] = new Vector3(0.75f, 1.1f, -0.4f);
        verts7[53] = new Vector3(0.75f, 1.1f, 0.4f);
        verts7[54] = new Vector3(0.8f, 1.1f, 0.4f);
        verts7[55] = new Vector3(0.8f, 1.1f, -0.4f);
        // Lower crosswalk
        verts7[56] = new Vector3(-0.4f, 1.1f, -0.8f);
        verts7[57] = new Vector3(-0.4f, 1.1f, -0.75f);
        verts7[58] = new Vector3(0.4f, 1.1f, -0.75f);
        verts7[59] = new Vector3(0.4f, 1.1f, -0.8f);
        verts7[60] = new Vector3(-0.4f, 1.1f, -0.55f);
        verts7[61] = new Vector3(-0.4f, 1.1f, -0.5f);
        verts7[62] = new Vector3(0.4f, 1.1f, -0.5f);
        verts7[63] = new Vector3(0.4f, 1.1f, -0.55f);
        tris7 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 7);
        MakeQuad(4, 5, 6, 7, 7);
        MakeQuad(8, 9, 10, 11, 7);
        MakeQuad(12, 13, 14, 15, 7);
        MakeQuad(16, 17, 18, 19, 7);
        MakeQuad(20, 21, 22, 23, 7);
        MakeQuad(24, 25, 26, 27, 7);
        MakeQuad(28, 29, 30, 31, 7);
        MakeQuad(32, 33, 34, 35, 7);
        MakeQuad(36, 37, 38, 39, 7);
        MakeQuad(40, 41, 42, 43, 7);
        MakeQuad(44, 45, 46, 47, 7);
        MakeQuad(48, 49, 50, 51, 7);
        MakeQuad(52, 53, 54, 55, 7);
        MakeQuad(56, 57, 58, 59, 7);
        MakeQuad(60, 61, 62, 63, 7);
        mesh7.vertices = verts7;
        mesh7.triangles = tris7;
        mesh7.RecalculateNormals();

        // T Junction (cut at top)
        num_verts = 36;
        num_tris = 18;
        verts8 = new Vector3[num_verts];
        verts8[0] = new Vector3(-1, 1.1f, 0.4f);
        verts8[1] = new Vector3(-1, 1.1f, 0.45f);
        verts8[2] = new Vector3(1, 1.1f, 0.45f);
        verts8[3] = new Vector3(1, 1.1f, 0.4f);
        verts8[4] = new Vector3(-1, 1.1f, -0.45f);
        verts8[5] = new Vector3(-1, 1.1f, -0.4f);
        verts8[6] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts8[7] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts8[8] = new Vector3(-0.45f, 1.1f, -1);
        verts8[9] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts8[10] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts8[11] = new Vector3(-0.4f, 1.1f, -1);
        verts8[12] = new Vector3(0.4f, 1.1f, -1);
        verts8[13] = new Vector3(0.4f, 1.1f, -0.4f);
        verts8[14] = new Vector3(0.45f, 1.1f, -0.4f);
        verts8[15] = new Vector3(0.45f, 1.1f, -1);
        verts8[16] = new Vector3(0.45f, 1.1f, -0.45f);
        verts8[17] = new Vector3(0.45f, 1.1f, -0.4f);
        verts8[18] = new Vector3(1, 1.1f, -0.4f);
        verts8[19] = new Vector3(1, 1.1f, -0.45f);
        verts8[20] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts8[21] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts8[22] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts8[23] = new Vector3(-0.25f, 1.1f, -0.05f);
        verts8[24] = new Vector3(0.25f, 1.1f, -0.05f);
        verts8[25] = new Vector3(0.25f, 1.1f, 0.05f);
        verts8[26] = new Vector3(0.75f, 1.1f, 0.05f);
        verts8[27] = new Vector3(0.75f, 1.1f, -0.05f);
        verts8[28] = new Vector3(-0.4f, 1.1f, -0.8f);
        verts8[29] = new Vector3(-0.4f, 1.1f, -0.75f);
        verts8[30] = new Vector3(0.4f, 1.1f, -0.75f);
        verts8[31] = new Vector3(0.4f, 1.1f, -0.8f);
        verts8[32] = new Vector3(-0.4f, 1.1f, -0.55f);
        verts8[33] = new Vector3(-0.4f, 1.1f, -0.5f);
        verts8[34] = new Vector3(0.4f, 1.1f, -0.5f);
        verts8[35] = new Vector3(0.4f, 1.1f, -0.55f);
        tris8 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 8);
        MakeQuad(4, 5, 6, 7, 8);
        MakeQuad(8, 9, 10, 11, 8);
        MakeQuad(12, 13, 14, 15, 8);
        MakeQuad(16, 17, 18, 19, 8);
        MakeQuad(20, 21, 22, 23, 8);
        MakeQuad(24, 25, 26, 27, 8);
        MakeQuad(28, 29, 30, 31, 8);
        MakeQuad(32, 33, 34, 35, 8);
        mesh8.vertices = verts8;
        mesh8.triangles = tris8;
        mesh8.RecalculateNormals();

        // T Junction (cut at right)
        verts9 = new Vector3[num_verts];
        verts9[0] = new Vector3(-1, 1.1f, 0.4f);
        verts9[1] = new Vector3(-1, 1.1f, 0.45f);
        verts9[2] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts9[3] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts9[4] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts9[5] = new Vector3(-0.45f, 1.1f, 1);
        verts9[6] = new Vector3(-0.4f, 1.1f, 1);
        verts9[7] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts9[8] = new Vector3(-1, 1.1f, -0.45f);
        verts9[9] = new Vector3(-1, 1.1f, -0.4f);
        verts9[10] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts9[11] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts9[12] = new Vector3(-0.45f, 1.1f, -1);
        verts9[13] = new Vector3(-0.45f, 1.1f, -0.4f);
        verts9[14] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts9[15] = new Vector3(-0.4f, 1.1f, -1);
        verts9[16] = new Vector3(0.4f, 1.1f, -1);
        verts9[17] = new Vector3(0.4f, 1.1f, 1);
        verts9[18] = new Vector3(0.45f, 1.1f, 1);
        verts9[19] = new Vector3(0.45f, 1.1f, -1);
        verts9[20] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts9[21] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts9[22] = new Vector3(0.05f, 1.1f, 0.75f);
        verts9[23] = new Vector3(0.05f, 1.1f, 0.25f);
        verts9[24] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts9[25] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts9[26] = new Vector3(0.05f, 1.1f, -0.25f);
        verts9[27] = new Vector3(0.05f, 1.1f, -0.75f);
        verts9[28] = new Vector3(-0.8f, 1.1f, -0.4f);
        verts9[29] = new Vector3(-0.8f, 1.1f, 0.4f);
        verts9[30] = new Vector3(-0.75f, 1.1f, 0.4f);
        verts9[31] = new Vector3(-0.75f, 1.1f, -0.4f);
        verts9[32] = new Vector3(-0.55f, 1.1f, -0.4f);
        verts9[33] = new Vector3(-0.55f, 1.1f, 0.4f);
        verts9[34] = new Vector3(-0.5f, 1.1f, 0.4f);
        verts9[35] = new Vector3(-0.5f, 1.1f, -0.4f);
        tris9 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 9);
        MakeQuad(4, 5, 6, 7, 9);
        MakeQuad(8, 9, 10, 11, 9);
        MakeQuad(12, 13, 14, 15, 9);
        MakeQuad(16, 17, 18, 19, 9);
        MakeQuad(20, 21, 22, 23, 9);
        MakeQuad(24, 25, 26, 27, 9);
        MakeQuad(28, 29, 30, 31, 9);
        MakeQuad(32, 33, 34, 35, 9);
        mesh9.vertices = verts9;
        mesh9.triangles = tris9;
        mesh9.RecalculateNormals();

        // T Junction (cut at bottom)
        verts10 = new Vector3[num_verts];
        verts10[0] = new Vector3(-1, 1.1f, -0.45f);
        verts10[1] = new Vector3(-1, 1.1f, -0.4f);
        verts10[2] = new Vector3(1, 1.1f, -0.4f);
        verts10[3] = new Vector3(1, 1.1f, -0.45f);
        verts10[4] = new Vector3(-1, 1.1f, 0.4f);
        verts10[5] = new Vector3(-1, 1.1f, 0.45f);
        verts10[6] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts10[7] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts10[8] = new Vector3(-0.45f, 1.1f, 0.4f);
        verts10[9] = new Vector3(-0.45f, 1.1f, 1);
        verts10[10] = new Vector3(-0.4f, 1.1f, 1);
        verts10[11] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts10[12] = new Vector3(0.4f, 1.1f, 0.4f);
        verts10[13] = new Vector3(0.4f, 1.1f, 1);
        verts10[14] = new Vector3(0.45f, 1.1f, 1);
        verts10[15] = new Vector3(0.45f, 1.1f, 0.4f);
        verts10[16] = new Vector3(0.45f, 1.1f, 0.4f);
        verts10[17] = new Vector3(0.45f, 1.1f, 0.45f);
        verts10[18] = new Vector3(1, 1.1f, 0.45f);
        verts10[19] = new Vector3(1, 1.1f, 0.4f);
        verts10[20] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts10[21] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts10[22] = new Vector3(0.05f, 1.1f, 0.75f);
        verts10[23] = new Vector3(0.05f, 1.1f, 0.25f);
        verts10[24] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts10[25] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts10[26] = new Vector3(0.05f, 1.1f, -0.25f);
        verts10[27] = new Vector3(0.05f, 1.1f, -0.75f);
        verts10[20] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts10[21] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts10[22] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts10[23] = new Vector3(-0.25f, 1.1f, -0.05f);
        verts10[24] = new Vector3(0.25f, 1.1f, -0.05f);
        verts10[25] = new Vector3(0.25f, 1.1f, 0.05f);
        verts10[26] = new Vector3(0.75f, 1.1f, 0.05f);
        verts10[27] = new Vector3(0.75f, 1.1f, -0.05f);
        verts10[28] = new Vector3(-0.4f, 1.1f, 0.5f);
        verts10[29] = new Vector3(-0.4f, 1.1f, 0.55f);
        verts10[30] = new Vector3(0.4f, 1.1f, 0.55f);
        verts10[31] = new Vector3(0.4f, 1.1f, 0.5f);
        verts10[32] = new Vector3(-0.4f, 1.1f, 0.75f);
        verts10[33] = new Vector3(-0.4f, 1.1f, 0.8f);
        verts10[34] = new Vector3(0.4f, 1.1f, 0.8f);
        verts10[35] = new Vector3(0.4f, 1.1f, 0.75f);
        tris10 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 10);
        MakeQuad(4, 5, 6, 7, 10);
        MakeQuad(8, 9, 10, 11, 10);
        MakeQuad(12, 13, 14, 15, 10);
        MakeQuad(16, 17, 18, 19, 10);
        MakeQuad(20, 21, 22, 23, 10);
        MakeQuad(24, 25, 26, 27, 10);
        MakeQuad(28, 29, 30, 31, 10);
        MakeQuad(32, 33, 34, 35, 10);
        mesh10.vertices = verts10;
        mesh10.triangles = tris10;
        mesh10.RecalculateNormals();

        // T Junction (cut at left)
        verts11 = new Vector3[num_verts];
        verts11[0] = new Vector3(-0.45f, 1.1f, -1);
        verts11[1] = new Vector3(-0.45f, 1.1f, 1);
        verts11[2] = new Vector3(-0.4f, 1.1f, 1);
        verts11[3] = new Vector3(-0.4f, 1.1f, -1);
        verts11[4] = new Vector3(0.4f, 1.1f, 0.4f);
        verts11[5] = new Vector3(0.4f, 1.1f, 1);
        verts11[6] = new Vector3(0.45f, 1.1f, 1);
        verts11[7] = new Vector3(0.45f, 1.1f, 0.4f);
        verts11[8] = new Vector3(0.45f, 1.1f, 0.4f);
        verts11[9] = new Vector3(0.45f, 1.1f, 0.45f);
        verts11[10] = new Vector3(1, 1.1f, 0.45f);
        verts11[11] = new Vector3(1, 1.1f, 0.4f);
        verts11[12] = new Vector3(0.4f, 1.1f, -1);
        verts11[13] = new Vector3(0.4f, 1.1f, -0.4f);
        verts11[14] = new Vector3(0.45f, 1.1f, -0.4f);
        verts11[15] = new Vector3(0.45f, 1.1f, -1);
        verts11[16] = new Vector3(0.45f, 1.1f, -0.45f);
        verts11[17] = new Vector3(0.45f, 1.1f, -0.4f);
        verts11[18] = new Vector3(1, 1.1f, -0.4f);
        verts11[19] = new Vector3(1, 1.1f, -0.45f);
        verts11[20] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts11[21] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts11[22] = new Vector3(0.05f, 1.1f, 0.75f);
        verts11[23] = new Vector3(0.05f, 1.1f, 0.25f);
        verts11[24] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts11[25] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts11[26] = new Vector3(0.05f, 1.1f, -0.25f);
        verts11[27] = new Vector3(0.05f, 1.1f, -0.75f);
        verts11[28] = new Vector3(0.5f, 1.1f, -0.4f);
        verts11[29] = new Vector3(0.5f, 1.1f, 0.4f);
        verts11[30] = new Vector3(0.55f, 1.1f, 0.4f);
        verts11[31] = new Vector3(0.55f, 1.1f, -0.4f);
        verts11[32] = new Vector3(0.75f, 1.1f, -0.4f);
        verts11[33] = new Vector3(0.75f, 1.1f, 0.4f);
        verts11[34] = new Vector3(0.8f, 1.1f, 0.4f);
        verts11[35] = new Vector3(0.8f, 1.1f, -0.4f);
        tris11 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 11);
        MakeQuad(4, 5, 6, 7, 11);
        MakeQuad(8, 9, 10, 11, 11);
        MakeQuad(12, 13, 14, 15, 11);
        MakeQuad(16, 17, 18, 19, 11);
        MakeQuad(20, 21, 22, 23, 11);
        MakeQuad(24, 25, 26, 27, 11);
        MakeQuad(28, 29, 30, 31, 11);
        MakeQuad(32, 33, 34, 35, 11);
        mesh11.vertices = verts11;
        mesh11.triangles = tris11;
        mesh11.RecalculateNormals();

        // Dead end (end at top)
        num_verts = 16;
        num_tris = 8;
        verts12 = new Vector3[num_verts];
        verts12[0] = new Vector3(-0.45f, 1.1f, -1);
        verts12[1] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts12[2] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts12[3] = new Vector3(-0.4f, 1.1f, -1);
        verts12[4] = new Vector3(0.4f, 1.1f, -1);
        verts12[5] = new Vector3(0.4f, 1.1f, 0.45f);
        verts12[6] = new Vector3(0.45f, 1.1f, 0.45f);
        verts12[7] = new Vector3(0.45f, 1.1f, -1);
        verts12[8] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts12[9] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts12[10] = new Vector3(0.4f, 1.1f, 0.45f);
        verts12[11] = new Vector3(0.4f, 1.1f, 0.4f);
        verts12[12] = new Vector3(-0.05f, 1.1f, -0.75f);
        verts12[13] = new Vector3(-0.05f, 1.1f, -0.25f);
        verts12[14] = new Vector3(0.05f, 1.1f, -0.25f);
        verts12[15] = new Vector3(0.05f, 1.1f, -0.75f);
        tris12 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 12);
        MakeQuad(4, 5, 6, 7, 12);
        MakeQuad(8, 9, 10, 11, 12);
        MakeQuad(12, 13, 14, 15, 12);
        mesh12.vertices = verts12;
        mesh12.triangles = tris12;
        mesh12.RecalculateNormals();

        // Dead end (end at right)
        verts13 = new Vector3[num_verts];
        verts13[0] = new Vector3(-1, 1.1f, 0.4f);
        verts13[1] = new Vector3(-1, 1.1f, 0.45f);
        verts13[2] = new Vector3(0.4f, 1.1f, 0.45f);
        verts13[3] = new Vector3(0.4f, 1.1f, 0.4f);
        verts13[4] = new Vector3(-1, 1.1f, -0.45f);
        verts13[5] = new Vector3(-1, 1.1f, -0.4f);
        verts13[6] = new Vector3(0.4f, 1.1f, -0.4f);
        verts13[7] = new Vector3(0.4f, 1.1f, -0.45f);
        verts13[8] = new Vector3(0.4f, 1.1f, -0.45f);
        verts13[9] = new Vector3(0.4f, 1.1f, 0.45f);
        verts13[10] = new Vector3(0.45f, 1.1f, 0.45f);
        verts13[11] = new Vector3(0.45f, 1.1f, -0.45f);
        verts13[12] = new Vector3(-0.75f, 1.1f, -0.05f);
        verts13[13] = new Vector3(-0.75f, 1.1f, 0.05f);
        verts13[14] = new Vector3(-0.25f, 1.1f, 0.05f);
        verts13[15] = new Vector3(-0.25f, 1.1f, -0.05f);
        tris13 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 13);
        MakeQuad(4, 5, 6, 7, 13);
        MakeQuad(8, 9, 10, 11, 13);
        MakeQuad(12, 13, 14, 15, 13);
        mesh13.vertices = verts13;
        mesh13.triangles = tris13;
        mesh13.RecalculateNormals();

        // Dead end (end at bottom)
        verts14 = new Vector3[num_verts];
        verts14[0] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts14[1] = new Vector3(-0.45f, 1.1f, 1);
        verts14[2] = new Vector3(-0.4f, 1.1f, 1);
        verts14[3] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts14[4] = new Vector3(0.4f, 1.1f, -0.45f);
        verts14[5] = new Vector3(0.4f, 1.1f, 1);
        verts14[6] = new Vector3(0.45f, 1.1f, 1);
        verts14[7] = new Vector3(0.45f, 1.1f, -0.45f);
        verts14[8] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts14[9] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts14[10] = new Vector3(0.4f, 1.1f, -0.4f);
        verts14[11] = new Vector3(0.4f, 1.1f, -0.45f);
        verts14[12] = new Vector3(-0.05f, 1.1f, 0.25f);
        verts14[13] = new Vector3(-0.05f, 1.1f, 0.75f);
        verts14[14] = new Vector3(0.05f, 1.1f, 0.75f);
        verts14[15] = new Vector3(0.05f, 1.1f, 0.25f);
        tris14 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 14);
        MakeQuad(4, 5, 6, 7, 14);
        MakeQuad(8, 9, 10, 11, 14);
        MakeQuad(12, 13, 14, 15, 14);
        mesh14.vertices = verts14;
        mesh14.triangles = tris14;
        mesh14.RecalculateNormals();

        // Dead end (end at left)
        verts15 = new Vector3[num_verts];
        verts15[0] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts15[1] = new Vector3(-0.4f, 1.1f, -0.4f);
        verts15[2] = new Vector3(1, 1.1f, -0.4f);
        verts15[3] = new Vector3(1, 1.1f, -0.45f);
        verts15[4] = new Vector3(-0.4f, 1.1f, 0.4f);
        verts15[5] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts15[6] = new Vector3(1, 1.1f, 0.45f);
        verts15[7] = new Vector3(1, 1.1f, 0.4f);
        verts15[8] = new Vector3(-0.45f, 1.1f, -0.45f);
        verts15[9] = new Vector3(-0.45f, 1.1f, 0.45f);
        verts15[10] = new Vector3(-0.4f, 1.1f, 0.45f);
        verts15[11] = new Vector3(-0.4f, 1.1f, -0.45f);
        verts15[12] = new Vector3(0.25f, 1.1f, -0.05f);
        verts15[13] = new Vector3(0.25f, 1.1f, 0.05f);
        verts15[14] = new Vector3(0.75f, 1.1f, 0.05f);
        verts15[15] = new Vector3(0.75f, 1.1f, -0.05f);
        tris15 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 15);
        MakeQuad(4, 5, 6, 7, 15);
        MakeQuad(8, 9, 10, 11, 15);
        MakeQuad(12, 13, 14, 15, 15);
        mesh15.vertices = verts15;
        mesh15.triangles = tris15;
        mesh15.RecalculateNormals();
    }

    public Mesh GetMesh(int variation) {
        if (variation == 1) {
            return mesh1;
        } else if (variation == 2) {
            return mesh2;
        } else if (variation == 3) {
            return mesh3;
        } else if (variation == 4) {
            return mesh4;
        } else if (variation == 5) {
            return mesh5;
        } else if (variation == 6) {
            return mesh6;
        } else if (variation == 7) {
            return mesh7;
        } else if (variation == 8) {
            return mesh8;
        } else if (variation == 9) {
            return mesh9;
        } else if (variation == 10) {
            return mesh10;
        } else if (variation == 11) {
            return mesh11;
        } else if (variation == 12) {
            return mesh12;
        } else if (variation == 13) {
            return mesh13;
        } else if (variation == 14) {
            return mesh14;
        } else {
            return mesh15;
        }
    }

    // make a triangle from three vertex indices (clockwise order)
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
        } else if (variation == 4) {
            int index = ntris4 * 3;
            ntris4++;
            tris4[index] = i1;
            tris4[index + 1] = i2;
            tris4[index + 2] = i3;
        } else if (variation == 5) {
            int index = ntris5 * 3;
            ntris5++;
            tris5[index] = i1;
            tris5[index + 1] = i2;
            tris5[index + 2] = i3;
        } else if (variation == 6) {
            int index = ntris6 * 3;
            ntris6++;
            tris6[index] = i1;
            tris6[index + 1] = i2;
            tris6[index + 2] = i3;
        } else if (variation == 7) {
            int index = ntris7 * 3;
            ntris7++;
            tris7[index] = i1;
            tris7[index + 1] = i2;
            tris7[index + 2] = i3;
        } else if (variation == 8) {
            int index = ntris8 * 3;
            ntris8++;
            tris8[index] = i1;
            tris8[index + 1] = i2;
            tris8[index + 2] = i3;
        } else if (variation == 9) {
            int index = ntris9 * 3;
            ntris9++;
            tris9[index] = i1;
            tris9[index + 1] = i2;
            tris9[index + 2] = i3;
        } else if (variation == 10) {
            int index = ntris10 * 3;
            ntris10++;
            tris10[index] = i1;
            tris10[index + 1] = i2;
            tris10[index + 2] = i3;
        } else if (variation == 11) {
            int index = ntris11 * 3;
            ntris11++;
            tris11[index] = i1;
            tris11[index + 1] = i2;
            tris11[index + 2] = i3;
        } else if (variation == 12) {
            int index = ntris12 * 3;
            ntris12++;
            tris12[index] = i1;
            tris12[index + 1] = i2;
            tris12[index + 2] = i3;
        } else if (variation == 13) {
            int index = ntris13 * 3;
            ntris13++;
            tris13[index] = i1;
            tris13[index + 1] = i2;
            tris13[index + 2] = i3;
        } else if (variation == 14) {
            int index = ntris14 * 3;
            ntris14++;
            tris14[index] = i1;
            tris14[index + 1] = i2;
            tris14[index + 2] = i3;
        } else if (variation == 15) {
            int index = ntris15 * 3;
            ntris15++;
            tris15[index] = i1;
            tris15[index + 1] = i2;
            tris15[index + 2] = i3;
        }
    }

    // make a quadrilateral from four vertex indices (clockwise order)
    void MakeQuad(int i1, int i2, int i3, int i4, int variation) {
        MakeTri(i1, i2, i3, variation);
        MakeTri(i1, i3, i4, variation);
    }

    // Update is called once per frame
    void Update() {

    }
}
