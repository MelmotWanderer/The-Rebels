using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    
    Vector2 CrosshairSize = new Vector3(20.0f, 20.0f); 
   
    [SerializeField] private RectTransform rt;
    void Start()
    {

        rt.sizeDelta = CrosshairSize;

    }

   
    
    public void Interact(bool isInteract)
    {
        if(isInteract)
        {
            rt.sizeDelta = CrosshairSize + new Vector2(10.0f, 10.0f);
        }
        else
        {
            rt.sizeDelta = CrosshairSize;
        }
       
    }
   
}
