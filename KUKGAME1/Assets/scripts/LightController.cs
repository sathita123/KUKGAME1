using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light light1;
    public string LightName = "Living Room";
    // Start is called before the first frame update
    private void Awake()
    {
        print("Hello Awake!");
    }
    
    void Start()
    {
        print("Hello World!");    
    }

    private void OnMouseDown()
    {
        print("Mouse Down");
        if (light1.enabled == false)
        {
            light1.enabled = true;
        }
        else
        {
            light1.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
