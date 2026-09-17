using UnityEngine;
using UnityEngine.InputSystem;

namespace Chapter.State
{

    public class WallState : MonoBehaviour, PlayerState
    {

        private PlayerController player;
        private bool hasWallJumped = false;

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
            if (hasWallJumped) return;
            player.wallCling = false;
            hasWallJumped = true;
            player.isWalled = 0;
            player.AddImpulse(new Vector3(-2f * player.isWalled, 10f, 0f));
            player.ChangeState(GetComponent<AirState>());
        }

        public void Grounded()
        {
            hasWallJumped = false;
            player.ChangeState(GetComponent<PassiveState>());
        }

        public void Walled() { }

    }

}