namespace MyAI
{
    using UnityEngine;
    using System.Collections;

//聚集
    public class SteeringForCohesion : Steering
    {

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
            Vector3 steeringForce = Vector3.zero;
            Vector3 centerOfMass = Vector3.zero;
            int neighborCount = 0;

            foreach (GameObject s in GetComponent<Radar>().neighbors)
            {
                if (s != null && s != gameObject)
                {
                    centerOfMass += s.transform.position;
                    neighborCount++;
                }
            }

            if (neighborCount > 0)
            {
                centerOfMass /= (float) neighborCount;
                desiredVelocity = (centerOfMass - transform.position).normalized * maxSpeed;
                steeringForce = desiredVelocity - m_vehicle.velocity;
            }

            return steeringForce;
        }
    }

}