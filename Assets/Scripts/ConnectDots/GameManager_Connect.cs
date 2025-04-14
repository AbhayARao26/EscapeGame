using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Edge _edgePrefab;
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private LineRenderer _highlight;

    private Dictionary<int, Point> points;
    private Dictionary<Vector2Int, Edge> edges;
    private Point startPoint, endPoint;
    private int currentId;
    private bool hasGameFinished;
    private bool isFirstTry = true;

    private void Awake()
    {
        hasGameFinished = false;
        points = new Dictionary<int, Point>();
        edges = new Dictionary<Vector2Int, Edge>();
        _highlight.gameObject.SetActive(false);
        currentId = -1;
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = _level.Col * 0.5f;
        camPos.y = _level.Row * 0.5f;
        Camera.main.transform.position = camPos;
        Camera.main.orthographicSize = Mathf.Max(_level.Col, _level.Row) + 2f;

        for (int i = 0; i < _level.Points.Count; i++)
        {
            Vector4 posData = _level.Points[i];
            Vector3 spawnPos = new Vector3(posData.x, posData.y, posData.z);
            int id = (int)posData.w;
            points[id] = Instantiate(_pointPrefab);
            points[id].Init(spawnPos, id);
        }

        for (int i = 0; i < _level.Edges.Count; i++)
        {
            Vector2Int normal = _level.Edges[i];
            Vector2Int reversed = new Vector2Int(normal.y, normal.x);
            Edge spawnEdge = Instantiate(_edgePrefab);
            edges[normal] = spawnEdge;
            edges[reversed] = spawnEdge;
            spawnEdge.Init(points[normal.x].Position, points[normal.y].Position);
        }
    }

    private void Update()
    {
        if (hasGameFinished) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Point point = hit.collider.GetComponent<Point>();
                if (point != null)
                {
                    if (startPoint == null)
                    {
                        startPoint = point;
                        currentId = point.Id;
                        _highlight.gameObject.SetActive(true);
                        _highlight.positionCount = 2;
                        _highlight.SetPosition(0, startPoint.Position);
                        _highlight.SetPosition(1, startPoint.Position);
                    }
                    else
                    {
                        endPoint = point;
                        if (IsStartAdd())
                        {
                            currentId = point.Id;
                            edges[new Vector2Int(startPoint.Id, endPoint.Id)].Add();
                            startPoint = endPoint;
                            _highlight.SetPosition(0, startPoint.Position);
                            _highlight.SetPosition(1, startPoint.Position);
                        }
                        else if (IsEndAdd())
                        {
                            edges[new Vector2Int(endPoint.Id, startPoint.Id)].Add();
                            _highlight.gameObject.SetActive(false);
                            startPoint = null;
                            endPoint = null;
                            currentId = -1;
                            CheckWin();
                        }
                        else
                        {
                            _highlight.gameObject.SetActive(false);
                            startPoint = null;
                            endPoint = null;
                            currentId = -1;
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0) && startPoint != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _highlight.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0));

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Point point = hit.collider.GetComponent<Point>();
                if (point != null && point != startPoint)
                {
                    endPoint = point;
                    if (IsStartAdd() || IsEndAdd())
                    {
                        _highlight.SetPosition(1, point.Position);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (startPoint != null && endPoint != null)
            {
                if (IsEndAdd())
                {
                    edges[new Vector2Int(endPoint.Id, startPoint.Id)].Add();
                    CheckWin();
                }
            }
            _highlight.gameObject.SetActive(false);
            startPoint = null;
            endPoint = null;
            currentId = -1;
        }
    }

    private bool IsStartAdd()
    {
        if (currentId != -1) return false;
        if (startPoint == null || endPoint == null) return false;
        Vector2Int edge = new Vector2Int(startPoint.Id, endPoint.Id);
        if (!edges.ContainsKey(edge)) return false;
        if (edges[edge].Filled) return false;
        return true;
    }

    private bool IsEndAdd()
    {
        if (startPoint == null || endPoint == null) return false;
        if (currentId != startPoint.Id) return false;

        Vector2Int edge = new Vector2Int(endPoint.Id, startPoint.Id);
        if (edges.TryGetValue(edge, out Edge result))
        {
            if (result == null || result.Filled) return false;
        }
        else
        {
            return false;
        }
        return true;
    }

    private void CheckWin()
    {
        foreach (var item in edges)
        {
            if (!item.Value.Filled)
            {
                return;
            }
        }
        hasGameFinished = true;
        StartCoroutine(GameFinished());
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(2f);
        
        if (isFirstTry)
        {
            SceneTransitionManager.Instance.LoadGameFinish();
        }
        else
        {
            SceneTransitionManager.Instance.LoadFallback();
        }
    }

    public void OnGameFailed()
    {
        isFirstTry = false;
        SceneTransitionManager.Instance.LoadFallback();
    }
}