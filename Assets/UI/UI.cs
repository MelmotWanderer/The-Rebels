using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Image stamina;
    public Text CountMoney;
    [SerializeField] private Color colChangeStaminaUp = new Color(1.0f, 0.0f, 0.0f);
    [SerializeField] private Color colChangeStaminaDown = new Color(1.0f, 0.6134787f, 0.4292453f);
    [SerializeField] private float _stamina;
   
    public void ChangeUIStamina(float Stamina, bool isRecoveryFirst)
    {
      
            if (!isRecoveryFirst)
            {
                ChangeColorStamina(colChangeStaminaDown);
            }
            else
            {
            ChangeColorStamina(colChangeStaminaUp);
             }
                
            
        
        
        stamina.fillAmount = Stamina; 
    }
    public void ChangeColorStamina(Color col)
    {
       
            stamina.color = col;
        
        
    }
    public void ChangeCountMoney(int price)
    {
        CountMoney.text = price.ToString();
    }
}
