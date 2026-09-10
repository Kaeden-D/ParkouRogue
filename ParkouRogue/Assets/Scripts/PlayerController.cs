using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{


    public class PlayerController : MonoBehaviour
    {

        private PlayerState state;
        private Rigidbody rb;

        //public Vector3 movVar = new Vector3(1f, 1f, 0f);

        void Start()
        {

            state = GetComponent<PassiveState>();
            rb = FindFirstObjectByType<Rigidbody>();
            state.Handle(this);

        }

        void Update()
        {


        }

        public void addForce(Vector3 dir)
        {
            rb.AddForce(dir);
        }

    }

}
