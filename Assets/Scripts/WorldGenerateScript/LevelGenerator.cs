using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public LevelChunck[] levelChunkData;
    public LevelChunck fritstChunk;
    private LevelChunck previousChunck;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;
    public int chunksToSpawn = 3;
    public int ChunksActive;
    public List<GameObject> chunkList;
    private GameObject root;


    void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PickAndSpawnChunk();
        }
    }

    void Start()
    {
        root = new GameObject("ProcederalMap");
        previousChunck = fritstChunk;

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    LevelChunck PickNextChunk()
    {
        List<LevelChunck> allowedChunkList = new List<LevelChunck>();
        LevelChunck nextChunk = null;

        LevelChunck.Direction nextRequiredDirection = LevelChunck.Direction.North;

        switch (previousChunck.exitDirection)
        {
            case LevelChunck.Direction.North:
                nextRequiredDirection = LevelChunck.Direction.South;
                spawnPosition = spawnPosition + new Vector3(0f, 0, previousChunck.chunckSize.y);

                break;
            case LevelChunck.Direction.East:
                nextRequiredDirection = LevelChunck.Direction.West;
                spawnPosition = spawnPosition + new Vector3(previousChunck.chunckSize.x, 0, 0);
                break;
            case LevelChunck.Direction.South:
                nextRequiredDirection = LevelChunck.Direction.North;
                spawnPosition = spawnPosition + new Vector3(0, 0, -previousChunck.chunckSize.y);
                break;
            case LevelChunck.Direction.West:
                nextRequiredDirection = LevelChunck.Direction.East;
                spawnPosition = spawnPosition + new Vector3(-previousChunck.chunckSize.x, 0, 0);

                break;
            default:
                break;
        }

        for (int i = 0; i < levelChunkData.Length; i++)
        {
            if (levelChunkData[i].entryDirection == nextRequiredDirection)
            {
                allowedChunkList.Add(levelChunkData[i]);
            }
        }

        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];

        ChunksActive++;
        return nextChunk;

    }

    void PickAndSpawnChunk()
    {
        LevelChunck chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.levelChunk[Random.Range(0, chunkToSpawn.levelChunk.Length)];
        previousChunck = chunkToSpawn;
        var chunk = Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity,root.transform);
        chunkList.Add(chunk);

        if(ChunksActive == 4)
        {
            var chunkDelete = chunkList[0];
            chunkList.RemoveAt(0);
            Destroy(chunkDelete);
            ChunksActive--;
        }

    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
