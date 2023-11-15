using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public GameObject _wallGO;

    public Tile predecessor = null;

    public enum TileType
    {
        Ground,
        Wall
    }

    private TMP_Text _text;
    private TileType _tileType;


    private Renderer _renderer;
    private int _x;
    private int _y;

    private float _cost = Mathf.Infinity;
    public Color _Color { get => _renderer.material.color; set => _renderer.material.color = value; }
    public string _Text { get => _text.text; set => _text.text = value; }

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _renderer = GetComponent<Renderer>();
        
    }

    public void Init(int x, int y, int type)
    {
        _x = x;
        _y = y;
        _TileType = (TileType)type;
        name = "Tile_" + x + "_" + y;
        _text.text = "Tile_" + x + "_" + y;
    }

    public TileType _TileType
    {
        get => _tileType;
        set
        {
            _tileType = value;
            switch (_tileType)
            {
                case TileType.Ground:
                    _wallGO.SetActive(false);
                    break;
                case TileType.Wall:
                    _wallGO.SetActive(true);
                    break;
            }
        }
    }

    public float CostToReach
    {
        get
        {
            switch (_tileType)
            {
                default:
                    return 1.0f;
            }
        }

    }

    public float Cost
    {
        get
        {
            return _cost == Mathf.Infinity ? Mathf.Infinity : _cost;
        }

        set
        {
            _cost = value;
        }
    }

    public int _X => _x;
    public int _Y => _y;
}

