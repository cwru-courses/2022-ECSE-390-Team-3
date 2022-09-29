using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveVFX : MonoBehaviour
{
    [SerializeField]
    Color leadingColor;
    [SerializeField]
    Color trailingColor;
    void Start()
    {
        // build mesh

        Vector2 dimensions = GetComponentInParent<Wave>().GetDimensions();

        Vector3 size = new Vector3(dimensions.x, dimensions.y, 1f);
        transform.localScale = size;
        Vector3 rotation = GetComponentInParent<Transform>().eulerAngles;
        
        transform.eulerAngles = rotation;

        // color the mesh

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        foreach(Vector3 point in mesh.vertices)
        {
            Debug.Log(point);
        }
        Debug.Log("\n");

        Color[] colors = new Color[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.Lerp(trailingColor, leadingColor, vertices[i].x);
        }

        mesh.colors = colors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
