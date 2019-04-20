using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;
using System.Linq;

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

    public static Dictionary<int, UnityEngine.Color> Colorcodes = new Dictionary<int, UnityEngine.Color>
    {
        { 2, new UnityEngine.Color()},
        { 4, new UnityEngine.Color()},
        { 8, new UnityEngine.Color()},
        { 16, new UnityEngine.Color()},
        { 32, new UnityEngine.Color()},
        { 64, new UnityEngine.Color()},
        { 128, new UnityEngine.Color()},
        { 256, new UnityEngine.Color()},
        { 512, new UnityEngine.Color()},
        { 1024, new UnityEngine.Color()},
        { 2048, new UnityEngine.Color()},
        { 4096, new UnityEngine.Color()},
        { 8192, new UnityEngine.Color()}
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
        float l = RectCanvas.rect.size.y;
        float r = l / (Main.Size * 6 + 1);
        Vector3 VectorBox = new Vector3((P.X + 1) * r * 6 - 2 * r + (RectCanvas.rect.width / 2 - Main.Min / 2), (p.Y + 1) * r * 6 - 2 * r + (RectCanvas.rect.height / 2 - Main.Min / 2), 0);
        Box.GetComponent<RectTransform>().position = VectorBox;
    }
    ~Block()
    {
        MonoBehaviour.Destroy(Box);
    }
}
