namespace MyAI
{
    using UnityEngine;
    using System.Collections;

//寻找行为
    public class SteeringForSeek : Steering
    {
        //寻找的目标
        public GameObject target;

        //期望速度
        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;
        private bool isPlanar;

        void Start()
        {
            //获取角色
            m_vehicle = GetComponent<Vehicle>();
            maxSpeed = m_vehicle.maxSpeed;
            isPlanar = m_vehicle.isPlanar;
        }


        public override Vector3 Force()
        {
            desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
            if (isPlanar)
                desiredVelocity.y = 0;
            //返回操控向量，预期速度和当前速度的差
            return (desiredVelocity - m_vehicle.velocity);
        }
    }


}