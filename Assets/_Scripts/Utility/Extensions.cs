using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Utility
{
    public static class Extensions
    {
        public static void Trigger(this ISubject<Unit> subject)
        {
            subject.OnNext(Unit.Default);
        }

        public static IDisposable Subscribe<T>(this UniRx.IObservable<T> source, Action action)
        {
            return source.Subscribe(_ => action());
        }

        public static void Times(this int amount, Action<int> action)
        {
            for (var i = 0; i < amount; i++)
                action(i);
        }

        public static float RandomValue(this Vector2 range)
        {
            return range.x + Random.value * (range.y - range.x);
        }
    }
}