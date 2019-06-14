using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;

public class Main : MonoBehaviour
{
    public static int Size { get; set; } = 4;
    private Block[,,,] Box { get; set; }

    public GameObject blockPrefab;
    public GameObject holder;
    public static int Score { get; private set; } = 0;

    enum Split { horizontal, vertical, deep, anakata };
    enum Direction { left, right };

    void Start()
    {
        Box = new Block[Size, Size, Size, Size];
    }


    void Update()
    {
        Box[0, 0, 0, 0] = new Block(blockPrefab, new FloatPoint4D(0, 0, 0, 0));
    }
}

public struct FloatPoint4D
{
    float X { get; set; }
    float Y { get; set; }
    float Z { get; set; }
    float W { get; set; }
    FloatPoint4D(float x = 0, float y = 0, float z = 0, float z = 0)
    {
        X = x;
        Y = y;
        Z = Z;
        W = W;
    }
}
