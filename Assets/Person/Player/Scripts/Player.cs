using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public int Soul;
    public int Money;
    private bool recovery = false;
    public bool recoveryFirst = false;
    private float SpeedStaminaUp = 40.0f;
    private float SpeedStaminaDown = 20.0f;
    
 
    [SerializeField] private UI canv;
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    void Awake()
    {
        GlobalEventManager.OnGetMoney.AddListener(ChangeCountMoney);
        GlobalEventManager.OnGetWeapon.AddListener(GettingWeapon);
    }
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

    public void ChangeCountMoney(int price)
    {
        Money += price;
        canv.ChangeCountMoney(Money);

    }
    public void GettingWeapon(Weapon weapon)
    {
        
        weapons.Add(weapon);
    }
    public List<Weapon> GetWeapons()
    {
      
            return weapons;
        
      
    }
}
