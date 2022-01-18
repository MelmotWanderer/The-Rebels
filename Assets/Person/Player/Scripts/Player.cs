using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public int Soul { get; set; }
    private bool recovery = false;
    public bool recoveryFirst = false;
    [SerializeField] private UI canv;
    private float SpeedStaminaUp = 40.0f;
    private float SpeedStaminaDown = 20.0f;

    void Start()
    {
        Speed = 4.0f;
        Stamina = 100.0f;
        
    }


    void Update() {

       
            ChangeStamina();
        
              
            
    }
     public void Running()
    {
        Stamina -= SpeedStaminaUp * Time.deltaTime;
        
    }
 
    public void ChangeStamina()
    {
          if(Stamina < 100.0f)
        {
            recovery = true;
            if(Stamina <= 0.0f)
            {
                recoveryFirst = true;
            }
        }else
        {
            recoveryFirst = false;
            recovery = false;
        }

        if (recovery)
        {
            Stamina += SpeedStaminaDown * Time.deltaTime;
        }
            

        canv.ChangeUIStamina(Stamina / 100.0f, recoveryFirst);


    }
   
}
