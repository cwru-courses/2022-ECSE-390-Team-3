using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HazardSound : MonoBehaviour
{

    public Tilemap tileMap;
    public List<Vector3> hazardPlaces;
    GameObject hazard;

    void Start()
    {
        hazard = GameObject.FindGameObjectsWithTag("Hazard")[0];
        hazardPlaces = new List<Vector3>();

        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)tileMap.transform.position.y));
                Vector3 place = tileMap.CellToWorld(localPlace);
                hazardPlaces.Add(place);
                if (tileMap.GetTile(localPlace) == hazard)
                {
                    //Tile at "place"
                    hazardPlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        Debug.Log(hazardPlaces.Count);
        foreach (Vector3 p in hazardPlaces)
        {
            Debug.Log(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
