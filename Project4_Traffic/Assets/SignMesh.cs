using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMesh : MonoBehaviour {

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

    private Vector3[] verts16;
    private int[] tris16;
    private int ntris16 = 0;
    private Mesh mesh16;

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
        mesh16 = new Mesh();

        int num_verts = 8;
        int num_tris = 4;
        // Sign post (front side)
        verts1 = new Vector3[num_verts];
        verts1[0] = new Vector3(0.7f, -1, -0.99f);
        verts1[1] = new Vector3(0.7f, 0.5f, -0.99f);
        verts1[2] = new Vector3(0.75f, 0.5f, -0.99f);
        verts1[3] = new Vector3(0.75f, -1, -0.99f);
        verts1[4] = new Vector3(0.75f, 0.5f, -0.99f);
        verts1[5] = new Vector3(0.7f, 0.5f, -0.99f);
        verts1[6] = new Vector3(0.7f, -1, -0.99f);
        verts1[7] = new Vector3(0.75f, -1, -0.99f);
        tris1 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 1);
        MakeQuad(4, 5, 6, 7, 1);
        mesh1.vertices = verts1;
        mesh1.triangles = tris1;
        mesh1.RecalculateNormals();

        // Sign post (right side)
        verts2 = new Vector3[num_verts];
        verts2[0] = new Vector3(0.99f, 0.5f, 0.7f);
        verts2[1] = new Vector3(0.99f, 0.5f, 0.75f);
        verts2[2] = new Vector3(0.99f, -1, 0.75f);
        verts2[3] = new Vector3(0.99f, -1, 0.7f);
        verts2[4] = new Vector3(0.99f, 0.5f, 0.75f);
        verts2[5] = new Vector3(0.99f, 0.5f, 0.7f);
        verts2[6] = new Vector3(0.99f, -1, 0.7f);
        verts2[7] = new Vector3(0.99f, -1, 0.75f);
        tris2 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 2);
        MakeQuad(4, 5, 6, 7, 2);
        mesh2.vertices = verts2;
        mesh2.triangles = tris2;
        mesh2.RecalculateNormals();

        // Sign post (back side)
        verts3 = new Vector3[num_verts];
        verts3[0] = new Vector3(-0.7f, 0.5f, 0.99f);
        verts3[1] = new Vector3(-0.75f, 0.5f, 0.99f);
        verts3[2] = new Vector3(-0.75f, -1, 0.99f);
        verts3[3] = new Vector3(-0.7f, -1, 0.99f);
        verts3[4] = new Vector3(-0.75f, 0.5f, 0.99f);
        verts3[5] = new Vector3(-0.7f, 0.5f, 0.99f);
        verts3[6] = new Vector3(-0.7f, -1, 0.99f);
        verts3[7] = new Vector3(-0.75f, -1, 0.99f);
        tris3 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 3);
        MakeQuad(4, 5, 6, 7, 3);
        mesh3.vertices = verts3;
        mesh3.triangles = tris3;
        mesh3.RecalculateNormals();


        // Sign post (left side)
        verts4 = new Vector3[num_verts];
        verts4[0] = new Vector3(-0.99f, 0.5f, -0.7f);
        verts4[1] = new Vector3(-0.99f, 0.5f, -0.75f);
        verts4[2] = new Vector3(-0.99f, -1, -0.75f);
        verts4[3] = new Vector3(-0.99f, -1, -0.7f);
        verts4[4] = new Vector3(-0.99f, 0.5f, -0.75f);
        verts4[5] = new Vector3(-0.99f, 0.5f, -0.7f);
        verts4[6] = new Vector3(-0.99f, -1, -0.7f);
        verts4[7] = new Vector3(-0.99f, -1, -0.75f);
        tris4 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 4);
        MakeQuad(4, 5, 6, 7, 4);
        mesh4.vertices = verts4;
        mesh4.triangles = tris4;
        mesh4.RecalculateNormals();

        num_verts = 32;
        num_tris = 16;
        // Stop sign (front side)
        verts5 = new Vector3[num_verts];
        verts5[0] = new Vector3(0.68f, 0.4f, -1);
        verts5[1] = new Vector3(0.68f, 0.6f, -1);
        verts5[2] = new Vector3(0.77f, 0.6f, -1);
        verts5[3] = new Vector3(0.77f, 0.4f, -1);
        verts5[4] = new Vector3(0.77f, 0.6f, -1);
        verts5[5] = new Vector3(0.68f, 0.6f, -1);
        verts5[6] = new Vector3(0.68f, 0.4f, -1);
        verts5[7] = new Vector3(0.77f, 0.4f, -1);
        verts5[8] = new Vector3(0.61f, 0.46f, -1);
        verts5[9] = new Vector3(0.61f, 0.54f, -1);
        verts5[10] = new Vector3(0.84f, 0.54f, -1);
        verts5[11] = new Vector3(0.84f, 0.46f, -1);
        verts5[12] = new Vector3(0.84f, 0.54f, -1);
        verts5[13] = new Vector3(0.61f, 0.54f, -1);
        verts5[14] = new Vector3(0.61f, 0.46f, -1);
        verts5[15] = new Vector3(0.84f, 0.46f, -1);
        verts5[16] = new Vector3(0.61f, 0.54f, -1);
        verts5[17] = new Vector3(0.68f, 0.6f, -1);
        verts5[18] = new Vector3(0.84f, 0.46f, -1);
        verts5[19] = new Vector3(0.77f, 0.4f, -1);
        verts5[20] = new Vector3(0.84f, 0.46f, -1);
        verts5[21] = new Vector3(0.68f, 0.6f, -1);
        verts5[22] = new Vector3(0.61f, 0.54f, -1);
        verts5[23] = new Vector3(0.77f, 0.4f, -1);
        verts5[24] = new Vector3(0.77f, 0.6f, -1);
        verts5[25] = new Vector3(0.84f, 0.54f, -1);
        verts5[26] = new Vector3(0.68f, 0.4f, -1);
        verts5[27] = new Vector3(0.61f, 0.46f, -1);
        verts5[28] = new Vector3(0.68f, 0.4f, -1);
        verts5[29] = new Vector3(0.84f, 0.54f, -1);
        verts5[30] = new Vector3(0.77f, 0.6f, -1);
        verts5[31] = new Vector3(0.61f, 0.46f, -1);
        tris5 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 5);
        MakeQuad(4, 5, 6, 7, 5);
        MakeQuad(8, 9, 10, 11, 5);
        MakeQuad(12, 13, 14, 15, 5);
        MakeQuad(16, 17, 18, 19, 5);
        MakeQuad(20, 21, 22, 23, 5);
        MakeQuad(24, 25, 26, 27, 5);
        MakeQuad(28, 29, 30, 31, 5);
        mesh5.vertices = verts5;
        mesh5.triangles = tris5;
        mesh5.RecalculateNormals();

        // Stop sign (right side)
        verts6 = new Vector3[num_verts];
        verts6[0] = new Vector3(1, 0.6f, 0.68f);
        verts6[1] = new Vector3(1, 0.6f, 0.77f);
        verts6[2] = new Vector3(1, 0.4f, 0.77f);
        verts6[3] = new Vector3(1, 0.4f, 0.68f);
        verts6[4] = new Vector3(1, 0.6f, 0.77f);
        verts6[5] = new Vector3(1, 0.6f, 0.68f);
        verts6[6] = new Vector3(1, 0.4f, 0.68f);
        verts6[7] = new Vector3(1, 0.4f, 0.77f);
        verts6[8] = new Vector3(1, 0.54f, 0.61f);
        verts6[9] = new Vector3(1, 0.54f, 0.84f);
        verts6[10] = new Vector3(1, 0.46f, 0.84f);
        verts6[11] = new Vector3(1, 0.46f, 0.61f);
        verts6[12] = new Vector3(1, 0.54f, 0.84f);
        verts6[13] = new Vector3(1, 0.54f, 0.61f);
        verts6[14] = new Vector3(1, 0.46f, 0.61f);
        verts6[15] = new Vector3(1, 0.46f, 0.84f);
        verts6[16] = new Vector3(1, 0.54f, 0.61f);
        verts6[17] = new Vector3(1, 0.6f, 0.68f);
        verts6[18] = new Vector3(1, 0.46f, 0.84f);
        verts6[19] = new Vector3(1, 0.4f, 0.77f);
        verts6[20] = new Vector3(1, 0.46f, 0.84f);
        verts6[21] = new Vector3(1, 0.6f, 0.68f);
        verts6[22] = new Vector3(1, 0.54f, 0.61f);
        verts6[23] = new Vector3(1, 0.4f, 0.77f);
        verts6[24] = new Vector3(1, 0.6f, 0.77f);
        verts6[25] = new Vector3(1, 0.54f, 0.84f);
        verts6[26] = new Vector3(1, 0.4f, 0.68f);
        verts6[27] = new Vector3(1, 0.46f, 0.61f);
        verts6[28] = new Vector3(1, 0.4f, 0.68f);
        verts6[29] = new Vector3(1, 0.54f, 0.84f);
        verts6[30] = new Vector3(1, 0.6f, 0.77f);
        verts6[31] = new Vector3(1, 0.46f, 0.61f);
        tris6 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 6);
        MakeQuad(4, 5, 6, 7, 6);
        MakeQuad(8, 9, 10, 11, 6);
        MakeQuad(12, 13, 14, 15, 6);
        MakeQuad(16, 17, 18, 19, 6);
        MakeQuad(20, 21, 22, 23, 6);
        MakeQuad(24, 25, 26, 27, 6);
        MakeQuad(28, 29, 30, 31, 6);
        mesh6.vertices = verts6;
        mesh6.triangles = tris6;
        mesh6.RecalculateNormals();

        // Stop sign (back side)
        verts7 = new Vector3[num_verts];
        verts7[0] = new Vector3(-0.68f, 0.6f, 1);
        verts7[1] = new Vector3(-0.77f, 0.6f, 1);
        verts7[2] = new Vector3(-0.77f, 0.4f, 1);
        verts7[3] = new Vector3(-0.68f, 0.4f, 1);
        verts7[4] = new Vector3(-0.77f, 0.6f, 1);
        verts7[5] = new Vector3(-0.68f, 0.6f, 1);
        verts7[6] = new Vector3(-0.68f, 0.4f, 1);
        verts7[7] = new Vector3(-0.77f, 0.4f, 1);
        verts7[8] = new Vector3(-0.61f, 0.54f, 1);
        verts7[9] = new Vector3(-0.84f, 0.54f, 1);
        verts7[10] = new Vector3(-0.84f, 0.46f, 1);
        verts7[11] = new Vector3(-0.61f, 0.46f, 1);
        verts7[12] = new Vector3(-0.84f, 0.54f, 1);
        verts7[13] = new Vector3(-0.61f, 0.54f, 1);
        verts7[14] = new Vector3(-0.61f, 0.46f, 1);
        verts7[15] = new Vector3(-0.84f, 0.46f, 1);
        verts7[16] = new Vector3(-0.61f, 0.54f, 1);
        verts7[17] = new Vector3(-0.68f, 0.6f, 1);
        verts7[18] = new Vector3(-0.84f, 0.46f, 1);
        verts7[19] = new Vector3(-0.77f, 0.4f, 1);
        verts7[20] = new Vector3(-0.84f, 0.46f, 1);
        verts7[21] = new Vector3(-0.68f, 0.6f, 1);
        verts7[22] = new Vector3(-0.61f, 0.54f, 1);
        verts7[23] = new Vector3(-0.77f, 0.4f, 1);
        verts7[24] = new Vector3(-0.77f, 0.6f, 1);
        verts7[25] = new Vector3(-0.84f, 0.54f, 1);
        verts7[26] = new Vector3(-0.68f, 0.4f, 1);
        verts7[27] = new Vector3(-0.61f, 0.46f, 1);
        verts7[28] = new Vector3(-0.68f, 0.4f, 1);
        verts7[29] = new Vector3(-0.84f, 0.54f, 1);
        verts7[30] = new Vector3(-0.77f, 0.6f, 1);
        verts7[31] = new Vector3(-0.61f, 0.46f, 1);
        tris7 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 7);
        MakeQuad(4, 5, 6, 7, 7);
        MakeQuad(8, 9, 10, 11, 7);
        MakeQuad(12, 13, 14, 15, 7);
        MakeQuad(16, 17, 18, 19, 7);
        MakeQuad(20, 21, 22, 23, 7);
        MakeQuad(24, 25, 26, 27, 7);
        MakeQuad(28, 29, 30, 31, 7);
        mesh7.vertices = verts7;
        mesh7.triangles = tris7;
        mesh7.RecalculateNormals();


        // Stop sign (left side)
        verts8 = new Vector3[num_verts];
        verts8[0] = new Vector3(-1, 0.6f, -0.68f);
        verts8[1] = new Vector3(-1, 0.6f, -0.77f);
        verts8[2] = new Vector3(-1, 0.4f, -0.77f);
        verts8[3] = new Vector3(-1, 0.4f, -0.68f);
        verts8[4] = new Vector3(-1, 0.6f, -0.77f);
        verts8[5] = new Vector3(-1, 0.6f, -0.68f);
        verts8[6] = new Vector3(-1, 0.4f, -0.68f);
        verts8[7] = new Vector3(-1, 0.4f, -0.77f);
        verts8[8] = new Vector3(-1, 0.54f, -0.61f);
        verts8[9] = new Vector3(-1, 0.54f, -0.84f);
        verts8[10] = new Vector3(-1, 0.46f, -0.84f);
        verts8[11] = new Vector3(-1, 0.46f, -0.61f);
        verts8[12] = new Vector3(-1, 0.54f, -0.84f);
        verts8[13] = new Vector3(-1, 0.54f, -0.61f);
        verts8[14] = new Vector3(-1, 0.46f, -0.61f);
        verts8[15] = new Vector3(-1, 0.46f, -0.84f);
        verts8[16] = new Vector3(-1, 0.54f, -0.61f);
        verts8[17] = new Vector3(-1, 0.6f, -0.68f);
        verts8[18] = new Vector3(-1, 0.46f, -0.84f);
        verts8[19] = new Vector3(-1, 0.4f, -0.77f);
        verts8[20] = new Vector3(-1, 0.46f, -0.84f);
        verts8[21] = new Vector3(-1, 0.6f, -0.68f);
        verts8[22] = new Vector3(-1, 0.54f, -0.61f);
        verts8[23] = new Vector3(-1, 0.4f, -0.77f);
        verts8[24] = new Vector3(-1, 0.6f, -0.77f);
        verts8[25] = new Vector3(-1, 0.54f, -0.84f);
        verts8[26] = new Vector3(-1, 0.4f, -0.68f);
        verts8[27] = new Vector3(-1, 0.46f, -0.61f);
        verts8[28] = new Vector3(-1, 0.4f, -0.68f);
        verts8[29] = new Vector3(-1, 0.54f, -0.84f);
        verts8[30] = new Vector3(-1, 0.6f, -0.77f);
        verts8[31] = new Vector3(-1, 0.46f, -0.61f);
        tris8 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 8);
        MakeQuad(4, 5, 6, 7, 8);
        MakeQuad(8, 9, 10, 11, 8);
        MakeQuad(12, 13, 14, 15, 8);
        MakeQuad(16, 17, 18, 19, 8);
        MakeQuad(20, 21, 22, 23, 8);
        MakeQuad(24, 25, 26, 27, 8);
        MakeQuad(28, 29, 30, 31, 8);
        mesh8.vertices = verts8;
        mesh8.triangles = tris8;
        mesh8.RecalculateNormals();

        num_verts = 8;
        num_tris = 4;
        // Rectangle (front)
        verts9 = new Vector3[num_verts];
        verts9[0] = new Vector3(0.65f, 0.4f, -1);
        verts9[1] = new Vector3(0.65f, 0.6f, -1);
        verts9[2] = new Vector3(0.8f, 0.6f, -1);
        verts9[3] = new Vector3(0.8f, 0.4f, -1);
        verts9[4] = new Vector3(0.8f, 0.6f, -1);
        verts9[5] = new Vector3(0.65f, 0.6f, -1);
        verts9[6] = new Vector3(0.65f, 0.4f, -1);
        verts9[7] = new Vector3(0.8f, 0.4f, -1);
        tris9 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 9);
        MakeQuad(4, 5, 6, 7, 9);
        mesh9.vertices = verts9;
        mesh9.triangles = tris9;
        mesh9.RecalculateNormals();

        // Rectangle (right)
        verts10 = new Vector3[num_verts];
        verts10[0] = new Vector3(1, 0.6f, 0.65f);
        verts10[1] = new Vector3(1, 0.6f, 0.8f);
        verts10[2] = new Vector3(1, 0.4f, 0.8f);
        verts10[3] = new Vector3(1, 0.4f, 0.65f);
        verts10[4] = new Vector3(1, 0.6f, 0.8f);
        verts10[5] = new Vector3(1, 0.6f, 0.65f);
        verts10[6] = new Vector3(1, 0.4f, 0.65f);
        verts10[7] = new Vector3(1, 0.4f, 0.8f);
        tris10 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 10);
        MakeQuad(4, 5, 6, 7, 10);
        mesh10.vertices = verts10;
        mesh10.triangles = tris10;
        mesh10.RecalculateNormals();

        // Rectangle (back)
        verts11 = new Vector3[num_verts];
        verts11[0] = new Vector3(-0.65f, 0.6f, 1);
        verts11[1] = new Vector3(-0.8f, 0.6f, 1);
        verts11[2] = new Vector3(-0.8f, 0.4f, 1);
        verts11[3] = new Vector3(-0.65f, 0.4f, 1);
        verts11[4] = new Vector3(-0.8f, 0.6f, 1);
        verts11[5] = new Vector3(-0.65f, 0.6f, 1);
        verts11[6] = new Vector3(-0.65f, 0.4f, 1);
        verts11[7] = new Vector3(-0.8f, 0.4f, 1);
        tris11 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 11);
        MakeQuad(4, 5, 6, 7, 11);
        mesh11.vertices = verts11;
        mesh11.triangles = tris11;
        mesh11.RecalculateNormals();


        // Rectangle (left)
        verts12 = new Vector3[num_verts];
        verts12[0] = new Vector3(-1, 0.6f, -0.65f);
        verts12[1] = new Vector3(-1, 0.6f, -0.8f);
        verts12[2] = new Vector3(-1, 0.4f, -0.8f);
        verts12[3] = new Vector3(-1, 0.4f, -0.65f);
        verts12[4] = new Vector3(-1, 0.6f, -0.8f);
        verts12[5] = new Vector3(-1, 0.6f, -0.65f);
        verts12[6] = new Vector3(-1, 0.4f, -0.65f);
        verts12[7] = new Vector3(-1, 0.4f, -0.8f);
        tris12 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 12);
        MakeQuad(4, 5, 6, 7, 12);
        mesh12.vertices = verts12;
        mesh12.triangles = tris12;
        mesh12.RecalculateNormals();

        // Diamond (front)
        verts13 = new Vector3[num_verts];
        verts13[0] = new Vector3(0.63f, 0.5f, -1);
        verts13[1] = new Vector3(0.725f, 0.6f, -1);
        verts13[2] = new Vector3(0.82f, 0.5f, -1);
        verts13[3] = new Vector3(0.725f, 0.4f, -1);

        verts13[4] = new Vector3(0.82f, 0.5f, -1);
        verts13[5] = new Vector3(0.725f, 0.6f, -1);
        verts13[6] = new Vector3(0.63f, 0.5f, -1);
        verts13[7] = new Vector3(0.725f, 0.4f, -1);
        tris13 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 13);
        MakeQuad(4, 5, 6, 7, 13);
        mesh13.vertices = verts13;
        mesh13.triangles = tris13;
        mesh13.RecalculateNormals();

        // Diamond (right)
        verts14 = new Vector3[num_verts];
        verts14[0] = new Vector3(1, 0.6f, 0.725f);
        verts14[1] = new Vector3(1, 0.5f, 0.82f);
        verts14[2] = new Vector3(1, 0.4f, 0.725f);
        verts14[3] = new Vector3(1, 0.5f, 0.63f);

        verts14[4] = new Vector3(1, 0.5f, 0.82f);
        verts14[5] = new Vector3(1, 0.6f, 0.725f);
        verts14[6] = new Vector3(1, 0.5f, 0.63f);
        verts14[7] = new Vector3(1, 0.4f, 0.725f);
        tris14 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 14);
        MakeQuad(4, 5, 6, 7, 14);
        mesh14.vertices = verts14;
        mesh14.triangles = tris14;
        mesh14.RecalculateNormals();

        // Diamond (back)
        verts15 = new Vector3[num_verts];
        verts15[0] = new Vector3(-0.725f, 0.6f, 1);
        verts15[1] = new Vector3(-0.82f, 0.5f, 1);
        verts15[2] = new Vector3(-0.725f, 0.4f, 1);
        verts15[3] = new Vector3(-0.63f, 0.5f, 1);

        verts15[4] = new Vector3(-0.82f, 0.5f, 1);
        verts15[5] = new Vector3(-0.725f, 0.6f, 1);
        verts15[6] = new Vector3(-0.63f, 0.5f, 1);
        verts15[7] = new Vector3(-0.725f, 0.4f, 1);
        tris15 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 15);
        MakeQuad(4, 5, 6, 7, 15);
        mesh15.vertices = verts15;
        mesh15.triangles = tris15;
        mesh15.RecalculateNormals();


        // Diamond (left)
        verts16 = new Vector3[num_verts];
        verts16[0] = new Vector3(-1, 0.6f, -0.725f);
        verts16[1] = new Vector3(-1, 0.5f, -0.82f);
        verts16[2] = new Vector3(-1, 0.4f, -0.725f);
        verts16[3] = new Vector3(-1, 0.5f, -0.63f);

        verts16[4] = new Vector3(-1, 0.5f, -0.82f);
        verts16[5] = new Vector3(-1, 0.6f, -0.725f);
        verts16[6] = new Vector3(-1, 0.5f, -0.63f);
        verts16[7] = new Vector3(-1, 0.4f, -0.725f);
        tris16 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 16);
        MakeQuad(4, 5, 6, 7, 16);
        mesh16.vertices = verts16;
        mesh16.triangles = tris16;
        mesh16.RecalculateNormals();
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
        } else if (variation == 15) {
            return mesh15;
        } else {
            return mesh16;
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
        } else if (variation == 16) {
            int index = ntris16 * 3;
            ntris16++;
            tris16[index] = i1;
            tris16[index + 1] = i2;
            tris16[index + 2] = i3;
        }
    }

    void MakeQuad(int i1, int i2, int i3, int i4, int variation) {
        MakeTri(i1, i2, i3, variation);
        MakeTri(i1, i3, i4, variation);
    }

    void Update() {

    }
}
