using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    public float spinSpeed = 10f;
    public string direction;
    void Start()
    {
        if(direction == null){
            direction = "forward";
        }
        if(direction != "up" && direction != "sideways"){
            direction = "forward";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == "forward"){
        transform.Rotate(Vector3.forward , spinSpeed * Time.deltaTime);
        }
        else if(direction == "sideways"){
            transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
        }
        else if(direction == "up"){
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
    }
}
