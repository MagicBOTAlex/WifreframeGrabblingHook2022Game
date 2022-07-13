using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Resources.DataModels;
using System;
using Newtonsoft.Json;

public class TowerGenScript : MonoBehaviour
{

    [TextAreaAttribute(15,20)]
    public string TerrainJson;
    public float DistanceMultiplayer = 1f;
    public float TowerScale = 1f;
    public GameObject BuildingBlock;
    public bool ReGenerateTowers = false;

    public System.Random random = new System.Random();

    private GameObject generatedTowersHolder;
    private TowerGenJObject jObject;

    void Start()
    {
        jObject = JsonConvert.DeserializeObject<TowerGenJObject>(TerrainJson);

        var xLen = jObject.TowerPlacements.GetLength(0);
        var yLen = jObject.TowerPlacements.GetLength(1);

        generatedTowersHolder = new GameObject("TowerHolder");
        generatedTowersHolder.transform.parent = transform;
        generatedTowersHolder.transform.position = new Vector3((0 - (xLen / 2f)) * TowerScale, 0, (0 - (yLen / 2f)) * TowerScale);
        Generate();
    }

    private void Update()
    {
        if (ReGenerateTowers)
        {
            ReGenerateTowers = false;

            if (generatedTowersHolder.transform.childCount > 0)
            {
                for (int i = 0; i < generatedTowersHolder.transform.childCount; i++)
                {
                    Destroy(generatedTowersHolder.transform.GetChild(i).gameObject);
                }
            }

            jObject = JsonConvert.DeserializeObject<TowerGenJObject>(TerrainJson);
            var xLen = jObject.TowerPlacements.GetLength(0);
            var yLen = jObject.TowerPlacements.GetLength(1);
            generatedTowersHolder.transform.position = new Vector3((0 - (xLen / 2f)) * TowerScale, 0, (0 - (yLen / 2f)) * TowerScale);
            Generate();
        }
    }

    void Generate()
    {
        System.Random random = new System.Random(jObject.Seed);

        for (int x = 0; x < jObject.TowerPlacements.GetLength(0); x++)
        {
            for (int y = 0; y < jObject.TowerPlacements.GetLength(1); y++)
            {
                if (jObject.TowerPlacements[x, y] == -1)
                    continue;
                else
                {
                    var generatedTower = GenerateTower($"Tower: ({x},{y})", new Vector3(x, 0, y), (TowerType)jObject.TowerPlacements[x, y]);
                    generatedTower.transform.parent = generatedTowersHolder.transform;
                    generatedTower.transform.localPosition = new Vector3(x, 0, y) * DistanceMultiplayer;
                    generatedTower.transform.localScale = new Vector3(TowerScale, TowerScale, TowerScale);
                }
            }
        }
    }

    private GameObject GenerateTower(string towerName, Vector3 TowerPosistion, TowerType towerType)
    {
        switch (towerType)
        {
            case TowerType.AlternatingSizeBlockTower:
                var generatedTower = new GameObject(towerName);
                for (int i = 0; i < random.Next(3,20); i++)
                {
                    if (i%2==0)
                    {
                        var block = Instantiate(BuildingBlock, new Vector3(0, i, 0), Quaternion.identity) as GameObject;
                        block.transform.localScale = new Vector3(3, 1, 3);
                        block.transform.parent = generatedTower.transform;
                    }
                    else
                    {
                        var block = Instantiate(BuildingBlock, new Vector3(0, i, 0), Quaternion.identity) as GameObject;
                        block.transform.localScale = new Vector3(1.5f, 1, 1.5f);
                        block.transform.parent = generatedTower.transform;
                    }
                }
                return generatedTower;
            default:
                break;
        }

        return null;
    }
}

enum TowerType
{
    AlternatingSizeBlockTower
    
}