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
        Box.GetComponent<RectTransform>().sizeDelta = new Vector2(h.GetComponent<RectTransform>().rect.size.y/(Main.Size + 1), h.GetComponent<RectTransform>().rect.size.y / (Main.Size + 1));
        P = p;
        Value = value;
    }

    private void SetPoint()
    {
        RectTransform RectBox = Box.GetComponent<RectTransform>();
        RectTransform RectCanvas = GameObject.Find("Holder").GetComponent<RectTransform>();
        Vector3 VectorBox = new Vector3(RectCanvas.rect.width / Main.Size * p.X + RectBox.rect.width, RectCanvas.rect.height / Main.Size * p.Y + RectBox.rect.height, 0);
        Box.GetComponent<RectTransform>().position = VectorBox;
    }
    ~Block()
    {
        MonoBehaviour.Destroy(Box);
    }
}
