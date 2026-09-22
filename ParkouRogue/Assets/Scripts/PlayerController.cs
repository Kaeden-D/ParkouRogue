using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Chapter.State
{


    public class PlayerController : MonoBehaviour
    {

        private PlayerState state = null;
        private Rigidbody rb;

        public float speed;
        public Vector3 absMaxVelocity;

        public string currentState;
        public float stateCooldown;
        public float stateTime;


        //State Variables: 

        public short isWalled = 0; //0 = Not Walled, -1 = Walled Left, 1 = Walled Right
        public bool wallCling = false;

        public bool hasAirJumped = false;
        public bool hasWallJumped = false;
        public float dashCooldown = 0.5f;
        public bool hasAxisDashed = false;
        public float axisDashTime = 0f;
        public bool hasClickDashed = false;
        public float clickDashTime = 0f;
        
        private float vert = 0f;
        private float side = 0f;

        private float slow = 1f;

        void Start()
        {

            rb = FindFirstObjectByType<Rigidbody>();
            ChangeState(GetComponent<PassiveState>());

        }

        void Update()
        {
            
            if (state.SideMove(side))
            {
                //Skips remaining conditions if true, as the state has handled its own unique movement
            }
            else if (side != 0 && (Mathf.Abs(rb.linearVelocity.x) < absMaxVelocity.x * slow || (rb.linearVelocity.x == 0f || Mathf.Sign(side) != Mathf.Sign(rb.linearVelocity.x))))
            {
                AddForce(new Vector3(side * speed * Time.deltaTime, 0f, 0f));
                //Applies a force in the direction of the player's input, if the player's horizontal velocity is below the maximum
            }
            else if (Mathf.Abs(rb.linearVelocity.x) < 0.01f)
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
                //Stops the player from sliding when the velocity is very low
            }
            else if (side == 0 && rb.linearVelocity.x != 0)
            {
                AddForce(new Vector3((speed / 3f) * (-rb.linearVelocity.x / Mathf.Abs(rb.linearVelocity.x)) * Time.deltaTime, 0f, 0f));
                //Applies a small frictional force in the opposite direction of the player's horizontal velocity
            }

            if (IsGrounded())
            {
                isWalled = 0; //Not Walled
                state.Grounded();
            }
            else if (isWalledLeft())
            {
                isWalled = -1; //On Wall to the Left
                state.Walled(); 
            }
            else if (isWalledRight())
            {
                isWalled = 1; //On Wall to the Right
                state.Walled(); 
            }
            else
            {
                isWalled = 0;
                ChangeState(GetComponent<AirState>());
            }

        }

        public void ChangeState(PlayerState upState)
        {
            if ((state == upState || upState == null) || //Skip if the state is already the same as the new state, or the new state is null
                (Time.time - stateTime < stateCooldown)) //Skip if the state has not been active for longer than the cooldown
                { return; } 
            stateTime = Time.time;
            state = upState;
            state.Handle(this);
            currentState = upState.ToString();
        }

        //Movement Handling: 

        public void AddForce(Vector3 dir)
        {
            rb.AddForce(dir);
        }

        public void AddImpulse(Vector3 dir)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }

        public float GetVerticalVelocity()
        {
            return rb.linearVelocity.y;
        }
        
        public void VerticalStop()
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
        }

        public float GetHorizontalVelocity()
        {
            return rb.linearVelocity.x;
        }

        public void HorizontalStop()
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }

        //State Handling: 

        public void AbilityReset()
        {
            hasAirJumped = false;
            hasWallJumped = false;
            if (clickDashTime + dashCooldown < Time.time) hasClickDashed = false;
            if (axisDashTime + dashCooldown < Time.time) hasAxisDashed = false;
        }

        public void ChangeSlow(float value)
        {
            slow = value;
        }

        public bool IsGrounded()
        {
             return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f);
        }

        public bool isWalledLeft()
        {
            return Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit, 0.5f);
        }

        public bool isWalledRight()
        {
            return Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit, 0.5f);
        }

        public void StickToWall()
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }

        //Player Input Handling:


        private void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                state.Jump();
            }
        }

        public void OnVerticals(InputValue value)
        {
            vert = value.Get<float>();
        }

        public void OnSideways(InputValue value)
        {
            side = value.Get<float>();
        }

        public void OnClick(InputValue value)
        {
            if (value.isPressed)
            {
                state.ClickDash();
            }
        }

        public void OnC(InputValue value)
        {
            if (value.isPressed)
            {
                state.AxisDash(vert, side);
            }
        }

    }

}
