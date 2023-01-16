using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicBoxManager : MonoBehaviour
{
    #region Expose

    [SerializeField]
    float _volumeUpPerParticle;
    [SerializeField]
    float _volumeDecayPerSecond;
    [SerializeField]
    float _volumeDecayDelay;

    //Changement de couleur des barres de son
    [SerializeField]
    SpriteRenderer[] _volumeBars;
    [SerializeField]
    Color _enabledColor;
    [SerializeField]
    Color _disabledColor;

    [SerializeField]
    GameObject _victoryUI;

    [SerializeField]
    int _musicBoxCount;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    void Start()
    {
       Time.timeScale = 1;
        _musicBoxCount = GameObject.FindGameObjectsWithTag("MusicBox").Length;
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > _startDecayTime)
        {
            _volume -= _volumeDecayPerSecond * Time.deltaTime;
            _volume = Mathf.Clamp01(_volume);
        }
        _audio.volume = _volume;

        UpdateRenderer();
        CheckVictory();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _volume += _volumeUpPerParticle;
        //_volume = Mathf.Min(_volume + _volumeUpPerParticle, 1);
        //_volume = Mathf.Min(_volume, 1);
        _volume = Mathf.Clamp01(_volume);

        _startDecayTime = Time.timeSinceLevelLoad + _volumeDecayDelay;
    }
    #endregion

    #region Methods

    void UpdateRenderer()
    {
        int numberBarsToEnabled = Mathf.FloorToInt(_volumeBars.Length * _volume); //+1 pour arrondir au supérieur
        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if (i < numberBarsToEnabled)
            {
                _volumeBars[i].color = _enabledColor;
            }
            else
            {
                _volumeBars[i].color = _disabledColor;
            }
        }
    }

    void CheckVictory()
    {
        int quantity = 0;
        int victory = 0;
        foreach (GameObject musicBox in GameObject.FindGameObjectsWithTag("MusicBox"))
        {
            quantity++;
            if(musicBox.GetComponent<AudioSource>().volume == 1)
            {
                victory++;
            }
        }
        if (quantity == victory)
        {
            Time.timeScale = 0;
            _victoryUI.SetActive(true);
        }
    }


    #endregion

    #region Private & Protected

    private float _volume;

    private float _startDecayTime;

    private AudioSource _audio;

    #endregion
}
