﻿//
// Copyright © Daniel Shervheim, 2019
// www.danielshervheim.com
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Artngame.CommonTools
{
    public class Mover : MonoBehaviour
    {
        private float pitch = 0f;
        private float yaw = 0f;

        public float moveSpeed = 1f;
        public float lookSpeed = 1f;

        private bool locked = false;

        // Update is called once per frame
        void Update()
        {
            Cursor.visible = locked;

            if (Input.GetKeyUp("l"))
            {
                locked = !locked;
            }

            if (!locked)
            {
                Vector3 forward = Vector3.Normalize(Vector3.Scale(transform.forward, new Vector3(1f, 0f, 1f)));
                Vector3 right = Vector3.Normalize(Vector3.Scale(transform.right, new Vector3(1f, 0f, 1f)));
                transform.position += (forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal")) * moveSpeed * Time.deltaTime;


                float vertical = System.Convert.ToSingle(Input.GetKey(KeyCode.Space)) - System.Convert.ToSingle(Input.GetKey(KeyCode.LeftShift));
                transform.position += vertical * Vector3.up * moveSpeed * Time.deltaTime;
            }
        }

        void LateUpdate()
        {
            if (!locked)
            {
                pitch -= Input.GetAxis("Mouse Y") * lookSpeed;
                yaw += Input.GetAxis("Mouse X") * lookSpeed;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(pitch, yaw, 0f), Time.deltaTime * 10f);
            }
        }
    }
}