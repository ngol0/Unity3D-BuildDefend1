using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Lam.DefenderBuilder.Enemies
{
    public enum PathType
    {
        DIRT,
        SNOW
    }

    [RequireComponent(typeof(Enemy))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] List<Waypoints> path = new List<Waypoints>();

        Enemy enemy;
        [SerializeField] PathType pathType;

        [Header("Event to raise")]
        [SerializeField] IntEventChannel onEnemyFinishPath;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        void OnEnable()
        {
            FindPath();
            ReturnToStartPosition();
            StartCoroutine(FollowPath());
        }

        private void FindPath()
        {
            path.Clear();
            GameObject pathParent = GameObject.FindGameObjectWithTag(pathType.ToString());

            foreach (Transform child in pathParent.transform)
            {
                var waypoint = child.GetComponent<Waypoints>();
                if (waypoint) path.Add(waypoint);
            }
        }

        private void ReturnToStartPosition()
        {
            transform.position = path[0].transform.position;
        }

        private void FinishPath()
        {
            gameObject.SetActive(false);

            //cause damage to headquarter
            onEnemyFinishPath?.RaiseEvent(enemy.HitPoint);
        }

        IEnumerator FollowPath()
        {
            foreach (Waypoints waypoint in path)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = waypoint.transform.position;
                float travelPercent = 0;

                transform.LookAt(endPos);

                while (travelPercent <= 1)
                {
                    travelPercent += Time.deltaTime * enemy.Speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    //yield return new WaitForEndOfFrame();

                    //yield return null waits for next frame to execute
                    //time.deltaTime has very small value - which makes sense only within the context of a frame
                    //ie: move object at 0.01wu/frame at framerate 60 => speed = 0.6wu/second
                    //if move 0.01 wu/second => no movement almost

                    //when program starts the loop, if had no yield, it simply runs all iterations within one frame
                    //so if you put the loop in the update function, the target will immediately jump to the end pos when prog starts
                    yield return null;
                }
            }

            FinishPath();
        }
    }
}

