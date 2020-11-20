using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private class PlanetSystem
    {
        public event EventHandler<int> OnScoreIncrement;

        private GameObject ufo;
        private GameObject planet;
        private List<Asteroid> asteroids;
        private List<Cow> cows;

        private GameObject asteroidPrefab;
        private GameObject cowPrefab;

        private float planetTime = 0.0f;
        private float prevSpawnTimerAsteroid = 0.0f;
        private float prevSpawnTimerCow = 0.0f;

        private float asteroidSpawnRate = 0.75f;
        private float cowSpawnRate = 3.55f;

        private float rotationSpeed = 0.02f;

        public void Init(GameObject _asteroidPrefab, GameObject _cowPrefab)
        {
            asteroidPrefab = _asteroidPrefab;
            cowPrefab = _cowPrefab;

            ufo = GameObject.Find("UFO");
            planet = GameObject.Find("Planet");

            asteroids = new List<Asteroid>();
            cows = new List<Cow>();

            ufo.GetComponentInChildren<TractorBeam>().OnCowHit += UFOCowHit; ;
            ufo.GetComponent<UFO>().OnAsteroidHit += UFOAsteroidHit;
        }

        private void UFOAsteroidHit(object sender, EventArgs e)
        {
            GameObject.Find("HP Text").GetComponent<TMPro.TMP_Text>().text = "HP: " + ufo.GetComponent<UFO>().health;
        }

        private void UFOCowHit(object sender, Cow e)
        {
            e.captured = true;
            e.transform.parent = ((TractorBeam)sender).transform;
            e.transform.localPosition = new Vector3(0, UnityEngine.Random.Range(1.0f,-1.0f), 0);
            e.transform.rotation = Quaternion.identity;
        }

        public void ClearCows()
        {
            cows.ForEach((e) => { DestroyImmediate(e.gameObject); });
            cows.Clear();
        }

        public void IncrementScore()
        {
            int scoreReturn = 0;
            cows.ForEach((e) => { if (e.captured) scoreReturn++; });
            OnScoreIncrement.Invoke(this, scoreReturn);
        }

        public void SetRotationSpeed(float value)
        {
            rotationSpeed = value;
        }

        public void PlanetUpdate()
        {
            planetTime += Time.deltaTime;

            float prevPlanetRot = planet.transform.rotation.eulerAngles.z;

            cows.ForEach((e) => { if(!e.captured) e.gameObject.transform.RotateAround(planet.transform.position,planet.transform.forward, rotationSpeed); });
            asteroids.ForEach((e) => { e.gameObject.transform.RotateAround(planet.transform.position, planet.transform.forward, rotationSpeed); });
            planet.transform.RotateAround(planet.transform.position, planet.transform.forward, rotationSpeed);

            if (planet.transform.rotation.eulerAngles.z - prevPlanetRot < 0.0f) 
            { 
                IncrementScore(); 
                ClearCows();
            }

            if(planetTime - prevSpawnTimerAsteroid > asteroidSpawnRate)
            {
                prevSpawnTimerAsteroid = planetTime;

                asteroids.Add(Instantiate(asteroidPrefab).GetComponent<Asteroid>());
                Vector2 asteroidPos = new Vector2(Mathf.Sin(65), Mathf.Cos(65)).normalized;
                asteroids.Last().transform.position = asteroidPos * (22.331f + UnityEngine.Random.Range(0.1f, 10.5f));
                asteroids.Last().transform.rotation = Quaternion.Euler(0, 0, -59);
                asteroids.Last().OnDestruction += AsteroidDestruction;
            }
            if (planetTime - prevSpawnTimerCow > cowSpawnRate)
            {
                prevSpawnTimerCow = planetTime;

                cows.Add(Instantiate(cowPrefab).GetComponent<Cow>());
                Vector2 cowPos = new Vector2(Mathf.Sin(65), Mathf.Cos(65)).normalized;
                cows.Last().transform.position = cowPos * 21.361f;
                cows.Last().transform.rotation = Quaternion.Euler(0, 0, -59);
                cows.Last().OnDestruction += CowDestruction;
            }
        }

        private void CowDestruction(object sender, EventArgs e)
        {
            cows.Remove((Cow)sender);
        }

        private void AsteroidDestruction(object sender, EventArgs e)
        {
            asteroids.Remove((Asteroid)sender);
        }
    }

    public int score = 0;

    public GameObject asteroidPrefab;
    public GameObject cowPrefab;

    private PlanetSystem planetSystem;

    void Start()
    {
        planetSystem = new PlanetSystem();
        planetSystem.Init(asteroidPrefab, cowPrefab);
        planetSystem.OnScoreIncrement += OnScoreIncrement;
    }

    private void OnScoreIncrement(object sender, int returnScore)
    {
        score += returnScore;
        GameObject.Find("Score Text").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;
    }

    void Update()
    {
        planetSystem.PlanetUpdate();
    }
}
