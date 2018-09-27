using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprints : MonoBehaviour {

    /*
    * Footprint 1:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    *
    * Footprint 2:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 0 1 1 1
    *
    * Footprint 3:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 0
    *
    * Footprint 4:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 0 0
    *
    * Footprint 5:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 0 1 1 0
    *
    * Footprint 6:
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 1 1 1 1
    * 0 0 1 1
    *
    */

    private int[,] footprint1 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1}, {1, 1, 1, 1}, {1, 1, 1, 1} };
    private int[,] footprint2 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 0, 1, 1, 1 } };
    private int[,] footprint3 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 0 } };
    private int[,] footprint4 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 0, 0 } };
    private int[,] footprint5 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 0, 1, 1, 0 } };
    private int[,] footprint6 = new int[5, 4] { { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 1, 1, 1, 1 }, { 0, 0, 1, 1 } };

    public int[,] GetFootprint(int number) {
        int[,] result = new int[5, 4];
        // make copy of footprint array
        for (int i = 0; i < result.GetLength(0); i++) {
            for (int j = 0; j < result.GetLength(1); j++) {
                if (number == 1) {
                    result[i, j] = footprint1[i, j];
                } else if (number == 2) {
                    result[i, j] = footprint2[i, j];
                } else if (number == 3) {
                    result[i, j] = footprint3[i, j];
                } else if (number == 4) {
                    result[i, j] = footprint4[i, j];
                } else if (number == 5) {
                    result[i, j] = footprint5[i, j];
                } else if (number == 6) {
                    result[i, j] = footprint6[i, j];
                }
            }
        }
        // return the copy
        return result;
    }
}
