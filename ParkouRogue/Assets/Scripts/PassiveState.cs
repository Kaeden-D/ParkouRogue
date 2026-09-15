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
            player.AddForce(new Vector3(0f, 500f, 0f));
            player.ChangeState(GetComponent<AirState>());
        }

    }

}