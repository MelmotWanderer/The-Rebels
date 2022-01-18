using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractiveItem
{
   
    [SerializeField] private  Animator DoorAnim;
    private  bool isOpened = false;


  public  void Open()
    {
        DoorAnim.SetBool("IsOpened", !isOpened);
        isOpened = !isOpened;
    }
    
}
