using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

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
            Box.GetComponent<Image>().color = Colorcodes[number];
        }
    }

    public static readonly Dictionary<int, Color32> Colorcodes = new Dictionary<int, Color32>
    {
        { 2,    new Color32(238,228,218,255)},
        { 4,    new Color32(237,224,200,255)},
        { 8,    new Color32(242,177,121,255)},
        { 16,   new Color32(245,149,99,255) },
        { 32,   new Color32(246,124,95,255) },
        { 64,   new Color32(246,94,59,255)  },
        { 128,  new Color32(237,207,114,255)},
        { 256,  new Color32(237,204,97,255) },
        { 512,  new Color32(237,200,80,255) },
        { 1024, new Color32(237,197,63,255) },
        { 2048, new Color32(237,194,46,255) },
        { 4096, new Color32(60,58,50,255)   },
        { 8192, new Color32(60,58,50,255)   }
    };
    public Block(GameObject g, Point p, int value)
    {
        GameObject holder = GameObject.Find("Holder");
        Box = MonoBehaviour.Instantiate(g, holder.transform);
        float r = Main.Min / (Main.Size*6+1);
        Box.GetComponent<RectTransform>().sizeDelta = new Vector2(5*r, 5*r);
        P = p;
        Value = value;
        SetPoint();
    }

    private void SetPoint()
    {
        RectTransform RectBox = Box.GetComponent<RectTransform>();
        RectTransform RectCanvas = GameObject.Find("Holder").GetComponent<RectTransform>();
        //float l = RectCanvas.rect.size.y;
        float r = Main.Min / (Main.Size * 6 + 1);
        Vector3 VectorBox = new Vector3((P.X + 1) * r * 6 - 2.5f * r + (RectCanvas.rect.width / 2 - Main.Min / 2), (p.Y + 1) * r * 6 - 2.5f * r + (RectCanvas.rect.height / 2 - Main.Min / 2), 0);
        Box.GetComponent<RectTransform>().position = VectorBox;
    }
    ~Block()
    {
        MonoBehaviour.Destroy(Box);
    }
}
