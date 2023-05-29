using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder
{
    public class SingletonMono<T> : MonoBehaviour
        where T : Component
    {
        public static T Instance;

        public virtual void Awake()
        {
            Singleton();
        }

        public void Singleton()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public class SceneDataPersist<T> : MonoBehaviour
        where T : Component
    {
        public static T Instance;

        public virtual void Awake()
        {
            DataPersist();
        }

        private void DataPersist()
        {
            if (Instance != null && Instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }

}
