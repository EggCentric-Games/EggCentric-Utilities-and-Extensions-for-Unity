using System.Collections;
using UnityEngine;

namespace EggCentric.Infrastructure
{
    public interface ICoroutineRunner : IService
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
    }
}