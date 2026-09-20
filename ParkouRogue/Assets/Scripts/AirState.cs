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

        public bool SideMove(float side) { return false; }

        public void Jump()
        {
            if (player.hasAirJumped) return;
            player.hasAirJumped = true;
            player.AddImpulse(new Vector3(0f, 5f, 0f));
        }

        public void ClickDash()
        {
            if (player.hasClickDashed) return;
            player.hasClickDashed = true;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue(); 
            player.AddImpulse((Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f)) - player.transform.position).normalized * 10f);
        }

        public void Grounded()
        {
            player.hasAirJumped = false;
            player.hasWallJumped = false;
            player.hasClickDashed = false;
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