using System.Collections;
using UnityEngine;

namespace EggCentric.Infrastructure
{
    public interface ICoroutineRunner : IService
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine routine);
        public void StopCoroutine(IEnumerator routine);
    }
}