using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class CustomCoroutine : MonoBehaviour
    {
        public static CustomCoroutine Instance { get; private set; }

        public void WaitOnConditionThenExecute(Func<bool> condition, Action action)
        {
            StartCoroutine(DoWaitOnConditionThenExecute(condition, action));
        }
        private IEnumerator DoWaitOnConditionThenExecute(Func<bool> condition, Action action)
        {
            yield return new WaitUntil(() => condition());
            action();
        }

        public void WaitThenExecute(float delaySeconds, Action action)
        {
            StartCoroutine(DoWaitThenExecute(delaySeconds, action));
        }

        private IEnumerator DoWaitThenExecute(float delaySeconds, Action action)
        {
            if (delaySeconds <= 0)
                yield return new WaitForEndOfFrame();
            else
                yield return new WaitForSeconds(delaySeconds);

            action();
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }
}
