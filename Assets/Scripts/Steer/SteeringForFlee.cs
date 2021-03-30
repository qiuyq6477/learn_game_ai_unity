namespace MyAI
{
    using UnityEngine;
    using System.Collections;

//离开
    public class SteeringForFlee : Steering
    {

        public GameObject target;

        //逃跑范围，在这个范围内才会逃跑
        public float fearDistance = 20;

        private Vector3 desiredVelocity;
        private Vehicle m_vehicle;
        private float maxSpeed;


        void Start()
        {
            m_vehicle = GetComponent<Vehicle>();
            maxSpeed = m_vehicle.maxSpeed;
        }


        public override Vector3 Force()
        {
            //忽略y值
            Vector3 tmpPos = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 tmpTargetPos = new Vector3(target.transform.position.x, 0, target.transform.position.z);

            if (Vector3.Distance(tmpPos, tmpTargetPos) > fearDistance)
                return new Vector3(0, 0, 0);

            desiredVelocity = (transform.position - target.transform.position).normalized * maxSpeed;
            return (desiredVelocity - m_vehicle.velocity);
        }
    }

}