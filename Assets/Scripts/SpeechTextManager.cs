using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.Android;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class SpeechTextManager : MonoBehaviour
{

    [SerializeField] private string language = "es-ES";
    [SerializeField] private Text uIText;

    [Serializable]

    public struct VoiceCommand
    {
        public string Keyword;
        public UnityEvent Response;
    }

    public VoiceCommand[] voiceCommands;

    private Dictionary<string, UnityEvent> commands = new Dictionary<string, UnityEvent>();

    private void Awake()
    {
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
        foreach (var command in voiceCommands)
        {
            commands.Add(command.Keyword.ToLower(), command.Response);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TextToSpeech.Instance.Setting(language, 1, 1);
        SpeechToText.Instance.Setting(language);

        SpeechToText.Instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.Instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.Instance.onDoneCallback = OnSpeakStop;
#if UNITY_ANDROID
        SpeechToText.Instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
    }

    // Speech to Text

    public void StartListening()
    {
        SpeechToText.Instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.Instance.StopRecording();
    }

    public void OnFinalSpeechResult(string result)
    {
        uIText.text = result;
        if (result != null)
        {
            var response = commands[result.ToLower()];
            if (response != null)
            {
                response?.Invoke();
            }
        }
    }

    public void OnPartialSpeechResult(string result)
    {
        uIText.text = result;
    }

    // Text to Speech

    public void StartSpeaking(string message)
    {
        TextToSpeech.Instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.Instance.StopSpeak();
    }

    // Estas funciones se pueden llamar para realizar otras acciones

    public void OnSpeakStart()
    {
        Debug.Log("Empezando a hablar...");
    }

    public void OnSpeakStop()
    {
        Debug.Log("Parar de hablar...");
    }
}
