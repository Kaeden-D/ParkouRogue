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
            player.changeSlow(0.5f);
        }

        void Update()
        {

            if (!player.isFalling())
            {
                player.changeSlow(1f);
                player.changeState(GetComponent<PassiveState>());
            }

        }

    }

}