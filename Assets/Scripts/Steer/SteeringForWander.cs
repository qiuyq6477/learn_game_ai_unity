using UnityEngine;

namespace MyAI
{
    using System;
    using UnityEngine;
    using System.Collections;
    using Random = Random;

//随机徘徊，与帧率有关
    public class SteeringForWander : Steering
    {

        //徘徊半径
        public float wanderRadius;

        //徘徊距离
        public float wanderDistance;

        //每秒加到目标的随机位移最大值
        public float wanderJitter;

        public bool isPlanar;
        //public GameObject targetIndicator;

        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;
        private Vector3 circleTarget;
        private Vector3 wanderTarget;


        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            maxSpeed = m_vehicle.maxSpeed;
            isPlanar = m_vehicle.isPlanar;
            //选取圆圈上的一个点作为初始点
            circleTarget = new Vector3(wanderRadius * 0.707f, 0, wanderRadius * 0.707f);
        }


        public override Vector3 Force()
        {
            //计算随机位移，([-wanderJitter, wanderJitter], [-wanderJitter, wanderJitter], [-wanderJitter, wanderJitter])
            Vector3 randomDisplacement = new Vector3((Random.value - 0.5f) * 2 * wanderJitter,
                (Random.value - 0.5f) * 2 * wanderJitter, (Random.value - 0.5f) * 2 * wanderJitter);

            if (isPlanar)
                randomDisplacement.y = 0;
            //把随机位移附加到初始点上
            circleTarget += randomDisplacement;
            //重新投影到圆上
            circleTarget = wanderRadius * circleTarget.normalized;

            wanderTarget = m_vehicle.velocity.normalized * wanderDistance + circleTarget + transform.position;

            desiredVelocity = (wanderTarget - transform.position).normalized * maxSpeed;
            return (desiredVelocity - m_vehicle.velocity);
        }

        private void OnDrawGizmos()
        {
            m_vehicle = GetComponent<Vehicle>();
            Gizmos.DrawWireSphere(m_vehicle.velocity.normalized * wanderDistance + transform.position, wanderRadius);
            // Gizmos.DrawLine(m_vehicle.velocity.normalized * wanderDistance + transform.position, m_vehicle.velocity.normalized * wanderDistance + circleTarget + transform.position);
            Gizmos.DrawLine(transform.position, wanderTarget);
        }
    }

}