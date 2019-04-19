using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static int Size { get; set; } = 3;
    public GameObject block;
    public GameObject Block { get => block;}
    private GameObject holder;
    public GameObject Holder { get => holder; }
    Block b;
    
    void Start()
    {
        holder = GameObject.Find("Holder");
        b = new Block(block, new System.Drawing.Point(0, 0));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            b.P = new Point(b.P.X, b.P.Y + 1);
        }
    }
}
