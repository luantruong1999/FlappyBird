using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
   [SerializeField]private float speed;

   private void Start()
   {
      Invoke("Destroy",5f);
   }

   private void Update()
   {
      transform.position+=Vector3.left*speed*Time.deltaTime;
   }

   private void Destroy()
   {
      Destroy(gameObject);
   }
}
