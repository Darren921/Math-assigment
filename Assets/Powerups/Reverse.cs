using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Reverse : MonoBehaviour
{
    static int alt = 1;
    public bool isReverse;
 
    private void Update()
    {

        var ReverseR = GameObject.Find("ReverseR").GetComponent<Reverse>();
        var ReverseS = GameObject.Find("ReverseSpeed").GetComponent<Reverse>();
        if (Input.GetMouseButtonDown(0))
        {
            LayerMask mask = LayerMask.GetMask("PowerUp");
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, mask);
            if (raycastHit.transform.name == "ReverseSpeed" && alt == 1)
            {
                ReverseS.isReverse = true;
                
            }
         
            if (raycastHit.transform.name == "ReverseR" && alt == 1)
            {
                ReverseR.isReverse = true;
          

            }
           
        }
     

    }
}
    
