using EggCentric.QoL;
using UnityEngine;

namespace EggCentric.Samplers
{
    public class PingPongStrategy : IBorderStrategy
    {
        public float Process(float value, float limit)
        {
            var result = value.PingPong(limit);
            Debug.Log($"Time: {value}, limit: {limit}, result: {result}");
            return result;
        }
    }
}