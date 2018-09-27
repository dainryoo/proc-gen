using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSubdivision : MonoBehaviour {
    private Vector3[] verts;
    private int[] tris;
    private int ntris;
    private Mesh mesh;
    private Dictionary<Face, Vector3> newFaces;
    private Dictionary<Vector3[], Vector3> newEdges;
    private Dictionary<Vector3, Vector3> newVertices;
    private Dictionary<Vector3, Vertex> vertexInfo;

    public Mesh Subdivide(Dictionary<Face, Vector3> faces, Dictionary<Vector3[], Vector3> edges, Dictionary<Vector3, Vector3> vertices, Vector3[] vertexPositions, int numSubdivisions) {
        // -------------------------------------- SUBDIVISION --------------------------------------
        while (numSubdivisions > 0) {
            numSubdivisions--;
            newFaces = new Dictionary<Face, Vector3>();
            newEdges = new Dictionary<Vector3[], Vector3>(new EdgeComparer());
            newVertices = new Dictionary<Vector3, Vector3>();
            vertexInfo = new Dictionary<Vector3, Vertex>();

            // for each face in the original list of faces
            foreach (KeyValuePair<Face, Vector3> face in faces) {
                Face currFace = face.Key;
                Vector3 newA = vertices[currFace.vertices[0]];
                Vector3 newB = vertices[currFace.vertices[1]];
                Vector3 newC = vertices[currFace.vertices[2]];
                Vector3 newD = vertices[currFace.vertices[3]];

                Vector3 edgePointAB;
                if (edges.ContainsKey(new Vector3[2] { currFace.vertices[0], currFace.vertices[1] })) {
                    edgePointAB = edges[new Vector3[2] { currFace.vertices[0], currFace.vertices[1] }];
                } else {
                    edgePointAB = edges[new Vector3[2] { currFace.vertices[1], currFace.vertices[0] }];
                }
                Vector3 edgePointBC;
                if (edges.ContainsKey(new Vector3[2] { currFace.vertices[1], currFace.vertices[2] })) {
                    edgePointBC = edges[new Vector3[2] { currFace.vertices[1], currFace.vertices[2] }];
                } else {
                    edgePointBC = edges[new Vector3[2] { currFace.vertices[2], currFace.vertices[1] }];
                }
                Vector3 edgePointCD;
                if (edges.ContainsKey(new Vector3[2] { currFace.vertices[2], currFace.vertices[3] })) {
                    edgePointCD = edges[new Vector3[2] { currFace.vertices[2], currFace.vertices[3] }];
                } else {
                    edgePointCD = edges[new Vector3[2] { currFace.vertices[3], currFace.vertices[2] }];
                }
                Vector3 edgePointDA;
                if (edges.ContainsKey(new Vector3[2] { currFace.vertices[3], currFace.vertices[0] })) {
                    edgePointDA = edges[new Vector3[2] { currFace.vertices[3], currFace.vertices[0] }];
                } else {
                    edgePointDA = edges[new Vector3[2] { currFace.vertices[0], currFace.vertices[3] }];
                }
                // for a quad face(a, b, c, d)
                // (new_a, edge_pointab, face_pointabcd, edge_pointda)
                // (new_b, edge_pointbc, face_pointabcd, edge_pointab)
                // (new_c, edge_pointcd, face_pointabcd, edge_pointbc)
                // (new_d, edge_pointda, face_pointabcd, edge_pointcd)
                Face fA = new Face(new Vector3[4] { newA, edgePointAB, currFace.facePoint, edgePointDA });
                Face fB = new Face(new Vector3[4] { newB, edgePointBC, currFace.facePoint, edgePointAB });
                Face fC = new Face(new Vector3[4] { newC, edgePointCD, currFace.facePoint, edgePointBC });
                Face fD = new Face(new Vector3[4] { newD, edgePointDA, currFace.facePoint, edgePointCD });
                // add the new faces to the face list
                newFaces.Add(fA, fA.facePoint);
                newFaces.Add(fB, fB.facePoint);
                newFaces.Add(fC, fC.facePoint);
                newFaces.Add(fD, fA.facePoint);
                // add the new faces' edges to the edges list (as well as the vertices list)
                UpdateNewEdges(fA, currFace);
                UpdateNewEdges(fB, currFace);
                UpdateNewEdges(fC, currFace);
                UpdateNewEdges(fD, currFace);
            }
            // update the face, edge, and vertex lists
            foreach (KeyValuePair<Vector3, Vertex> v in vertexInfo) {
                Vertex vertexToAdd = vertexInfo[v.Key];
                newVertices.Add(vertexToAdd.coord, vertexToAdd.barycenter);
            }
            faces = newFaces;
            edges = newEdges;
            vertices = newVertices;
        }


        // -------------------------------------- MESH CREATION --------------------------------------
        mesh = new Mesh();
        ntris = 0;
        // find the total number of vertices in the mesh
        int numVerts = 0;
        // find thte total number of faces
        int numFaces = 0;
        // for each face
        foreach (KeyValuePair<Face, Vector3> face in faces) {
            // add number of vertices of the current face
            numVerts += face.Key.vertices.Length;
            numFaces++;
        }
        verts = new Vector3[numVerts];

        int currVert = 0;
        // for each face
        foreach (KeyValuePair<Face, Vector3> face in faces) {
            // for each vertex of the current face
            for (int j = 0; j < face.Key.vertices.Length; j++) {
                // save as a mesh vertex
                verts[currVert] = face.Key.vertices[j];
                currVert++;
            }
            numVerts += face.Key.vertices.Length;
        }

        int num_tris = numFaces * 2;  // need 2 triangles per face
        tris = new int[num_tris * 3];  // need 3 vertices per triangle

        for (int i = 0; i < numFaces; i++) {
            MakeQuad(i * 4, i * 4 + 1, i * 4 + 2, i * 4 + 3);
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        return mesh;
    }
    
    void UpdateNewEdges(Face f, Face currFace) {
        // iterate through all the face's vertices
        for (int i = 0; i < f.vertices.Length; i++) {
            int j = i + 1;
            if (j >= f.vertices.Length) {
                j = 0;
            }
            
            // add the edge (current vertex plus the next vertex)
            if (newEdges.ContainsKey(new Vector3[2] { f.vertices[i], f.vertices[j] })) {
                Vector3 newEdgePoint = newEdges[new Vector3[2] { f.vertices[i], f.vertices[j] }];
                newEdgePoint = (newEdgePoint * 3 + currFace.facePoint) / 4f;
                newEdges[new Vector3[2] { f.vertices[i], f.vertices[j] }] = newEdgePoint;
            } else if (newEdges.ContainsKey(new Vector3[2] { f.vertices[j], f.vertices[i] })) {
                Vector3 newEdgePoint = newEdges[new Vector3[2] { f.vertices[j], f.vertices[i] }];
                newEdgePoint = (newEdgePoint * 3 + currFace.facePoint) / 4f;
                newEdges[new Vector3[2] { f.vertices[j], f.vertices[i] }] = newEdgePoint;
            } else {
                Edge newEdge = new Edge(f.vertices[i], f.vertices[j], f, null);
                newEdges.Add(new Vector3[2] { f.vertices[i], f.vertices[j] }, newEdge.edgePoint);
            }

            // add the vertex
            if (!vertexInfo.ContainsKey(f.vertices[i])) {
                vertexInfo.Add(f.vertices[i], new Vertex(f.vertices[i], new HashSet<Face> { f }, new HashSet<Edge> { new Edge(f.vertices[i], f.vertices[j], null, null) }));
            } else {
                HashSet<Face> oldFaceSet = vertexInfo[f.vertices[i]].faces;
                oldFaceSet.Add(f);
                HashSet<Edge> oldEdgeSet = vertexInfo[f.vertices[i]].edges;
                if (!oldEdgeSet.Contains(new Edge(f.vertices[i], f.vertices[j], null, null)) 
                    && !oldEdgeSet.Contains(new Edge(f.vertices[j], f.vertices[i], null, null))) {
                    oldEdgeSet.Add(new Edge(f.vertices[i], f.vertices[j], null, null));
                }
                vertexInfo[f.vertices[i]] = new Vertex(f.vertices[i], oldFaceSet, oldEdgeSet);
            }
        }
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

public class Face : System.IEquatable<Face> {
    public Vector3[] vertices;
    // average of all points of the face
    public Vector3 facePoint;

    public Face(Vector3[] v) {
        vertices = v;

        facePoint = new Vector3(0, 0, 0);
        for (int i = 0; i < vertices.Length; i++) {
            facePoint += vertices[i];
        }
        facePoint = facePoint / vertices.Length;
    }

    public override int GetHashCode() {
        if (vertices.Length == 3) {
            return Mathf.RoundToInt(vertices[0].x * vertices[1].x * vertices[2].x * 37);
        } else {
            return Mathf.RoundToInt(vertices[0].x * vertices[1].x * vertices[2].x * vertices[3].x * 37);
        }
    }

    public override bool Equals(object obj) {
        return Equals(obj as Face);
    }

    public bool Equals(Face otherFace) {
        if (this.vertices.Length != otherFace.vertices.Length) {
            return false;
        }
        // copy vertices arrays (doing this juuust in case I run into issues with deep/shallow copy)
        Vector3[] thisVertices = new Vector3[this.vertices.Length];
        Vector3[] otherVertices = new Vector3[otherFace.vertices.Length];
        for (int i = 0; i < thisVertices.Length; i++) {
            thisVertices[i] = this.vertices[i];
            otherVertices[i] = otherFace.vertices[i];
        }
        // attemping to sort the vertices arrays .... 
        for (int i = 0; i < thisVertices.Length; i++) {
            for (int j = i+1; j < thisVertices.Length; j++) {
                if (Vector3.Distance(thisVertices[i], thisVertices[j]) > 0) {
                    Vector3 temp = thisVertices[i];
                    thisVertices[i] = thisVertices[j];
                    thisVertices[j] = temp;
                }
                if (Vector3.Distance(otherVertices[i], otherVertices[j]) > 0) {
                    Vector3 temp = otherVertices[i];
                    otherVertices[i] = otherVertices[j];
                    otherVertices[j] = temp;
                }
            }
        }

        if (thisVertices.Length == 4 && otherVertices.Length == 4) {
            return thisVertices[0] == otherVertices[0] && thisVertices[1] == otherVertices[1] && thisVertices[2] == otherVertices[2] && thisVertices[3] == otherVertices[3];
        } else {
            return thisVertices[0] == otherVertices[0] && thisVertices[1] == otherVertices[1] && thisVertices[2] == otherVertices[2];
        }
    }
}

public class Edge : System.IEquatable<Edge> {
    // two endpoints
    public Vector3 vert1;
    public Vector3 vert2;
    // average of the two endpoints
    public Vector3 edgeMidpoint;
    // average of the two neighboring face points and the two endpoints
    public Vector3 edgePoint;

    public Face face1;
    public Face face2;

    public Edge(Vector3 v1, Vector3 v2, Face f1, Face f2) {

        vert1 = v1;
        vert2 = v2;
        edgeMidpoint = (vert1 + vert2) / 2;

        if (f1 == null && f2 == null) {
            face1 = null;
            face2 = null;
            edgePoint = new Vector3(0, 0, 0);
        } else if (f2 == null) {
            face1 = f1;
            face2 = null;
            edgePoint = (vert1 + vert2 + face1.facePoint) / 3;
        } else {
            face1 = f1;
            face2 = f2;
            edgePoint = (vert1 + vert2 + face1.facePoint + face2.facePoint) / 4;
        }
    }

    public override int GetHashCode() {
        return Mathf.RoundToInt(edgeMidpoint.x * 7 + edgeMidpoint.y * 11 + edgeMidpoint.z * 13);
    }

    public override bool Equals(object obj) {
        return Equals(obj as Edge);
    }

    public bool Equals(Edge otherEdge) {
        return (Vector3.Distance(this.vert1, otherEdge.vert1) == 0 && Vector3.Distance(this.vert2, otherEdge.vert2) == 0)
            || (Vector3.Distance(this.vert2, otherEdge.vert1) == 0 && Vector3.Distance(this.vert1, otherEdge.vert2) == 0);
    }
}

public class Vertex : System.IEquatable<Vertex> {
    // xyz geometry of vertex
    public Vector3 coord;

    // adj faces and edges
    public HashSet<Face> faces;
    public HashSet<Edge> edges;

    public Vector3 barycenter;

    public Vertex(Vector3 position, HashSet<Face> adjFaces, HashSet<Edge> adjEdges) {
        coord = position;
        faces = adjFaces;
        edges = adjEdges;

        // n is the number of adj faces
        int n = faces.Count;

        if (n > 0) {
            // P is the original coord
            Vector3 p = coord;
            // F is the average of all face points for faces touching this P
            Vector3 f = new Vector3(0, 0, 0);
            foreach (Face currFace in faces) {
                f += currFace.facePoint;
            }
            f = f / faces.Count;
            // R is the average of all edge midpoints for edges touching P
            Vector3 r = new Vector3(0, 0, 0);
            foreach (Edge currEdge in edges) {
                r += currEdge.edgeMidpoint;
            }
            r = r / edges.Count;
            // formula from wikipedia
            barycenter = (f + 2 * r + (n - 3) * p) / n;
        } else {
            barycenter = new Vector3(0, 0, 0);
        }
    }

    public override int GetHashCode() {
        return Mathf.RoundToInt(coord.x) * Mathf.RoundToInt(coord.y) * Mathf.RoundToInt(coord.z) * 37;
    }

    public override bool Equals(object obj) {
        return Equals(obj as Vertex);
    }

    public bool Equals(Vertex otherVertex) {
        return this.coord.x == otherVertex.coord.x && this.coord.y == otherVertex.coord.y && this.coord.z == otherVertex.coord.z;
    }
}

public class EdgeComparer : IEqualityComparer<Vector3[]> {
    public bool Equals(Vector3[] x, Vector3[] y) {
        if (x.Length != y.Length) {
            return false;
        }
        for (int i = 0; i < x.Length; i++) {
            if (x[i] != y[i]) {
                return false;
            }
        }
        return true;
    }
    public int GetHashCode(Vector3[] obj) {
        int result = 7;
        for (int i = 0; i < obj.Length; i++) {
            unchecked {
                result = result * 11 + Mathf.RoundToInt(obj[i].x);
            }
        }
        return result;
    }
}