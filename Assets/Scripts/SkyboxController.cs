using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material skyboxMaterialAtHeight1; // The new material for the first height
    public Material skyboxMaterialAtHeight2; // The new material for the second height
    public float height1; // The height threshold for the first material change
    public float height2; // The height threshold for the second material change

    private Material originalSkyboxMaterial;
    private bool changed1;
    private bool changed2;

    void Start()
    {
        originalSkyboxMaterial = RenderSettings.skybox;
        changed1 = false;
        changed2 = false;
    }

    void Update()
    {
        float characterHeight = transform.position.y;

        // Change skybox material at the first height threshold
        if (!changed1 && characterHeight >= height1)
        {
            RenderSettings.skybox = skyboxMaterialAtHeight1;
            changed1 = true;
        }

        // Change skybox material at the second height threshold
        if (!changed2 && characterHeight >= height2)
        {
            RenderSettings.skybox = skyboxMaterialAtHeight2;
            changed2 = true;
        }
    }

    void OnDestroy()
    {
        // Reset the skybox material when the script is destroyed
        RenderSettings.skybox = originalSkyboxMaterial;
    }
}