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

        //public Vector3 movVar = new Vector3(1f, 1f, 0f);
        private float side = 0f;

        private float slow = 1f;

        void Start()
        {

            state = GetComponent<PassiveState>();
            rb = FindFirstObjectByType<Rigidbody>();
            state.Handle(this);

        }

        void Update()
        {

            if (Mathf.Abs(rb.linearVelocity.x) < absMaxVelocity.x * slow)
            {
                addForce(new Vector3(side * speed, 0f, 0f));
            }
            else if (side == 0)
            {
                rb.linearVelocity.Set(0f, rb.linearVelocity.y, 0f);
            }

        }

        public void OnSideways(InputValue value)
        {
            side = value.Get<float>();
        }

        public void changeState(PlayerState upState)
        {
            state = upState;
            state.Handle(this);
        }

        public void addForce(Vector3 dir)
        {
            rb.AddForce(dir);
        }

        public void changeSlow(float value)
        {
            slow = value;
        }

        public bool isFalling()
        {
            return rb.linearVelocity.y != 0;
        }

    }

}
