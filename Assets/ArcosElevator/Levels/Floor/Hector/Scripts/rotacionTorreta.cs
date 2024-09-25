using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class rotacionTorreta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            
            
       
           
        
    }
    public void RotateTurret()
    {
        transform.eulerAngles += new Vector3(0, 45, 0);
    }
}
