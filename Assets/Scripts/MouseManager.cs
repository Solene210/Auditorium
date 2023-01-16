using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class MouseManager : MonoBehaviour
{
    #region Expose

    [SerializeField]
    LayerMask _layerMask;

    [SerializeField]
    Texture2D _mouseTextureMove;
    [SerializeField]
    Texture2D _mouseTextureResize;

    [SerializeField]
    float _forceRatio;

    #endregion

    #region Unity Lyfecycle
    void Start()
    {
        
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Move"))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
                Vector2 hotSpot = new Vector2(64, 64);
                Cursor.SetCursor(_mouseTextureMove, hotSpot, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    _activeEffector = hit.collider.transform.parent;
                }

                if (_activeEffector != null)
                {
                    _activeEffector.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else if (hit.collider.CompareTag("Resize"))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
                Vector2 hotSpot = new Vector2(64, 64);
                Cursor.SetCursor(_mouseTextureResize, hotSpot, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    _activeEffector = hit.collider.transform;
                }
                if (_activeEffector != null)
                {
                    float radius = Vector2.Distance(_activeEffector.position, hit.point);
                    _activeEffector.GetComponent<CircleShape>().Radius = Mathf.Clamp(radius, 1, 2.5f);

                    _activeEffector.GetComponent<AreaEffector2D>().forceMagnitude = radius * _forceRatio;
                }
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _activeEffector = null;
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    private Transform _activeEffector;

    #endregion
}
