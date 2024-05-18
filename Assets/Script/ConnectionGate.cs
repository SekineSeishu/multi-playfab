using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Interface;

namespace CustomConnectionHandler
{
    [RequireComponent(typeof(BoxCollider))]
    public class ConnectionGate : MonoBehaviour
    {
        public ConnectionData ConnectionData;
        private float _lastTimeInside;

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponentInParent<NetworkObject>().HasInputAuthority)
            {
                _lastTimeInside = Time.time;
                InterfaceManager.Instance.GateUI.ShowGate(ConnectionData);
            }
        }

        private void LateUpdate()
        {
            if (Time.time - _lastTimeInside >= .5f)
            {
                InterfaceManager.Instance.GateUI.Defocus();
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
