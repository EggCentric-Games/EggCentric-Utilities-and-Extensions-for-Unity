using System;
using Random = UnityEngine.Random;

public static class RandomExtensions
{
    public static T WithSeed<T>(Func<T> func, int seed)
    {
        var previousState = Random.state;
        Random.InitState(seed);
        var result = func();
        Random.state = previousState;

        return result;
    }
}