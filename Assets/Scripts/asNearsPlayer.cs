using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoLERP : MonoBehaviour
{
    private Transform pill;

    public float maxDistance = 7f;
    public Color endColor = Color.red;
    private Color startColor;
    private SkinnedMeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pill = player.transform;
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        
        // Initialize startColor with the current color of the material
        startColor = meshRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance between ghost and player
        float distanceToPill = Vector3.Distance(transform.position, pill.position);

        // Calculate lerp parameter based on distance
        float t = Mathf.Clamp01(distanceToPill / maxDistance);

        // Lerp color
        Color lerpedColor = Color.Lerp(startColor, endColor, t);

        meshRenderer.material.color = lerpedColor;
    }
}