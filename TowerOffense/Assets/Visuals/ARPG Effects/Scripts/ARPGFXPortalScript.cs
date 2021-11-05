using UnityEngine;
using System.Collections;
using NaughtyAttributes;

namespace ARPGFX
{


    public class ARPGFXPortalScript : MonoBehaviour
    {

        public GameObject portalOpenPrefab;
        public GameObject portalIdlePrefab;
        public GameObject portalClosePrefab;
        private GameObject portalOpen;
        private GameObject portalIdle;
        private GameObject portalClose;

        public float portalLifetime = 4.0f;

        bool _shouldClose = false;
        public bool ShouldClose
        {
            get
            {
                return _shouldClose;
            }
            set
            {
                this._shouldClose = value;
                if (!this._shouldClose) StartPortalSequence();
            }
        }


        void Start()
        {
            portalOpen = Instantiate(portalOpenPrefab, transform.position, transform.rotation);
            portalIdle = Instantiate(portalIdlePrefab, transform.position, transform.rotation);
            portalIdle.SetActive(false);
            portalClose = Instantiate(portalClosePrefab, transform.position, transform.rotation);
            portalClose.SetActive(false);

            InvokeRepeating(nameof(CheckIfShouldClose), 0.0f, 0.5f);


        }

        [Button("Start Sequence")]
        public void StartPortalSequence()
        {
            ShouldClose = false;
            StartCoroutine("PortalLoop");
        }
        [Button("Stop Sequence")]
        public void StopPortalSequence() => ShouldClose = true;
        IEnumerator PortalLoop()
        {
            while (!ShouldClose)
            {
                if (!portalOpen.activeSelf) portalOpen.SetActive(true);


                yield return new WaitForSeconds(0.8f);

                portalIdle.SetActive(true);

                yield return new WaitForSeconds(portalLifetime);

            }
        }
        void CheckIfShouldClose()
        {
            if (ShouldClose)
            {
                portalOpen.SetActive(false);
                portalIdle.SetActive(false);
                portalClose.SetActive(true);
            }
        }
    }

}