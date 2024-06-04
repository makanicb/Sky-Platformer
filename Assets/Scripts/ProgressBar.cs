using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private RectTransform bar;
    private Vector2 origSize;
    public BulletTimeController bt;
    // Start is called before the first frame update
    void Start()
    {
        bar = gameObject.GetComponent<RectTransform>();
        origSize = bar.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        float value = bt.getRemainingDuration() / bt.duration;
        bar.sizeDelta = new Vector2(origSize.x * value, origSize.y);
    }

}
