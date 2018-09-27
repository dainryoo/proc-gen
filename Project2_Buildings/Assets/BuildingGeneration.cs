using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGeneration : MonoBehaviour {
    
    public int seed;
    private int currSeed;

    private Mesh bottomPlane;
    private Mesh topPlane;
    private Mesh leftPlane;
    private Mesh backPlane;
    private Mesh rightPlane;
    private Mesh frontPlane;

    private Mesh cube;

    public Material roofMat;
    private Color white = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    private Color green = new Color(0.42f, 0.68f, 0.24f, 1.0f);
    private Color black = new Color(0.1f, 0.1f, 0.1f, 1.0f);
    private Color brown = new Color(0.51f, 0.34f, 0.24f, 1.0f);
    public Material windowMat;

    private GameObject currBuilding;
    private GameObject currRoof;
    // Each floor array contains [reference to self, currCeiling, currLeftWall, currRightWall, currFrontWall, currBackWall, currGround]
    private GameObject[] currFloor;
    // and the floors array holds many of these floor arrays
    private GameObject[][] floors;
    private GameObject currWall;
    private Color mainColor;
    private Color subColor;

    void Start() {
        // Set up meshes
        cube = this.GetComponent<CubeMesh>().GetMesh();
        bottomPlane = this.GetComponent<PlaneMesh>().GetMesh(1);
        topPlane = this.GetComponent<PlaneMesh>().GetMesh(2);
        leftPlane = this.GetComponent<PlaneMesh>().GetMesh(3);
        backPlane = this.GetComponent<PlaneMesh>().GetMesh(4);
        rightPlane = this.GetComponent<PlaneMesh>().GetMesh(5);
        frontPlane = this.GetComponent<PlaneMesh>().GetMesh(6);
        // Generate a set of buildings
        seed = 23;
        currSeed = seed;
        Random.InitState(seed);
        GenerateBuilding(-12, 0.5f, 1);
        GenerateBuilding(-4, 0.5f, 1);
        GenerateBuilding(4, 0.5f, 1);
        GenerateBuilding(12, 0.5f, 1);
    }
    
    void Update() {
        // If seed has been changed, generate a new set of buildings
        if (currSeed != seed) {
            currSeed = seed;
            foreach (GameObject s in GameObject.FindGameObjectsWithTag("Player")) {
                Destroy(s);
            }
            Random.InitState(seed);
            GenerateBuilding(-12, 0.5f, 1);
            GenerateBuilding(-4, 0.5f, 1);
            GenerateBuilding(4, 0.5f, 1);
            GenerateBuilding(12, 0.5f, 1);
        }
    }

    // Building xyz is the position of the upper left corner of the building
    void GenerateBuilding(float buildingX, float buildingY, float buildingZ) {
        mainColor = new Color(Random.Range(0.1f, 0.4f), Random.Range(0.1f, 0.4f), Random.Range(0.2f, 0.95f), 1.0f);
        subColor = new Color(Random.Range(0.6f, 1), Random.Range(0.6f, 0.9f), Random.Range(0.7f, 1), 1.0f);

        // Some stuff to make object hierarchy look better
        currBuilding = new GameObject("Building");
        currBuilding.gameObject.tag = "Player";
        currRoof = new GameObject("Roof");
        currRoof.transform.parent = currBuilding.transform;

        // Get a random footprint from the Footprint Library
        int footPrintSelect = Random.Range(1, 7);
        int[,] footprint = this.GetComponent<Footprints>().GetFootprint(footPrintSelect);

        // Determine a height for the front and back sections
        int backHeight = Random.Range(2, 5);
        int frontHeight = Random.Range(2, backHeight+1);

        // Update the footprint so that each cell has a height
        // (i iterates through building depth, i.e. back to front)
        for (int i = 0; i < footprint.GetLength(0); i++) {
            // (j iterates through building width, i.e. left to right)
            for (int j = 0; j < footprint.GetLength(1); j++) {
                if (footprint[i,j] == 1) {
                    // If we're looking at a front cell
                    if (i == footprint.GetLength(0)-1) {
                        footprint[i, j] = frontHeight;
                    } else {
                        footprint[i, j] = backHeight;
                    }
                }
            }
        }

        // Booleans that determine which specific walls that the current cells needs
        bool needsFrontWall = false;
        bool needsBackWall = false;
        bool needsLeftWall = false;
        bool needsRightWall = false;

        // New array of floors for the current building
        floors = new GameObject[5][];
        // Set up the walls for each floor in the array of floors
        // (More stuff to make object hiearchy look better)
        int count = backHeight-1;
        while (count >= 0) {
            floors[count] = new GameObject[7];
            floors[count][0] = new GameObject("Floor " + (count + 1));
            floors[count][0].transform.parent = currBuilding.transform;
            floors[count][1] = new GameObject("Ceiling");
            floors[count][2] = new GameObject("Left Wall");
            floors[count][3] = new GameObject("Right Wall");
            floors[count][4] = new GameObject("Front Wall");
            floors[count][5] = new GameObject("Back Wall");
            floors[count][6] = new GameObject("Ground");
            for (int i = 0; i < 7; i++) {
                floors[count][i].transform.parent = floors[count][0].transform;
            }
            count--;
        }

        // Keep track of the building heights and upper coordinates for roof creation later
        float[][] mainRoofCoords = new float[4][];
        float[][] frontRoofCoords = new float[4][];

        // Generate window type for each cell as well as which cell will have the door
        int doorCell = Random.Range(0, footprint.GetLength(1));
        // Type 1: Wall without window
        // Type 2: Wall with left part of large window
        // Type -2: Wall with right part of large window
        // Type 2+: Wall with small window
        int[] leftRightWindowType = new int[5];
        int type = 0;
        for (int i = 0; i < 5; i++) {
            type = Random.Range(1, 6);
            if (type == 2 && i == 4) {
                type = 4;
            }
            leftRightWindowType[i] = type;
            if (type == 2) {
                leftRightWindowType[i + 1] = -2;
                i++;
            }
        }
        int[] frontBackWindowType = new int[4];
        for (int i = 0; i < 4; i++) {
            type = Random.Range(1, 6);
            if (type == 2 && i == 3) {
                type = 4;
            }
            frontBackWindowType[i] = type;
            if (type == 2) {
                frontBackWindowType[i + 1] = -2;
                i++;
            }
        }

        // Loop through all the cells of the building and build the walls!
        // (i iterates through building depth, i.e. back to front)
        for (int i = 0; i < footprint.GetLength(0); i++) {
            // (j iterates through building width, i.e. left to right)
            for (int j = 0; j < footprint.GetLength(1); j++) {
                // xyz position of current cell with respect to building's xyz
                float x = buildingX + j;
                float y = buildingY - 1;
                float z = buildingZ - i;
                // Total height/number of floors for current cell
                int maxHeight = footprint[i, j];
                int height = maxHeight;
                
                // For each floor of the current cell
                while (height > 0) {
                    // Determine the current floor we're on
                    if (height == 1) {
                        currFloor = floors[0];
                    } else if (height == 2) {
                        currFloor = floors[1];
                    } else if (height == 3) {
                        currFloor = floors[2];
                    } else if (height == 4) {
                        currFloor = floors[3];
                    } else {
                        currFloor = floors[4];
                    }
                    // Determine which walls we need
                    if (i == footprint.GetLength(0)-1 || footprint[i + 1, j] < height) {
                        needsFrontWall = true;
                    } else {
                        needsFrontWall = false;
                    }
                    if (i == 0 || footprint[i - 1, j] < height) {
                        needsBackWall = true;
                    } else {
                        needsBackWall = false;
                    }
                    if (j == 0 || footprint[i, j - 1] == 0) {
                        needsLeftWall = true;
                    } else {
                        needsLeftWall = false;
                    }
                    if (j == footprint.GetLength(1)-1 || footprint[i, j + 1] == 0) {
                        needsRightWall = true;
                    } else {
                        needsRightWall = false;
                    }

                    // Keep track of the building heights and upper coordinates for roof creation
                    if (height == maxHeight) {
                        // Check whether we need to save the main roof or the mini front roof
                        float[][] roofCoords;
                        bool isMainRoof = true;
                        if ((footPrintSelect != 1 && i == footprint.GetLength(0)-1) || height != backHeight) {
                            roofCoords = frontRoofCoords;
                            isMainRoof = false;
                        } else {
                            roofCoords = mainRoofCoords;
                        }

                        // Save upper left coord of roof
                        if ((isMainRoof && needsLeftWall && needsBackWall) || (!isMainRoof && needsLeftWall)) {
                            roofCoords[0] = new float[] { x - 0.5f, z + 0.5f };
                        }
                        // Upper right coord of roof
                        if ((isMainRoof && needsRightWall && needsBackWall) || (!isMainRoof && needsRightWall)) {
                            roofCoords[1] = new float[] { x + 0.5f, z + 0.5f };
                        }
                        // Lower left coord of roof
                        if ((isMainRoof && needsLeftWall && needsFrontWall) || (isMainRoof && needsLeftWall && i == footprint.GetLength(0)-2) || (!isMainRoof && needsLeftWall)) {
                            roofCoords[2] = new float[] { x - 0.5f, z - 0.5f };
                        }
                        // Lower right coord of roof
                        if ((isMainRoof && needsRightWall && needsFrontWall) || (isMainRoof && needsRightWall && i == footprint.GetLength(0)-2) || (!isMainRoof && needsRightWall)) {
                            roofCoords[3] = new float[] { x + 0.5f, z - 0.5f };
                        }
                    }

                    // Build the approppriate walls
                    // Ceiling
                    currWall = currFloor[1];
                    //      (the roof)
                    PlacePlane(x, (y + height)*2 + 0.5f, z, topPlane, 0.5f, 0.5f, 0.5f, null, mainColor);
                    //      (the ceiling)
                    PlacePlane(x, (y + height)*2 + 1.5f, z, bottomPlane, 0.5f, 0.5f, 0.5f, null, white);
                    // Left wall
                    if (needsLeftWall) {
                        currWall = currFloor[2];
                        // (the inside of the building)
                        PlacePlane(x - 1, (y + height) * 2, z, rightPlane, 0.5f, 1, 0.5f, null, white);
                        // (the outside of the building)
                        if (j != 0) {
                            // Wall without window
                            PlacePlane(x, (y + height) * 2, z, leftPlane, 0.5f, 1, 0.5f, null, subColor);
                        } else {
                            if (leftRightWindowType[i] == 1) {
                                // Wall without window
                                PlacePlane(x, (y + height) * 2, z, leftPlane, 0.5f, 1, 0.5f, null, subColor);
                            } else if (leftRightWindowType[i] == 2
                                && !(i == footprint.GetLength(0) - 2 && height > footprint[i + 1, j])) {
                                // Wall with large window left part
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z + 0.4f, leftPlane, 0.5f, 1, 0.1f, null, subColor);
                                PlaceLargeWindowLeft(x, (y + height) * 2, z - 0.1f, leftPlane, 0.5f, 0.5f, 0.4f, -0.51f, 0);
                            } else if (leftRightWindowType[i] == -2) {
                                // Wall with large window right part
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z - 0.4f, leftPlane, 0.5f, 1, 0.1f, null, subColor);
                                PlaceLargeWindowRight(x, (y + height) * 2, z+0.1f, leftPlane, 0.5f, 0.5f, 0.4f, -0.51f, 0);
                            } else {
                                // Wall with small window
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, leftPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z - 0.4f, leftPlane, 0.5f, 1, 0.1f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z + 0.4f, leftPlane, 0.5f, 1, 0.1f, null, subColor);
                                // Small window
                                PlaceSmallWindow(x, (y + height) * 2, z, leftPlane, 0.5f, 0.5f, 0.3f, -0.51f, 0);
                            }
                        }
                        //      (trim)
                        PlacePlane(x-0.01f, (y + height) * 2 + 1, z, leftPlane, 0.5f, 0.05f, 0.5f, null, white);
                    }
                    // Right wall
                    if (needsRightWall) {
                        currWall = currFloor[3];
                        // (the inside of the building)
                        PlacePlane(x + 1, (y + height) * 2, z, leftPlane, 0.5f, 1, 0.5f, null, white);
                        // (the outside of the building)
                        if (j != footprint.GetLength(1)-1) {
                            // Wall without window
                            PlacePlane(x, (y + height) * 2, z, rightPlane, 0.5f, 1, 0.5f, null, subColor);
                        } else {
                            if (leftRightWindowType[i] == 1) {
                                // Wall without window
                                PlacePlane(x, (y + height) * 2, z, rightPlane, 0.5f, 1, 0.5f, null, subColor);
                            } else if (leftRightWindowType[i] == 2
                                    && !(i == footprint.GetLength(0)-2 && height > footprint[i+1, j])) {
                                // Wall with large window left part
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z + 0.4f, rightPlane, 0.5f, 1, 0.1f, null, subColor);
                                // Large window left part
                                PlaceLargeWindowLeft(x, (y + height) * 2, z - 0.9f, rightPlane, 0.5f, 0.5f, 0.4f, 0.51f, 0);
                            } else if (leftRightWindowType[i] == -2) {
                                // Wall with large window right part
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z - 0.4f, rightPlane, 0.5f, 1, 0.1f, null, subColor);
                                // Large window right part
                                PlaceLargeWindowRight(x, (y + height) * 2, z+0.9f, rightPlane, 0.5f, 0.5f, 0.4f, 0.51f, 0);
                            } else {
                                // Wall with small window
                                PlacePlane(x, (y + height) * 2 + 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2 - 0.75f, z, rightPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z - 0.4f, rightPlane, 0.5f, 1, 0.1f, null, subColor);
                                PlacePlane(x, (y + height) * 2, z + 0.4f, rightPlane, 0.5f, 1, 0.1f, null, subColor);
                                // Small window
                                PlaceSmallWindow(x, (y + height) * 2, z, rightPlane, 0.5f, 0.5f, 0.3f, 0.51f, 0);
                            }
                        }
                        //      (trim)
                        PlacePlane(x+0.01f, (y + height) * 2 + 1, z, rightPlane, 0.5f, 0.05f, 0.5f, null, white);
                    }
                    // Front wall
                    if (needsFrontWall) {
                        currWall = currFloor[4];
                        // (the inside of the building)
                        PlacePlane(x, (y + height) * 2, z - 1, backPlane, 0.5f, 1, 0.5f, null, white);
                        // (the outside of the building)
                        if (i == footprint.GetLength(0)-2 && footprint[i+1,j] > 0 && footprint[i+1,j] == height-1) {
                            // Wall without window
                            PlacePlane(x, (y + height) * 2, z, frontPlane, 0.5f, 1, 0.5f, null, mainColor);
                        } else {
                            if (j == doorCell && height == 1) {
                                if (Random.Range(1,3) == 1) {
                                    // Wall with tall door
                                    PlacePlane(x, (y + height) * 2 + 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x, (y + height) * 2 - 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x - 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    PlacePlane(x + 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    // Door
                                    PlaceDoor(x, (y + height) * 2, z, frontPlane, 0.3f, 0.5f, 0.5f, black, 0);
                                } else {
                                    // Wall with lower door
                                    PlacePlane(x, (y + height) * 2 + 0.55f, z, frontPlane, 0.5f, 0.45f, 0.5f, null, mainColor);
                                    PlacePlane(x, (y + height) * 2 - 0.95f, z, frontPlane, 0.5f, 0.05f, 0.5f, null, mainColor);
                                    PlacePlane(x - 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    PlacePlane(x + 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    // Door
                                    PlaceDoor(x, (y + height) * 2 - 0.4f, z, frontPlane, 0.3f, 0.5f, 0.5f, black, 1);
                                }
                            } else {
                                if (frontBackWindowType[j] == 2 
                                        && !(height == 1 && j+1 == doorCell) 
                                        && footprint[i, j+1] != 0
                                        && !(i == footprint.GetLength(0) - 2 && ((height == 1 && footprint[i + 1, j + 1] > 0) || footprint[i+1, j+1] > height-2)) ) {
                                    // Wall with large window left part
                                    PlacePlane(x, (y + height) * 2 + 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x, (y + height) * 2 - 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x - 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    PlaceLargeWindowLeft(x + 0.1f, (y + height) * 2, z, frontPlane, 0.4f, 0.5f, 0.5f, 0, -0.51f);
                                    // Large window left part
                                } else if (frontBackWindowType[j] == -2 
                                        && !(height == 1 && j-1 == doorCell) 
                                        && footprint[i, j-1] != 0 
                                        && !(i == footprint.GetLength(0)-2 && ((height == 1 && footprint[i + 1, j - 1] > 0) || footprint[i + 1, j - 1] > height - 2))) {
                                    // Wall with large window right part
                                    PlacePlane(x, (y + height) * 2 + 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x, (y + height) * 2 - 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x + 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    // Large window right part
                                    PlaceLargeWindowRight(x-0.1f, (y + height) * 2, z, frontPlane, 0.4f, 0.5f, 0.5f, 0, -0.51f);
                                } else {
                                    // Wall with small window
                                    PlacePlane(x, (y + height) * 2 + 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x, (y + height) * 2 - 0.75f, z, frontPlane, 0.5f, 0.25f, 0.5f, null, mainColor);
                                    PlacePlane(x - 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    PlacePlane(x + 0.4f, (y + height) * 2, z, frontPlane, 0.1f, 1, 0.5f, null, mainColor);
                                    // Small window
                                    PlaceSmallWindow(x, (y + height) * 2, z, frontPlane, 0.3f, 0.5f, 0.5f, 0, -0.51f);
                                }
                            }
                        }
                        //      (trim)
                        PlacePlane(x, (y + height) * 2 + 1, z - 0.01f, frontPlane, 0.5f, 0.05f, 0.5f, null, white);
                        if (needsLeftWall) {
                            PlacePlane(x-0.5f +0.04f, (y + height) * 2, z - 0.01f, frontPlane, 0.05f, 1, 0.5f, null, white);
                            PlacePlane(x - 0.01f, (y + height) * 2, z-0.5f +0.04f, leftPlane, 0.5f, 1, 0.05f, null, white);
                        }
                        if (needsRightWall) {
                            PlacePlane(x + 0.5f - 0.04f, (y + height) * 2, z - 0.01f, frontPlane, 0.05f, 1, 0.5f, null, white);
                            PlacePlane(x + 0.01f, (y + height) * 2, z - 0.5f - 0.04f, rightPlane, 0.5f, 1, 0.05f, null, white);
                        }
                    }
                    // Back wall
                    if (needsBackWall) {
                        currWall = currFloor[5];
                        // (the inside of the building)
                        PlacePlane(x, (y + height) * 2, z + 1, frontPlane, 0.5f, 1, 0.5f, null, white);

                        if (frontBackWindowType[j] == 1) {
                            PlacePlane(x, (y + height) * 2, z, backPlane, 0.5f, 1, 0.5f, null, subColor);
                        } else if (frontBackWindowType[j] == 2) {
                            // Wall with large window left part
                            PlacePlane(x, (y + height) * 2 + 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x, (y + height) * 2 - 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x - 0.4f, (y + height) * 2, z, backPlane, 0.1f, 1, 0.5f, null, subColor);
                            PlaceLargeWindowLeft(x + 0.9f, (y + height) * 2, z, backPlane, 0.4f, 0.5f, 0.5f, 0, 0.51f);
                            // Large window left part
                        } else if (frontBackWindowType[j] == -2) {
                            // Wall with large window right
                            PlacePlane(x, (y + height) * 2 + 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x, (y + height) * 2 - 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x + 0.4f, (y + height) * 2, z, backPlane, 0.1f, 1, 0.5f, null, subColor);
                            // Large window right part
                            PlaceLargeWindowRight(x-0.9f, (y + height) * 2, z, backPlane, 0.4f, 0.5f, 0.5f, 0, 0.51f);
                        } else {
                            // Wall with small window
                            PlacePlane(x, (y + height) * 2 + 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x, (y + height) * 2 - 0.75f, z, backPlane, 0.5f, 0.25f, 0.5f, null, subColor);
                            PlacePlane(x - 0.4f, (y + height) * 2, z, backPlane, 0.1f, 1, 0.5f, null, subColor);
                            PlacePlane(x + 0.4f, (y + height) * 2, z, backPlane, 0.1f, 1, 0.5f, null, subColor);
                            // Small window
                            PlaceSmallWindow(x, (y + height) * 2, z, backPlane, 0.3f, 0.5f, 0.5f, 0, 0.51f);
                        }

                        //      (trim)
                        PlacePlane(x, (y + height) * 2 + 1, z + 0.01f, backPlane, 0.5f, 0.05f, 0.5f, null, white);
                        if (needsLeftWall) {
                            PlacePlane(x - 0.5f + 0.04f, (y + height) * 2, z + 0.025f, backPlane, 0.05f, 1, 0.5f, null, white);
                            PlacePlane(x - 0.01f, (y + height) * 2, z + 0.5f - 0.04f, leftPlane, 0.5f, 1, 0.05f, null, white);
                        }
                        if (needsRightWall) {
                            PlacePlane(x + 0.5f - 0.04f, (y + height) * 2, z + 0.025f, backPlane, 0.05f, 1, 0.5f, null, white);
                            PlacePlane(x + 0.01f, (y + height) * 2, z + 0.5f - 0.04f, rightPlane, 0.5f, 1, 0.05f, null, white);
                        }
                    }
                    // Floor
                    currWall = currFloor[6];
                    //      (the carpet/flooring)
                    PlacePlane(x, (y + height - 1) * 2, z, topPlane, 0.5f, 1, 0.5f, null, white);
                    //      (the building ground)
                    if (height == 1) {
                        PlacePlane(x, (y + height) * 2, z, bottomPlane, 0.5f, 1, 0.5f, null, black);
                    }
                    height--;
                }
            }
        }

        // Choose a random roof style
        int roofSelect = Random.Range(1, 3);
        bool isGable = false;
        if (roofSelect == 1) {
            isGable = true;
        }
        
        if ((backHeight == frontHeight && footPrintSelect == 1)) {
            // Case 1: One uncrossed roof
            if (isGable) {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetGableMesh(1.5f, 0.3f, mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
                PlaceRoof(backHeight * 2, roofMeshes[2], null, subColor, "Main Roof Wall");
            } else {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetHipMesh(1.5f, 0.3f, 1, mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
            }
        } else if (backHeight != frontHeight) {
            // Case 2: One uncrossed roof in back and one uncrossed roof in front
            if (isGable) {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetGableMesh(1.5f, 0.3f, mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
                PlaceRoof(backHeight * 2, roofMeshes[2], null, subColor, "Main Roof Wall");
                roofMeshes = this.GetComponent<RoofMesh>().GetCrossGableMesh(1.5f, 0.1f, 0, 0, frontRoofCoords[0], frontRoofCoords[1], frontRoofCoords[2], frontRoofCoords[3]);
                PlaceRoof(frontHeight * 2, roofMeshes[0], roofMat, black, "Front Roof");
                PlaceRoof(frontHeight * 2, roofMeshes[1], null, white, "Front Roof Lining");
                PlaceRoof(frontHeight * 2, roofMeshes[2], null, mainColor, "Front Roof Wall");
            } else {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetHipMesh(1.5f, 0.3f, 1, mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
                roofMeshes = this.GetComponent<RoofMesh>().GetCrossHipMesh(1.5f, 0.3f, 0.35f, 0.66f, frontRoofCoords[0], frontRoofCoords[1], frontRoofCoords[2], frontRoofCoords[3]);
                PlaceRoof(frontHeight * 2, roofMeshes[0], roofMat, black, "Cross Roof");
                PlaceRoof(frontHeight * 2, roofMeshes[1], null, white, "Cross Roof Lining");
            }
        } else {
            // Case 3: One crossed roof
            if (isGable) {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetGableMesh(1.5f, 0.3f, mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
                PlaceRoof(backHeight * 2, roofMeshes[2], null, subColor, "Main Roof Wall");
                roofMeshes = this.GetComponent<RoofMesh>().GetCrossGableMesh(1.5f, 0.1f, 2f, 0.3f, frontRoofCoords[0], frontRoofCoords[1], frontRoofCoords[2], frontRoofCoords[3]);
                PlaceRoof(frontHeight * 2, roofMeshes[0], roofMat, black, "Cross Roof");
                PlaceRoof(frontHeight * 2, roofMeshes[1], null, white, "Cross Roof Lining");
                PlaceRoof(frontHeight * 2, roofMeshes[2], null, mainColor, "Cross Roof Wall");
            } else {
                Mesh[] roofMeshes = this.GetComponent<RoofMesh>().GetHipMesh(1.5f, 0.3f, Mathf.Abs((frontRoofCoords[0][0] - frontRoofCoords[1][0])/2), mainRoofCoords[0], mainRoofCoords[1], mainRoofCoords[2], mainRoofCoords[3]);
                PlaceRoof(backHeight * 2, roofMeshes[0], roofMat, black, "Main Roof");
                PlaceRoof(backHeight * 2, roofMeshes[1], null, white, "Main Roof Lining");
                roofMeshes = this.GetComponent<RoofMesh>().GetCrossHipMesh(1.5f, 0.3f, 2.3f, 0.7f, frontRoofCoords[0], frontRoofCoords[1], frontRoofCoords[2], frontRoofCoords[3]);
                PlaceRoof(frontHeight * 2, roofMeshes[0], roofMat, black, "Cross Roof");
                PlaceRoof(frontHeight * 2, roofMeshes[1], null, white, "Cross Roof Lining");
            }
        }    
    }


    void PlaceDoor(float x, float y, float z, Mesh m, float xscale, float yscale, float zscale, Color c, int type) {
        // This is the main part of the door (and is in the same plane as the wall!)
        GameObject s = new GameObject("Door");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xscale, yscale, zscale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;
        s.transform.parent = currWall.transform;

        // Door knob
        GameObject t = new GameObject("Door Detail");
        t.AddComponent<MeshFilter>();
        t.AddComponent<MeshRenderer>();
        t.transform.position = new Vector3(x+0.17f, y - 0.06f, z - 0.53f);
        t.transform.localScale = new Vector3(0.04f, 0.04f, 0.02f);
        t.GetComponent<MeshFilter>().mesh = cube;
        rend = t.GetComponent<Renderer>();
        rend.material.color = white;
        t.transform.parent = s.transform;

        // Door upper trim
        t = new GameObject("Door Detail");
        t.AddComponent<MeshFilter>();
        t.AddComponent<MeshRenderer>();
        t.transform.position = new Vector3(x, y + 0.5f, z-0.53f);
        t.transform.localScale = new Vector3(xscale, 0.05f, 0.02f);
        t.GetComponent<MeshFilter>().mesh = cube;
        rend = t.GetComponent<Renderer>();
        rend.material.color = white;
        t.transform.parent = s.transform;
        // Door left trim
        t = new GameObject("Door Detail");
        t.AddComponent<MeshFilter>();
        t.AddComponent<MeshRenderer>();
        t.transform.position = new Vector3(x-0.33f, y+0.025f, z-0.53f);
        t.transform.localScale = new Vector3(0.03f, yscale+0.025f, 0.02f);
        t.GetComponent<MeshFilter>().mesh = cube;
        rend = t.GetComponent<Renderer>();
        rend.material.color = white;
        t.transform.parent = s.transform;
        // Door right trim
        t = new GameObject("Door Detail");
        t.AddComponent<MeshFilter>();
        t.AddComponent<MeshRenderer>();
        t.transform.position = new Vector3(x + 0.33f, y + 0.025f, z - 0.53f);
        t.transform.localScale = new Vector3(0.03f, yscale + 0.025f, 0.02f);
        t.GetComponent<MeshFilter>().mesh = cube;
        rend = t.GetComponent<Renderer>();
        rend.material.color = white;
        t.transform.parent = s.transform;


        if (type == 0) {
            // Tall door with stairs
            yscale = 0.25f;
            zscale = 0.15f;
            y = 0.25f;
            z -= 0.65f;
            t = new GameObject("Stair");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, y, z);
            t.transform.localScale = new Vector3(xscale, yscale, zscale);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;

            yscale -= 0.08f;
            zscale -= 0.04f;
            y -= 0.08f;
            z -= 0.26f;
            t = new GameObject("Stair");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, y, z);
            t.transform.localScale = new Vector3(xscale, yscale, zscale);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;

            yscale -= 0.08f;
            y -= 0.08f;
            z -= 0.22f;
            t = new GameObject("Stair");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, y, z);
            t.transform.localScale = new Vector3(xscale, yscale, zscale);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;
        } else {
            // Lower door with mini roof

            t = new GameObject("Door Roof");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, y + 0.95f, z - 0.87f);
            t.transform.localScale = new Vector3(xscale * 1.3f, 0.08f, 0.36f);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = black;
            t.transform.parent = s.transform;

            t = new GameObject("Door Roof");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, y + 0.9f, z - 0.91f);
            t.transform.localScale = new Vector3(xscale * 1.5f, 0.04f, 0.4f);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;

            t = new GameObject("Door Roof Pillar");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x - 0.35f, y + 0.3f, z - 1.2f);
            t.transform.localScale = new Vector3(0.06f, yscale + 0.4f, 0.06f);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;

            t = new GameObject("Door Roof Pillar");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x + 0.35f, y + 0.3f, z - 1.2f);
            t.transform.localScale = new Vector3(0.06f, yscale + 0.4f, 0.06f);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = white;
            t.transform.parent = s.transform;

            t = new GameObject("Doormat");
            t.AddComponent<MeshFilter>();
            t.AddComponent<MeshRenderer>();
            t.transform.position = new Vector3(x, 0, z - 0.7f);
            t.transform.localScale = new Vector3(xscale * 0.9f, 0.02f, 0.12f);
            t.GetComponent<MeshFilter>().mesh = cube;
            rend = t.GetComponent<Renderer>();
            rend.material.color = brown;
            t.transform.parent = s.transform;
        }
    }

    void PlaceLargeWindowLeft(float x, float y, float z, Mesh m, float xscale, float yscale, float zscale, float xPadding, float zPadding) {
        // This is the main part of the window (and is in the same plane as the wall!)
        GameObject s = new GameObject("Window");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xscale, yscale, zscale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material = windowMat;
        s.transform.parent = currWall.transform;

        bool frontBack = false;

        if (zPadding == 0) {
            xscale = 0.01f;
        } else {
            frontBack = true;
            zscale = 0.01f;
        }

        GameObject sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + xPadding, y + 0.5f, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;

        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + xPadding, y, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.03f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;


        // Window "balcony"

        float tempZ = zPadding;
        float tempX = xPadding;
        if (frontBack) {
            tempZ *= 1.7f;
            zscale = 0.04f;
        } else {
            tempX *= 1.7f;
            xscale = 0.04f;
        }

        // Top of railing
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + tempX, y - 0.3f, z + tempZ);
        sill.transform.localScale = new Vector3(xscale, 0.04f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = black;
        sill.transform.parent = s.transform;

        if (frontBack) {
            if (zPadding < 0) {
                tempX = -0.35f;
            } else {
                tempX = 0.35f;
            }
            tempZ = zPadding * 1.3f;
        } else {
            if (xPadding < 0) {
                tempZ = 0.35f;
            } else {
                tempZ = -0.35f;
            }
            tempX = xPadding * 1.3f;
        }
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + tempX, y - 0.3f, z + tempZ);
        if (frontBack) {
            tempX = zscale;
            tempZ = xscale * 0.4f;
        } else {
            tempZ = xscale;
            tempX = zscale * 0.4f;
        }
        sill.transform.localScale = new Vector3(tempX, 0.04f, tempZ);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = black;
        sill.transform.parent = s.transform;

        if (frontBack) {
            zscale = 0.2f;
            zPadding *= 1.38f;
        } else {
            xscale = 0.2f;
            xPadding *= 1.38f;
        }


        // Floor of balcony
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        rend = sill.GetComponent<Renderer>();
        sill.GetComponent<MeshFilter>().mesh = cube;
        sill.transform.position = new Vector3(x + xPadding, y - 0.5f, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
        rend.material.color = black;
        sill.transform.parent = s.transform;

        // Balcony rails 
        float poleX = 0.04f;
        float poleY = 0.15f;
        float poleZ = 0.04f;

        if (frontBack) {
            if (zPadding < 0) {
                xPadding = 0.35f;
            } else {
                xPadding = -0.35f;
            }
            zPadding *= -1.22f;
        } else {
            if (xPadding < 0) {
                zPadding = -0.35f;
            } else {
                zPadding = 0.35f;
            }
            xPadding *= -1.22f;
        }

        GameObject pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y-0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;

        float spacing = 0.3f;

        if (frontBack) {
            if (xPadding < 0) {
                xPadding += spacing;
            } else {
                xPadding -= spacing;
            }
        } else {
            if (zPadding < 0) {
                zPadding += spacing;
            } else {
                zPadding -= spacing;
            }
        }
        pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y-0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;

        if (frontBack) {
            if (xPadding < 0) {
                xPadding += spacing;
            } else {
                xPadding -= spacing;
            }
        } else {
            if (zPadding < 0) {
                zPadding += spacing;
            } else {
                zPadding -= spacing;
            }
        }
        pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y - 0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;

    }
    void PlaceLargeWindowRight(float x, float y, float z, Mesh m, float xscale, float yscale, float zscale, float xPadding, float zPadding) {
        // This is the main part of the window (and is in the same plane as the wall!)
        GameObject s = new GameObject("Window");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xscale, yscale, zscale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material = windowMat;
        s.transform.parent = currWall.transform;

        bool frontBack = false;

        if (zPadding == 0) {
            xscale = 0.01f;
        } else {
            frontBack = true;
            zscale = 0.01f;
        }

        GameObject sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + xPadding, y + 0.5f, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;

        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + xPadding, y, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.03f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;


        // Window "balcony"

        float tempZ = zPadding;
        float tempX = xPadding;
        if (frontBack) {
            tempZ *= 1.7f;
            zscale = 0.04f;
        } else {
            tempX *= 1.7f;
            xscale = 0.04f;
        }

        // Top of railing
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + tempX, y-0.3f, z + tempZ);
        sill.transform.localScale = new Vector3(xscale, 0.04f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = black;
        sill.transform.parent = s.transform;

        if (frontBack) {
            if (zPadding < 0) {
                tempX = 0.35f;
            } else {
                tempX = -0.35f;
            }
            tempZ = zPadding* 1.3f;
        } else {
            if (xPadding < 0) {
                tempZ = -0.35f;
            } else {
                tempZ = 0.35f;
            }
            tempX = xPadding* 1.3f;            
        }
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + tempX, y - 0.3f, z + tempZ);
        if (frontBack) {
            tempX = zscale;
            tempZ = xscale * 0.4f;
        } else {
            tempZ = xscale;
            tempX = zscale * 0.4f;
        }
        sill.transform.localScale = new Vector3(tempX, 0.04f, tempZ);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = black;
        sill.transform.parent = s.transform;

        if (frontBack) {
            zscale = 0.2f;
            zPadding *= 1.38f;
        } else {
            xscale = 0.2f;
            xPadding *= 1.38f;
        }

        // Floor of balcony
        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        rend = sill.GetComponent<Renderer>();
        sill.GetComponent<MeshFilter>().mesh = cube;
        sill.transform.position = new Vector3(x + xPadding, y - 0.5f, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
        rend.material.color = black;
        sill.transform.parent = s.transform;

        // Balcony rails 
        float poleX = 0.04f;
        float poleY = 0.15f;
        float poleZ = 0.04f;

        if (frontBack) {
            if (zPadding < 0) {
                xPadding = -0.35f;
            } else {
                xPadding = 0.35f;
            }
            zPadding *= -1.22f;
        } else {
            if (xPadding < 0) {
                zPadding = 0.35f;
            } else {
                zPadding = -0.35f;
            }
            xPadding *= -1.22f;
        }

        GameObject pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y - 0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;


        float spacing = 0.3f;

        if (frontBack) {
            if (xPadding < 0) {
                xPadding += spacing;
            } else {
                xPadding -= spacing;
            }
        } else {
            if (zPadding < 0) {
                zPadding += spacing;
            } else {
                zPadding -= spacing;
            }
        }
        pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y-0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;

        if (frontBack) {
            if (xPadding < 0) {
                xPadding += spacing;
            } else {
                xPadding -= spacing;
            }
        } else {
            if (zPadding < 0) {
                zPadding += spacing;
            } else {
                zPadding -= spacing;
            }
        }
        pole = new GameObject("Window Detail");
        pole.AddComponent<MeshFilter>();
        pole.AddComponent<MeshRenderer>();
        rend = pole.GetComponent<Renderer>();
        pole.GetComponent<MeshFilter>().mesh = cube;
        pole.transform.position = new Vector3(x - xPadding, y - 0.35f, z - zPadding);
        pole.transform.localScale = new Vector3(poleX, poleY, poleZ);
        rend.material.color = black;
        pole.transform.parent = s.transform;
    }

    void PlaceSmallWindow(float x, float y, float z, Mesh m, float xscale, float yscale, float zscale, float xPadding, float zPadding) {
        // This is the main part of the window (and is in the same plane as the wall!)
        GameObject s = new GameObject("Window");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xscale, yscale, zscale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material = windowMat;
        s.transform.parent = currWall.transform;

        if (zPadding == 0) {
            zscale = 0.3f;
            xscale = 0.01f;
        } else {
            zscale = 0.01f;
        }
        
        GameObject sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x+xPadding, y+0.5f, z+zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;

        sill = new GameObject("Window Detail");
        sill.AddComponent<MeshFilter>();
        sill.AddComponent<MeshRenderer>();
        sill.transform.position = new Vector3(x + xPadding, y, z + zPadding);
        sill.transform.localScale = new Vector3(xscale, 0.03f, zscale);
        sill.GetComponent<MeshFilter>().mesh = cube;
        rend = sill.GetComponent<Renderer>();
        rend.material.color = white;
        sill.transform.parent = s.transform;

        
        if (Random.Range(1,7) == 1) {
            sill = new GameObject("Grass");
            sill.AddComponent<MeshFilter>();
            sill.AddComponent<MeshRenderer>();
            rend = sill.GetComponent<Renderer>();
            sill.GetComponent<MeshFilter>().mesh = cube;
            sill.transform.position = new Vector3(x + (xPadding*1.05f), y - 0.31f, z + (zPadding*1.05f));
            sill.transform.localScale = new Vector3((xscale * 0.8f), Random.Range(0.04f, 0.2f), (zscale * 0.8f));
            rend.material.color = green;
            sill.transform.parent = s.transform;


            if (xPadding == 0) {
                zscale *= 4;
                xscale *= 1.01f;
            } else {
                xscale *= 4;
                zscale *= 1.01f;
            }
            sill = new GameObject("Window Detail");
            sill.AddComponent<MeshFilter>();
            sill.AddComponent<MeshRenderer>();
            rend = sill.GetComponent<Renderer>();
            sill.GetComponent<MeshFilter>().mesh = cube;
            sill.transform.position = new Vector3(x + (xPadding*1.06f), y - 0.47f, z + (zPadding*1.06f));
            sill.transform.localScale = new Vector3(xscale, 0.1f, zscale);
            rend.material.color = brown;
            sill.transform.parent = s.transform;
        } else {
            sill = new GameObject("Window Detail");
            sill.AddComponent<MeshFilter>();
            sill.AddComponent<MeshRenderer>();
            rend = sill.GetComponent<Renderer>();
            sill.GetComponent<MeshFilter>().mesh = cube;
            sill.transform.position = new Vector3(x + xPadding, y - 0.5f, z + zPadding);
            sill.transform.localScale = new Vector3(xscale, 0.05f, zscale);
            rend.material.color = white;
            sill.transform.parent = s.transform;
        }
        

    }

    void PlacePlane(float x, float y, float z, Mesh m, float xscale, float yscale, float zscale, Material mat, Color c) {
        GameObject s = new GameObject("Plane");
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(xscale, yscale, zscale);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        if (mat == null) {
            rend.material.color = c;
        } else {
            rend.material = mat;
        }
        s.transform.parent = currWall.transform;
    }

    void PlaceRoof(float y, Mesh m, Material mat, Color c, string name) {
        GameObject s = new GameObject(name);
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.transform.position = new Vector3(0, y, 0);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        if (mat == null) {
            rend.material.color = c;
        } else {
            rend.material = mat;
        }
        s.transform.parent = currRoof.transform;
    }

    

}
