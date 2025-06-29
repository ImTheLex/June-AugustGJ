using System;
using System.Collections;
using System.Collections.Generic;
using Shared;
using UnityEngine;

namespace Player.Runtime
{
    public class PlayerBehaviour : Universe
    {
        public string m_playerName;
        private string _playerUUID;
        private float _timePlayed;

        private void Awake()
        {
        }

        void Start()
        {
            //SaveSystem.Instance.m_onSaving.AddListener(SavePlayerData);
        }
        
        // Update is called once per frame
        void Update()
        {
            _timePlayed += Time.deltaTime;
        }

        private void OnEnable()
        {
        }

        /*public void InitializePlayer()
        {
            var pd = GetComponent<PlayerData>();
            m_playerName = pd.m_playerName;
            _playerUUID = pd.m_playerUUID;
            _timePlayed = pd.m_timePlayed;
            transform.position = pd.m_playerPosition;

        }

        public void SavePlayerData()
        {
            var pd = new PlayerSaveData();
            pd.m_playerName = m_playerName;
            pd.m_playerUUID = _playerUUID;
            pd.m_timePlayed = _timePlayed;
            pd.m_playerPosition = transform.position;
            SaveSystem.Instance.m_selectedSaveData = pd;
            
        }*/
    }
}
