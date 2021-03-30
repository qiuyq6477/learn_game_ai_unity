namespace MyAI
{
    using UnityEngine;
    using System.Collections;

//追逐
    public class SteeringForPursuit : Steering
    {

        public GameObject target;
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
            Vector3 toTarget = target.transform.position - transform.position;
            //计算追逐者和目标朝向的夹角
            float relativeDirection = Vector3.Dot(transform.forward, target.transform.forward);

            if ((Vector3.Dot(toTarget, transform.forward) > 0) && (relativeDirection < -0.95f))
            {
                //不进行预测
                desiredVelocity = (target.transform.position - transform.position).normalized * maxSpeed;
                return (desiredVelocity - m_vehicle.velocity);
            }

            //计算预测时间，正比于追逐者与逃避者的距离，反比于追逐者和目标的速度和
            float lookaheadTime = toTarget.magnitude / (maxSpeed + target.GetComponent<Vehicle>().velocity.magnitude);

            desiredVelocity =
                (target.transform.position + target.GetComponent<Vehicle>().velocity * lookaheadTime -
                 transform.position).normalized * maxSpeed;
            return (desiredVelocity - m_vehicle.velocity);

        }
    }

}