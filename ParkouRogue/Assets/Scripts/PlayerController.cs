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
        public int skipFrame = 0;

        //public Vector3 movVar = new Vector3(1f, 1f, 0f);
        private float side = 0f;

        private float slow = 1f;

        void Start()
        {

            rb = FindFirstObjectByType<Rigidbody>();
            ChangeState(GetComponent<PassiveState>());

        }

        void Update()
        {

            if (Mathf.Abs(rb.linearVelocity.x) < absMaxVelocity.x * slow || rb.linearVelocity.x * side < 0)
            {
                AddForce(new Vector3(side * speed, 0f, 0f));
            }
            else if (side == 0)
            {
                rb.linearVelocity.Set(0f, rb.linearVelocity.y, 0f);
            }

            //Skips for 1 frame
            if (skipFrame > 0)
            {
                skipFrame--;
                return;
            }

            if (IsGrounded())
                state.Grounded();

        }

        public void OnSideways(InputValue value)
        {
            side = value.Get<float>();
        }

        public void ChangeState(PlayerState upState)
        {
            Debug.Log("A");
            state = upState;
            state.Handle(this);
            currentState = upState.ToString();
        }

        public void AddForce(Vector3 dir)
        {
            rb.AddForce(dir);
        }

        public void AddImpulse(Vector3 dir)
        {
            rb.AddForce(dir, ForceMode.Impulse);
        }

        public void ChangeSlow(float value)
        {
            slow = value;
        }

        public bool IsGrounded()
        {
            bool test = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 0.5f);
            if (rb.linearVelocity.y == 0)
            {
                Debug.Log(test);
                return test;
            }
            return false;
        }

        //State Handling: 


        //Player Input Handling:


        private void OnJump(InputValue value)
        {
            if (value.isPressed)
            {
                skipFrame = 2;
                state.Jump();
            }
        }

    }

}
