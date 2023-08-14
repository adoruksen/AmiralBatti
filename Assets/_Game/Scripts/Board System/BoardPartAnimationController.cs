using System;
using UnityEngine;

namespace BoardSystem
{
    public class BoardPartAnimationController : MonoBehaviour
    {
        [SerializeField] private GameObject miss;
        [SerializeField] private GameObject hit;
        [SerializeField] private GameObject boardPart;


        public void HitHandle()
        {
            hit.SetActive(true);            
        }

        public void MissHandle()
        {
            miss.SetActive(true);
        }

        public void SinkHandle()
        {
            boardPart.SetActive(false);
        }
    }
}

