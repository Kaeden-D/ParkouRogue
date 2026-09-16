using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{

    public class PassiveState : MonoBehaviour, PlayerState
    {

        private PlayerController player;

        public void Handle(PlayerController controller)
        {
            if(!player)
            {
                player = controller;
            }
        }

        public void Jump()
        {
            player.AddImpulse(new Vector3(0f, 10f, 0f));
            player.ChangeState(GetComponent<AirState>());
        }

        public void Grounded() { }

    }

}