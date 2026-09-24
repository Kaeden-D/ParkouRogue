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

        public void AxisDash(float vert, float side)
        {
            if (player.hasAxisDashed || (vert == 0f && side == 0f)) return;
            //Skip if the player's axis dash has not reset yet or if the dash direction is zero

            player.hasAxisDashed = true;
            player.axisDashTime = Time.time;

            if (Mathf.Sign(vert) != Mathf.Sign(player.GetVerticalVelocity())) player.VerticalStop();
            if (Mathf.Sign(side) != Mathf.Sign(player.GetHorizontalVelocity())) player.HorizontalStop();
            //Stops the player from moving in the opposite direction of the dash, to prevent the dash from being cancelled out by existing momentum

            player.AddImpulse((new Vector3(side, vert, 0f)).normalized * 10f);
            player.ChangeState(GetComponent<AirState>());
        }

        public void ClickDash()
        {
            if (player.hasClickDashed) return;
            //Skip if the player's click dash has not reset yet

            player.hasClickDashed = true;
            player.clickDashTime = Time.time;

            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 direction = (Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)) - player.transform.position).normalized;
            //Get the mouse position in screen coordinates, and convert it into the direction from the player

            if (Mathf.Sign(direction.y) != Mathf.Sign(player.GetVerticalVelocity())) player.VerticalStop();
            if (Mathf.Sign(direction.x) != Mathf.Sign(player.GetHorizontalVelocity())) player.HorizontalStop();
            //Stops the player from moving in the opposite direction of the dash, to prevent the dash from being cancelled out by existing momentum

            player.AddImpulse(direction * 10f);
        }

        public void Grounded()
        {
            player.AbilityReset();
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