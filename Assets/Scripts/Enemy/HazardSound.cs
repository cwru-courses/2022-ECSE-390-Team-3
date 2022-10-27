using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HazardSound : MonoBehaviour
{

    public AudioSource audioSource;
    public Tilemap tileMap;
    List<Vector3> hazardPlaces;
    GameObject hazard;
    public float range;
    public float maxVolume = 5f;

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
                if (tileMap.HasTile(localPlace))
                {
                    //Hazard tile at "place"
                    hazardPlaces.Add(place);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }
        playAudio();

    }

    void playAudio()
    {
        audioSource.Stop();
        audioSource.loop = true;
        audioSource.volume = 0.2f;
        audioSource.Play();
    }

    void adjustVolume(float distance)
    {
        if(distance > range)
        {
            audioSource.volume = 0f;
        }
        else{
            audioSource.volume = maxVolume * (2 / distance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float minDist = 999999;
        float dist = 999999;
        foreach (Vector3 place in hazardPlaces)
        {
            dist = Vector3.Distance(place, transform.position);
            if(dist < minDist)
            {
                minDist = dist;
            }
        }
        adjustVolume(minDist);
    }
}
