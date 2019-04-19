using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Block
{
    GameObject Box;
    Point p;
    public Point P { get => p; set { p = value; SetPoint(); } }

    public Block(GameObject g, Point p)
    {
        this.p = p;
        float w = g.GetComponent<RectTransform>().rect.width;
        float rr = g.GetComponent<RectTransform>().rect.height;
        GameObject h = GameObject.Find("Holder");
        RectTransform r = h.GetComponent<RectTransform>();
        Box = MonoBehaviour.Instantiate(g, h.transform);
        SetPoint();
    }

    private void SetPoint()
    {
        RectTransform r = Box.GetComponent<RectTransform>();
        RectTransform h = GameObject.Find("Holder").GetComponent<RectTransform>();
        Vector3 v = new Vector3(h.rect.width / Main.Size * p.X + r.rect.width, h.rect.height / Main.Size * p.Y + r.rect.height, 0);
        Box.GetComponent<RectTransform>().position = v;

    }
}
