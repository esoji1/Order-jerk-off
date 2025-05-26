using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets._Project.Scripts.Core
{
    public class FPSCounter : MonoBehaviour
    {
        public float updateInterval = 0.5f; //How often should the number update
        public TextMeshProUGUI _fpsText;

        float accum = 0.0f;
        int frames = 0;
        float timeleft;
        float fps;

        private void Start()
        {
            timeleft = updateInterval;
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0)
            {
                fps = (accum / frames);
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;
            }

            _fpsText.text = fps.ToString("F0") + " FPS";
        }
    }
}
