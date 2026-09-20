using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{

    public class WallState : MonoBehaviour, PlayerState
    {

        private PlayerController player;

        public void Handle(PlayerController controller)
        {
            if (!player)
            {
                player = controller;
            }
        }

        public bool SideMove(float side)
        {
            if (!Mathf.Approximately(side, 0f))
            {
                if (Mathf.Sign(side) == Mathf.Sign(player.isWalled))
                {
                    return player.wallCling = true;
                }
                else
                {
                    player.isWalled = 0;
                    player.ChangeState(GetComponent<AirState>());
                    return player.wallCling = false;
                }
            }
            player.isWalled = 0;
            return player.wallCling = false;
        }

        public void Jump()
        {
            if (player.hasWallJumped) return;
            short wallSide = player.isWalled;
            player.wallCling = false;
            player.hasWallJumped = true;
            player.isWalled = 0;
            player.AddImpulse(new Vector3(-5f * wallSide, 10f, 0f));
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
            player.wallCling = false;
            player.hasAirJumped = false;
            player.hasWallJumped = false;
            player.hasClickDashed = false;
            player.ChangeState(GetComponent<PassiveState>());
        }

        public void Walled()
        {
            if (player.wallCling)
            {
                player.StickToWall();
            }
            else
            {
                player.ChangeState(GetComponent<AirState>());
            }
        }

    }

}