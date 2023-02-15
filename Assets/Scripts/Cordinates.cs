using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cordinates : MonoBehaviour
{
    // Start is called before the first frame update
    public Tilemap Tilemap;
    void Start()
    {
        Vector3 tilePosition;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);
        Debug.Log(string.Format("X={0}, Y={1}", Tilemap.size.x, Tilemap.size.y));
        for (int i = 0; i < Tilemap.size.x; i++)
        {
            for (int j = 0; j < Tilemap.size.y; j++)
            {
                coordinate.x = i; coordinate.y = j;
                tilePosition = Tilemap.CellToWorld(coordinate);
                Debug.Log(string.Format("Position of tile [{0}, {1}] = ({2}, {3})", coordinate.x, coordinate.y, tilePosition.x, tilePosition.y));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
