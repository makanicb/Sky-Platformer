using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoLERP : MonoBehaviour
{
    private Transform pill;

    public float maxDistance = 7f;
    public Color endColor = Color.red;
    public Color startColor = Color.white;
    private SkinnedMeshRenderer meshRenderer;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pill = player.transform;
        meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      //calculate distance between ghost and player
        float distanceToPill = Vector3.Distance(transform.position, pill.position);

        //calculate lerp parameter based on distance
        float t = Mathf.Clamp01(distanceToPill / maxDistance);

        //lerp color
        Color lerpedColor = Color.Lerp(startColor, endColor, t);

        meshRenderer.material.color = lerpedColor;
    }
}
