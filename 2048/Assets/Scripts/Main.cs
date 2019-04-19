using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static int Size { get; set; } = 3;
    private Block[][] Box { get; set; }
    public GameObject block;
    private GameObject holder;
    public GameObject Holder { get => holder; }
    Block b;
    
    void Start()
    {
        holder = GameObject.Find("Holder");
        //b = new Block(block, new System.Drawing.Point(0, 0));
        Box = new Block[Size][];
        for (int i = 0; i < Size; i++) Box[i] = new Block[Size];
    }

    enum Split { horizontal, vertical };
    enum Direction { left, right };
    void Update()
    {       
        int? split = null;
        int? direction = null;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            split = (int)Split.horizontal;
            direction = (int)Direction.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            split = (int)Split.horizontal;
            direction = (int)Direction.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            split = (int)Split.horizontal;
            direction = (int)Direction.right;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            split = (int)Split.horizontal;
            direction = (int)Direction.left;
        }
        if (split == null) return;
        Block[][] Temp = new Block[Size][];
        for (int i = 0; i < Size; i++) Temp[i] = new Block[Size];
        for(int i = 0; i < Size; i++)
        {
            for(int u = 0; u < Size; u++)
            {
                if (split == (int)Split.horizontal) Temp[i][u] = Box[i][u];
                else Temp[i][u] = Box[u][i];
            }
        }
    }
}
