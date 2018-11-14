using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    private static AudioMaster _instance;
    public static AudioMaster Instance
    {
        get
        {
            if (!(_instance is AudioMaster))
            {
                GameObject gO;
                _instance = FindObjectOfType<AudioMaster>();

                if (!(_instance is AudioMaster))
                {
                    gO = new GameObject("Audio Master");
                    _instance = gO.AddComponent<AudioMaster>();
                }
                else
                {
                    gO = _instance.gameObject;
                }
                DontDestroyOnLoad(gO);
            }
            return _instance;
        }
    }
#pragma warning disable 414
	uint bankID = 0;
#pragma warning restore 414
	int bankIDUnload;
    public bool loadBankByScript;
    public string soundBankName;
    public bool mute;
    private static AudioMaster instance;
    float lastBeat;

    public delegate void OnBeat(float tempo);
#pragma warning disable 67
	public event OnBeat OnBeatEvent;

    public delegate void OnBar();
    public event OnBar OnBarEvent;
#pragma warning restore 67

	private void Awake()
    {
        if (_instance is AudioMaster)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = Instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    //private void OnDisable()
    //{
    //    AkSoundEngine.UnloadBank(soundBankName, System.IntPtr.Zero, out bankIDUnload);
    //}
    
    //public void Start()
    //{
    //    if (bankID == 0 || bankIDUnload == 0) { }//I don't like warnings

    //    if (loadBankByScript)
    //    {
    //        AkSoundEngine.LoadBank(soundBankName, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    //    }

    //    if (mute)
    //    {
    //        AkSoundEngine.SetVolumeThreshold(0.0f);
    //    }
    //}

    //public void PlayEvent(string eventName, GameObject gameObj)
    //{
    //    AkSoundEngine.PostEvent(eventName, gameObj);
    //}

    //public void CallEvent(string eventName)
    //{
    //    AkSoundEngine.PostEvent(eventName, gameObject);
    //}

    //public void StopEvent(string eventName, GameObject gameObj, int fadeout)
    //{
    //    uint eventID;
    //    eventID = AkSoundEngine.GetIDFromString(eventName);
    //    AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObj, fadeout, AkCurveInterpolation.AkCurveInterpolation_Sine);
    //}

    //public void PauseEvent(string eventName, int fadeout)
    //{
    //    uint eventID;
    //    eventID = AkSoundEngine.GetIDFromString(eventName);
    //    AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
    //}

    //public void ResumeEvent(string eventName, int fadeout)
    //{
    //    uint eventID;
    //    eventID = AkSoundEngine.GetIDFromString(eventName);
    //    AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, fadeout * 1000, AkCurveInterpolation.AkCurveInterpolation_Sine);
    //}

    //public void SetSwitch(string switchGroupName, string switchName, GameObject gameObj)
    //{
    //    AkSoundEngine.SetSwitch(switchGroupName, switchName, gameObj);
    //}

    //public void SetRTPCValue(string rtpcName, float value, GameObject gameObj)
    //{
    //    AkSoundEngine.SetRTPCValue(rtpcName, value, gameObj);
    //}

    //public void PlayEventMusicCallback(string eventName, GameObject gameObj)
    //{
    //    AkSoundEngine.PostEvent(eventName, gameObj, (uint)AkCallbackType.AK_MusicSyncBeat | (uint)AkCallbackType.AK_MusicSyncBar, MusicCallback, null);
    //}

    //void MusicCallback(object in_cookie, AkCallbackType in_eType, AkCallbackInfo in_pCallbackInfo)
    //{
    //    AkMusicSyncCallbackInfo musicInfo = in_pCallbackInfo as AkMusicSyncCallbackInfo;
    //    if (in_eType == AkCallbackType.AK_MusicSyncBeat)
    //    {
    //        if (OnBeatEvent is OnBeat)
    //        {
    //            OnBeatEvent(musicInfo.segmentInfo_fBeatDuration);
    //        }
    //    }
    //    if (in_eType == AkCallbackType.AK_MusicSyncBar)
    //    {
    //        if (OnBarEvent is OnBar)
    //        {
    //            OnBarEvent();
    //        }
    //    }
    //}

    void CalculateTempo(float beatTime)
    {
        if (lastBeat != 0.0f)
        {
            beatTime = beatTime - lastBeat;
            print(60.0f / beatTime);
        }
        lastBeat = Time.time;
    }
}