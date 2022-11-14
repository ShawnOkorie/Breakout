using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Goal : MonoBehaviour
    {
        private Rigidbody2D myRigidbody2D;
        private object z;

        private void Awake()
        {
            myRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            print("Player" + z + " Goal");
        }
    }

