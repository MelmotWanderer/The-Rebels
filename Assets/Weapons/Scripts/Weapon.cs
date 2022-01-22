using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : InteractiveItem
{
    public float PhysicDamage;
    private bool inHand;
    private Player _player;
    private PlayerController _playerController;
    private Animator _anim;
   
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
        _anim = GetComponent<Animator>();
       
    }

    public void PickUpWeapon()
    {

        Equip();
        WeaponInHand();



    }
    private void WeaponInHand()
    {
        _playerController.SetWeaponInHand(this);
       
    }
    public void PlayAnimationWeaponInHand()
    {
        _anim.SetTrigger("Equip");
        _anim.SetBool("inHand", true);
    }
    public void Attack()
    {
        _anim.SetTrigger("OnAttack");
      
    }
    
    private void Equip()
    {
        isInteractive = false;
        if (_playerController.GetWeaponInHand())
        {

            _playerController.DeleteWeaponInHand();
        }
        Transform RightHand = _playerController.RightHand;
        
        
        GlobalEventManager.GetWeapon(this);
        transform.SetParent(RightHand);
        transform.position = new Vector3(RightHand.localPosition.x, RightHand.localPosition.y, RightHand.localPosition.z);
        transform.localRotation = Quaternion.Euler(90.0f, 0, 0.0f);
        gameObject.layer = 3;
    }
}
