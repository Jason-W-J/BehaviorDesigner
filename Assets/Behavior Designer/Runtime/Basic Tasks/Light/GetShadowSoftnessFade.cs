#if UNITY_4_6 || UNITY_4_7
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityLight
{
    [TaskCategory("Basic/Light")]
    [TaskDescription("Stores the color of the light.")]
    public class GetShadowSoftnessFade : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [RequiredField]
        [Tooltip("The color to store")]
        public SharedFloat storeValue;

        // cache the light component
        private Light light;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                light = currentGameObject.GetComponent<Light>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (light == null) {
                UnityEngine.Debug.LogWarning("Light is null");
                return TaskStatus.Failure;
            }

            storeValue = light.shadowSoftnessFade;
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            storeValue = 0;
        }
    }
}
#endif