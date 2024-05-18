using CustomConnectionHandler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Device;

namespace Interface
{
    public class InterfaceManager : MonoBehaviour
    {
        public static InterfaceManager Instance;

        public ConnectionGateUI GateUI;
        public DungeonLobbyUI DungeonLobbyUI;
        public MessageUI MessageUI;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void ClearInterface()
        {
            UIScreen.activeScreen.Defocus();
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

