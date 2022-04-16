using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform m_player;

    #region Road Stuffs
    public GameObject road;
    public Vector3 roadSpawnPos;
    public float roadSpawnDistance;
    public float roadDistance;
    #endregion

    #region Obstacle Stuffs
    public List<GameObject> obstacleList = new List<GameObject>();
    public Vector3 obstacleSpawnPos;
    public float obstacleSpawnDistance;
    public float obstacleDistance;
    #endregion

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float newRoadDistance = roadSpawnPos.z - m_player.position.z;
        if (newRoadDistance < roadSpawnDistance) {
            //Spawn Road
            Instantiate(road, roadSpawnPos, road.transform.rotation);

            //Add Distance
            roadSpawnPos = new Vector3(roadSpawnPos.x, roadSpawnPos.y, roadSpawnPos.z + roadDistance);
        }
        float newObstacleDistance = obstacleSpawnPos.z - m_player.position.z;
        if (newObstacleDistance < obstacleSpawnDistance) {
            //Spawn Obstacle
            SpawnObstacle();

            //Randomize Distance
            float randomDistance = Random.Range(obstacleDistance - 7, obstacleDistance + 7);

            float spawnPosX = 2.5f;
            int dice = Random.Range(0, 2);
            if (dice == 0)
            {
                spawnPosX = -2.5f;
            }

            //Add Distance
            obstacleSpawnPos = new Vector3(spawnPosX, obstacleSpawnPos.y, obstacleSpawnPos.z + randomDistance);
        }
    }

    public void SpawnObstacle()
    {
        int dice = Random.Range(0, obstacleList.Count);
        Instantiate(obstacleList[dice], obstacleSpawnPos, obstacleList[dice].transform.rotation);
    }
}
