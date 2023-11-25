using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public static GameManager instance;


        private void Awake()
        {
            instance = this;

        }

        public Transform GetPlayer()
        {
            return target;
        }
    }
}
