using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Chapter.State
{


    public class PlayerController : MonoBehaviour
    {

        private PlayerState state;
        private Rigidbody rb;

        public float speed;
        public Vector3 absMaxVelocity;

        public string currentState;
        public float stateCooldown;
        public float stateTime;


        //State Variables: 

        public short isWalled = 0; //0 = Not Walled, -1 = Walled Left, 1 = Walled Right
        public bool wallCling = false;

        private float side = 0f;

        private float slow = 1f;

        void Start()
        {

            rb = FindFirstObjectByType<Rigidbody>();
            ChangeState(GetComponent<PassiveState>());

        }

        void Update()
        {
            
            if (wallCling) { VerticalStop(); }
            if (state.SideMove(side))
            {
                //Skips remaining conditions if true, as the state has handled its own unique movement
            }
            else if (Mathf.Abs(rb.linearVelocity.x) < absMaxVelocity.x * slow && side != 0)
            {
                AddForce(new Vector3(side * speed, 0f, 0f));
            }
            else if (side == 0 && rb.linearVelocity.x != 0)
            {
                AddForce(new Vector3((speed / 4f) * (-rb.linearVelocity.x / Mathf.Abs(rb.linearVelocity.x)), 0f, 0f));
            }
            if (Mathf.Approximately(rb.linearVelocity.x, 0f))
            {
                rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            }

            if (IsGrounded())
                isWalled = 0; //Not Walled
                state.Grounded();

            if (isWalledLeft())
            {
                isWalled = -1; //On Wall to the Left
                state.Walled(); 
            }
            else if (isWalledRight())
            {
                isWalled = 1; //On Wall to the Right
                state.Walled(); 
            }

        }

        public void OnSideways(InputValue value)
        {
            side = value.Get<float>();
        }

        public void ChangeState(PlayerState upState)
        {
            if (Time.time - stateTime > stateCooldown || upState is PassiveState)
            {
                stateTime = Time.time;
                state = upState;
                state.Handle(this);
                currentState = upState.ToString();
            }
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
        
        public void VerticalStop()
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
        }

        //State Handling: 

        public void ChangeSlow(float value)
        {
            slow = value;
        }

        public bool IsGrounded()
        {
            if (rb.linearVelocity.y == 0)
            {
                return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f);
            }
            return false;
        }

        public bool isWalledLeft()
        {
            if (rb.linearVelocity.x == 0)
            {
                return Physics.Raycast(transform.position, Vector3.left, out RaycastHit hit, 0.5f);
            }
            return false;
        }

        public bool isWalledRight()
        {
            if (rb.linearVelocity.x == 0 || true)
            {
                return Physics.Raycast(transform.position, Vector3.right, out RaycastHit hit, 0.5f);
            }
            return false;
        }

        //Player Input Handling:


        private void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                state.Jump();
            }
        }

    }

}
