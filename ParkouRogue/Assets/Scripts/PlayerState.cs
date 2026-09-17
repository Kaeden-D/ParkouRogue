using UnityEngine;

namespace Chapter.State
{

    public interface PlayerState
    {

        public void Handle(PlayerController controller);
        public bool SideMove(float side);
        public void Jump();
        public void Grounded();
        public void Walled();

    }

}