using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : InteractiveItem
{
    public int Price;
    
   


    public void PickUpMoney()
    {
        GlobalEventManager.GetMoney(Price);
        Destroy(gameObject);
    }
}
