using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;

public class Block
{
    private GameObject box;
    public GameObject Box { get => box; private set => box = value; }

    Point p;
    public Point P { get => p; set { p = value; SetPoint(); } }

    private int number;
    public int Value { get => number; set {
            number = value;
            Box.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();
        }
    }

    public Block(GameObject g, Point p, int value)
    {
        GameObject h = GameObject.Find("Holder");
        Box = MonoBehaviour.Instantiate(g, h.transform);
        float l = h.GetComponent<RectTransform>().rect.size.y;
        float r = l / (Main.Size*6+1);
        Box.GetComponent<RectTransform>().sizeDelta = new Vector2(5*r, 5*r);
        P = p;
        Value = value;
    }

    private void SetPoint()
    {
        RectTransform RectBox = Box.GetComponent<RectTransform>();
        RectTransform RectCanvas = GameObject.Find("Holder").GetComponent<RectTransform>();
        float l = RectCanvas.rect.size.y;
        float r = l / (Main.Size * 6 + 1);
        Vector3 VectorBox = new Vector3((P.X + 1) * r * 6 - 2 * r, (p.Y + 1) * r * 6 - 2 * r, 0);
        Box.GetComponent<RectTransform>().position = VectorBox;
    }
    ~Block()
    {
        MonoBehaviour.Destroy(Box);
    }
}
