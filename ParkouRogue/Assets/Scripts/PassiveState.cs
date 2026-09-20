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

        public bool SideMove(float side) { return false; }

        public void Jump()
        {
            player.AddImpulse(new Vector3(0f, 10f, 0f));
            player.ChangeState(GetComponent<AirState>());
        }

        public void ClickDash()
        {
            if (player.hasClickDashed) return;
            player.hasClickDashed = true;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue(); 
            player.AddImpulse((Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f)) - player.transform.position).normalized * 10f);
            player.ChangeState(GetComponent<AirState>());
        }

        public void Grounded()
        {
            player.hasAirJumped = false;
            player.hasWallJumped = false;
            player.hasClickDashed = false;
        }

        public void Walled() { }

    }

}