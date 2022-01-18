using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _characterController;
    InputHandler m_InputHandler;
    Player _player;
    [SerializeField] private Transform Head;
    
    private CameraMovement _camMovement; 
    private float RotationMultiplier = 2.0f;
    private float m_CameraVerticalAngle;
    private float RotationSpeed = 5.0f;    
    private float HeightCharacterStanding = 2.0f;
    private float HeightCharacterCrouch = 1.4f;
    private float CameraHeightRatio = 0.22f;
    private float SpeedMultiple = 1.0f;
    private bool isCrouching = false;
    private bool Sprinting = false;
   

    void Start()
    {
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
        m_InputHandler = GetComponent<InputHandler>();
        _player = GetComponent<Player>();
        _camMovement = GetComponentInChildren<CameraMovement>();
        
       
        
    }
   
    void Update()
    {
    
        if (m_InputHandler.GetCrouchInputDown())
        {
            isCrouching = !isCrouching;
        }
        if (m_InputHandler.GetMoveInput() != new Vector3(0.0f, 0.0f, 0.0f))
        {
            _camMovement.Move(1.0f, true);
            if (m_InputHandler.GetSprintInputHeld())
            {
                _camMovement.Move(1.4f, true);
                isCrouching = false;
                Sprinting = true;

            }
            else
            {
                Sprinting = false;
            }
        }else
        {
            _camMovement.Move(1.0f, false);
        }
       
    
        Rotation();
        Move();
        Crouching();
    }



    void Rotation()
    {
        transform.Rotate(new Vector3(0f, (m_InputHandler.GetLookInputsHorizontal() * RotationSpeed * RotationMultiplier), 0f), Space.Self);

        m_CameraVerticalAngle += m_InputHandler.GetLookInputsVertical() * RotationSpeed * -RotationMultiplier;

       
        m_CameraVerticalAngle = Mathf.Clamp(m_CameraVerticalAngle, -89f, 89f);

        
        Head.transform.localEulerAngles = new Vector3(m_CameraVerticalAngle, 0, 0);

    }
    void Move()
    {
        
            if (_player.recoveryFirst)
            {

                Sprinting = false;
            }
        
            
            
        
        if (Sprinting) { SpeedMultiple = 2.0f; _player.Running(); } 
        else if (isCrouching) { SpeedMultiple = 0.6f; }
        else { SpeedMultiple = 1.0f; }
        
        Vector3 forward = transform.TransformDirection(m_InputHandler.GetMoveInput());
       
        _characterController.SimpleMove(forward * (_player.Speed * SpeedMultiple));
    }

    void Crouching()
    {
        if (isCrouching)
        {
            
            
            Head.transform.localPosition = Vector3.up * HeightCharacterCrouch * CameraHeightRatio;
            _characterController.height = HeightCharacterCrouch;
        }

        else
        {
            _characterController.height = HeightCharacterStanding;
            Head.transform.localPosition = Vector3.up * HeightCharacterStanding * CameraHeightRatio;
        }
    }
}
