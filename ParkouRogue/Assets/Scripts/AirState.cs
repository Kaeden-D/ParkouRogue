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

        public void Jump() { }

        public void Grounded()
        {
            player.ChangeSlow(1f);
            player.ChangeState(GetComponent<PassiveState>());
        }

    }

}