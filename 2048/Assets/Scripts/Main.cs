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
    public static int Size { get; set; } = 20;
    private Block[][] Box { get; set; }

    public GameObject block;
    public GameObject bg;

    private GameObject holder;
    public GameObject Holder { get => holder; }
    private static float min;
    public static float Min { get => min; private set => min = value; }
    void Start()
    {
        holder = GameObject.Find("Holder");
        Min = new List<float> { holder.GetComponent<RectTransform>().rect.size.x, holder.GetComponent<RectTransform>().rect.size.y }.Min();

        Box = new Block[Size][];
        for (int i = 0; i < Size; i++) Box[i] = new Block[Size];
        #region background
        GameObject g = Instantiate(bg, holder.transform);
        g.GetComponent<RectTransform>().sizeDelta = new Vector2(Min, Min);
        #endregion
        Box[0][0] = new Block(block, new Point(0, 0), 2);
        Box[0][2] = new Block(block, new Point(2, 0), 2);
    }
    void OnGUI()
    {
        GameObject g = GameObject.Find("BackGround(Clone)");
        Texture t = g.GetComponent<Image>().mainTexture;
        float r = Min / (Size * 6 + 1);
        UnityEngine.Color origin = GUI.color;
        GUI.color = new Color32(187, 173, 160, 255);
        for (int i = 0; i < Size + 1; i++)
        {
            Rect a = new Rect(new Vector2(Holder.GetComponent<RectTransform>().rect.size.x / 2 - Min / 2 + i * 6 * r, Holder.GetComponent<RectTransform>().rect.size.y / 2 - Min / 2),new Vector2(r, Min));
            GUI.DrawTexture(a, t);
        }
        for (int i = 0; i < Size + 1; i++)
        {
            Rect a = new Rect(new Vector2(Holder.GetComponent<RectTransform>().rect.size.x / 2 - Min / 2, Holder.GetComponent<RectTransform>().rect.size.y / 2 - Min / 2 + i * 6 * r), new Vector2(Min, r));
            GUI.DrawTexture(a, t);
        }
        GUI.color = origin;
    }

    enum Split { horizontal, vertical };
    enum Direction { left, right };
    void Update()
    {       
        int? split = null;
        int? direction = null;
        #region input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
            split = (int)Split.vertical;
            direction = (int)Direction.right;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            split = (int)Split.vertical;
            direction = (int)Direction.left;
        }
        if (split == null) return;
        move(split.Value, direction.Value);
        #endregion
        
    }
    private void move(int split, int direction)
    {
        #region split array
        Block[][] Temp = new Block[Size][];
        for (int i = 0; i < Size; i++) Temp[i] = new Block[Size];
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (split == (int)Split.horizontal) Temp[i][u] = Box[i][u];
                else Temp[i][u] = Box[u][i];
            }
        }
        #endregion
        #region move blocks
        for (int i = 0; i < Size; i++)
        {
            List<Block> TempList = (from t in Temp[i] where t != null select t).ToList();
            for (int u = 1; u < TempList.Count; u++)
            {
                if (TempList[u].Value == TempList[u - 1].Value)
                {
                    TempList[u].Value *= 2;
                    Destroy(TempList[u - 1].Box);
                    TempList.RemoveAt(u - 1);
                }
            }
            Temp[i] = new Block[Size];
            for (int u = 0; u < TempList.Count; u++)
            {
                Temp[i][(direction == (int)Direction.right) ? (Size - u - 1) : (u)] = TempList[(direction == (int)Direction.right) ? (TempList.Count - u - 1) : (u)];
            }
            //if (direction == (int)Direction.right) Array.Reverse(Temp[i]);
        }
        #endregion
        #region rebuild array
        for (int i = 0; i < Size; i++)
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
        #endregion
        #region set points
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (Box[i][u] != null) Box[i][u].P = new Point(u, i);
            }
        }
        #endregion
        #region place new block
        List<Point> places = new List<Point>();
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (Box[i][u] == null) places.Add(new Point(i, u));
            }
        }
        Point TheChosenOne = places[UnityEngine.Random.Range(0, places.Count)];
        Box[TheChosenOne.X][TheChosenOne.Y] = new Block(block, new Point(TheChosenOne.Y, TheChosenOne.X), 2);
        #endregion
        GC.Collect();
    }


    private void whynot()
    {
        move
    }
}
