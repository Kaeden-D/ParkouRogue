using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{

    public class AirState : MonoBehaviour, PlayerState
    {

        private PlayerController player;

        public void Handle(PlayerController controller)
        {
            if (!player)
            {
                player = controller;
            }
            player.ChangeSlow(0.5f);
        }

        public void Jump()
        {

        }

        void Update()
        {

            if (player == null)
                return;

            if (!player.IsGrounded())
            {
                player.ChangeSlow(1f);
                player.ChangeState(GetComponent<PassiveState>());
            }

        }

    }

}