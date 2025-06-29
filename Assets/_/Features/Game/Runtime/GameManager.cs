using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Player.Runtime;
using Pool.Runtime;
using Shared;
using Unity.Cinemachine;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

namespace Game
{
    public class GameManager : Universe
    {
        public bool m_canPlay;
        [SerializeField] private List<Transform> _playersSpawns;

        [SerializeField]
        //private SaveSystem _saveSystem;
        public CinemachineCamera _cinemachine;
        
        void Awake()
        {
            
            /*_saveSystem.m_onRegisterPlayerEvent.AddListener(StartGame);
            _saveSystem.m_onDataFoundEvent.AddListener(OnDataFound);
            _saveSystem.m_onDataSet.AddListener(StartGame);*/
            DontDestroyOnLoad(gameObject);

        }

        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            for (int i = 0; i < PoolManager.Instance.m_playerList.Count; i++)
            {
                var go = PoolManager.Instance.GetPlayer();
                //var pd = _saveSystem.LoadPlayerData();
                //var psd = _saveSystem.LoadSelectedData();

                /*var pd = go.AddComponent<PlayerData>();
                InitializePlayerConfig(pd,psd);
                go.GetComponent<PlayerBehaviour>().InitializePlayer();*/
                
                var pc = go.GetComponent<PlayerController>();
                //pc._InputDebug = m_inputDebug;
                pc._camera = _cinemachine.transform;
                _cinemachine.Follow = go.transform;
                _cinemachine.LookAt = go.transform;

                go.transform.position = _playersSpawns[i].position;
                //go.name = pd.m_playerName;
                //InitializeVirtualCamera(go);
                go.SetActive(true);
                m_canPlay = true;
                //go.name = "Player " + (i + 1);
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
        /*private void InitializeVirtualCamera(GameObject go)
        {
            _cinemachine.Follow = go.transform;
            _cinemachine.LookAt = go.transform;
        }
        private void InitializePlayerConfig(PlayerData pd,PlayerSaveData psd)
        {
            pd.m_playerName = psd.m_playerName;
            pd.m_playerUUID = psd.m_playerUUID;
            pd.m_timePlayed = psd.m_timePlayed;
            pd.m_playerPosition = psd.m_playerPosition;
        }
        */
    }
}
