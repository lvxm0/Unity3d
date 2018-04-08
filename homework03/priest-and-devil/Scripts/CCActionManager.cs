using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;

namespace homework
{
    public class CCActionManager : SSActionManager
    {
        public void moveboat(BoatController boat)
        {
            moveBoat action = moveBoat.getAction(boat.getDestination(), boat.movingSpeed);
            this.addAction(boat.getGameobj(), action, this);
        }

        public void moveCharacter(MyCharacterController characterCtrl, Vector3 destination)
        {
            Vector3 currentPos = characterCtrl.getPos();
            Vector3 middlePos = currentPos;
            if (destination.y > currentPos.y)
            {       //from low(boat) to high(coast)
                middlePos.y = destination.y;
            }
            else
            {   //from high(coast) to low(boat)
                middlePos.x = destination.x;
            }
            SSAction action1 = moveBoat.getAction(middlePos, characterCtrl.movingSpeed);
            SSAction action2 = moveBoat.getAction(destination, characterCtrl.movingSpeed);
            SSAction seqAction = SequenceAction.getAction(1, 0, new List<SSAction> { action1, action2 });
            this.addAction(characterCtrl.getGameobj(), seqAction, this);
        }

    }
}
