using EggCentric.Infrastructure;
using System.Collections;
using UnityEngine;

namespace EggCentric.LifeCycleHandling
{
    public interface ICoroutineRunner : IService
    {
        public Coroutine StartCoroutine(IEnumerator routine);
        public void StopCoroutine(Coroutine routine);
        public void StopCoroutine(IEnumerator routine);
    }
}