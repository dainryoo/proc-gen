using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMesh : MonoBehaviour {

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

    void Awake() {
        mesh1 = new Mesh();
        mesh2 = new Mesh();
        mesh3 = new Mesh();
        mesh4 = new Mesh();
        mesh5 = new Mesh();
        mesh6 = new Mesh();

        int num_verts = 4;
        Vector2[] uvs = new Vector2[num_verts];

        // bottom counterclockwise
        verts1 = new Vector3[num_verts];
        verts1[0] = new Vector3(1, -1, -1);
        verts1[1] = new Vector3(1, -1, 1);
        verts1[2] = new Vector3(-1, -1, 1);
        verts1[3] = new Vector3(-1, -1, -1);
        int num_tris = 2;
        tris1 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 1);
        mesh1.vertices = verts1;
        mesh1.triangles = tris1;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts1[i].z, -verts1[i].x);
        }
        mesh1.uv = uvs;
        mesh1.RecalculateNormals();
        // top clockwise
        verts2 = new Vector3[num_verts];
        verts2[0] = new Vector3(-1, 1, -1);
        verts2[1] = new Vector3(-1, 1, 1);
        verts2[2] = new Vector3(1, 1, 1);
        verts2[3] = new Vector3(1, 1, -1);
        tris2 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 2);
        mesh2.vertices = verts2;
        mesh2.triangles = tris2;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts2[i].x, verts2[i].z);
        }
        mesh2.uv = uvs;
        mesh2.RecalculateNormals();
        // left clockwise
        verts3 = new Vector3[num_verts];
        verts3[0] = new Vector3(-1, 1, 1);
        verts3[1] = new Vector3(-1, 1, -1);
        verts3[2] = new Vector3(-1, -1, -1);
        verts3[3] = new Vector3(-1, -1, 1);
        tris3 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 3);
        mesh3.vertices = verts3;
        mesh3.triangles = tris3;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(-verts3[i].z, verts3[i].y);
        }
        mesh3.uv = uvs;
        mesh3.RecalculateNormals();
        // back counterclockwise
        verts4 = new Vector3[num_verts];
        verts4[0] = new Vector3(1, 1, 1);
        verts4[1] = new Vector3(-1, 1, 1);
        verts4[2] = new Vector3(-1, -1, 1);
        verts4[3] = new Vector3(1, -1, 1);
        tris4 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 4);
        mesh4.vertices = verts4;
        mesh4.triangles = tris4;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(-verts4[i].x, verts4[i].y);
        }
        mesh4.uv = uvs;
        mesh4.RecalculateNormals();
        // right clockwise
        verts5 = new Vector3[num_verts];
        verts5[0] = new Vector3(1, 1, -1);
        verts5[1] = new Vector3(1, 1, 1);
        verts5[2] = new Vector3(1, -1, 1);
        verts5[3] = new Vector3(1, -1, -1);
        tris5 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 5);
        mesh5.vertices = verts5;
        mesh5.triangles = tris5;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts5[i].z, verts5[i].y);
        }
        mesh5.uv = uvs;
        mesh5.RecalculateNormals();
        // front clockwise
        verts6 = new Vector3[num_verts];
        verts6[0] = new Vector3(-1, 1, -1);
        verts6[1] = new Vector3(1, 1, -1);
        verts6[2] = new Vector3(1, -1, -1);
        verts6[3] = new Vector3(-1, -1, -1);
        tris6 = new int[num_tris * 3];
        MakeQuad(0, 1, 2, 3, 6);
        mesh6.vertices = verts6;
        mesh6.triangles = tris6;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts6[i].x, verts6[i].y);
        }
        mesh6.uv = uvs;
        mesh6.RecalculateNormals();
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
        } else {
            return mesh6;
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
        }
    }


    void MakeQuad(int i1, int i2, int i3, int i4, int variation) {
        MakeTri(i1, i2, i3, variation);
        MakeTri(i1, i3, i4, variation);
    }
}
