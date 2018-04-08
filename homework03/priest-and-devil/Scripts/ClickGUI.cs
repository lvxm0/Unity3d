using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;

namespace homework
{
    public class ClickGUI : MonoBehaviour
    {
        UserAction action;
      //  CCActionManager actionManager;
        MyCharacterController characterController;

        public void setController(MyCharacterController characterCtrl)
        {
            characterController = characterCtrl;
        }

        void Start()
        {
            action = Director.getInstance().currentSceneController as UserAction;
           // actionManager = Director.getInstance().currentSceneController as CCActionManager;
        }

        void OnMouseDown()
        {
            if (gameObject.name == "boat")
            {
                action.moveBoat();

            }
            else
            {
                action.characterIsClicked(characterController);
               // actionManager.Update();
            }
        }
    }

}
