using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour {
    private int[,] grid;
    private GameObject vehicleGameObjects;
    private Vehicle[] vehicles;
    public int numberOfVehicles;
    private int numCars;

    public AudioClip[] sfx = new AudioClip[8];

    // "Connections table" we discussed in lecture:
    // Key   = int[original tile, new destination tile we're going into, original tile queue number we were on]
    // Value = int queue number we will be in for the new destination tile (if two digit, that means there are two options for the next queue)
    private Dictionary<int[], int> connections;

    // Where the car should actually go in xyz-pos based on the tile and the queue it's in:
    // Key   = int[tile number, queue within the tile]
    // Value = [values to add to i, and j to get the correct position for the queue at the tile, and rotation]
    private Dictionary<int[], float[]> tilePos;

    // List of the 3-way or 4-way intersections
    private List<Intersection> intersections;

    // List of all the [i,j] coordinates for the vehicle starting positions
    private HashSet<int[]> carStartingPositions;
    // A way to check if a car is occupying some [i,j] cell
    private Dictionary<int[], bool> hasCar;

    void Start() {
        numberOfVehicles = 60;

        connections = new Dictionary<int[], int>(new IntArrayComparer());
        connections.Add(new int[] { 1, 2, 0 }, 0);
        connections.Add(new int[] { 1, 3, 0 }, 1);
        connections.Add(new int[] { 1, 3, 1 }, 0);
        connections.Add(new int[] { 1, 4, 1 }, 1);
        connections.Add(new int[] { 1, 5, 0 }, 16);
        connections.Add(new int[] { 1, 5, 1 }, 15);
        connections.Add(new int[] { 1, 6, 0 }, 0);
        connections.Add(new int[] { 1, 7, 1 }, 0);
        connections.Add(new int[] { 1, 8, 0 }, 50);
        connections.Add(new int[] { 1, 9, 0 }, 23);
        connections.Add(new int[] { 1, 9, 1 }, 14);
        connections.Add(new int[] { 1, 10, 1 }, 13);
        connections.Add(new int[] { 1, 11, 0 }, 14);
        connections.Add(new int[] { 1, 11, 1 }, 50);

        connections.Add(new int[] { 2, 1, 1 }, 1);
        connections.Add(new int[] { 2, 3, 0 }, 0);
        connections.Add(new int[] { 2, 4, 0 }, 1);
        connections.Add(new int[] { 2, 4, 1 }, 0);
        connections.Add(new int[] { 2, 5, 0 }, 15);
        connections.Add(new int[] { 2, 5, 1 }, 27);
        connections.Add(new int[] { 2, 6, 1 }, 1);
        connections.Add(new int[] { 2, 7, 0 }, 0);
        connections.Add(new int[] { 2, 8, 1 }, 13);
        connections.Add(new int[] { 2, 9, 0 }, 14);
        connections.Add(new int[] { 2, 9, 1 }, 50);
        connections.Add(new int[] { 2, 10, 0 }, 13);
        connections.Add(new int[] { 2, 10, 1 }, 24);
        connections.Add(new int[] { 2, 11, 0 }, 50);

        connections.Add(new int[] { 3, 1, 0 }, 1);
        connections.Add(new int[] { 3, 1, 1 }, 0);
        connections.Add(new int[] { 3, 2, 1 }, 1);
        connections.Add(new int[] { 3, 4, 0 }, 0);
        connections.Add(new int[] { 3, 5, 0 }, 27);
        connections.Add(new int[] { 3, 5, 1 }, 34);
        connections.Add(new int[] { 3, 6, 0 }, 1);
        connections.Add(new int[] { 3, 7, 1 }, 1);
        connections.Add(new int[] { 3, 8, 0 }, 13);
        connections.Add(new int[] { 3, 8, 1 }, 24);
        connections.Add(new int[] { 3, 9, 0 }, 50);
        connections.Add(new int[] { 3, 10, 0 }, 24);
        connections.Add(new int[] { 3, 10, 1 }, 50);
        connections.Add(new int[] { 3, 11, 1 }, 23);

        connections.Add(new int[] { 4, 1, 0 }, 0);
        connections.Add(new int[] { 4, 2, 0 }, 1);
        connections.Add(new int[] { 4, 2, 1 }, 0);
        connections.Add(new int[] { 4, 3, 1 }, 1);
        connections.Add(new int[] { 4, 5, 0 }, 34);
        connections.Add(new int[] { 4, 5, 1 }, 60);
        connections.Add(new int[] { 4, 6, 1 }, 0);
        connections.Add(new int[] { 4, 7, 0 }, 1);
        connections.Add(new int[] { 4, 8, 0 }, 24);
        connections.Add(new int[] { 4, 8, 1 }, 50);
        connections.Add(new int[] { 4, 9, 1 }, 23);
        connections.Add(new int[] { 4, 10, 0 }, 50);
        connections.Add(new int[] { 4, 11, 0 }, 23);
        connections.Add(new int[] { 4, 11, 1 }, 14);

        connections.Add(new int[] { 5, 1, 0 }, 0);
        connections.Add(new int[] { 5, 1, 3 }, 1);
        connections.Add(new int[] { 5, 1, 4 }, 0);
        connections.Add(new int[] { 5, 1, 7 }, 1);
        connections.Add(new int[] { 5, 2, 0 }, 1);
        connections.Add(new int[] { 5, 2, 1 }, 0);
        connections.Add(new int[] { 5, 2, 4 }, 1);
        connections.Add(new int[] { 5, 2, 6 }, 0);
        connections.Add(new int[] { 5, 3, 1 }, 1);
        connections.Add(new int[] { 5, 3, 2 }, 0);
        connections.Add(new int[] { 5, 3, 5 }, 0);
        connections.Add(new int[] { 5, 3, 6 }, 1);
        connections.Add(new int[] { 5, 4, 2 }, 1);
        connections.Add(new int[] { 5, 4, 3 }, 0);
        connections.Add(new int[] { 5, 4, 5 }, 1);
        connections.Add(new int[] { 5, 4, 7 }, 0);
        connections.Add(new int[] { 5, 5, 0 }, 34);
        connections.Add(new int[] { 5, 5, 1 }, 60);
        connections.Add(new int[] { 5, 5, 2 }, 15);
        connections.Add(new int[] { 5, 5, 3 }, 27);
        connections.Add(new int[] { 5, 5, 4 }, 34);
        connections.Add(new int[] { 5, 5, 6 }, 60);
        connections.Add(new int[] { 5, 5, 5 }, 15);
        connections.Add(new int[] { 5, 5, 7 }, 27);
        connections.Add(new int[] { 5, 6, 1 }, 0);
        connections.Add(new int[] { 5, 6, 3 }, 1);
        connections.Add(new int[] { 5, 6, 6 }, 0);
        connections.Add(new int[] { 5, 6, 7 }, 1);
        connections.Add(new int[] { 5, 7, 0 }, 1);
        connections.Add(new int[] { 5, 7, 2 }, 0);
        connections.Add(new int[] { 5, 7, 4 }, 1);
        connections.Add(new int[] { 5, 7, 5 }, 0);
        connections.Add(new int[] { 5, 8, 0 }, 24);
        connections.Add(new int[] { 5, 8, 1 }, 50);
        connections.Add(new int[] { 5, 8, 3 }, 13);
        connections.Add(new int[] { 5, 8, 7 }, 13);
        connections.Add(new int[] { 5, 8, 4 }, 2);
        connections.Add(new int[] { 5, 8, 6 }, 50);
        connections.Add(new int[] { 5, 9, 1 }, 23);
        connections.Add(new int[] { 5, 9, 2 }, 14);
        connections.Add(new int[] { 5, 9, 3 }, 50);
        connections.Add(new int[] { 5, 9, 5 }, 14);
        connections.Add(new int[] { 5, 9, 6 }, 23);
        connections.Add(new int[] { 5, 9, 7 }, 50);
        connections.Add(new int[] { 5, 10, 0 }, 50);
        connections.Add(new int[] { 5, 10, 2 }, 13);
        connections.Add(new int[] { 5, 10, 3 }, 24);
        connections.Add(new int[] { 5, 10, 4 }, 50);
        connections.Add(new int[] { 5, 10, 5 }, 13);
        connections.Add(new int[] { 5, 10, 7 }, 24);
        connections.Add(new int[] { 5, 11, 0 }, 23);
        connections.Add(new int[] { 5, 11, 1 }, 14);
        connections.Add(new int[] { 5, 11, 2 }, 50);
        connections.Add(new int[] { 5, 11, 4 }, 23);
        connections.Add(new int[] { 5, 11, 5 }, 50);
        connections.Add(new int[] { 5, 11, 6 }, 14);

        connections.Add(new int[] { 6, 1, 1 }, 1);
        connections.Add(new int[] { 6, 2, 0 }, 0);
        connections.Add(new int[] { 6, 3, 0 }, 1);
        connections.Add(new int[] { 6, 4, 1 }, 0);
        connections.Add(new int[] { 6, 5, 0 }, 60);
        connections.Add(new int[] { 6, 5, 1 }, 27);
        connections.Add(new int[] { 6, 6, 0 }, 0);
        connections.Add(new int[] { 6, 6, 1 }, 1);
        connections.Add(new int[] { 6, 8, 0 }, 50);
        connections.Add(new int[] { 6, 8, 1 }, 13);
        connections.Add(new int[] { 6, 9, 0 }, 23);
        connections.Add(new int[] { 6, 9, 1 }, 50);
        connections.Add(new int[] { 6, 10, 1 }, 24);
        connections.Add(new int[] { 6, 11, 0 }, 14);

        connections.Add(new int[] { 7, 1, 1 }, 0);
        connections.Add(new int[] { 7, 2, 1 }, 1);
        connections.Add(new int[] { 7, 3, 0 }, 0);
        connections.Add(new int[] { 7, 4, 0 }, 1);
        connections.Add(new int[] { 7, 5, 0 }, 15);
        connections.Add(new int[] { 7, 5, 1 }, 34);
        connections.Add(new int[] { 7, 7, 0 }, 0);
        connections.Add(new int[] { 7, 7, 1 }, 1);
        connections.Add(new int[] { 7, 8, 1 }, 24);
        connections.Add(new int[] { 7, 9, 0 }, 14);
        connections.Add(new int[] { 7, 10, 0 }, 13);
        connections.Add(new int[] { 7, 10, 1 }, 50);
        connections.Add(new int[] { 7, 11, 0 }, 50);
        connections.Add(new int[] { 7, 11, 1 }, 23);

        connections.Add(new int[] { 8, 1, 2 }, 1);
        connections.Add(new int[] { 8, 1, 3 }, 1);
        connections.Add(new int[] { 8, 2, 0 }, 0);
        connections.Add(new int[] { 8, 2, 4 }, 0);
        connections.Add(new int[] { 8, 3, 0 }, 1);
        connections.Add(new int[] { 8, 3, 4 }, 1);
        connections.Add(new int[] { 8, 3, 1 }, 0);
        connections.Add(new int[] { 8, 3, 5 }, 0);
        connections.Add(new int[] { 8, 4, 1 }, 1);
        connections.Add(new int[] { 8, 4, 2 }, 0);
        connections.Add(new int[] { 8, 4, 3 }, 0);
        connections.Add(new int[] { 8, 4, 5 }, 1);
        connections.Add(new int[] { 8, 5, 0 }, 60);
        connections.Add(new int[] { 8, 5, 1 }, 15);
        connections.Add(new int[] { 8, 5, 2 }, 27);
        connections.Add(new int[] { 8, 5, 3 }, 27);
        connections.Add(new int[] { 8, 5, 4 }, 60);
        connections.Add(new int[] { 8, 5, 5 }, 15);
        connections.Add(new int[] { 8, 6, 0 }, 0);
        connections.Add(new int[] { 8, 6, 2 }, 1);
        connections.Add(new int[] { 8, 6, 3 }, 1);
        connections.Add(new int[] { 8, 6, 4 }, 0);
        connections.Add(new int[] { 8, 7, 1 }, 0);
        connections.Add(new int[] { 8, 7, 5 }, 0);
        connections.Add(new int[] { 8, 8, 0 }, 50);
        connections.Add(new int[] { 8, 8, 2 }, 13);
        connections.Add(new int[] { 8, 8, 3 }, 13);
        connections.Add(new int[] { 8, 9, 0 }, 23);
        connections.Add(new int[] { 8, 9, 1 }, 14);
        connections.Add(new int[] { 8, 9, 2 }, 50);
        connections.Add(new int[] { 8, 9, 3 }, 50);
        connections.Add(new int[] { 8, 9, 4 }, 23);
        connections.Add(new int[] { 8, 9, 5 }, 14);
        connections.Add(new int[] { 8, 10, 1 }, 13);
        connections.Add(new int[] { 8, 10, 2 }, 24);
        connections.Add(new int[] { 8, 10, 3 }, 24);
        connections.Add(new int[] { 8, 10, 5 }, 13);
        connections.Add(new int[] { 8, 11, 0 }, 14);
        connections.Add(new int[] { 8, 11, 1 }, 50);
        connections.Add(new int[] { 8, 11, 4 }, 14);
        connections.Add(new int[] { 8, 11, 5 }, 50);

        connections.Add(new int[] { 9, 1, 0 }, 1);
        connections.Add(new int[] { 9, 1, 2 }, 0);
        connections.Add(new int[] { 9, 1, 4 }, 1);
        connections.Add(new int[] { 9, 1, 5 }, 0);
        connections.Add(new int[] { 9, 2, 1 }, 0);
        connections.Add(new int[] { 9, 2, 2 }, 1);
        connections.Add(new int[] { 9, 2, 3 }, 0);
        connections.Add(new int[] { 9, 2, 5 }, 1);
        connections.Add(new int[] { 9, 3, 1 }, 1);
        connections.Add(new int[] { 9, 3, 3 }, 1);
        connections.Add(new int[] { 9, 4, 0 }, 0);
        connections.Add(new int[] { 9, 4, 4 }, 0);
        connections.Add(new int[] { 9, 5, 0 }, 27);
        connections.Add(new int[] { 9, 5, 1 }, 60);
        connections.Add(new int[] { 9, 5, 2 }, 34);
        connections.Add(new int[] { 9, 5, 3 }, 60);
        connections.Add(new int[] { 9, 5, 4 }, 27);
        connections.Add(new int[] { 9, 5, 5 }, 34);
        connections.Add(new int[] { 9, 6, 0 }, 1);
        connections.Add(new int[] { 9, 6, 1 }, 0);
        connections.Add(new int[] { 9, 6, 3 }, 0);
        connections.Add(new int[] { 9, 6, 4 }, 1);
        connections.Add(new int[] { 9, 7, 2 }, 1);
        connections.Add(new int[] { 9, 7, 5 }, 1);
        connections.Add(new int[] { 9, 8, 0 }, 13);
        connections.Add(new int[] { 9, 8, 1 }, 50);
        connections.Add(new int[] { 9, 8, 2 }, 24);
        connections.Add(new int[] { 9, 8, 3 }, 50);
        connections.Add(new int[] { 9, 8, 4 }, 13);
        connections.Add(new int[] { 9, 8, 5 }, 24);
        connections.Add(new int[] { 9, 9, 0 }, 50);
        connections.Add(new int[] { 9, 9, 1 }, 23);
        connections.Add(new int[] { 9, 9, 3 }, 23);
        connections.Add(new int[] { 9, 9, 4 }, 50);
        connections.Add(new int[] { 9, 10, 0 }, 24);
        connections.Add(new int[] { 9, 10, 2 }, 50);
        connections.Add(new int[] { 9, 10, 4 }, 24);
        connections.Add(new int[] { 9, 10, 5 }, 50);
        connections.Add(new int[] { 9, 11, 1 }, 14);
        connections.Add(new int[] { 9, 11, 2 }, 23);
        connections.Add(new int[] { 9, 11, 3 }, 14);
        connections.Add(new int[] { 9, 11, 5 }, 23);

        connections.Add(new int[] { 10, 1, 0 }, 0);
        connections.Add(new int[] { 10, 1, 4 }, 0);
        connections.Add(new int[] { 10, 2, 0 }, 1);
        connections.Add(new int[] { 10, 2, 1 }, 0);
        connections.Add(new int[] { 10, 2, 4 }, 1);
        connections.Add(new int[] { 10, 2, 5 }, 0);
        connections.Add(new int[] { 10, 3, 1 }, 1);
        connections.Add(new int[] { 10, 3, 2 }, 0);
        connections.Add(new int[] { 10, 3, 3 }, 0);
        connections.Add(new int[] { 10, 3, 5 }, 1);
        connections.Add(new int[] { 10, 4, 2 }, 1);
        connections.Add(new int[] { 10, 4, 3 }, 1);
        connections.Add(new int[] { 10, 5, 0 }, 34);
        connections.Add(new int[] { 10, 5, 1 }, 60);
        connections.Add(new int[] { 10, 5, 2 }, 15);
        connections.Add(new int[] { 10, 5, 3 }, 15);
        connections.Add(new int[] { 10, 5, 4 }, 34);
        connections.Add(new int[] { 10, 5, 5 }, 60);
        connections.Add(new int[] { 10, 6, 1 }, 0);
        connections.Add(new int[] { 10, 6, 5 }, 0);
        connections.Add(new int[] { 10, 7, 0 }, 1);
        connections.Add(new int[] { 10, 7, 2 }, 0);
        connections.Add(new int[] { 10, 7, 3 }, 0);
        connections.Add(new int[] { 10, 7, 4 }, 1);
        connections.Add(new int[] { 10, 8, 0 }, 24);
        connections.Add(new int[] { 10, 8, 1 }, 50);
        connections.Add(new int[] { 10, 8, 4 }, 24);
        connections.Add(new int[] { 10, 8, 5 }, 50);
        connections.Add(new int[] { 10, 9, 1 }, 23);
        connections.Add(new int[] { 10, 9, 2 }, 14);
        connections.Add(new int[] { 10, 9, 3 }, 14);
        connections.Add(new int[] { 10, 9, 5 }, 23);
        connections.Add(new int[] { 10, 10, 0 }, 50);
        connections.Add(new int[] { 10, 10, 2 }, 13);
        connections.Add(new int[] { 10, 10, 3 }, 13);
        connections.Add(new int[] { 10, 10, 4 }, 50);
        connections.Add(new int[] { 10, 11, 0 }, 23);
        connections.Add(new int[] { 10, 11, 1 }, 14);
        connections.Add(new int[] { 10, 11, 2 }, 50);
        connections.Add(new int[] { 10, 11, 3 }, 50);
        connections.Add(new int[] { 10, 11, 4 }, 23);
        connections.Add(new int[] { 10, 11, 5 }, 14);

        connections.Add(new int[] { 11, 1, 1 }, 0);
        connections.Add(new int[] { 11, 1, 2 }, 1);
        connections.Add(new int[] { 11, 1, 3 }, 0);
        connections.Add(new int[] { 11, 1, 5 }, 1);
        connections.Add(new int[] { 11, 2, 1 }, 1);
        connections.Add(new int[] { 11, 2, 3 }, 1);
        connections.Add(new int[] { 11, 3, 0 }, 0);
        connections.Add(new int[] { 11, 3, 4 }, 0);
        connections.Add(new int[] { 11, 4, 0 }, 1);
        connections.Add(new int[] { 11, 4, 2 }, 0);
        connections.Add(new int[] { 11, 4, 4 }, 1);
        connections.Add(new int[] { 11, 4, 5 }, 0);
        connections.Add(new int[] { 11, 5, 0 }, 15);
        connections.Add(new int[] { 11, 5, 1 }, 34);
        connections.Add(new int[] { 11, 5, 2 }, 27);
        connections.Add(new int[] { 11, 5, 3 }, 34);
        connections.Add(new int[] { 11, 5, 4 }, 15);
        connections.Add(new int[] { 11, 5, 5 }, 27);
        connections.Add(new int[] { 11, 6, 2 }, 1);
        connections.Add(new int[] { 11, 6, 5 }, 1);
        connections.Add(new int[] { 11, 7, 0 }, 0);
        connections.Add(new int[] { 11, 7, 1 }, 1);
        connections.Add(new int[] { 11, 7, 3 }, 1);
        connections.Add(new int[] { 11, 7, 4 }, 0);
        connections.Add(new int[] { 11, 8, 1 }, 24);
        connections.Add(new int[] { 11, 8, 2 }, 13);
        connections.Add(new int[] { 11, 8, 3 }, 24);
        connections.Add(new int[] { 11, 8, 5 }, 13);
        connections.Add(new int[] { 11, 9, 0 }, 14);
        connections.Add(new int[] { 11, 9, 2 }, 50);
        connections.Add(new int[] { 11, 9, 4 }, 14);
        connections.Add(new int[] { 11, 9, 5 }, 50);
        connections.Add(new int[] { 11, 10, 0 }, 13);
        connections.Add(new int[] { 11, 10, 1 }, 50);
        connections.Add(new int[] { 11, 10, 2 }, 24);
        connections.Add(new int[] { 11, 10, 5 }, 24);
        connections.Add(new int[] { 11, 10, 3 }, 50);
        connections.Add(new int[] { 11, 10, 4 }, 13);
        connections.Add(new int[] { 11, 11, 0 }, 50);
        connections.Add(new int[] { 11, 11, 1 }, 23);
        connections.Add(new int[] { 11, 11, 3 }, 23);
        connections.Add(new int[] { 11, 11, 4 }, 50);

        tilePos = new Dictionary<int[], float[]>(new IntArrayComparer());
        tilePos.Add(new int[] { 1, 0 }, new float[] { 0.25f, 0.25f, 180 });
        tilePos.Add(new int[] { 1, 1 }, new float[] { -0.25f, -0.25f, 90 });

        tilePos.Add(new int[] { 2, 0 }, new float[] { -0.25f, 0.25f, 90 });
        tilePos.Add(new int[] { 2, 1 }, new float[] { 0.25f, -0.25f, 0 });

        tilePos.Add(new int[] { 3, 0 }, new float[] { -0.25f, -0.25f, 0 });
        tilePos.Add(new int[] { 3, 1 }, new float[] { 0.25f, 0.25f, -90 });

        tilePos.Add(new int[] { 4, 0 }, new float[] { 0.25f, -0.25f, -90 });
        tilePos.Add(new int[] { 4, 1 }, new float[] { -0.25f, 0.25f, 180 });

        tilePos.Add(new int[] { 5, 0 }, new float[] { 0.25f, 0.25f, -90 });
        tilePos.Add(new int[] { 5, 1 }, new float[] { -0.25f, 0.25f, 180 });
        tilePos.Add(new int[] { 5, 2 }, new float[] { -0.25f, -0.25f, 90 });
        tilePos.Add(new int[] { 5, 3 }, new float[] { 0.25f, -0.25f, 0 });
        tilePos.Add(new int[] { 5, 4 }, new float[] { 0.25f, 0, -90 });
        tilePos.Add(new int[] { 5, 5 }, new float[] { -0.25f, 0, 90 });
        tilePos.Add(new int[] { 5, 6 }, new float[] { 0, 0.25f, 180 });
        tilePos.Add(new int[] { 5, 7 }, new float[] { 0, -0.25f, 0 });

        tilePos.Add(new int[] { 6, 0 }, new float[] { 0, 0.25f, 180 });
        tilePos.Add(new int[] { 6, 1 }, new float[] { 0, -0.25f, 0 });

        tilePos.Add(new int[] { 7, 0 }, new float[] { -0.25f, 0, 90 });
        tilePos.Add(new int[] { 7, 1 }, new float[] { 0.25f, 0, -90 });

        tilePos.Add(new int[] { 8, 0 }, new float[] { 0, 0.25f, 180 });
        tilePos.Add(new int[] { 8, 1 }, new float[] { -0.25f, -0.25f, 90 });
        tilePos.Add(new int[] { 8, 2 }, new float[] { 0.25f, -0.25f, 0 });
        tilePos.Add(new int[] { 8, 3 }, new float[] { 0, -0.25f, 0 });
        tilePos.Add(new int[] { 8, 4 }, new float[] { 0.25f, 0.25f, 180 });
        tilePos.Add(new int[] { 8, 5 }, new float[] { -0.25f, 0.25f, 90 });

        tilePos.Add(new int[] { 9, 0 }, new float[] { 0, -0.25f, 0 });
        tilePos.Add(new int[] { 9, 1 }, new float[] { -0.25f, 0.25f, 180 });
        tilePos.Add(new int[] { 9, 2 }, new float[] { 0.25f, 0.25f, -90 });
        tilePos.Add(new int[] { 9, 3 }, new float[] { 0, 0.25f, 180 });
        tilePos.Add(new int[] { 9, 4 }, new float[] { -0.25f, -0.25f, 0});
        tilePos.Add(new int[] { 9, 5 }, new float[] { 0.25f, -0.25f, -90 });

        tilePos.Add(new int[] { 10, 0 }, new float[] { 0.25f, 0, -90 });
        tilePos.Add(new int[] { 10, 1 }, new float[] { -0.25f, 0.25f, 180 });
        tilePos.Add(new int[] { 10, 2 }, new float[] { -0.25f, -0.25f, 90 });
        tilePos.Add(new int[] { 10, 3 }, new float[] { -0.25f, 0, 90 });
        tilePos.Add(new int[] { 10, 4 }, new float[] { 0.25f, -0.25f, -90 });
        tilePos.Add(new int[] { 10, 5 }, new float[] { 0.25f, 0.25f, 180 });

        tilePos.Add(new int[] { 11, 0 }, new float[] { -0.25f, 0, 90 });
        tilePos.Add(new int[] { 11, 1 }, new float[] { 0.25f, 0.25f, -90 });
        tilePos.Add(new int[] { 11, 2 }, new float[] { 0.25f, -0.25f, 0 });
        tilePos.Add(new int[] { 11, 3 }, new float[] { 0.25f, 0, -90 });
        tilePos.Add(new int[] { 11, 4 }, new float[] { -0.25f, 0.25f, 90 });
        tilePos.Add(new int[] { 11, 5 }, new float[] { -0.25f, -0.25f, 0 });
    }

    // Based on the current [i,j] and queue we're on, which [i, j] grid tile do we move to next?
    int[] GetNextIJ(int i, int j, int q) {
        int newI = i;
        int newJ = j;
        int currTile = grid[i, j];
        if (currTile == 1) {
            if (q == 0) {
                newI = i - 1;
            } else {
                newJ = j + 1;
            }
        } else if (currTile == 2) {
            if (q == 0) {
                newJ = j + 1;
            } else {
                newI = i + 1;
            }
        } else if (currTile == 3) {
            if (q == 0) {
                newI = i + 1;
            } else {
                newJ = j - 1;
            }
        } else if (currTile == 4) {
            if (q == 0) {
                newJ = j - 1;
            } else {
                newI = i - 1;
            }
        } else if (currTile == 5) {
            if (q == 0) {
                newJ = j - 1;
            } else if (q == 1) {
                newI = i - 1;
            } else if (q == 2) {
                newJ = j + 1;
            } else if (q == 3) {
                newI = i + 1;
            } else if (q == 4) {
                newJ = j - 1;
            } else if (q == 5) {
                newJ = j + 1;
            } else if (q == 6) {
                newI = i - 1;
            } else {
                newI = i + 1;
            }
        } else if (currTile == 6) {
            if (q == 0) {
                newI = i - 1;
            } else {
                newI = i + 1;
            }
        } else if (currTile == 7) {
            if (q == 0) {
                newJ = j + 1;
            } else {
                newJ = j - 1;
            }
        } else if (currTile == 8) {
            if (q == 0) {
                newI = i - 1;
            } else if (q == 1) {
                newJ = j + 1;
            } else if (q == 2) {
                newI = i + 1;
            } else if (q == 3) {
                newI = i + 1;
            } else if (q == 4) {
                newI = i - 1;
            } else {
                newJ = j + 1;
            }
        } else if (currTile == 9) {
            if (q == 0) {
                newI = i + 1;
            } else if (q == 1) {
                newI = i - 1;
            } else if (q == 2) {
                newJ = j - 1;
            } else if (q == 3){
                newI = i - 1;
            } else if (q == 4) {
                newI = i + 1;
            } else {
                newJ = j - 1;
            }
        } else if (currTile == 10) {
            if (q == 0) {
                newJ = j - 1;
            } else if (q == 1) {
                newI = i - 1;
            } else if (q == 2) {
                newJ = j + 1;
            } else if (q == 3) {
                newJ = j + 1;
            } else if (q == 4) {
                newJ = j - 1;
            } else {
                newI = i - 1;
            }
        } else if (currTile == 11) {
            if (q == 0) {
                newJ = j + 1;
            } else if (q == 1) {
                newJ = j - 1;
            } else if (q == 2) {
                newI = i + 1;
            } else if (q == 3) {
                newJ = j - 1;
            } else if (q == 4) {
                newJ = j + 1;
            } else {
                newI = i + 1;
            }
        }
        return new int[] { newI, newJ };
    }


    public void UpdateGrid(int[,] newGrid) {
        // Clean up exisiting traffic
        foreach (GameObject s in GameObject.FindGameObjectsWithTag("Respawn")) {
            Destroy(s);
        }

        grid = newGrid;
        // make a new list of Intersection objects based on the new grids
        intersections = new List<Intersection>();
        for (int a = 0; a < grid.GetLength(0); a++) {
            for (int b = 0; b < grid.GetLength(1); b++) {
                if (grid[a, b] == 5 || grid[a, b] > 7) {
                    intersections.Add(new Intersection(a, b, grid[a, b]));
                }
            }
        }

        hasCar = new Dictionary<int[], bool>(new IntArrayComparer());
        vehicleGameObjects = new GameObject("Traffic");
        vehicleGameObjects.gameObject.tag = "Respawn";
        numCars = numberOfVehicles;
        vehicles = new Vehicle[numCars];
        // Create the appropriate number of cars
        for (int a = 0; a < numCars; a++) {
            int i = 0; // horizontal
            int j = 0; // vertical
            int q = 1; // queue
            // choose a random [i,j] and queue for the vehicle
            while(hasCar.ContainsKey(new int[] { i, j, q }) || grid[i,j] == 0) {
                i = Random.Range(0, grid.GetLength(0));
                j = Random.Range(0, grid.GetLength(1));
                q = Random.Range(0, 2);
            }
            hasCar.Add(new int[] { i, j, q }, true);

            int[] nextTileIJ = GetNextIJ(i, j, q);
            int nextI = nextTileIJ[0];
            int nextJ = nextTileIJ[1];
            int currTile = grid[i, j];
            int nextTile = grid[nextI, nextJ];
            int nextQ = connections[new int[] { currTile, nextTile, q }];
            if (nextQ > 9) {
                int option1 = nextQ % 10; // ones digit
                int option2 = nextQ / 10; // tens digit
                if (Random.value < 0.5f) {
                    nextQ = option1;
                } else {
                    nextQ = option2;
                }
            }
            float[] startPos = tilePos[new int[] { currTile, q }];
            float[] destPos = tilePos[new int[] { nextTile, nextQ }];
            Vehicle v = new Vehicle(startPos, destPos, i, j, nextI, nextJ, q, nextQ, currTile, nextTile, sfx[Random.Range(0, sfx.Length)]);
            v.obj.transform.parent = vehicleGameObjects.transform;
            vehicles[a] = v;
        }
    }


	void Update () {
        numberOfVehicles = Mathf.Max(numberOfVehicles, 0);
        numberOfVehicles = Mathf.Min(numberOfVehicles, 200);

        // Check to make sure the number of desired vehicles hasn't changed
        if (numberOfVehicles != numCars) {
            // if it has, then redraw all the cars
            numCars = numberOfVehicles;
            UpdateGrid(grid);
        } else {
            for (int i = 0; i < vehicles.Length; i++) {
                if (vehicles[i].arrived) {
                    // If a car has arrived at its destination, update its destination
                    int oldI = vehicles[i].currI;
                    int oldJ = vehicles[i].currJ;
                    int oldQ = vehicles[i].currQ;
                    if (hasCar.ContainsKey(new int[] { oldI, oldJ, oldQ })) {
                        hasCar[new int[] { oldI, oldJ, oldQ }] = false;
                    } else {
                        hasCar.Add(new int[] { oldI, oldJ, oldQ }, false);
                    }

                    int currI = vehicles[i].destI;
                    int currJ = vehicles[i].destJ;
                    int currQ = vehicles[i].destQ;
                    int[] nextTileIJ = GetNextIJ(currI, currJ, currQ);
                    int nextI = nextTileIJ[0];
                    int nextJ = nextTileIJ[1];
                    int currTile = grid[currI, currJ];
                    int nextTile = grid[nextI, nextJ];
                    int nextQ = connections[new int[] { currTile, nextTile, currQ }];
                    if (nextQ > 9) {
                        // Pick one of the two digits if the queue is represented as a two digit number (aka there are multiple choices for queues)
                        int option1 = nextQ % 10; // ones digit
                        int option2 = (nextQ - option1) / 10; // tens digit
                        if (Random.value < 0.5f) {
                            nextQ = option1;
                        } else {
                            nextQ = option2;
                        }
                    }
                    float[] nextPos = tilePos[new int[] { nextTile, nextQ }];
                    vehicles[i].UpdateVelocity(nextPos, nextI, nextJ, nextQ, nextTile);

                    
                    if (hasCar.ContainsKey(new int[] { currI, currJ, currQ })) {
                        hasCar[new int[] { currI, currJ, currQ }] = true;
                    } else {
                        hasCar.Add(new int[] { currI, currJ, currQ }, true);
                    }

                    if (intersections.Contains(new Intersection(nextI, nextJ, nextTile))) {
                        // If we're about to go into an intersection, enter that intersection's queue
                        Intersection currIntersection = intersections.Find(intersection => (intersection.i == nextI && intersection.j == nextJ));
                        currIntersection.AddCar(vehicles[i]);
                    }
                } else {
                    // If it hasn't arrived yet, keep driving
                    //vehicles[i].Drive();
                    if (!hasCar.ContainsKey(new int[] { vehicles[i].destI, vehicles[i].destJ, vehicles[i].destQ }) 
                        || !hasCar[new int[] { vehicles[i].destI, vehicles[i].destJ, vehicles[i].destQ}]) {
                        // But only if the destination is free of cars
                        vehicles[i].Drive(false);
                    } else {
                        // if there are cars in front, can drive, but just slow down
                        vehicles[i].Drive(true);
                    }
                }
            }

            // Update all the intersections and their vehicle queues
            foreach (Intersection intersection in intersections) {
                intersection.MaintainTraffic();
            }
        }
	}
}

public class Vehicle {
    public int currI;
    public int currJ;
    public int currQ;
    public int currTileType;
    public int destI;
    public int destJ;
    public int destQ;
    public int destTileType;

    public float currRot;
    public float destRot;

    public GameObject obj;
    public GameObject head;
    public GameObject leftWing;
    public GameObject rightWing;
    public Vector3 destination;
    public Vector3 velocity;

    public bool arrived;
    public bool stopped;

    public float yPos;
    public float speedVariable;
    public float bounce;
    public int bounceSpeed;
    
    public AudioClip honk;
    public bool canHonk;

    float decceleration;

    public Vehicle(float[] startPos, float[] destPos, int i, int j, int nextI, int nextJ, int q, int nextQ, int currTile, int destTile, AudioClip sfx) {
        obj = new GameObject("Chicken");
        obj.gameObject.tag = "Respawn";
        GameObject body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.gameObject.name = "Head";
        head.transform.localScale = new Vector3(0.23f, 0.23f, 0.23f);
        head.transform.position = new Vector3(0.03f, 0.06f, 0);
        body.gameObject.name = "Body";
        body.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        head.transform.parent = obj.transform;
        body.transform.parent = obj.transform;
        GameObject misc = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        misc.gameObject.name = "Comb";
        misc.transform.localScale = new Vector3(0.12f, 0.05f, 0.05f);
        misc.transform.position = new Vector3(0.05f, 0.15f, 0);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.red;
        misc.transform.parent = head.transform;
        misc = GameObject.CreatePrimitive(PrimitiveType.Cube);
        misc.gameObject.name = "Beak";
        misc.transform.localScale = new Vector3(0.03f, 0.02f, 0.03f);
        misc.transform.Rotate(0, 45, 0);
        misc.transform.position = new Vector3(0.14f, 0.08f, 0);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        misc.transform.parent = head.transform;
        misc = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        misc.gameObject.name = "Eye";
        misc.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        misc.transform.position = new Vector3(0.12f, 0.09f, 0.05f);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.black;
        misc.transform.parent = head.transform;
        misc = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        misc.gameObject.name = "Eye";
        misc.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        misc.transform.position = new Vector3(0.12f, 0.09f, -0.05f);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.black;
        misc.transform.parent = head.transform;
        leftWing = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        leftWing.gameObject.name = "Left Wing";
        leftWing.transform.localScale = new Vector3(0.2f, 0.15f, 0.07f);
        leftWing.transform.position = new Vector3(0, 0, 0.12f);
        leftWing.transform.parent = body.transform;
        leftWing.transform.Rotate(0, 30, 0);
        rightWing = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rightWing.gameObject.name = "Right Wing";
        rightWing.transform.localScale = new Vector3(0.2f, 0.15f, 0.07f);
        rightWing.transform.position = new Vector3(0, 0, -0.12f);
        rightWing.transform.parent = body.transform;
        rightWing.transform.Rotate(0, -30, 0);
        misc = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        misc.gameObject.name = "Left Leg";
        misc.transform.localScale = new Vector3(0.03f, 0.1f, 0.03f);
        misc.transform.position = new Vector3(0, -0.1f, 0.05f);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        misc.transform.parent = body.transform;
        misc = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        misc.gameObject.name = "Right Leg";
        misc.transform.localScale = new Vector3(0.03f, 0.1f, 0.03f);
        misc.transform.position = new Vector3(0, -0.1f, -0.05f);
        misc.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        misc.transform.parent = body.transform;
        obj.transform.localScale = new Vector3(Random.Range(0.9f, 1), Random.Range(0.9f, 1), Random.Range(0.9f, 1));

        canHonk = true;
        honk = sfx;

        yPos = 0.4f;
        speedVariable = Random.Range(12,20f);
        bounce = Random.Range(0.01f, 0.04f);
        //bounce = 0;
        bounceSpeed = Random.Range(20, 27);
        arrived = false;
        stopped = false;
        currI = i;
        currJ = j;
        currQ = q;
        currTileType = currTile;
        currRot = startPos[2];
        destI = nextI;
        destJ = nextJ;
        destQ = nextQ;
        destTileType = destTile;
        destRot = destPos[2];

        obj.transform.position = new Vector3(currI * 2f + startPos[0], yPos, -currJ * 2f + startPos[1]);
        destination = new Vector3(destI * 2f + destPos[0], yPos, -destJ * 2f + destPos[1]);
        velocity = (destination - obj.transform.position) / speedVariable;
        decceleration = 1;
    }

    public void Drive(bool slowDown) {
        if (slowDown) {
            decceleration = Mathf.Max(decceleration-Random.Range(0.05f,0.07f), 0);
        } else {
            decceleration = 1;
            canHonk = true;
        }
        if (decceleration == 0 && canHonk && Random.value < 0.3f) {
            AudioSource.PlayClipAtPoint(honk, obj.transform.position); // honk honk
            canHonk = false; // honking more than one time is too rude
        }

        obj.transform.position += new Vector3(0, bounce * Mathf.Sin(bounceSpeed * Time.time), 0); // bounce

        Vector3 currXZ = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
        Vector3 targetXZ = new Vector3(destination.x, 0, destination.z);
        if (Vector3.SqrMagnitude(currXZ - targetXZ) < 0.01f) {
            // if we've reached the destination
            arrived = true;
        } else if (!stopped) {
            // else if we haven't reached the destination and aren't stopped at an intersection
            obj.transform.position += (velocity * decceleration);
            obj.transform.rotation = Quaternion.Euler(0, currRot, 0);
        }
    }

    public void UpdateVelocity(float[] destPos, int nextI, int nextJ, int nextQ, int nextTile) {
        currI = destI;
        currJ = destJ;
        currQ = destQ;
        currRot = destRot;
        currTileType = destTileType;
        destI = nextI;
        destJ = nextJ;
        destQ = nextQ;
        destRot = destPos[2];
        destTileType = nextTile;

        destination = new Vector3(destI * 2f + destPos[0], yPos, -destJ * 2f + destPos[1]);
        velocity = (destination - obj.transform.position) / speedVariable;
        arrived = false;

        // Turn signals :)
        if ((destTileType == 8 && destQ == 2) || (destTileType == 8 && destQ == 1) || (destTileType == 9 && destQ == 1) || (destTileType == 9 && destQ == 2)
                    || (destTileType == 10 && destQ == 1) || (destTileType == 10 && destQ == 2) || (destTileType == 11 && destQ == 1) || (destTileType == 11 && destQ == 2) 
                    || (destTileType == 5 && destQ == 0) || (destTileType == 5 && destQ == 1) || (destTileType == 5 && destQ == 2) || (destTileType == 5 && destQ == 3)) {
            rightWing.gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else {
            rightWing.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        if ((destTileType == 8 && destQ == 4) || (destTileType == 9 && destQ == 4) || (destTileType == 10 && destQ == 4) || (destTileType == 11 && destQ == 4)
            || (destTileType == 8 && destQ == 5) || (destTileType == 9 && destQ == 5) || (destTileType == 10 && destQ == 5) || (destTileType == 11 && destQ == 5)) {
            leftWing.gameObject.GetComponent<Renderer>().material.color = Color.red;
        } else {
            leftWing.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}

public class Intersection : System.IEquatable<Intersection> {
    public int i;
    public int j;
    public int tileType;
    public Queue<Vehicle> q;

    public Vehicle frontCar;

    public Intersection(int i, int j, int tile) {
        this.i = i;
        this.j = j;
        tileType = tile;
        q = new Queue<Vehicle>();
        frontCar = null;
    }

    public void MaintainTraffic() {
        if (frontCar != null) {
            if (frontCar.stopped) {
                // if the first car is stopped, let it start driving
                frontCar.stopped = false;
                //frontCar.head.gameObject.GetComponent<Renderer>().material.color = Color.green;
            } else if (frontCar.currI == i && frontCar.currJ == j) {
                // else if the first car is going, and it's off the intersection
                //frontCar.head.gameObject.GetComponent<Renderer>().material.color = Color.white;
                q.Dequeue();
                if (q.Count > 0) {
                    frontCar = q.Peek();
                } else {
                    frontCar = null;
                }
            }
        }
    }

    public void AddCar(Vehicle v) {
        if (q.Count == 0) {
            frontCar = v;
        }
        q.Enqueue(v);
        v.stopped = true;
        //v.head.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public override int GetHashCode() {
        return 31 * this.i + 17 * this.j;
    }
    public override bool Equals(object other) {
        return other is Intersection && Equals((Intersection)other);
    }
    public bool Equals(Intersection other) {
        return this.i == other.i && this.j == other.j;
    }
}

public class IntArrayComparer : IEqualityComparer<int[]> {
    public bool Equals(int[] x, int[] y) {
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
    public int GetHashCode(int[] obj) {
        int result = 17;
        for (int i = 0; i < obj.Length; i++) {
            unchecked {
                result = result * 23 + obj[i];
            }
        }
        return result;
    }
}