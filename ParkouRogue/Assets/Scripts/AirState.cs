using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{

    public class AirState : MonoBehaviour, PlayerState
    {

        private PlayerController player;
        private bool hasAirJumped = false;

        public void Handle(PlayerController controller)
        {
            if (!player)
            {
                player = controller;
            }
            player.ChangeSlow(0.5f);
        }

        public bool SideMove(float side) { return false; }

        public void Jump()
        {
            if (hasAirJumped) return;
            hasAirJumped = true;
            player.AddImpulse(new Vector3(0f, 5f, 0f));
        }

        public void Grounded()
        {
            hasAirJumped = false;
            player.ChangeSlow(1f);
            player.ChangeState(GetComponent<PassiveState>());
        }

        public void Walled()
        {
            player.ChangeSlow(1f);
            player.ChangeState(GetComponent<WallState>());
        }

    }

}