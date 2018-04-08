using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;
namespace homework
{
    /*删除以前的移动方式 =w=
    public class Moveable : MonoBehaviour
    {
        private float movespeed = 20;// 私有

        int status;//0站立，1移动到折点，2为从折点移动到终点
                   //主要在上下船，先平移到边缘处，然后在下降高度。从（0,0）到（0,1）再到（1,1）
        Vector3 dest;
        Vector3 point;

        void Update ()
        {
            if(status == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, point, movespeed * Time.deltaTime);
                if(transform.position == point)
                {
                    status = 2;
                }
            }
            else if (status == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, dest, movespeed * Time.deltaTime);
                if (transform.position == dest)
                {
                    status = 0;
                }
            }
        }
        public void setdest(Vector3 des)
        {
            dest = des;//y为高度，x为横向距离
            point = des;//未改变之前，设为目的地（1,1）
            if(des.y == transform.position.y)//对象的高度没变,在船上
            {
                status = 2;//直接开向目的地

            } else if(des.y < transform.position.y)//高度下降，进船
            {
                point.y = transform.position.y;//（1,1)-> (0,1) ->（0,0）
            }
            else//高度上升，出船
            {
                point.x = transform.position.x;//(0,0)->(0,1)->(1,1)
            }
            status = 1;//已经确定了中间点，先前往中间点
        }
        public void reset()
        {
            status = 0;
        }
    }
    */
    public class MyCharacterController
    {
        readonly GameObject character;
       // readonly Moveable moveableScript;
        readonly ClickGUI clickGUI;
        readonly int characterType; // 0->牧师, 1->恶魔
        public readonly float movingSpeed = 20;

        // change frequently
        bool _isOnBoat;
        CoastController coastController;


        public MyCharacterController(string which_character)
        {

            if (which_character == "priest")
            {
                character = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 0;

            }
            else
            {
                character = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
                characterType = 1;
            }
           // moveableScript = character.AddComponent(typeof(Moveable)) as Moveable;

            clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
            clickGUI.setController(this);
        }

        public void setName(string name)
        {
            character.name = name;
        }

        public void setPosition(Vector3 pos)
        {
            character.transform.position = pos;
        }
        /*
        public void moveToPosition(Vector3 destination)
        {
            moveableScript.setdest(destination);
        }
        */

        public int getType()
        {  // 0->priest, 1->devil
            return characterType;
        }

        public string getName()
        {
            return character.name;
        }

        public void getOnBoat(BoatController boatCtrl)
        {
            coastController = null;
            character.transform.parent = boatCtrl.getGameobj().transform;
            _isOnBoat = true;
        }

        public void getOnCoast(CoastController coastCtrl)
        {
            coastController = coastCtrl;
            character.transform.parent = null;
            _isOnBoat = false;
        }

        public bool isOnBoat()
        {
            return _isOnBoat;
        }

        public CoastController getCoastController()
        {
            return coastController;
        }

        public void reset()
        {
           // moveableScript.reset();
            coastController = (Director.getInstance().currentSceneController as FirstController).fromCoast;
            getOnCoast(coastController);
            setPosition(coastController.getEmptyPosition());
            coastController.getOnCoast(this);
        }

        public Vector3 getPos()
        {
            return this.character.transform.position;
        }

        public GameObject getGameobj()
        {
            return this.character;
        }

    }



    // 对岸边的管理
    /*-----------------------------------CoastController------------------------------------------*/
    public class CoastController
    {
        readonly GameObject coast;
        readonly Vector3 from_pos = new Vector3(9, 1, 0);//起始岸的位置
        readonly Vector3 to_pos = new Vector3(-9, 1, 0);
        readonly Vector3[] positions;
        readonly int to_or_from;    // to->-1, from->1

        // change frequently
        MyCharacterController[] passengerPlaner;//6个空位

        public CoastController(int _to_or_from)
        {
            positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
            new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};

            passengerPlaner = new MyCharacterController[6];

            to_or_from = _to_or_from;

            if (_to_or_from == 1)
            {
                coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
                coast.name = "from";
                //to_or_from = 1;
            }
            else if(_to_or_from == -1)
            {
                coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
                coast.name = "to";
                //to_or_from = -1;
            }
        }

        public int getEmptyIndex()
        {
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos = positions[getEmptyIndex()];
            pos.x *= to_or_from;
            return pos;
        }

        public void getOnCoast(MyCharacterController characterCtrl)
        {
            int index = getEmptyIndex();
            passengerPlaner[index] = characterCtrl;
        }

        public MyCharacterController getOffCoast(string passenger_name)
        {   // 0->priest, 1->devil
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] != null && passengerPlaner[i].getName() == passenger_name)
                {
                    MyCharacterController charactorCtrl = passengerPlaner[i];
                    passengerPlaner[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("cant find passenger on coast: " + passenger_name);
            return null;
        }

        public int get_to_or_from()
        {
            return to_or_from;
        }

        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passengerPlaner.Length; i++)
            {
                if (passengerPlaner[i] == null)
                    continue;
                if (passengerPlaner[i].getType() == 0)
                {  // 0->priest, 1->devil
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }

        public void reset()
        {
            passengerPlaner = new MyCharacterController[6];
        }
    }

    //对船只的管理
    /*-----------------------------------BoatController------------------------------------------*/
    public class BoatController
    {
        readonly GameObject boat;
        //readonly Moveable moveableScript;
        readonly ClickGUI clickGUI;
        readonly Vector3 fromPosition = new Vector3(5, 1, 0);
        readonly Vector3 toPosition = new Vector3(-5, 1, 0);
        readonly Vector3[] from_positions;
        readonly Vector3[] to_positions;

        public readonly float movingSpeed = 15;

        // change frequently
        int to_or_from; // to->-1; from->1
        MyCharacterController[] passenger = new MyCharacterController[2];

        public BoatController()
        {
            to_or_from = 1;

            from_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
            to_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

            boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)), fromPosition, Quaternion.identity, null) as GameObject;
            boat.name = "boat";

           // moveableScript = boat.AddComponent(typeof(Moveable)) as Moveable;
            clickGUI = boat.AddComponent(typeof(ClickGUI)) as ClickGUI;
        }

        
        public void Move()
        {
            if (to_or_from == -1)
            {
               // moveableScript.setdest(fromPosition);
                to_or_from = 1;
            }
            else
            {
               // moveableScript.setdest(toPosition);
                to_or_from = -1;
            }
        }

        public Vector3 getDestination()
        {
            if (to_or_from == -1)
            {
                return fromPosition;
            }
            else
            {
                return toPosition;
            }
        }

        public int getEmptyIndex()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool isEmpty()
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public Vector3 getEmptyPosition()
        {
            Vector3 pos;
            int emptyIndex = getEmptyIndex();
            if (to_or_from == -1)
            {
                pos = to_positions[emptyIndex];
            }
            else
            {
                pos = from_positions[emptyIndex];
            }
            return pos;
        }

        public void GetOnBoat(MyCharacterController characterCtrl)
        {
            int index = getEmptyIndex();
            passenger[index] = characterCtrl;
        }

        public MyCharacterController GetOffBoat(string passenger_name)
        {
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] != null && passenger[i].getName() == passenger_name)
                {
                    MyCharacterController charactorCtrl = passenger[i];
                    passenger[i] = null;
                    return charactorCtrl;
                }
            }
            Debug.Log("Cant find passenger in boat: " + passenger_name);
            return null;
        }

        public GameObject getGameobj()
        {
            return boat;
        }

        public int get_to_or_from()
        { // to->-1; from->1
            return to_or_from;
        }

        public int[] getCharacterNum()
        {
            int[] count = { 0, 0 };
            for (int i = 0; i < passenger.Length; i++)
            {
                if (passenger[i] == null)
                    continue;
                if (passenger[i].getType() == 0)
                {    // 0->priest, 1->devil
                    count[0]++;
                }
                else
                {
                    count[1]++;
                }
            }
            return count;
        }

        public void reset()
        {
            //moveableScript.reset();
            if (to_or_from == -1)
            {
                Move();
            }
            passenger = new MyCharacterController[2];
        }
    }


}
