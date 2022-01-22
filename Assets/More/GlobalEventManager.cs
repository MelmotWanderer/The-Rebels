using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager : MonoBehaviour
{
    public static UnityEvent<int> OnGetMoney = new UnityEvent<int>();
    public static UnityEvent<Weapon> OnGetWeapon = new UnityEvent<Weapon>();
    public static void GetMoney(int price)
    {
        OnGetMoney.Invoke(price);
    }
    public static void GetWeapon(Weapon weapon)
    {
        OnGetWeapon.Invoke(weapon);
    }
}
