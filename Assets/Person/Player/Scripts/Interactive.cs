using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Crosshair ch;
    [SerializeField] private float _maxDistance;
    private Ray _ray;
    private RaycastHit _hit;
    InputHandler m_InputHandler;
    
    void Start()
    {
        m_InputHandler = GetComponent<InputHandler>();

    }

   
    void Update()
    {
        Ray();
        DrawRay();
        Interact();
       
    }
    private void Ray()
    {
        _ray = PlayerCamera.ScreenPointToRay(new Vector2(Screen.width / 2.0f, Screen.height / 2.0f));
    }

    private void DrawRay()
    {
        if(Physics.Raycast(_ray, out _hit, _maxDistance))
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.blue);
        }
    }
    private void Interact()
    {
         Debug.DrawRay(_ray.origin, _ray.direction * _maxDistance, Color.green);
       
        if (_hit.transform != null && _hit.transform.GetComponent<InteractiveItem>() && _hit.transform.GetComponent<InteractiveItem>().isInteractive)
            {
            ch.Interact(true);
            if (m_InputHandler.GetInteractInput())
            {
                if (_hit.transform.GetComponent<Door>())
                 {
                _hit.transform.GetComponent<Door>().Open();
                   
                }

             if(_hit.transform.GetComponent<Money>())
                {
                    _hit.transform.GetComponent<Money>().PickUpMoney();
                }
             if(_hit.transform.GetComponent<Weapon>())
                {
                    _hit.transform.GetComponent<Weapon>().PickUpWeapon();
                }
            }
            }else
            {
                 ch.Interact(false);
             }

         


    }
}
