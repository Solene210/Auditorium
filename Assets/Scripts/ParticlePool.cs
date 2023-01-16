using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    #region Expose

    [SerializeField]
    GameObject _particlePrefab;
    [SerializeField]
    int _maxParticleNumber;
    #endregion

    #region Unity Lyfecycle
    private void Awake()
    {
        _particles = new GameObject[_maxParticleNumber];
        for (int i = 0; i < _maxParticleNumber; i++)
        {
            _particles[i] = Instantiate(_particlePrefab, transform);
            _particles[i].SetActive(false);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
    #endregion

    #region Methods

    public GameObject GetParticle()
    {
        for (int i = 0; i < _maxParticleNumber; i++)
        {
            if (!_particles[i].activeInHierarchy)
            {
                return _particles[i];
            }
        }
        return null;
    }

    #endregion

    #region Private & Protected

    private GameObject[] _particles;

    #endregion
}
