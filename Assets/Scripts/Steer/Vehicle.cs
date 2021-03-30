namespace MyAI
{
    using UnityEngine;
    using System.Collections;
//using System.Collections.Generic;

    public class Vehicle : MonoBehaviour
    {

        //角色包含的操控行为列表
        private Steering[] steerings;

        //最大速度
        public float maxSpeed = 10;

        //能施加的最大的力
        public float maxForce = 100;

        protected float sqrMaxSpeed;

        //角色的质量
        public float mass = 1;

        //角色当前速度
        public Vector3 velocity;

        //控制转向时的速度
        public float damping = 0.9f;

        //操控力的计算间隔，为达到更大的帧率，不需要每帧更新
        public float computeInterval = 0.2f;

        //是否在二维平面，如果是，忽略y值
        public bool isPlanar = true;

        private Vector3 steeringForce;

        //角色当前的加速度
        protected Vector3 acceleration;

        private float timer;


        protected void Start()
        {
            steeringForce = new Vector3(0, 0, 0);
            sqrMaxSpeed = maxSpeed * maxSpeed;
            timer = 0;

            steerings = GetComponents<Steering>();
        }


        void Update()
        {
            timer += Time.deltaTime;
            steeringForce = Vector3.zero;
            //ticked part, we will not compute force every frame
            if (timer > computeInterval)
            {
                foreach (Steering s in steerings)
                {
                    if (s.enabled)
                        steeringForce += s.Force() * s.weight;
                }

                steeringForce = Vector3.ClampMagnitude(steeringForce, maxForce);
                acceleration = steeringForce / mass;

                timer = 0;
            }
        }
    }

}