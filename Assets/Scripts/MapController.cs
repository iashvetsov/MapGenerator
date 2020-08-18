using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    MapDataGenerator data = new MapDataGenerator();
    public Text _text;
    private int[,] map;

    private const int SIZE = 4;

    void Start()
    {
        map = data.GetMapData(SIZE, SIZE, 2);

        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = "";

        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                _text.text += map[x, y];
            }
            _text.text += "\n";
        }
    }
}
