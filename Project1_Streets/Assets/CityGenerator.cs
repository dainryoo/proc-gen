using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour {
    
    public GameObject meshes;
    public int seed;
    private int currSeed;

    private Mesh cube;
    private Mesh straight1;
    private Mesh straight2;
    private Mesh turn1;
    private Mesh turn2;
    private Mesh turn3;
    private Mesh turn4;
    private Mesh fourWay;
    private Mesh tJunction1;
    private Mesh tJunction2;
    private Mesh tJunction3;
    private Mesh tJunction4;
    private Mesh deadEnd1;
    private Mesh deadEnd2;
    private Mesh deadEnd3;
    private Mesh deadEnd4;

    private Mesh paintStraight1;
    private Mesh paintStraight2;
    private Mesh paintTurn1;
    private Mesh paintTurn2;
    private Mesh paintTurn3;
    private Mesh paintTurn4;
    private Mesh paintFourWay;
    private Mesh paintTJunction1;
    private Mesh paintTJunction2;
    private Mesh paintTJunction3;
    private Mesh paintTJunction4;
    private Mesh paintDeadEnd1;
    private Mesh paintDeadEnd2;
    private Mesh paintDeadEnd3;
    private Mesh paintDeadEnd4;

    private Mesh signPost1;
    private Mesh signPost2;
    private Mesh signPost3;
    private Mesh signPost4;
    private Mesh stopSign1;
    private Mesh stopSign2;
    private Mesh stopSign3;
    private Mesh stopSign4;
    private Mesh rectSign1;
    private Mesh rectSign2;
    private Mesh rectSign3;
    private Mesh rectSign4;
    private Mesh diamondSign1;
    private Mesh diamondSign2;
    private Mesh diamondSign3;
    private Mesh diamondSign4;

    private Color black = new Color(0.1f, 0.1f, 0.1f, 1.0f);
    private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color grey = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    private Color red = new Color(0.96f, 0.28f, 0.28f, 1.0f);
    private Color yellow = new Color(0.97f, 0.83f, 0.23f, 1.0f);
    private Color lightGreen = new Color(0.61f, 0.85f, 0.42f, 1.0f);
    private Color darkGreen = new Color(0.07f, 0.51f, 0.30f, 1.0f);
    private Color blue = new Color(0.57f, 0.84f, 0.96f, 1.0f);

    private int[,] grid;

    private int numRows = 40;
    private int numColumns = 30;

    void Start() {
        seed = 12;
        currSeed = seed;

        cube = meshes.GetComponent<CubeMesh>().GetMesh();
        straight1 = meshes.GetComponent<StraightMesh>().GetMesh(1);
        straight2 = meshes.GetComponent<StraightMesh>().GetMesh(2);
        turn1 = meshes.GetComponent<TurnMesh>().GetMesh(1);
        turn2 = meshes.GetComponent<TurnMesh>().GetMesh(2);
        turn3 = meshes.GetComponent<TurnMesh>().GetMesh(3);
        turn4 = meshes.GetComponent<TurnMesh>().GetMesh(4);
        fourWay = meshes.GetComponent<FourWayMesh>().GetMesh();
        tJunction1 = meshes.GetComponent<TJunctionMesh>().GetMesh(1);
        tJunction2 = meshes.GetComponent<TJunctionMesh>().GetMesh(2);
        tJunction3 = meshes.GetComponent<TJunctionMesh>().GetMesh(3);
        tJunction4 = meshes.GetComponent<TJunctionMesh>().GetMesh(4);
        deadEnd1 = meshes.GetComponent<DeadEndMesh>().GetMesh(1);
        deadEnd2 = meshes.GetComponent<DeadEndMesh>().GetMesh(2);
        deadEnd3 = meshes.GetComponent<DeadEndMesh>().GetMesh(3);
        deadEnd4 = meshes.GetComponent<DeadEndMesh>().GetMesh(4);
        
        paintStraight1 = meshes.GetComponent<PaintLinesMesh>().GetMesh(1);
        paintStraight2 = meshes.GetComponent<PaintLinesMesh>().GetMesh(2);
        paintTurn1 = meshes.GetComponent<PaintLinesMesh>().GetMesh(3);
        paintTurn2 = meshes.GetComponent<PaintLinesMesh>().GetMesh(4);
        paintTurn3 = meshes.GetComponent<PaintLinesMesh>().GetMesh(5);
        paintTurn4 = meshes.GetComponent<PaintLinesMesh>().GetMesh(6);
        paintFourWay = meshes.GetComponent<PaintLinesMesh>().GetMesh(7);
        paintTJunction1 = meshes.GetComponent<PaintLinesMesh>().GetMesh(8);
        paintTJunction2 = meshes.GetComponent<PaintLinesMesh>().GetMesh(9);
        paintTJunction3 = meshes.GetComponent<PaintLinesMesh>().GetMesh(10);
        paintTJunction4 = meshes.GetComponent<PaintLinesMesh>().GetMesh(11);
        paintDeadEnd1 = meshes.GetComponent<PaintLinesMesh>().GetMesh(12);
        paintDeadEnd2 = meshes.GetComponent<PaintLinesMesh>().GetMesh(13);
        paintDeadEnd3 = meshes.GetComponent<PaintLinesMesh>().GetMesh(14);
        paintDeadEnd4 = meshes.GetComponent<PaintLinesMesh>().GetMesh(15);

        signPost1 = meshes.GetComponent<SignMesh>().GetMesh(1);
        signPost2 = meshes.GetComponent<SignMesh>().GetMesh(2);
        signPost3 = meshes.GetComponent<SignMesh>().GetMesh(3);
        signPost4 = meshes.GetComponent<SignMesh>().GetMesh(4);
        stopSign1 = meshes.GetComponent<SignMesh>().GetMesh(5);
        stopSign2 = meshes.GetComponent<SignMesh>().GetMesh(6);
        stopSign3 = meshes.GetComponent<SignMesh>().GetMesh(7);
        stopSign4 = meshes.GetComponent<SignMesh>().GetMesh(8);
        rectSign1 = meshes.GetComponent<SignMesh>().GetMesh(9);
        rectSign2 = meshes.GetComponent<SignMesh>().GetMesh(10);
        rectSign3 = meshes.GetComponent<SignMesh>().GetMesh(11);
        rectSign4 = meshes.GetComponent<SignMesh>().GetMesh(12);
        diamondSign1 = meshes.GetComponent<SignMesh>().GetMesh(13);
        diamondSign2 = meshes.GetComponent<SignMesh>().GetMesh(14);
        diamondSign3 = meshes.GetComponent<SignMesh>().GetMesh(15);
        diamondSign4 = meshes.GetComponent<SignMesh>().GetMesh(16);

        GameObject ground = new GameObject("Ground");
        ground.AddComponent<MeshFilter>();
        ground.AddComponent<MeshRenderer>();
        ground.transform.position = new Vector3((numRows - 1), 0, -(numColumns - 1));
        ground.transform.localScale = new Vector3((numRows - 1), 0.1f, (numColumns - 1));
        ground.GetComponent<MeshFilter>().mesh = cube;
        ground.GetComponent<Renderer>().material.color = lightGreen;

        GenerateStreets();
    }

    // Delete old shapes and generate new ones if seed is updated
    void Update() {
        if (currSeed != seed) {
            currSeed = seed;
            foreach (GameObject s in GameObject.FindGameObjectsWithTag("Player")) {
                Destroy(s);
            }
            GenerateStreets();
        }
    }

    void GenerateStreets() {
        Random.InitState(seed);
        GenerateGrid();
        
        Mesh currMesh = null;
        Mesh currPaint = null;
        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j < numColumns; j++) {
                float x = i * 2.0f;
                float z = j * -2.0f;

                currPaint = null;
                bool hasTop = false;
                bool hasBottom = false;
                bool hasLeft = false;
                bool hasRight = false;

                if (grid[i,j] == 1) {

                    if (i+1 < numRows && grid[i+1, j] == 1) {
                        hasRight = true;
                    }
                    if (i-1 >= 0 && grid[i-1,j] == 1) {
                        hasLeft = true;
                    }
                    if (j+1 < numColumns && grid[i, j+1] == 1) {
                        hasBottom = true;
                    }
                    if (j-1 >= 0 && grid[i,j-1] == 1) {
                        hasTop = true;
                    }

                    if (!hasTop && !hasBottom && hasLeft && hasRight) {
                        currMesh = straight1;
                        currPaint = paintStraight1;
                        if (Random.Range(0.0f, 1.0f) < 0.4f) {
                            PlaceSign(x, 0.0f, z, grey, signPost2);
                            PlaceSign(x, 0.0f, z, white, rectSign2);
                        } else if (Random.Range(0.0f, 1.0f) < 0.4f) {
                            PlaceSign(x, 0.0f, z, grey, signPost4);
                            PlaceSign(x, 0.0f, z, white, rectSign4);
                        }
                    } else if (hasTop && hasBottom && !hasLeft && !hasRight) {
                        currMesh = straight2;
                        currPaint = paintStraight2;
                        if (Random.Range(0.0f, 1.0f) < 0.4f) {
                            PlaceSign(x, 0.0f, z, grey, signPost1);
                            PlaceSign(x, 0.0f, z, white, rectSign1);
                        } else if (Random.Range(0.0f, 1.0f) < 0.4f) {
                            PlaceSign(x, 0.0f, z, grey, signPost3);
                            PlaceSign(x, 0.0f, z, white, rectSign3);
                        }
                    } else if (!hasTop && hasBottom && hasLeft && !hasRight) {
                        currMesh = turn1;
                        currPaint = paintTurn1;
                    } else if (hasTop && !hasBottom && hasLeft && !hasRight) {
                        currMesh = turn2;
                        currPaint = paintTurn2;
                    } else if (hasTop && !hasBottom && !hasLeft && hasRight) {
                        currMesh = turn3;
                        currPaint = paintTurn3;
                    } else if (!hasTop && hasBottom && !hasLeft && hasRight) {
                        currMesh = turn4;
                        currPaint = paintTurn4;
                    } else if (hasTop && hasBottom && hasLeft && hasRight) {
                        currMesh = fourWay;
                        currPaint = paintFourWay;
                        PlaceSign(x, 0.0f, z, grey, signPost1);
                        PlaceSign(x, 0.0f, z, red, stopSign1);
                        PlaceSign(x, 0.0f, z, grey, signPost2);
                        PlaceSign(x, 0.0f, z, red, stopSign2);
                        PlaceSign(x, 0.0f, z, grey, signPost3);
                        PlaceSign(x, 0.0f, z, red, stopSign3);
                        PlaceSign(x, 0.0f, z, grey, signPost4);
                        PlaceSign(x, 0.0f, z, red, stopSign4);
                    } else if (!hasTop && hasBottom && hasLeft && hasRight) {
                        currMesh = tJunction1;
                        currPaint = paintTJunction1;
                    } else if (hasTop && hasBottom && hasLeft && !hasRight) {
                        currMesh = tJunction2;
                        currPaint = paintTJunction2;
                    } else if (hasTop && !hasBottom && hasLeft && hasRight) {
                        currMesh = tJunction3;
                        currPaint = paintTJunction3;
                    } else if (hasTop && hasBottom && !hasLeft && hasRight) {
                        currMesh = tJunction4;
                        currPaint = paintTJunction4;
                    } else if (!hasTop && hasBottom && !hasLeft && !hasRight) {
                        currMesh = deadEnd1;
                        currPaint = paintDeadEnd1;
                        PlaceSign(x, 0.0f, z, grey, signPost1);
                        PlaceSign(x, 0.0f, z, yellow, diamondSign1);
                    } else if (!hasTop && !hasBottom && hasLeft && !hasRight) {
                        currMesh = deadEnd2;
                        currPaint = paintDeadEnd2;
                        PlaceSign(x, 0.0f, z, grey, signPost4);
                        PlaceSign(x, 0.0f, z, yellow, diamondSign4);
                    } else if (hasTop && !hasBottom && !hasLeft && !hasRight) {
                        currMesh = deadEnd3;
                        currPaint = paintDeadEnd3;
                        PlaceSign(x, 0.0f, z, grey, signPost3);
                        PlaceSign(x, 0.0f, z, yellow, diamondSign3);
                    } else if (!hasTop && !hasBottom && !hasLeft && hasRight) {
                        currMesh = deadEnd4;
                        currPaint = paintDeadEnd4;
                        PlaceSign(x, 0.0f, z, grey, signPost2);
                        PlaceSign(x, 0.0f, z, yellow, diamondSign2);
                    }
                    PlaceStreet(x, 0.0f, z, black, currMesh);
                    Paint(x, -0.899f, z, white, currPaint); 
                } else {
                    if (i + 1 < numRows && grid[i + 1, j] == 0) {
                        hasRight = true;
                    }
                    if (i - 1 >= 0 && grid[i - 1, j] == 0) {
                        hasLeft = true;
                    }
                    if (j + 1 < numColumns && grid[i, j + 1] == 0) {
                        hasBottom = true;
                    }
                    if (j - 1 >= 0 && grid[i, j - 1] == 0) {
                        hasTop = true;
                    }
                    if (hasRight && hasLeft && hasBottom && hasTop) {
                        PlaceNatureDecor(x, 0.0f, z, darkGreen, cube, 1.0f, 0.15f, 1.0f);
                        if (Random.Range(0.0f, 1.0f) < 0.5f) {
                            PlaceNatureDecor(x, 0.0f, z, darkGreen, cube, Random.Range(0.2f, 0.4f), Random.Range(0.3f, 0.6f), Random.Range(0.2f, 0.4f));
                        } else {
                            
                            PlaceNatureDecor(x, 0.0f, z, grey, cube, 0.05f, 1f, 0.05f);
                            PlaceNatureDecor(x, 1.0f, z, darkGreen, cube, 0.3f, 0.3f, 0.3f);
                        }
                    } else {
                        if (Random.Range(0.0f, 1.0f) < 0.1f) {
                            PlaceBuilding(x, 0.0f, z, grey, cube);
                        }
                    }
                }
            }
        }
    }

    void GenerateGrid() {
        grid = new int[numRows, numColumns];

        for (int i = 0; i < numRows; i++) {
            for (int j = 0; j < numColumns; j++) {
                if (i == 0 || j == 0 || i + 1 == numRows || j + 1 == numColumns) { // streets on border
                    grid[i, j] = 1;
                } else {
                    grid[i, j] = 0;
                }
            }
        }

        GridSubdivision(true, 0, numRows-1, 0, numColumns-1);
    }

    void GridSubdivision(bool dividingX, int minX, int maxX, int minY, int maxY) {
        if (dividingX && (maxX-minX < 6) || !dividingX && (maxY-minY < 6)) {
            return;
        }
        
        if (dividingX) {
            Random.InitState((maxY-minY)*seed);
            int rand = Random.Range(minX + 2, (maxX - 2) + 1);
            int gap = Random.Range(minY, maxY);
            for (int j = minY; j <= maxY; j++) {
                if (j == gap && Random.Range(0.0f, 1.0f) < 0.4f) {
                    j++;
                } else {
                    grid[rand, j] = 1;
                }
            }
            GridSubdivision(false, minX, rand, minY, maxY);
            GridSubdivision(false, rand, maxX, minY, maxY);
        } else {
            Random.InitState((maxX-minX)*seed);
            int rand = Random.Range(minY + 2, (maxY - 2) + 1);
            int gap = Random.Range(minX, maxX);
            for (int i = minX; i <= maxX; i++) {
                if (i == gap && Random.Range(0.0f, 1.0f) < 0.4f) {
                    i++;
                } else {
                    grid[i, rand] = 1;
                }
            }
            GridSubdivision(true, minX, maxX, minY, rand);
            GridSubdivision(true, minX, maxX, rand, maxY);
        }
    }

    void PlaceStreet(float x, float y, float z, Color c, Mesh m) {
        GameObject s = new GameObject("Street");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(1, 0.2f, 1);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
    }

    void Paint(float x, float y, float z, Color c, Mesh m) {
        GameObject s = new GameObject("Paint");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.position = new Vector3(x, y, z);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
    }

    void PlaceSign(float x, float y, float z, Color c, Mesh m) {
        GameObject s = new GameObject("Street Sign");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.position = new Vector3(x, y, z);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
    }

    void PlaceNatureDecor(float x, float y, float z, Color c, Mesh m, float xScale, float yScale, float zScale) {
        GameObject s = new GameObject("Nature");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xScale, yScale, zScale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
    }

    void PlaceBuilding(float x, float y, float z, Color c, Mesh m) {
        GameObject s = new GameObject("Building");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(1, Random.Range(1.5f, 3f), 1);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
    }
}
