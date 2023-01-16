using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticulesSpawner : MonoBehaviour
{
    #region Expose

    [Header("Donn�es des particules")]
    [SerializeField] 
    private ParticlePool _pool;
    [SerializeField]
    [Tooltip("Drag de la particule")]
    private float _particleDrag;
    [SerializeField] 
    [Tooltip("Vitessee de la particule m/s")]
    private float _particleSpeed;

    [Header("Donn�es des spawn")]
    [SerializeField] 
    private float _spawnDelay;
    [SerializeField] 
    private float _nextSpawnTime;
    [SerializeField]
    [Range(0.5f, 2)]
    private float _spawnerRadius;

    [Header("Valeurs pour dessiner le gizmo")]
    [SerializeField]
    Color _gizmoColor;
    [SerializeField]
    bool _drawGizmo;

    #endregion

    #region Unity Lyfecycle
    void Start()
    {
        
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad > _nextSpawnTime)
        {
            //Spawn des particules
            GameObject newParticle = SpawnParticle();

            //Lauch des particules
            LaunchParticle(newParticle);

            //Debug.Log("Spawn particle");
            _nextSpawnTime = Time.timeSinceLevelLoad + _spawnDelay;
        }
    }

    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _spawnerRadius);
            Gizmos.DrawRay(transform.position, transform.right * _particleSpeed);
        }
    }
    #endregion

    #region Methods

    private GameObject SpawnParticle()
    {
        Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)transform.position;
        GameObject particle = _pool.GetParticle();
        if (particle != null)
        {
            particle.GetComponent<TrailRenderer>().Clear();
            particle.SetActive(true);
            particle.transform.position = position;
        }
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        if (particle != null)
        {
            Rigidbody2D rigidbody = particle.GetComponent<Rigidbody2D>();
            rigidbody.drag = _particleDrag;
            rigidbody.velocity = transform.right * _particleSpeed;
        }
    }

    #endregion

    #region Private & Protected

    #endregion
}
