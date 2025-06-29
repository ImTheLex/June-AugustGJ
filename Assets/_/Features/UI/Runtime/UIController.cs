using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Shared;
using UnityEngine;
using UnityEngine.Events;
using GameManager = Game.GameManager;

//using SaveSystem = SaveSystem;

namespace UI.Runtime
{
    public class UIController : Universe
    {
        public UnityEvent m_saveEvent;
        public UnityEvent m_quitEvent;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private GameObject _pauseMenu;
        //[SerializeField] private SaveSystem _saveSystem;
        [SerializeField] private DialogUI _dialogueUI;
        [SerializeField] private GameManager _gameManager;
        
        
        //[SerializeField] private GameObject _intro;

        private void Awake()
        {
            _inputReader.Initialize();
            _inputReader.EnableUIMap();
            _inputReader.PauseEvent += Pause;
            _inputReader.ContinueEvent += Continue;
            _pauseMenu.SetActive(false);
            
            //_saveSystem.m_onRegisterPlayerEvent.AddListener(OnRegisterPlayerComplete);
            //saveSystem.m_onDataFoundEvent.AddListener(DisplayData);
        }


        private void Pause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _inputReader.EnableUIMap();
            }
            else
            {
                _inputReader.EnablePlayerMap();
            }
            _pauseMenu.SetActive(_isPaused);

        }


        public void Save()
        {
            m_saveEvent.Invoke();
            //_saveSystem.Save();
        }

        public void Quit()
        {
            m_quitEvent.Invoke();
            //_gameManager.Quit();
        }
        private void DisplayData()
        {
            
        }
        public void OnRegisterPlayerComplete()
        {
            //_intro.SetActive(false);
        }
        public void Continue()
        {
            _dialogueUI.NextLine();
        }

        private void Update()
        {
            if (_gameManager.m_canPlay == true && _isPlaying == false)
            {
                //_intro.SetActive(false);
                foreach (var go in _deactivateObjects)
                {
                    go.SetActive(false);
                }
                //_loadData.SetActive(false);
                _inputReader.EnablePlayerMap();
                _isPlaying = true;
            }
        }
        [SerializeField] List<GameObject> _deactivateObjects = new List<GameObject>();
        private bool _isPaused = false;
        private bool _isPlaying = false;
        //[SerializeField] private GameObject _loadData;
    }
}
