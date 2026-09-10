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

        void Update()
        {

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                player.addForce(new Vector3(0f, 100f, 0f));
            }

        }

    }

}