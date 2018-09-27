using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator : MonoBehaviour {

    public int seed;
    private int currSeed;
    public int subdivisionDepth; // increase if Unity can handle it
    private int currDepth;

    private Color[] bodyColors;
    private Color[] beakColors;
    private Vector3 eyePos;
    private Vector3 beakPos;
    private Vector3 legPos;
    private float legHeight;
    private Vector3 wingPos;
    private Vector3 tailPos;
    private GameObject currChicken;

    void Start() {
        seed = 0;
        currSeed = seed;
        Random.InitState(seed);
        subdivisionDepth = 3;
        currDepth = subdivisionDepth;
        Color black = new Color(0.1f, 0.1f, 0.1f, 1);
        Color white = new Color(1, 1, 1, 1);
        Color yellow = new Color(1, 0.77f, 0.44f, 1);
        Color brown = new Color(0.59f, 0.52f, 0.43f, 1);
        Color gray = new Color(0.622f, 0.69f, 0.74f, 1);
        bodyColors = new Color[5] { black, white, yellow, brown, gray };
        beakColors = new Color[4] { new Color(1, 0.71f, 0.47f,  1), new Color(1, 0.57f, 0.2f, 1), new Color(1, 0.82f, 0.2f, 1), new Color(1, 0.88f, 0.46f, 1) };

        GenerateChicken(-16, 1, 0, subdivisionDepth);
        GenerateChicken(-8, 1, 0, subdivisionDepth);
        GenerateChicken(0, 1, 0, subdivisionDepth);
        GenerateChicken(8, 1, 0, subdivisionDepth);
        GenerateChicken(16, 1, 0, subdivisionDepth); 
    }

    void Update() {
        if (currSeed != seed || currDepth != subdivisionDepth) {
            currSeed = seed;
            currDepth = subdivisionDepth;
            foreach (GameObject s in GameObject.FindGameObjectsWithTag("Player")) {
                Destroy(s);
            }
            Random.InitState(seed);
            GenerateChicken(-16, 1, 0, subdivisionDepth);
            GenerateChicken(-8, 1, 0, subdivisionDepth);
            GenerateChicken(0, 1, 0, subdivisionDepth);
            GenerateChicken(8, 1, 0, subdivisionDepth);
            GenerateChicken(16, 1, 0, subdivisionDepth);
        }
    }

    void GenerateChicken(float x, float y, float z, int depth) {
        currChicken = new GameObject("Chicken");
        currChicken.gameObject.tag = "Player";
        Color bodyColor = bodyColors[Random.Range(1, bodyColors.Length)] + new Color(Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f), 0);
        Color beakColor = beakColors[Random.Range(0, beakColors.Length)];

        // BODY
        Mesh body = GenerateBody(depth);
        y += legHeight;
        PlaceMesh(x, y, z, bodyColor, body, "Body");
        // EYES
        PlaceEyes(x, y + eyePos.y, z + eyePos.z, eyePos.x, bodyColors[0]);
        // BEAK
        Mesh beak = GenerateCube(0.4f, Random.Range(0.1f, 0.3f), Random.Range(0.6f, 1), 1);
        PlaceMesh(x, y + beakPos.y, z + beakPos.z, beakColor, beak, "Beak");
        // COMB
        PlaceComb(x, y + eyePos.y + 0.8f, z + eyePos.z, Random.Range(0,4));
        // WINGS
        Mesh wing = GenerateCube(0.5f, 0.8f, 1.5f, depth);
        PlaceMesh(x + 1.5f, y, z + 1, bodyColor, wing, "Left Wing");
        PlaceMesh(x - 1.5f, y, z + 1, bodyColor, wing, "Right Wing");
        // LEGS
        PlaceLegs(x, y, z, beakColor);
        // TAIL
        PlaceTail(x, y, z, bodyColor, Random.Range(0, 6));
        // EGG
        PlaceEgg(x, 0, z + 3, bodyColors[Random.Range(1, bodyColors.Length)], beakColors[Random.Range(0, beakColors.Length)], bodyColors[1], bodyColors[0], Random.Range(0, 3));
    }

    void PlaceMesh(float x, float y, float z, Color c, Mesh m, string name) {
        GameObject s = new GameObject(name);
        s.AddComponent<MeshFilter>();
        s.AddComponent<MeshRenderer>();
        s.gameObject.tag = "Player";
        s.transform.parent = currChicken.transform;
        s.transform.position = new Vector3(x, y, z);
        s.transform.localScale = new Vector3(1, 1, 1);
        s.GetComponent<MeshFilter>().mesh = m;
        Renderer rend = s.GetComponent<Renderer>();
        rend.material.color = c;

        if (name == "Left Wing") {
            s.transform.Rotate(new Vector3(0, 20, 0));
        } else if (name == "Right Wing") {
            s.transform.Rotate(new Vector3(0, -20, 0));
        }
    }

    void PlaceEyes(float x, float y, float z, float eyeWidth, Color c) {
        GameObject eye = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        eye.name = "Eyes";
        eye.gameObject.tag = "Player";
        eye.transform.parent = currChicken.transform;
        eye.transform.position = new Vector3(x, y, z);
        eye.transform.localScale = new Vector3(0.3f, eyeWidth, 0.3f);
        eye.transform.Rotate(0, 0, 90);
        eye.GetComponent<Renderer>().material.color = c;
    }

    void PlaceComb(float x, float y, float z, int combType) {
        // Six kinds of combs:
        // Rose, rose with wattles
        // Single, single with wattles
        // Walnut, walnut with wattles
        Color c = new Color(1, 0.1f, 0.08f, 1);

        if (combType == 0) {
            // ROSE COMB
            GameObject comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb";
            comb.gameObject.tag = "Player";
            comb.transform.parent = currChicken.transform;
            comb.transform.position = new Vector3(x, y, z);
            comb.transform.localScale = new Vector3(0.3f, 0.9f, 0.9f);
            comb.GetComponent<Renderer>().material.color = c;
        } else  if (combType == 1) {
            // SINGLE COMB
            GameObject combMaster = new GameObject("Comb");
            combMaster.transform.parent = currChicken.transform;

            int combDirection;
            if (Random.Range(0,2) == 0) {
                combDirection = -10;
            } else {
                combDirection = 10;
            }

            GameObject comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb Part";
            comb.gameObject.tag = "Player";
            comb.transform.parent = combMaster.transform;
            comb.transform.position = new Vector3(x, y - 0.2f, z - 0.5f);
            comb.transform.localScale = new Vector3(0.2f, 0.8f, 0.6f);
            comb.GetComponent<Renderer>().material.color = c;
            comb.transform.Rotate(new Vector3(-30, 0, combDirection));

            comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb Part";
            comb.gameObject.tag = "Player";
            comb.transform.parent = combMaster.transform;
            comb.transform.position = new Vector3(x, y, z - 0.3f);
            comb.transform.localScale = new Vector3(0.2f, 0.8f, 0.6f);
            comb.GetComponent<Renderer>().material.color = c;
            comb.transform.Rotate(new Vector3(-15, 0, 0));

            comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb Part";
            comb.gameObject.tag = "Player";
            comb.transform.parent = combMaster.transform;
            comb.transform.position = new Vector3(x, y + 0.3f, z);
            comb.transform.localScale = new Vector3(0.2f, 0.8f, 0.6f);
            comb.GetComponent<Renderer>().material.color = c;
            comb.transform.Rotate(new Vector3(0, 0, -1 * combDirection));

            comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb Part";
            comb.gameObject.tag = "Player";
            comb.transform.parent = combMaster.transform;
            comb.transform.position = new Vector3(x, y + 0.1f, z + 0.4f);
            comb.transform.localScale = new Vector3(0.2f, 0.8f, 0.6f);
            comb.GetComponent<Renderer>().material.color = c;
            comb.transform.Rotate(new Vector3(30, 0, 0));
        } else {
            // WALNUT COMB (kind of)
            GameObject comb = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            comb.name = "Comb";
            comb.gameObject.tag = "Player";
            comb.transform.parent = currChicken.transform;
            comb.transform.position = new Vector3(x, y - 0.5f, z - 0.5f);
            comb.transform.localScale = new Vector3(0.3f, 0.7f, 0.9f);
            comb.GetComponent<Renderer>().material.color = c;
        }
        if (Random.Range(0,3) == 0) {
            // WATTLE
            GameObject wattleMaster = new GameObject("Wattle");
            wattleMaster.transform.parent = currChicken.transform;
            GameObject wattle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            wattle.name = "Wattle Part";
            wattle.gameObject.tag = "Player";
            wattle.transform.parent = wattleMaster.transform;
            wattle.transform.position = new Vector3(x+0.08f, y - 1.1f, z - 0.8f);
            wattle.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
            wattle.GetComponent<Renderer>().material.color = c;
            wattle.transform.Rotate(0, 0, 17);

            wattle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            wattle.name = "Wattle Part";
            wattle.gameObject.tag = "Player";
            wattle.transform.parent = wattleMaster.transform;
            wattle.transform.position = new Vector3(x - 0.08f, y - 1.1f, z - 0.8f);
            wattle.transform.localScale = new Vector3(0.2f, 0.4f, 0.3f);
            wattle.GetComponent<Renderer>().material.color = c;
            wattle.transform.Rotate(0, 0, -17);
        } 
    }

    void PlaceLegs(float x, float y, float z, Color c) {
        float legDistance = Random.Range(0.4f, 0.8f); // distance between two legs
        float legThickness = Random.Range(0.15f, 0.3f);
        float legZ = Random.Range(0, 0.5f); // how far back leg is along z-axis
        int numberToes = Random.Range(2, 4);
        float toeLength = Random.Range(0.7f, 1);
        int numberThumbs = Random.Range(0, 2);
        // Right Leg
        GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        leg.name = "Right Leg";
        leg.gameObject.tag = "Player";
        leg.transform.parent = currChicken.transform;
        leg.transform.position = new Vector3(x - legDistance, y - legHeight/1.2f, z + legZ);
        leg.transform.localScale = new Vector3(legThickness, 1.2f, legThickness);
        leg.GetComponent<Renderer>().material.color = c;
        // Right Foot
        PlaceFoot(x - legDistance, 0, z + legZ - toeLength/2 + legThickness/2, c, numberToes, toeLength, "Right", numberThumbs, leg);
        // Left Leg
        leg = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        leg.name = "Left Leg";
        leg.gameObject.tag = "Player";
        leg.transform.parent = currChicken.transform;
        leg.transform.position = new Vector3(x + legDistance, y - legHeight / 1.2f, z + legZ);
        leg.transform.localScale = new Vector3(legThickness, 1.2f, legThickness);
        leg.GetComponent<Renderer>().material.color = c;
        // Left Foot
        PlaceFoot(x + legDistance, 0, z + legZ - toeLength/2 + legThickness/2, c, numberToes, toeLength, "Left", numberThumbs, leg);
    }

    void PlaceFoot(float x, float y, float z, Color c, int numberToes, float toeLength, string footName, int numberThumbs, GameObject leg) {
        // Four kinds of feet: 
        // 2 toes + back "thumb"
        // just 2 toes
        // 3 toes + back "thumb"
        // just 3 toes

        float toeThickness = 0.12f;
        GameObject foot = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (numberToes == 2) {
            foot.name = footName + " Foot Right Toe";
            foot.gameObject.tag = "Player";
            foot.transform.position = new Vector3(x - 0.1f, y + toeThickness/2, z);
            foot.transform.localScale = new Vector3(toeThickness, toeThickness, toeLength);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.Rotate(new Vector3(0, 20, 0));
            foot.transform.parent = leg.transform;

            foot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            foot.name = footName + " Foot Left Toe";
            foot.gameObject.tag = "Player";
            foot.transform.position = new Vector3(x + 0.1f, y + toeThickness / 2, z);
            foot.transform.localScale = new Vector3(toeThickness, toeThickness, toeLength);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.Rotate(new Vector3(0, -20, 0));
            foot.transform.parent = leg.transform;
        } else {
            foot.name = footName + " Foot Middle Toe";
            foot.gameObject.tag = "Player";
            foot.transform.position = new Vector3(x, y + toeThickness / 2, z);
            foot.transform.localScale = new Vector3(toeThickness, toeThickness, toeLength);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.parent = leg.transform;

            foot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            foot.name = footName + " Foot Right Toe";
            foot.gameObject.tag = "Player";
            foot.transform.position = new Vector3(x - 0.2f, y + toeThickness / 2, z);
            foot.transform.localScale = new Vector3(toeThickness, toeThickness, toeLength);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.Rotate(new Vector3(0, 30, 0));
            foot.transform.parent = leg.transform;

            foot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            foot.name = footName + " Foot Left Toe";
            foot.gameObject.tag = "Player"; 
            foot.transform.position = new Vector3(x + 0.2f, y + toeThickness / 2, z);
            foot.transform.localScale = new Vector3(toeThickness, toeThickness, toeLength);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.Rotate(new Vector3(0, -30, 0));
            foot.transform.parent = leg.transform;
        }
        if (numberThumbs > 0) {
            foot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            foot.name = footName + " Foot Thumb";
            foot.gameObject.tag = "Player";
            foot.transform.position = new Vector3(x, y + toeThickness / 2 + 0.02f, z + 0.5f);
            foot.transform.localScale = new Vector3(toeThickness*0.8f, toeThickness * 0.8f, toeLength/3);
            foot.GetComponent<Renderer>().material.color = c;
            foot.transform.parent = leg.transform;
            foot.transform.Rotate(new Vector3(3, 0, 0));
        }
    }

    void PlaceTail(float x, float y, float z, Color c, int tailType) {
        // Six variations of tails
        GameObject tailMaster = new GameObject("Tail");
        tailMaster.transform.parent = currChicken.transform;

        if (tailType < 3) {
            // SIMPLE THIN TAIL
            GameObject tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tail.name = "Tail Feather";
            tail.gameObject.tag = "Player";
            tail.transform.parent = tailMaster.transform;
            tail.transform.position = new Vector3(x, y + 0.8f, z + 1.8f);
            tail.transform.localScale = new Vector3(0.7f, 3, 1.3f);
            tail.GetComponent<Renderer>().material.color = c;
            tail.transform.Rotate(12, 0, 0);

            tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            tail.name = "Tail Feather";
            tail.gameObject.tag = "Player";
            tail.transform.parent = tailMaster.transform;
            tail.transform.position = new Vector3(x, y + 0.8f, z + 2.2f);
            tail.transform.localScale = new Vector3(0.7f, 2.5f, 1.3f);
            tail.GetComponent<Renderer>().material.color = c;
            tail.transform.Rotate(8, 0, 0);

            if (tailType < 2) {
                // FLARING THREE PART TAIL
                tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x - 0.5f, y + 0.8f, z + 1.8f);
                tail.transform.localScale = new Vector3(0.7f, 2.5f, 1.3f);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(12, 0, 30);

                tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x - 0.5f, y + 0.8f, z + 2.2f);
                tail.transform.localScale = new Vector3(0.7f, 2, 1.3f);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(8, 0, 30);

                tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x + 0.5f, y + 0.8f, z + 1.8f);
                tail.transform.localScale = new Vector3(0.7f, 2.5f, 1.3f);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(12, 0, -30);

                tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x + 0.5f, y + 0.8f, z + 2.2f);
                tail.transform.localScale = new Vector3(0.7f, 2, 1.3f);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(8, 0, -30);

                if (tailType < 1) {
                    // FLARING FIVE PART TAIL
                    tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    tail.name = "Tail Feather";
                    tail.gameObject.tag = "Player";
                    tail.transform.parent = tailMaster.transform;
                    tail.transform.position = new Vector3(x - 0.5f, y + 1, z + 2.1f);
                    tail.transform.localScale = new Vector3(0.7f, 3.5f, 1.3f);
                    tail.GetComponent<Renderer>().material.color = c;
                    tail.transform.Rotate(15, 0, 15);

                    tail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    tail.name = "Tail Feather";
                    tail.gameObject.tag = "Player";
                    tail.transform.parent = tailMaster.transform;
                    tail.transform.position = new Vector3(x + 0.5f, y + 1, z + 2.1f);
                    tail.transform.localScale = new Vector3(0.7f, 3.5f, 1.3f);
                    tail.GetComponent<Renderer>().material.color = c;
                    tail.transform.Rotate(15, 0, -15);
                }
            }
        } else {
            // SINGLE THIN TAIL
            GameObject tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tail.name = "Tail Feather";
            tail.gameObject.tag = "Player";
            tail.transform.parent = tailMaster.transform;
            tail.transform.position = new Vector3(x, y + 2, z + 2);
            tail.transform.localScale = new Vector3(1, 0.1f, 3);
            tail.transform.Rotate(90, 0, 90);
            tail.GetComponent<Renderer>().material.color = c;
            tail.transform.Rotate(0, -20, 0);

            if (tailType == 4) {
                // THREE PART FLARE
                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x - 0.3f, y + 1.6f, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(10, -20, 0);

                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x + 0.3f, y + 1.6f, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(-10, -20, 0);
            } else {
                // FIVE PART FLARE
                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x - 0.4f, y + 1.6f, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(10, -20, 0);

                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x + 0.4f, y + 1.6f, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(-10, -20, 0);

                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x + 0.8f, y + 1, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(-25, -20, 0);

                tail = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                tail.name = "Tail Feather";
                tail.gameObject.tag = "Player";
                tail.transform.parent = tailMaster.transform;
                tail.transform.position = new Vector3(x - 0.8f, y + 1, z + 2);
                tail.transform.localScale = new Vector3(1, 0.1f, 3);
                tail.transform.Rotate(90, 0, 90);
                tail.GetComponent<Renderer>().material.color = c;
                tail.transform.Rotate(25, -20, 0);
            }
        }
    }

    void PlaceEgg(float x, float y, float z, Color shellColor, Color yolkColor, Color white, Color black, int eggType) {
        // Three variations of egg:
        // Whole
        // Fried
        // Chick!

        if (eggType == 0) {
            GameObject egg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            egg.name = "Egg";
            egg.gameObject.tag = "Player";
            egg.transform.position = new Vector3(x, y + 0.7f, z);
            egg.transform.localScale = new Vector3(1, 1.3f, 1);
            egg.GetComponent<Renderer>().material.color = shellColor;
            egg.transform.parent = currChicken.transform;
        } else if (eggType == 1) {
            GameObject eggMaster = new GameObject("Egg");
            eggMaster.transform.parent = currChicken.transform;
            GameObject egg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            egg.name = "Egg White";
            egg.gameObject.tag = "Player";
            egg.transform.position = new Vector3(x, y + 0.2f, z);
            egg.transform.localScale = new Vector3(Random.Range(2, 2.1f), 0.4f, Random.Range(2, 2.1f));
            egg.GetComponent<Renderer>().material.color = white;
            egg.transform.parent = eggMaster.transform;

            egg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            egg.name = "Egg Yolk";
            egg.gameObject.tag = "Player";
            egg.transform.position = new Vector3(x, y + 0.4f, z);
            egg.transform.localScale = new Vector3(0.8f, 0.3f, 0.85f);
            egg.GetComponent<Renderer>().material.color = yolkColor;
            egg.transform.parent = currChicken.transform;
        } else {
            GameObject chickMaster = new GameObject("Chick!");
            chickMaster.transform.parent = currChicken.transform;

            GameObject chick = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            chick.name = "Head";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x, y + 1f, z - 0.1f);
            chick.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            chick.GetComponent<Renderer>().material.color = shellColor;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            chick.name = "Left Eye";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x + 0.18f, y + 1.1f, z - 0.33f);
            chick.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            chick.GetComponent<Renderer>().material.color = black;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            chick.name = "Right Eye";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x - 0.18f, y + 1.1f, z - 0.33f);
            chick.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            chick.GetComponent<Renderer>().material.color = black;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Cube);
            chick.name = "Beak";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x, y + 1.1f, z - 0.45f);
            chick.transform.localScale = new Vector3(0.1f, 0.05f, 0.1f);
            chick.GetComponent<Renderer>().material.color = yolkColor;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            chick.name = "Body";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x, y + 0.7f, z);
            chick.transform.localScale = new Vector3(0.9f, 0.9f, 1);
            chick.GetComponent<Renderer>().material.color = shellColor;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Cube);
            chick.name = "Right Leg";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x - 0.15f, y + 0.2f, z);
            chick.transform.localScale = new Vector3(0.05f, 0.4f, 0.05f);
            chick.GetComponent<Renderer>().material.color = yolkColor;
            chick.transform.parent = chickMaster.transform;

            chick = GameObject.CreatePrimitive(PrimitiveType.Cube);
            chick.name = "Left Leg";
            chick.gameObject.tag = "Player";
            chick.transform.position = new Vector3(x + 0.15f, y + 0.2f, z);
            chick.transform.localScale = new Vector3(0.05f, 0.4f, 0.05f);
            chick.GetComponent<Renderer>().material.color = yolkColor;
            chick.transform.parent = chickMaster.transform;
        }
    }

    Mesh GenerateBody(int depth) {
        Vector3[] v = new Vector3[20];
        Dictionary<Face, Vector3> fDict = new Dictionary<Face, Vector3>(); // Face dict key = Face, value = face point
        Dictionary<Vector3[], Vector3> eDict = new Dictionary<Vector3[], Vector3>(new EdgeComparer()); // Edge dict key = Edge, value = edge point
        Dictionary<Vector3, Vector3> vDict = new Dictionary<Vector3, Vector3>(); // Vertex dict key = Vertex, value = barycenter

        float topTailHeight = Random.Range(1f, 3.5f);
        float bottomTailHeight = Random.Range(0, topTailHeight + 0.1f);
        float topWidth = Random.Range(0, 2f);
        float bottomWidth = Random.Range(0.9f, 2f);
        float neckHeight = Random.Range(1, 4f);
        // use the eyePos's x value to save how "wide" the eyes need to be
        // the width of top (and a bit of the bottom) influences how "wide"
        // taller neck means less influence from width
        eyePos = new Vector3(Mathf.Pow((topWidth + bottomWidth) / (neckHeight), 0.1f), neckHeight, -1.1f);
        beakPos = new Vector3(0, neckHeight - Random.Range(0, 0.2f), -1.5f);
        legHeight = Random.Range(0.5f, 2.4f);

        v[0] = new Vector3(-1, 1 + neckHeight, 0);
        v[1] = new Vector3(1, 1 + neckHeight, 0);
        v[2] = new Vector3(1, 1 + neckHeight, -2);
        v[3] = new Vector3(-1, 1 + neckHeight, -2);
        v[4] = new Vector3(-1, 1, 0);
        v[5] = new Vector3(1, 1, 0);
        v[6] = new Vector3(1, 1, -2);
        v[7] = new Vector3(-1, 1, -2);
        v[8] = new Vector3(-1 - bottomWidth, -1, 0);
        v[9] = new Vector3(1 + bottomWidth, -1, 0);
        v[10] = new Vector3(1, -1, -2);
        v[11] = new Vector3(-1, -1, -2);
        v[12] = new Vector3(-1 - topWidth, 1, 2);
        v[13] = new Vector3(-1, 0 + topTailHeight, 3);
        v[14] = new Vector3(1, 0 + topTailHeight, 3);
        v[15] = new Vector3(1 + topWidth, 1, 2);
        v[16] = new Vector3(-1 - bottomWidth, -1, 2);
        v[17] = new Vector3(-1, -1 + bottomTailHeight, 3);
        v[18] = new Vector3(1, -1 + bottomTailHeight, 3);
        v[19] = new Vector3(1 + bottomWidth, -1, 2);

        // Save faces and their face points
        Face f0 = new Face(new Vector3[4] { v[3], v[0], v[1], v[2] });
        Face f1 = new Face(new Vector3[4] { v[4], v[12], v[15], v[5] });
        Face f2 = new Face(new Vector3[4] { v[12], v[13], v[14], v[15] });
        Face f3 = new Face(new Vector3[4] { v[10], v[9], v[8], v[11] });
        Face f4 = new Face(new Vector3[4] { v[9], v[19], v[16], v[8] });
        Face f5 = new Face(new Vector3[4] { v[19], v[18], v[17], v[16] });
        Face f6 = new Face(new Vector3[4] { v[3], v[2], v[6], v[7] });
        Face f7 = new Face(new Vector3[4] { v[7], v[6], v[10], v[11] });
        Face f8 = new Face(new Vector3[4] { v[1], v[0], v[4], v[5] });
        Face f9 = new Face(new Vector3[4] { v[14], v[13], v[17], v[18] });
        Face f10 = new Face(new Vector3[4] { v[2], v[1], v[5], v[6] });
        Face f11 = new Face(new Vector3[4] { v[6], v[5], v[9], v[10] });
        Face f12 = new Face(new Vector3[4] { v[5], v[15], v[19], v[9] });
        Face f13 = new Face(new Vector3[4] { v[15], v[14], v[18], v[19] });
        Face f14 = new Face(new Vector3[4] { v[0], v[3], v[7], v[4] });
        Face f15 = new Face(new Vector3[4] { v[4], v[7], v[11], v[8] });
        Face f16 = new Face(new Vector3[4] { v[12], v[4], v[8], v[16] });
        Face f17 = new Face(new Vector3[4] { v[13], v[12], v[16], v[17] });
        fDict.Add(f0, f0.facePoint);
        fDict.Add(f1, f1.facePoint);
        fDict.Add(f2, f2.facePoint);
        fDict.Add(f3, f3.facePoint);
        fDict.Add(f4, f4.facePoint);
        fDict.Add(f5, f5.facePoint);
        fDict.Add(f6, f6.facePoint);
        fDict.Add(f7, f7.facePoint);
        fDict.Add(f8, f8.facePoint);
        fDict.Add(f9, f9.facePoint);
        fDict.Add(f10, f10.facePoint);
        fDict.Add(f11, f11.facePoint);
        fDict.Add(f12, f12.facePoint);
        fDict.Add(f13, f13.facePoint);
        fDict.Add(f14, f14.facePoint);
        fDict.Add(f15, f15.facePoint);
        fDict.Add(f16, f16.facePoint);
        fDict.Add(f17, f17.facePoint);

        // Save edges and their edge points
        Edge e0 = new Edge(v[3], v[2], f0, f6);
        Edge e1 = new Edge(v[7], v[6], f6, f7);
        Edge e2 = new Edge(v[11], v[10], f7, f3);
        Edge e3 = new Edge(v[0], v[1], f0, f8);
        Edge e4 = new Edge(v[4], v[5], f8, f1);
        Edge e5 = new Edge(v[8], v[9], f3, f4);
        Edge e6 = new Edge(v[12], v[15], f1, f2);
        Edge e7 = new Edge(v[16], v[19], f4, f5);
        Edge e8 = new Edge(v[13], v[14], f2, f9);
        Edge e9 = new Edge(v[17], v[18], f5, f9);
        Edge e10 = new Edge(v[2], v[1], f0, f10);
        Edge e11 = new Edge(v[6], v[5], f10, f11);
        Edge e12 = new Edge(v[10], v[9], f11, f3);
        Edge e13 = new Edge(v[3], v[0], f0, f14);
        Edge e14 = new Edge(v[7], v[4], f14, f15);
        Edge e15 = new Edge(v[11], v[8], f15, f3);
        Edge e16 = new Edge(v[5], v[15], f12, f1);
        Edge e17 = new Edge(v[9], v[19], f12, f4);
        Edge e18 = new Edge(v[4], v[12], f1, f16);
        Edge e19 = new Edge(v[8], v[16], f4, f16);
        Edge e20 = new Edge(v[15], v[14], f2, f13);
        Edge e21 = new Edge(v[19], v[18], f13, f5);
        Edge e22 = new Edge(v[12], v[13], f2, f17);
        Edge e23 = new Edge(v[16], v[17], f17, f5);
        Edge e24 = new Edge(v[2], v[6], f6, f10);
        Edge e25 = new Edge(v[6], v[10], f7, f11);
        Edge e26 = new Edge(v[3], v[7], f6, f14);
        Edge e27 = new Edge(v[7], v[11], f7, f15);
        Edge e28 = new Edge(v[1], v[5], f10, f8);
        Edge e29 = new Edge(v[5], v[9], f11, f12);
        Edge e30 = new Edge(v[0], v[4], f14, f8);
        Edge e31 = new Edge(v[4], v[8], f15, f16);
        Edge e32 = new Edge(v[15], v[19], f12, f13);
        Edge e33 = new Edge(v[12], v[16], f16, f17);
        Edge e34 = new Edge(v[14], v[18], f13, f9);
        Edge e35 = new Edge(v[13], v[17], f9, f17);
        eDict.Add(new Vector3[2] { e0.vert1, e0.vert2 }, e0.edgePoint);
        eDict.Add(new Vector3[2] { e1.vert1, e1.vert2 }, e1.edgePoint);
        eDict.Add(new Vector3[2] { e2.vert1, e2.vert2 }, e2.edgePoint);
        eDict.Add(new Vector3[2] { e3.vert1, e3.vert2 }, e3.edgePoint);
        eDict.Add(new Vector3[2] { e4.vert1, e4.vert2 }, e4.edgePoint);
        eDict.Add(new Vector3[2] { e5.vert1, e5.vert2 }, e5.edgePoint);
        eDict.Add(new Vector3[2] { e6.vert1, e6.vert2 }, e6.edgePoint);
        eDict.Add(new Vector3[2] { e7.vert1, e7.vert2 }, e7.edgePoint);
        eDict.Add(new Vector3[2] { e8.vert1, e8.vert2 }, e8.edgePoint);
        eDict.Add(new Vector3[2] { e9.vert1, e9.vert2 }, e9.edgePoint);
        eDict.Add(new Vector3[2] { e10.vert1, e10.vert2 }, e10.edgePoint);
        eDict.Add(new Vector3[2] { e11.vert1, e11.vert2 }, e11.edgePoint);
        eDict.Add(new Vector3[2] { e12.vert1, e12.vert2 }, e12.edgePoint);
        eDict.Add(new Vector3[2] { e13.vert1, e13.vert2 }, e13.edgePoint);
        eDict.Add(new Vector3[2] { e14.vert1, e14.vert2 }, e14.edgePoint);
        eDict.Add(new Vector3[2] { e15.vert1, e15.vert2 }, e15.edgePoint);
        eDict.Add(new Vector3[2] { e16.vert1, e16.vert2 }, e16.edgePoint);
        eDict.Add(new Vector3[2] { e17.vert1, e17.vert2 }, e17.edgePoint);
        eDict.Add(new Vector3[2] { e18.vert1, e18.vert2 }, e18.edgePoint);
        eDict.Add(new Vector3[2] { e19.vert1, e19.vert2 }, e19.edgePoint);
        eDict.Add(new Vector3[2] { e20.vert1, e20.vert2 }, e20.edgePoint);
        eDict.Add(new Vector3[2] { e21.vert1, e21.vert2 }, e21.edgePoint);
        eDict.Add(new Vector3[2] { e22.vert1, e22.vert2 }, e22.edgePoint);
        eDict.Add(new Vector3[2] { e23.vert1, e23.vert2 }, e23.edgePoint);
        eDict.Add(new Vector3[2] { e24.vert1, e24.vert2 }, e24.edgePoint);
        eDict.Add(new Vector3[2] { e25.vert1, e25.vert2 }, e25.edgePoint);
        eDict.Add(new Vector3[2] { e26.vert1, e26.vert2 }, e26.edgePoint);
        eDict.Add(new Vector3[2] { e27.vert1, e27.vert2 }, e27.edgePoint);
        eDict.Add(new Vector3[2] { e28.vert1, e28.vert2 }, e28.edgePoint);
        eDict.Add(new Vector3[2] { e29.vert1, e29.vert2 }, e29.edgePoint);
        eDict.Add(new Vector3[2] { e30.vert1, e30.vert2 }, e30.edgePoint);
        eDict.Add(new Vector3[2] { e31.vert1, e31.vert2 }, e31.edgePoint);
        eDict.Add(new Vector3[2] { e32.vert1, e32.vert2 }, e32.edgePoint);
        eDict.Add(new Vector3[2] { e33.vert1, e33.vert2 }, e33.edgePoint);
        eDict.Add(new Vector3[2] { e34.vert1, e34.vert2 }, e34.edgePoint);
        eDict.Add(new Vector3[2] { e35.vert1, e35.vert2 }, e35.edgePoint);

        // input adj faces and adj edges for each vertex
        Vertex v0 = new Vertex(v[0], new HashSet<Face> { f0, f8, f14 }, new HashSet<Edge> { e3, e13, e30 });
        Vertex v1 = new Vertex(v[1], new HashSet<Face> { f0, f8, f10 }, new HashSet<Edge> { e3, e10, e28 });
        Vertex v2 = new Vertex(v[2], new HashSet<Face> { f0, f6, f10 }, new HashSet<Edge> { e0, e10, e24 });
        Vertex v3 = new Vertex(v[3], new HashSet<Face> { f0, f6, f14 }, new HashSet<Edge> { e0, e13, e26 });
        Vertex v4 = new Vertex(v[4], new HashSet<Face> { f1, f8, f14, f15, f16 }, new HashSet<Edge> { e4, e14, e18, e30, e31 });
        Vertex v5 = new Vertex(v[5], new HashSet<Face> { f1, f8, f10, f11, f12 }, new HashSet<Edge> { e4, e11, e16, e28, e29 });
        Vertex v6 = new Vertex(v[6], new HashSet<Face> { f6, f7, f10, f11 }, new HashSet<Edge> { e1, e11, e24, e25 });
        Vertex v7 = new Vertex(v[7], new HashSet<Face> { f6, f7, f14, f15 }, new HashSet<Edge> { e1, e14, e26, e27 });
        Vertex v8 = new Vertex(v[8], new HashSet<Face> { f3, f4, f15, f16 }, new HashSet<Edge> { e5, e15, e19, e31 });
        Vertex v9 = new Vertex(v[9], new HashSet<Face> { f3, f4, f11, f12 }, new HashSet<Edge> { e5, e12, e17, e29 });
        Vertex v10 = new Vertex(v[10], new HashSet<Face> { f3, f7, f11 }, new HashSet<Edge> { e2, e12, e25 });
        Vertex v11 = new Vertex(v[11], new HashSet<Face> { f3, f7, f15 }, new HashSet<Edge> { e2, e15, e27 });
        Vertex v12 = new Vertex(v[12], new HashSet<Face> { f1, f2, f16, f17 }, new HashSet<Edge> { e6, e18, e22, e33 });
        Vertex v13 = new Vertex(v[13], new HashSet<Face> { f2, f9, f17 }, new HashSet<Edge> { e8, e22, e35 });
        Vertex v14 = new Vertex(v[14], new HashSet<Face> { f2, f9, f13 }, new HashSet<Edge> { e8, e20, e34 });
        Vertex v15 = new Vertex(v[15], new HashSet<Face> { f1, f2, f12, f13 }, new HashSet<Edge> { e6, e16, e20, e32 });
        Vertex v16 = new Vertex(v[16], new HashSet<Face> { f4, f5, f16, f17 }, new HashSet<Edge> { e7, e19, e23, e33 });
        Vertex v17 = new Vertex(v[17], new HashSet<Face> { f5, f9, f17 }, new HashSet<Edge> { e9, e23, e35 });
        Vertex v18 = new Vertex(v[18], new HashSet<Face> { f5, f9, f13 }, new HashSet<Edge> { e9, e21, e34 });
        Vertex v19 = new Vertex(v[19], new HashSet<Face> { f4, f5, f12, f13 }, new HashSet<Edge> { e7, e17, e21, e32 });
        vDict.Add(v0.coord, v0.barycenter);
        vDict.Add(v1.coord, v1.barycenter);
        vDict.Add(v2.coord, v2.barycenter);
        vDict.Add(v3.coord, v3.barycenter);
        vDict.Add(v4.coord, v4.barycenter);
        vDict.Add(v5.coord, v5.barycenter);
        vDict.Add(v6.coord, v6.barycenter);
        vDict.Add(v7.coord, v7.barycenter);
        vDict.Add(v8.coord, v8.barycenter);
        vDict.Add(v9.coord, v9.barycenter);
        vDict.Add(v10.coord, v10.barycenter);
        vDict.Add(v11.coord, v11.barycenter);
        vDict.Add(v12.coord, v12.barycenter);
        vDict.Add(v13.coord, v13.barycenter);
        vDict.Add(v14.coord, v14.barycenter);
        vDict.Add(v15.coord, v15.barycenter);
        vDict.Add(v16.coord, v16.barycenter);
        vDict.Add(v17.coord, v17.barycenter);
        vDict.Add(v18.coord, v18.barycenter);
        vDict.Add(v19.coord, v19.barycenter);

        return this.GetComponent<MeshSubdivision>().Subdivide(fDict, eDict, vDict, v, depth);
    }

    Mesh GenerateCube(float xScale, float yScale, float zScale, int depth) {
        Vector3[] v = new Vector3[8];
        Dictionary<Face, Vector3> fDict = new Dictionary<Face, Vector3>(); // Face dict key = Face, value = face point
        Dictionary<Vector3[], Vector3> eDict = new Dictionary<Vector3[], Vector3>(new EdgeComparer()); // Edge dict key = Edge, value = edge point
        Dictionary<Vector3, Vector3> vDict = new Dictionary<Vector3, Vector3>(); // Vertex dict key = Vertex, value = barycenter

        v[0] = new Vector3(1 * xScale, 1 * yScale, 1 * zScale);
        v[1] = new Vector3(1 * xScale, -1 * yScale, 1 * zScale);
        v[2] = new Vector3(1 * xScale, -1 * yScale, -1 * zScale);
        v[3] = new Vector3(1 * xScale, 1 * yScale, -1 * zScale);
        v[4] = new Vector3(-1 * xScale, 1 * yScale, -1 * zScale);
        v[5] = new Vector3(-1 * xScale, -1 * yScale, -1 * zScale);
        v[6] = new Vector3(-1 * xScale, -1 * yScale, 1 * zScale);
        v[7] = new Vector3(-1 * xScale, 1 * yScale, 1 * zScale);

        // Save faces and their face points
        Face f0 = new Face(new Vector3[4] { v[0], v[1], v[2], v[3] });
        Face f1 = new Face(new Vector3[4] { v[4], v[3], v[2], v[5] });
        Face f2 = new Face(new Vector3[4] { v[7], v[4], v[5], v[6] });
        Face f3 = new Face(new Vector3[4] { v[0], v[7], v[6], v[1] });
        Face f4 = new Face(new Vector3[4] { v[7], v[0], v[3], v[4] });
        Face f5 = new Face(new Vector3[4] { v[5], v[2], v[1], v[6] });
        fDict.Add(f0, f0.facePoint);
        fDict.Add(f1, f1.facePoint);
        fDict.Add(f2, f2.facePoint);
        fDict.Add(f3, f3.facePoint);
        fDict.Add(f4, f4.facePoint);
        fDict.Add(f5, f5.facePoint);

        // Save edges and their edge points
        Edge e0 = new Edge(v[0], v[1], f0, f3);
        Edge e1 = new Edge(v[1], v[2], f0, f5);
        Edge e2 = new Edge(v[2], v[3], f0, f1);
        Edge e3 = new Edge(v[0], v[3], f0, f4);
        Edge e4 = new Edge(v[6], v[7], f2, f3);
        Edge e5 = new Edge(v[5], v[6], f2, f5);
        Edge e6 = new Edge(v[4], v[5], f1, f2);
        Edge e7 = new Edge(v[4], v[7], f2, f4);
        Edge e8 = new Edge(v[3], v[4], f1, f4);
        Edge e9 = new Edge(v[0], v[7], f3, f4);
        Edge e10 = new Edge(v[2], v[5], f1, f5);
        Edge e11 = new Edge(v[1], v[6], f3, f5);
        eDict.Add(new Vector3[2] { v[0], v[1] }, e0.edgePoint);
        eDict.Add(new Vector3[2] { v[1], v[2] }, e1.edgePoint);
        eDict.Add(new Vector3[2] { v[2], v[3] }, e2.edgePoint);
        eDict.Add(new Vector3[2] { v[0], v[3] }, e3.edgePoint);
        eDict.Add(new Vector3[2] { v[6], v[7] }, e4.edgePoint);
        eDict.Add(new Vector3[2] { v[5], v[6] }, e5.edgePoint);
        eDict.Add(new Vector3[2] { v[4], v[5] }, e6.edgePoint);
        eDict.Add(new Vector3[2] { v[4], v[7] }, e7.edgePoint);
        eDict.Add(new Vector3[2] { v[3], v[4] }, e8.edgePoint);
        eDict.Add(new Vector3[2] { v[0], v[7] }, e9.edgePoint);
        eDict.Add(new Vector3[2] { v[2], v[5] }, e10.edgePoint);
        eDict.Add(new Vector3[2] { v[1], v[6] }, e11.edgePoint);

        // input adj faces and adj edges for each vertex
        Vertex v0 = new Vertex(v[0], new HashSet<Face> { f0, f3, f4 }, new HashSet<Edge> { e0, e3, e9 });
        Vertex v1 = new Vertex(v[1], new HashSet<Face> { f0, f3, f5 }, new HashSet<Edge> { e0, e1, e11 });
        Vertex v2 = new Vertex(v[2], new HashSet<Face> { f0, f1, f5 }, new HashSet<Edge> { e1, e2, e10 });
        Vertex v3 = new Vertex(v[3], new HashSet<Face> { f0, f1, f4 }, new HashSet<Edge> { e2, e3, e8 });
        Vertex v4 = new Vertex(v[4], new HashSet<Face> { f1, f2, f4 }, new HashSet<Edge> { e6, e7, e8 });
        Vertex v5 = new Vertex(v[5], new HashSet<Face> { f1, f2, f5 }, new HashSet<Edge> { e5, e6, e10 });
        Vertex v6 = new Vertex(v[6], new HashSet<Face> { f2, f3, f5 }, new HashSet<Edge> { e4, e5, e11 });
        Vertex v7 = new Vertex(v[7], new HashSet<Face> { f2, f3, f4 }, new HashSet<Edge> { e4, e7, e9 });
        vDict.Add(v0.coord, v0.barycenter);
        vDict.Add(v1.coord, v1.barycenter);
        vDict.Add(v2.coord, v2.barycenter);
        vDict.Add(v3.coord, v3.barycenter);
        vDict.Add(v4.coord, v4.barycenter);
        vDict.Add(v5.coord, v5.barycenter);
        vDict.Add(v6.coord, v6.barycenter);
        vDict.Add(v7.coord, v7.barycenter);

        return this.GetComponent<MeshSubdivision>().Subdivide(fDict, eDict, vDict, v, depth);
    }
}