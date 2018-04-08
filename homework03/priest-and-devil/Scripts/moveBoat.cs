using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using homework;

namespace homework
{
    //动作类的实现，通过Vector3.MoveTowards完成每帧位置变换。实现一系列动作的类执行更新
    public class moveBoat : SSAction
    {
        public Vector3 target;
        public float speed;

        private moveBoat() { }
        public override void Start()
        {
            //
        }
        public static moveBoat getAction(Vector3 target, float speed)
        {
            moveBoat action = ScriptableObject.CreateInstance<moveBoat>();
            action.target = target;
            action.speed = speed;
            return action;
        }

        public override void Update()
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            if (this.transform.position == target)
            {
                this.destroy = true;
                this.callback.actionDone(this);
            }
        }
    }

    public class SequenceAction : SSAction, ISSActionCallback
    {
        public List<SSAction> sequence;
        public int repeat = 1; // 1->only do it for once, -1->repeat forever
        public int currentActionIndex = 0;

        public static SequenceAction getAction(int repeat, int currentActionIndex, List<SSAction> sequence)
        {
            SequenceAction action = ScriptableObject.CreateInstance<SequenceAction>();
            action.sequence = sequence;
            action.repeat = repeat;
            action.currentActionIndex = currentActionIndex;
            return action;
        }

        public override void Update()
        {
            if (sequence.Count == 0) return;
            if (currentActionIndex < sequence.Count)
            {
                sequence[currentActionIndex].Update();
            }
        }

        public void actionDone(SSAction source)
        {
            source.destroy = false;
            this.currentActionIndex++;
            if (this.currentActionIndex >= sequence.Count)
            {
                this.currentActionIndex = 0;
                if (repeat > 0) repeat--;
                if (repeat == 0)
                {
                    this.destroy = true;
                    this.callback.actionDone(this);
                }
            }
        }

        public override void Start()
        {
            foreach (SSAction action in sequence)
            {
                action.gameobject = this.gameobject;
                action.transform = this.transform;
                action.callback = this;
                action.Start();
            }
        }

        void OnDestroy()
        {
            foreach (SSAction action in sequence)
            {
                DestroyObject(action);
            }
        }
    }
}
