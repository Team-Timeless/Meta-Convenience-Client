using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonDebug : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text voiceDebugText;

    [SerializeField] private UnityEngine.UI.Text devicesInfoText;

    [SerializeField] public Photon.Voice.Unity.Recorder recorder = null;
    void Update()
    {
        if (Photon.Voice.Unity.UnityMicrophone.devices == null || Photon.Voice.Unity.UnityMicrophone.devices.Length == 0)
        {
            this.devicesInfoText.enabled = true;
            this.devicesInfoText.color = Color.red;
            this.devicesInfoText.text = "No microphone device detected!";
        }
        else if (Photon.Voice.Unity.UnityMicrophone.devices.Length == 1)
        {
            this.devicesInfoText.text = string.Format("Mic.: {0}", Photon.Voice.Unity.UnityMicrophone.devices[0]);
        }
        else
        {
            this.devicesInfoText.text = string.Format("Multi.Mic.Devices:\n0. {0} (active)\n", Photon.Voice.Unity.UnityMicrophone.devices[0]);
            for (int i = 1; i < Photon.Voice.Unity.UnityMicrophone.devices.Length; i++)
            {
                this.devicesInfoText.text = string.Concat(this.devicesInfoText.text, string.Format("{0}. {1}\n", i, Photon.Voice.Unity.UnityMicrophone.devices[i]));
            }
        }
        this.voiceDebugText.text = string.Format("Amp: avg. {0:0.000000}, peak {1:0.000000}", this.recorder.LevelMeter.CurrentAvgAmp, this.recorder.LevelMeter.CurrentPeakAmp);
    }
}
