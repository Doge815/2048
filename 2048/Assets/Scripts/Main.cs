using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;
using System.Linq;

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
        Box[0][0] = new Block(block, new Point(0, 0));
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
        for(int i = 0; i < Size; i++)
        {
            List<Block> TempList = Temp[i].ToList();
            Temp[i] = new Block[Size];
            for (int u = 0; u < TempList.Count; u++)
            {
                Temp[i][Size - u - 1] = TempList[u];
                Temp[i][(direction == (int)Direction.right) ? (Size - u - 1) : (u)] = TempList[u];
            }
        }
        for(int i = 0; i < Size; i++)
        {
            Box[i] = new Block[Size];
        }
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (split == (int)Split.horizontal) Box[i][u] = Temp[i][u];
                else Box[i][u] = Temp[u][i];
            }
        }
        for(int i = 0; i < Size; i++)
        {
            for(int u = 0; u < Size; u++)
            {
                if (Box[i][u] != null) Box[i][u].P = new Point(u, i);
            }
        }
    }
}
