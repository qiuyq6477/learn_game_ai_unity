namespace MyAI
{
    using UnityEngine;
    using System.Collections;

    [RequireComponent(typeof(Radar))]
    public class SteeringForSeparation : Steering
    {

        //可接受的距离
        public float comfortDistance = 1;

        //距离过近时的惩罚因子
        public float multiplierInsideComfortDistance = 2;

        void Start()
        {

        }

        public override Vector3 Force()
        {
            Vector3 steeringForce = Vector3.zero;

            foreach (GameObject s in GetComponent<Radar>().neighbors)
            {
                if (s != null && s != gameObject)
                {
                    //计算与邻居之间的距离
                    Vector3 toNeighbor = transform.position - s.transform.position;
                    float length = toNeighbor.magnitude;
                    //计算邻居的操控里，与距离成反比
                    steeringForce += toNeighbor.normalized / length;
                    //如果小于可接受距离，需要更大的力来分离
                    if (length < comfortDistance)
                        steeringForce *= multiplierInsideComfortDistance;
                }
            }

            return steeringForce;
        }
    }

}