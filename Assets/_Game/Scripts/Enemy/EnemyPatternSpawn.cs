using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using _Game.Scripts.Enemy;
using Random = UnityEngine.Random;


public class EnemyPatternSpawn : MonoBehaviour
{
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField]private Transform [] spawnPositions;
    [SerializeField]private int spawnDelay;
    [SerializeField]private int spawnRate;
    [SerializeField]private List<Position> standingPositions= new List<Position>();
    private int currentNo;
    //public event Action <int, int>OnGetTarget ;
    public delegate Transform GetTarget(int position, int currentList);
    public GetTarget OnGetTarget;
    [SerializeField] private Transform enemiesContainer;

    private void OnEnable()
    {
        OnGetTarget += GetCurrentTarget;
    }

    private void OnDisable()
    {
        OnGetTarget -= GetCurrentTarget;
    }

    private void Start()
    {
        SpawnGrid();
    }

    public int GetStandingPositionCount()
    {
        return standingPositions.Count;
    }
    public void SpawnGrid()
    {
        Position positions = standingPositions[0];
        SpawnEnemies(positions.grid);
    }
    private async void SpawnEnemies(List<Transform> grid)
    {
        await Task.Delay(spawnDelay);
        Vector3 spawnPosition = spawnPositions[ Random.Range(0, spawnPositions.Length)].position;
        for (int i = 0; i <grid.Count; i++)
        {
            GameObject go = Instantiate(enemyPrefab,spawnPosition,Quaternion.identity);
            go.transform.parent = enemiesContainer;
            go.GetComponent<Enemy>().SetTarget(grid[i],i,this);
            await Task.Delay(spawnRate);
        }
    }
    private Transform GetCurrentTarget(int position,int currentList)
    {
        return standingPositions[currentList].grid[position];
    }
    
}
[System.Serializable]
public class Position
{
    public List<Transform> grid;
}