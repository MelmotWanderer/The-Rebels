
using UnityEngine;



    public class InputHandler : MonoBehaviour
    {
        [Tooltip("Sensitivity multiplier for moving the camera around")]
        public float LookSensitivity = 1f;

        [Tooltip("Additional sensitivity multiplier for WebGL")]
        public float WebglLookSensitivityMultiplier = 0.25f;

        [Tooltip("Limit to consider an input when using a trigger on a controller")]
        public float TriggerAxisThreshold = 0.4f;

        [Tooltip("Used to flip the vertical input axis")]
        public bool InvertYAxis = false;

        [Tooltip("Used to flip the horizontal input axis")]
        public bool InvertXAxis = false;

      
        bool m_FireInputWasHeld;

     


      

        public Vector3 GetMoveInput()
        {
           
                Vector3 move = new Vector3(Input.GetAxisRaw(GameConstants.k_AxisNameHorizontal), 0f,
                    Input.GetAxisRaw(GameConstants.k_AxisNameVertical));

                // constrain move input to a maximum magnitude of 1, otherwise diagonal movement might exceed the max move speed defined
                move = Vector3.ClampMagnitude(move, 1);

                return move;
            

       
        }

        public float GetLookInputsHorizontal()
        {
            return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameHorizontal);
        }

        public float GetLookInputsVertical()
        {
            return GetMouseOrStickLookAxis(GameConstants.k_MouseAxisNameVertical);
        }

        public bool GetJumpInputDown()
        {
        
                return Input.GetButtonDown(GameConstants.k_ButtonNameJump);
  
        }

        public bool GetJumpInputHeld()
        {
          
                return Input.GetButton(GameConstants.k_ButtonNameJump);
    
        }

        public bool GetInteractInput()
           {
                return Input.GetButtonDown(GameConstants.k_ButtonNameInteract);
            }

    
    

        public bool GetSprintInputHeld()
        {
         
                return Input.GetButton(GameConstants.k_ButtonNameSprint);
        }

        public bool GetCrouchInputDown()
        {
            
                return Input.GetButtonDown(GameConstants.k_ButtonNameCrouch);
            
        }

        public bool GetCrouchInputReleased()
        {
           
                return Input.GetButtonUp(GameConstants.k_ButtonNameCrouch);
        }

     

        float GetMouseOrStickLookAxis(string mouseInputName)
        {
            
                // Check if this look input is coming from the mouse
             
                float i = Input.GetAxisRaw(mouseInputName);

                // handle inverting vertical input
                if (InvertYAxis)
                    i *= -1f;

                // apply sensitivity multiplier
                i *= LookSensitivity;

               
                    // reduce mouse input amount to be equivalent to stick movement
                    i *= 0.01f;
#if UNITY_WEBGL
                    // Mouse tends to be even more sensitive in WebGL due to mouse acceleration, so reduce it even more
                    i *= WebglLookSensitivityMultiplier;
#endif
                

                return i;
            

          
        }
   }
