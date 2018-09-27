using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofMesh : MonoBehaviour {

    private Vector3[] verts;
    private Vector3[] verts2;
    private int[] tris;
    private int ntris;
    private Mesh mesh;
    private float thickness = 0.2f; // how thick the roofs are


    // Returns an array of meshes
    // The first mesh is the actual roof
    // The second mesh is the roof lining
    public Mesh[] GetCrossHipMesh(float roofHeight, float roofOverhang, float crossDepth, float hipDepth, float[] upperLeft, float[] upperRight, float[] lowerLeft, float[] lowerRight) {
        Mesh[] meshes = new Mesh[2];

        mesh = new Mesh();
        ntris = 0;
        int num_verts = 11;
        int num_verts2 = 32;
        verts = new Vector3[num_verts];
        verts2 = new Vector3[num_verts2];
        Vector2[] uvs = new Vector2[num_verts];

        float xUpper = (lowerLeft[0]+lowerRight[0])/2;
        float yUpper = roofHeight;
        float zUpper = lowerLeft[1] + hipDepth;

        float xUL = 0;
        float yUL = 0;
        float zUL = 0;
        float xUR = 0;
        float yUR = 0;
        float zUR = 0;
        float xLR = lowerRight[0] + roofOverhang;
        float yLR = 0;
        float zLR = lowerRight[1] - roofOverhang;
        float xLL = lowerLeft[0] - roofOverhang;
        float yLL = 0;
        float zLL = lowerLeft[1] - roofOverhang;

        // Front triangle
        verts[0] = new Vector3(xLL, yLL, zLL);
        verts[1] = new Vector3(xUpper, yUpper, zUpper);
        verts[2] = new Vector3(xLR, yLR, zLR);

        verts2[0] = new Vector3(xLL, yLL, zLL);
        verts2[1] = new Vector3(xLR, yLR, zLR);
        verts2[2] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[3] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[4] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[5] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[6] = new Vector3(xLR, yLR - thickness, zLR+roofOverhang);
        verts2[7] = new Vector3(xLL, yLL - thickness, zLL+roofOverhang);

        xUR = xUpper;
        yUR = yUpper;
        zUR = zUpper;
        xUL = xUR;
        yUL = roofHeight;
        zUL = zUR + crossDepth;
        xLL = upperLeft[0]-roofOverhang;
        yLL = 0;
        zLL = upperLeft[1];
        xLR = lowerLeft[0] - roofOverhang;
        yLR = 0;
        zLR = lowerLeft[1] - roofOverhang;

        // Left slope
        verts[3] = new Vector3(xUL, yUL, zUL);
        verts[4] = new Vector3(xUR, yUR, zUR);
        verts[5] = new Vector3(xLR, yLR, zLR);
        verts[6] = new Vector3(xLL, yLL, zLL);

        verts2[8] = new Vector3(xLL, yLL, zLL);
        verts2[9] = new Vector3(xLR, yLR, zLR);
        verts2[10] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[11] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[12] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[13] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[14] = new Vector3(xLR+roofOverhang, yLR - thickness, zLR);
        verts2[15] = new Vector3(xLL+roofOverhang, yLL - thickness, zLL);



        xUR = xUL;
        yUR = yUL;
        zUR = zUL;
        xUL = (lowerLeft[0] + lowerRight[0])/2;
        yUL = roofHeight;
        zUL = lowerRight[1] + hipDepth;
        xLR = upperRight[0]+roofOverhang;
        yLR = yLL;
        zLR = zLL;
        xLL = xLR;
        yLL = 0;
        zLL = lowerRight[1] - roofOverhang;

        // Right slope
        verts[7] = new Vector3(xUL, yUL, zUL);
        verts[8] = new Vector3(xUR, yUR, zUR);
        verts[9] = new Vector3(xLR, yLR, zLR);
        verts[10] = new Vector3(xLL, yLL, zLL);

        verts2[16] = new Vector3(xLL, yLL, zLL);
        verts2[17] = new Vector3(xLR, yLR, zLR);
        verts2[18] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[19] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[20] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[21] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[22] = new Vector3(xLR - roofOverhang, yLR - thickness, zLR);
        verts2[23] = new Vector3(xLL - roofOverhang, yLL - thickness, zLL);

        

        int num_tris = 5;
        tris = new int[num_tris * 3];

        MakeTri(0, 1, 2);
        MakeQuad(3, 4, 5, 6);
        MakeQuad(7, 8, 9, 10);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            if (i < 3) {
                uvs[i] = new Vector2(verts[i].x, verts[i].z);
            } else if (i < 7) {
                uvs[i] = new Vector2(verts[i].z, verts[i].x);
            } else {
                uvs[i] = new Vector2(verts[i].z, -verts[i].x);
            }
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[0] = mesh;

        // White lining
        mesh = new Mesh();
        ntris = 0;
        num_tris = 16;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);
        MakeQuad(16, 17, 18, 19);
        MakeQuad(20, 21, 22, 23);

        mesh.vertices = verts2;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts2];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts2[i].z, -verts2[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts2;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[1] = mesh;

        return meshes;
    }

    // Returns an array of meshes
    // The first mesh is the actual roof
    // The second mesh is the roof lining
    public Mesh[] GetHipMesh(float roofHeight, float roofOverhang, float hipDepth, float[] upperLeft, float[] upperRight, float[] lowerLeft, float[] lowerRight) {
        Mesh[] meshes = new Mesh[2];

        mesh = new Mesh();
        ntris = 0;
        int num_verts = 14;
        int num_verts2 = 32;
        verts = new Vector3[num_verts];
        verts2 = new Vector3[num_verts2];
        Vector2[] uvs = new Vector2[num_verts];

        // The left triangle
        float xUpper = upperLeft[0] + hipDepth;
        float yUpper = roofHeight;
        float zUpper = (upperLeft[1]+lowerLeft[1])/2;

        float xUL = 0;
        float yUL = 0;
        float zUL = 0;
        float xUR = 0;
        float yUR = 0;
        float zUR = 0;
        float xLR = lowerLeft[0] - roofOverhang;
        float yLR = 0;
        float zLR = lowerLeft[1] - roofOverhang;
        float xLL = upperLeft[0] - roofOverhang;
        float yLL = 0;
        float zLL = upperLeft[1] + roofOverhang;

        // Left triangle
        verts[0] = new Vector3(xLL, yLL, zLL);
        verts[1] = new Vector3(xUpper, yUpper, zUpper);
        verts[2] = new Vector3(xLR, yLR, zLR);

        verts2[0] = new Vector3(xLL, yLL, zLL);
        verts2[1] = new Vector3(xLR, yLR, zLR);
        verts2[2] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[3] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[4] = new Vector3(xLL, yLL-thickness, zLL);
        verts2[5] = new Vector3(xLR, yLR-thickness, zLR);
        verts2[6] = new Vector3(xLR+roofOverhang, yLR-thickness, zLR);
        verts2[7] = new Vector3(xLL+ roofOverhang, yLL-thickness, zLL);

        xUL = xUpper;
        yUL = yUpper;
        zUL = zUpper;
        xUR = upperRight[0] - hipDepth;
        yUR = roofHeight;
        zUR = (upperRight[1] + lowerRight[1]) / 2;
        xLL = xLR;
        yLL = yLR;
        zLL = zLR;
        xLR = lowerRight[0] + roofOverhang;
        yLR = 0;
        zLR = lowerRight[1] - roofOverhang;

        // Front slope
        verts[3] = new Vector3(xUL, yUL, zUL);
        verts[4] = new Vector3(xUR, yUR, zUR);
        verts[5] = new Vector3(xLR, yLR, zLR);
        verts[6] = new Vector3(xLL, yLL, zLL);

        verts2[8] = new Vector3(xLL, yLL, zLL);
        verts2[9] = new Vector3(xLR, yLR, zLR);
        verts2[10] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[11] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[12] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[13] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[14] = new Vector3(xLR, yLR - thickness, zLR+roofOverhang);
        verts2[15] = new Vector3(xLL, yLL - thickness, zLL+roofOverhang);

        xUpper = xUR;
        yUpper = yUR;
        zUpper = zUR;
        xLL = xLR;
        yLL = yLR;
        zLL = zLR;
        xLR = upperRight[0] + roofOverhang;
        yLR = 0;
        zLR = upperRight[1] + roofOverhang;

        // Right triangle
        verts[7] = new Vector3(xLL, yLL, zLL);
        verts[8] = new Vector3(xUpper, yUpper, zUpper);
        verts[9] = new Vector3(xLR, yLR, zLR);

        verts2[16] = new Vector3(xLL, yLL, zLL);
        verts2[17] = new Vector3(xLR, yLR, zLR);
        verts2[18] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[19] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[20] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[21] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[22] = new Vector3(xLR-roofOverhang, yLR - thickness, zLR);
        verts2[23] = new Vector3(xLL- roofOverhang, yLL - thickness, zLL);

        xUR = xUpper;
        yUR = yUpper;
        zUR = zUpper;
        xUL = upperLeft[0] + hipDepth;
        yUL = roofHeight;
        zUL = (upperLeft[1] + lowerLeft[1]) / 2;
        xLL = upperLeft[0] - roofOverhang;
        yLL = 0;
        zLL = upperLeft[1] + roofOverhang;

        // Back slope
        verts[10] = new Vector3(xUR, yUR, zUR);
        verts[11] = new Vector3(xUL, yUL, zUL);
        verts[12] = new Vector3(xLL, yLL, zLL);
        verts[13] = new Vector3(xLR, yLR, zLR);

        verts2[27] = new Vector3(xLL, yLL, zLL);
        verts2[26] = new Vector3(xLR, yLR, zLR);
        verts2[25] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[24] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[28] = new Vector3(xLL, yLL - thickness, zLL);
        verts2[29] = new Vector3(xLR, yLR - thickness, zLR);
        verts2[30] = new Vector3(xLR, yLR - thickness, zLR - roofOverhang);
        verts2[31] = new Vector3(xLL, yLL - thickness, zLL - roofOverhang);

        int num_tris = 6;
        tris = new int[num_tris * 3];

        MakeTri(0, 1, 2);
        MakeQuad(3, 4, 5, 6);
        MakeTri(7, 8, 9);
        MakeQuad(10, 11, 12, 13);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            if (i < 3) {
                uvs[i] = new Vector2(verts[i].z, verts[i].x);
            } else if (i < 7) {
                uvs[i] = new Vector2(verts[i].x, verts[i].z);
            } else if (i < 10) {
                uvs[i] = new Vector2(verts[i].z, -verts[i].x);
            } else {
                uvs[i] = new Vector2(verts[i].x, -verts[i].z);
            }
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[0] = mesh;

        // White lining
        mesh = new Mesh();
        ntris = 0;
        num_tris = 16;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);
        MakeQuad(16, 17, 18, 19);
        MakeQuad(20, 21, 22, 23);
        MakeQuad(24, 25, 26, 27);
        MakeQuad(28, 29, 30, 31);

        mesh.vertices = verts2;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts2];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts2[i].z, -verts2[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts2;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[1] = mesh;

        return meshes;
    }


    // Returns an array of meshes
    // The first mesh is the actual roof
    // The second mesh is the roof lining
    // The third mesh is the triangular wall underneath the roof
    public Mesh[] GetCrossGableMesh(float roofHeight, float roofOverhang, float crossStretch, float crossPadding, float[] upperLeft, float[] upperRight, float[] lowerLeft, float[] lowerRight) {
        Mesh[] meshes = new Mesh[3];

        mesh = new Mesh();
        ntris = 0;
        int num_verts = 16;
        int num_verts2 = 16;
        verts = new Vector3[num_verts];
        verts2 = new Vector3[num_verts2];
        Vector2[] uvs = new Vector2[num_verts];

        float xUpperUL = (upperLeft[0] + upperRight[0]) / 2;
        float yUpperUL = roofHeight;
        float zUpperUL = upperLeft[1]+ crossStretch;
        float xUpperUR = (lowerLeft[0] + lowerRight[0]) / 2;
        float yUpperUR = roofHeight;
        float zUpperUR = lowerRight[1] - roofOverhang;
        float xUpperLR = lowerLeft[0] - roofOverhang;
        float yUpperLR = 0;
        float zUpperLR = lowerRight[1] - roofOverhang - crossPadding;
        float xUpperLL = upperLeft[0] - roofOverhang;
        float yUpperLL = 0;
        float zUpperLL = upperLeft[1];

        float xBottomUL = (upperLeft[0] + upperRight[0]) / 2;
        float yBottomUL = roofHeight - thickness;
        float zBottomUL = upperLeft[1]+ crossStretch;
        float xBottomUR = (lowerLeft[0] + lowerRight[0]) / 2;
        float yBottomUR = roofHeight - thickness;
        float zBottomUR = lowerRight[1] - roofOverhang;
        float xBottomLR = lowerLeft[0] - roofOverhang;
        float yBottomLR = 0 - thickness;
        float zBottomLR = lowerRight[1] - roofOverhang - crossPadding;
        float xBottomLL = upperLeft[0] - roofOverhang;
        float yBottomLL = 0 - thickness;
        float zBottomLL = upperLeft[1];

        // Left slope's left face
        verts[0] = new Vector3(xUpperUL, yUpperUL, zUpperUL);
        verts[1] = new Vector3(xUpperUR, yUpperUR, zUpperUR);
        verts[2] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts[3] = new Vector3(xUpperLL, yUpperLL, zUpperLL);
        // Left slope's right face 
        verts[4] = new Vector3(xBottomUR, yBottomUR, zBottomUR);
        verts[5] = new Vector3(xBottomUL, yBottomUL, zBottomUL);
        verts[6] = new Vector3(xBottomLL, yBottomLL, zBottomLL);
        verts[7] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        // Left slope's front face
        verts2[0] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        verts2[1] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts2[2] = new Vector3(xUpperUR, yUpperUR, zUpperUR);
        verts2[3] = new Vector3(xBottomUR, yBottomUR, zBottomUR);
        // Left slope's bottom face
        verts2[4] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts2[5] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        verts2[6] = new Vector3(xBottomLL, yBottomLL, zBottomLL);
        verts2[7] = new Vector3(xUpperLL, yUpperLL, zUpperLL);


        xUpperUL = (upperLeft[0] + upperRight[0]) / 2;
        yUpperUL = roofHeight;
        zUpperUL = upperLeft[1]+crossStretch;
        xUpperUR = (lowerLeft[0] + lowerRight[0]) / 2;
        yUpperUR = roofHeight;
        zUpperUR = lowerRight[1] - roofOverhang;
        xUpperLR = lowerRight[0] + roofOverhang;
        yUpperLR = 0;
        zUpperLR = lowerRight[1] - roofOverhang;
        xUpperLL = upperRight[0] + roofOverhang;
        yUpperLL = 0;
        zUpperLL = upperLeft[1] - crossPadding;

        xBottomUL = (upperLeft[0] + upperRight[0]) / 2;
        yBottomUL = roofHeight - thickness;
        zBottomUL = upperLeft[1]+ crossStretch;
        xBottomUR = (lowerLeft[0] + lowerRight[0]) / 2;
        yBottomUR = roofHeight - thickness;
        zBottomUR = lowerRight[1] - roofOverhang;
        xBottomLR = lowerRight[0] + roofOverhang;
        yBottomLR = 0 - thickness;
        zBottomLR = lowerRight[1] - roofOverhang;
        xBottomLL = upperRight[0] + roofOverhang;
        yBottomLL = 0 - thickness;
        zBottomLL = upperLeft[1] - crossPadding;

        // Right slope's top face
        verts[8] = new Vector3(xUpperLL, yUpperLL, zUpperLL);
        verts[9] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts[10] = new Vector3(xUpperUR, yUpperUR, zUpperUR);
        verts[11] = new Vector3(xUpperUL, yUpperUL, zUpperUL);
        // Right slope's right face
        verts2[8] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        verts2[9] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts2[10] = new Vector3(xUpperLL, yUpperLL, zUpperLL);
        verts2[11] = new Vector3(xBottomLL, yBottomLL, zBottomLL);
        // Right slope's front face 
        verts2[12] = new Vector3(xBottomUR, yBottomUR, zBottomUR);
        verts2[13] = new Vector3(xUpperUR, yUpperUR, zUpperUR);
        verts2[14] = new Vector3(xUpperLR, yUpperLR, zUpperLR);
        verts2[15] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        // Right slope's bottom face
        verts[12] = new Vector3(xBottomUL, yBottomUL, zBottomUL);
        verts[13] = new Vector3(xBottomUR, yBottomUR, zBottomUR);
        verts[14] = new Vector3(xBottomLR, yBottomLR, zBottomLR);
        verts[15] = new Vector3(xBottomLL, yBottomLL, zBottomLL);

        int num_tris = 8;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            if (i < 8) {
                uvs[i] = new Vector2(verts[i].z, verts[i].x);
            } else {
                uvs[i] = new Vector2(verts[i].z, -verts[i].x);
            }
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[0] = mesh;


        // White lining
        mesh = new Mesh();
        ntris = 0;
        num_tris = 8;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);

        mesh.vertices = verts2;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts2];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts2[i].z, -verts2[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts2;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[1] = mesh;


        // Triangular wall
        mesh = new Mesh();
        ntris = 0;
        num_verts = 6;
        verts = new Vector3[num_verts];
        uvs = new Vector2[num_verts];

        verts[0] = new Vector3(lowerLeft[0], 0, lowerLeft[1]);
        verts[1] = new Vector3(lowerRight[0], 0, lowerRight[1]);
        verts[2] = new Vector3((lowerLeft[0]+lowerRight[0])/2, roofHeight, lowerRight[1]);
        verts[3] = new Vector3((lowerLeft[0] + lowerRight[0]) / 2, roofHeight, lowerRight[1]);
        verts[4] = new Vector3(lowerRight[0], 0, lowerRight[1]);
        verts[5] = new Vector3(lowerLeft[0], 0, lowerLeft[1]);

        num_tris = 2;
        tris = new int[num_tris * 3];

        MakeTri(0, 1, 2);
        MakeTri(3, 4, 5);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts[i].z, -verts[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[2] = mesh;

        return meshes;
    }

    // Returns an array of meshes
    // The first mesh is the actual roof
    // The second mesh is the roof lining
    // The third mesh is the triangular wall underneath the roof
    public Mesh[] GetGableMesh(float roofHeight, float roofOverhang, float[] upperLeft, float[] upperRight, float[] lowerLeft, float[] lowerRight) {
        Mesh[] meshes = new Mesh[3];

        mesh = new Mesh();
        ntris = 0;
        int num_verts = 40-24;
        int num_verts2 = 24;
        verts = new Vector3[num_verts];
        verts2 = new Vector3[num_verts2];
        Vector2[] uvs = new Vector2[num_verts];

        float xFrontUL = (lowerLeft[0] + upperLeft[0]) / 2 - roofOverhang;
        float yFrontUL = roofHeight;
        float zFrontUL = (lowerLeft[1] + upperLeft[1]) / 2;
        float xFrontUR = (lowerRight[0] + upperRight[0]) / 2 + roofOverhang;
        float yFrontUR = roofHeight;
        float zFrontUR = (lowerRight[1] + upperRight[1]) / 2;
        float xFrontLR = lowerRight[0] + roofOverhang;
        float yFrontLR = 0;
        float zFrontLR = lowerRight[1] - roofOverhang;
        float xFrontLL = lowerLeft[0] - roofOverhang;
        float yFrontLL = 0;
        float zFrontLL = lowerLeft[1] - roofOverhang;

        float xBackUL = (lowerLeft[0] + upperLeft[0]) / 2 - roofOverhang;
        float yBackUL = roofHeight - thickness;
        float zBackUL = (lowerLeft[1] + upperLeft[1]) / 2;
        float xBackUR = (lowerRight[0] + upperRight[0]) / 2 + roofOverhang;
        float yBackUR = roofHeight - thickness;
        float zBackUR = (lowerRight[1] + upperRight[1]) / 2;
        float xBackLR = lowerRight[0] + roofOverhang;
        float yBackLR = 0 - thickness;
        float zBackLR = lowerRight[1] - roofOverhang;
        float xBackLL = lowerLeft[0] - roofOverhang;
        float yBackLL = 0 - thickness;
        float zBackLL = lowerLeft[1] - roofOverhang;

        // Front slope's upper face
        verts[0] = new Vector3(xFrontUL, yFrontUL, zFrontUL);
        verts[1] = new Vector3(xFrontUR, yFrontUR, zFrontUR);
        verts[2] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts[3] = new Vector3(xFrontLL, yFrontLL, zFrontLL);
        // Front slope's lower face 
        verts[4] = new Vector3(xBackUR, yBackUR, zBackUR);
        verts[5] = new Vector3(xBackUL, yBackUL, zBackUL);
        verts[6] = new Vector3(xBackLL, yBackLL, zBackLL);
        verts[7] = new Vector3(xBackLR, yBackLR, zBackLR);
        // Front slope's left face
        verts2[0] = new Vector3(xBackUL, yBackUL, zBackUL);
        verts2[1] = new Vector3(xFrontUL, yFrontUL, zFrontUL);
        verts2[2] = new Vector3(xFrontLL, yFrontLL, zFrontLL);
        verts2[3] = new Vector3(xBackLL, yBackLL, zBackLL);
        // Front slope's right face
        verts2[4] = new Vector3(xBackLR, yBackLR, zBackLR);
        verts2[5] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts2[6] = new Vector3(xFrontUR, yFrontUR, zFrontUR);
        verts2[7] = new Vector3(xBackUR, yBackUR, zBackUR);
        // Front slope's bottom face
        verts2[8] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts2[9] = new Vector3(xBackLR, yBackLR, zBackLR);
        verts2[10] = new Vector3(xBackLL, yBackLL, zBackLL);
        verts2[11] = new Vector3(xFrontLL, yFrontLL, zFrontLL);

        xFrontUL = (lowerLeft[0] + upperLeft[0]) / 2 - roofOverhang;
        yFrontUL = roofHeight - thickness;
        zFrontUL = (lowerLeft[1] + upperLeft[1]) / 2;
        xFrontUR = (lowerRight[0] + upperRight[0]) / 2 + roofOverhang;
        yFrontUR = roofHeight - thickness;
        zFrontUR = (lowerRight[1] + upperRight[1]) / 2;
        xFrontLR = lowerRight[0] + roofOverhang;
        yFrontLR = 0 - thickness;
        zFrontLR = upperRight[1] + roofOverhang;
        xFrontLL = lowerLeft[0] - roofOverhang;
        yFrontLL = 0 - thickness;
        zFrontLL = upperLeft[1] + roofOverhang;

        xBackUL = (lowerLeft[0] + upperLeft[0]) / 2 - roofOverhang;
        yBackUL = roofHeight;
        zBackUL = (lowerLeft[1] + upperLeft[1]) / 2;
        xBackUR = (lowerRight[0] + upperRight[0]) / 2 + roofOverhang;
        yBackUR = roofHeight;
        zBackUR = (lowerRight[1] + upperRight[1]) / 2;
        xBackLR = lowerRight[0] + roofOverhang;
        yBackLR = 0;
        zBackLR = upperRight[1] + roofOverhang;
        xBackLL = lowerLeft[0] - roofOverhang;
        yBackLL = 0;
        zBackLL = upperLeft[1] + roofOverhang;

        // Back slope's upper face
        verts[8] = new Vector3(xFrontUL, yFrontUL, zFrontUL);
        verts[9] = new Vector3(xFrontUR, yFrontUR, zFrontUR);
        verts[10] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts[11] = new Vector3(xFrontLL, yFrontLL, zFrontLL);
        // Back slope's lower face 
        verts[12] = new Vector3(xBackUR, yBackUR, zBackUR);
        verts[13] = new Vector3(xBackUL, yBackUL, zBackUL);
        verts[14] = new Vector3(xBackLL, yBackLL, zBackLL);
        verts[15] = new Vector3(xBackLR, yBackLR, zBackLR);
        // Back slope's left face
        verts2[12] = new Vector3(xBackUL, yBackUL, zBackUL);
        verts2[13] = new Vector3(xFrontUL, yFrontUL, zFrontUL);
        verts2[14] = new Vector3(xFrontLL, yFrontLL, zFrontLL);
        verts2[15] = new Vector3(xBackLL, yBackLL, zBackLL);
        // Back slope's right face
        verts2[16] = new Vector3(xBackLR, yBackLR, zBackLR);
        verts2[17] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts2[18] = new Vector3(xFrontUR, yFrontUR, zFrontUR);
        verts2[19] = new Vector3(xBackUR, yBackUR, zBackUR);
        // Back slope's bottom face
        verts2[20] = new Vector3(xFrontLR, yFrontLR, zFrontLR);
        verts2[21] = new Vector3(xBackLR, yBackLR, zBackLR);
        verts2[22] = new Vector3(xBackLL, yBackLL, zBackLL);
        verts2[23] = new Vector3(xFrontLL, yFrontLL, zFrontLL);
        int num_tris = 8;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            if (i < 7) {
                uvs[i] = new Vector2(verts[i].x, verts[i].z);
            } else {
                uvs[i] = new Vector2(verts[i].x, -verts[i].z);
            }
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[0] = mesh;

        // white lining
        mesh = new Mesh();
        ntris = 0;
        num_tris = 12;
        tris = new int[num_tris * 3];

        MakeQuad(0, 1, 2, 3);
        MakeQuad(4, 5, 6, 7);
        MakeQuad(8, 9, 10, 11);
        MakeQuad(12, 13, 14, 15);
        MakeQuad(16, 17, 18, 19);
        MakeQuad(20, 21, 22, 23);

        mesh.vertices = verts2;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts2];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts2[i].z, -verts2[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts2;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[1] = mesh;
        

        // Triangular walls
        mesh = new Mesh();
        ntris = 0;
        num_verts = 12;
        verts = new Vector3[num_verts];
        uvs = new Vector2[num_verts];

        // the left triangular wall
        verts[0] = new Vector3(upperLeft[0], 0, upperLeft[1]);
        verts[1] = new Vector3(upperLeft[0], roofHeight, (upperLeft[1] + lowerLeft[1])/2);
        verts[2] = new Vector3(lowerLeft[0], 0, lowerLeft[1]);
        verts[3] = new Vector3(lowerLeft[0], 0, lowerLeft[1]);
        verts[4] = new Vector3(upperLeft[0], roofHeight, (upperLeft[1] + lowerLeft[1]) / 2);
        verts[5] = new Vector3(upperLeft[0], 0, upperLeft[1]);

        // the right triangular wall
        verts[6] = new Vector3(upperRight[0], 0, upperRight[1]);
        verts[7] = new Vector3(upperRight[0], roofHeight, (upperRight[1] + lowerRight[1]) / 2);
        verts[8] = new Vector3(lowerRight[0], 0, lowerRight[1]);
        verts[9] = new Vector3(lowerRight[0], 0, lowerRight[1]);
        verts[10] = new Vector3(upperRight[0], roofHeight, (upperRight[1] + lowerRight[1]) / 2);
        verts[11] = new Vector3(upperRight[0], 0, upperRight[1]);

        num_tris = 4;
        tris = new int[num_tris * 3];

        MakeTri(0, 1, 2);
        MakeTri(3, 4, 5);
        MakeTri(6, 7, 8);
        MakeTri(9, 10, 11);

        mesh.vertices = verts;
        mesh.triangles = tris;
        uvs = new Vector2[num_verts];
        for (int i = 0; i < uvs.Length; i++) {
            uvs[i] = new Vector2(verts[i].z, -verts[i].x);
        }
        mesh.uv = uvs;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        meshes[2] = mesh;

        return meshes;
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
}
