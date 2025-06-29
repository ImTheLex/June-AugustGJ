using System.Collections;
using System.Collections.Generic;
using PlasticGui;
using Player.Runtime;
using Shared;
using UnityEngine;

namespace Pool.Runtime
{
    public class PoolManager : Universe
    {
        
        [Header("Player")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private int _playerPoolSize = 4;
        [SerializeField] private Transform _playerPoolPositions;
        
        public static PoolManager Instance;
        public List<GameObject> m_playerList = new List<GameObject>();
        
        private PoolSystem _playerPool;

        private void Awake()
        {
            Instance = this;
            
            var playerComponent = _playerPrefab;
            m_playerList.Add(playerComponent);
            _playerPool = new PoolSystem(playerComponent, _playerPoolSize, _playerPoolPositions);
            
        }

        public GameObject GetPlayer()
        {
            Debug.Log("GetPlayer");
            return _playerPool.Get();
        }

        public void ReturnPlayer(GameObject player)
        {
            _playerPool.ReturnToPool(player);
        }
    }

}
