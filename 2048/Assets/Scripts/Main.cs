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
    private Block[,] Box { get; set; }

    public GameObject blockPrefab;
    public GameObject backgroundPrefab;
    public GameObject holder;

    private GameObject backgroundInstance;
    private GameObject Scoreboard;

    public static float Min { get; private set; }
    public static int Score { get; private set; } = 0;

    enum Split { horizontal, vertical };
    enum Direction { left, right };
    void Start()
    {
        Min = Mathf.Min(holder.GetComponent<RectTransform>().rect.size.x, holder.GetComponent<RectTransform>().rect.size.y);

        Box = new Block[Size,Size];
        #region background
        backgroundInstance = Instantiate(backgroundPrefab, holder.transform);
        backgroundInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(Min, Min);

        Scoreboard.GetComponent<RectTransform>().sizeDelta = new Vector2((holder.GetComponent<RectTransform>().rect.size.x - Min) / 2, holder.GetComponent<RectTransform>().rect.size.y);
        Scoreboard.GetComponent<RectTransform>().position = new Vector3((holder.GetComponent<RectTransform>().rect.size.x - Min) / 4, Min / 2);

        #endregion
        Box[0,0] = new Block(blockPrefab, new Point(0, 0), 2);
        Box[0,2] = new Block(blockPrefab, new Point(2, 0), 2); ;
    }
    void OnGUI()
    {
        Texture t = backgroundInstance.GetComponent<Image>().mainTexture;
        float r = Min / (Size * 6 + 1);
        UnityEngine.Color origin = GUI.color;
        GUI.color = new Color32(187, 173, 160, 255);
        for (int i = 0; i < Size + 1; i++)
        {
            Rect a = new Rect(new Vector2(holder.GetComponent<RectTransform>().rect.size.x / 2 - Min / 2 + i * 6 * r, holder.GetComponent<RectTransform>().rect.size.y / 2 - Min / 2),new Vector2(r, Min));
            GUI.DrawTexture(a, t);
        }
        for (int i = 0; i < Size + 1; i++)
        {
            Rect a = new Rect(new Vector2(holder.GetComponent<RectTransform>().rect.size.x / 2 - Min / 2, holder.GetComponent<RectTransform>().rect.size.y / 2 - Min / 2 + i * 6 * r), new Vector2(Min, r));
            GUI.DrawTexture(a, t);
        }
        GUI.color = origin;
    }

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
        if (split.HasValue) move(split.Value, direction.Value);
        #endregion
        
    }
    private void move(int split, int direction)
    {
        #region split array
        Block[,] Temp = new Block[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (split == (int)Split.horizontal) Temp[i,u] = Box[i, u];
                else Temp[i, u] = Box[u, i];
            }
        }
        #endregion
        #region move blocks
        for (int i = 0; i < Size; i++)
        {
            List<Block> TempList = (from u in Enumerable.Range(0, Size) where Temp[i, u] != null select Temp[i, u]).ToList();
            for (int u = 1; u < TempList.Count; u++)
            {
                if (TempList[u].Value == TempList[u - 1].Value)
                {
                    TempList[u].Value *= 2;
                    Score += TempList[u].Value;
                    Destroy(TempList[u - 1].Box);
                    TempList.RemoveAt(u - 1);
                }
            }

            for (int u = 0; u < TempList.Count; u++)
            {
                int newU = (direction == (int)Direction.right) ? (Size - u - 1) : u;

                Block block;
                if (u < TempList.Count) {
                    block = TempList[(direction == (int)Direction.right) ? (TempList.Count - u - 1) : u];
                    block.P = new Point(i, newU);
                }
                else block = null;

                Temp[i, newU] = block;
            }
            //if (direction == (int)Direction.right) Array.Reverse(Temp[i]);
        }
        #endregion
        #region rebuild array
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (split == (int)Split.horizontal) Box[i,u] = Temp[i,u];
                else Box[i,u] = Temp[u,i];
            }
        }
        #endregion
        #region place new block
        List<Point> places = new List<Point>();
        for (int i = 0; i < Size; i++)
        {
            for (int u = 0; u < Size; u++)
            {
                if (Box[i,u] == null) places.Add(new Point(i, u));
            }
        }
        Point TheChosenOne = places[UnityEngine.Random.Range(0, places.Count)];
        Box[TheChosenOne.X,TheChosenOne.Y] = new Block(blockPrefab, new Point(TheChosenOne.Y, TheChosenOne.X), 2);
        #endregion
    }


    int funny = 0;
    private void whynot()
    {
        move(funny%2, (funny==1||funny==2)?(1):(0));
        if (++funny > 3) funny = 0;
    }
}
