using UnityEngine;
using System.Collections.Generic;

namespace AIAssignment3
{
    //The class for holding neighbor data and for retrieving certain category of neighbors
    public class Neighbors
    {
        //Cache
        float neighborRadius;
        Transform transform;
        Collider2D collider;
        int lifeformType;

        LayerMask layerHideSpot;
        LayerMask layerObstacle;

        //Properties
        public List<Transform> Predators { get; private set; }
        public List<Transform> SameFlock { get; private set; }
        //public List<Transform> SameTierDifferentFlock { get; private set; }
        public List<Lifeform> Preys { get; private set; }
        public List<Transform> Obstacles { get; private set; }
        public List<Transform> HideSpots { get; private set; }

        public Neighbors(Lifeform lifeform)
        {
            //Cache
            neighborRadius = lifeform.Flock.NeighborRadius;
            transform = lifeform.transform;
            collider = lifeform.Collider;
            lifeformType = (int)lifeform.LifeFormType;

            layerHideSpot = Settings.instance.Layer_HideSpot;
            layerObstacle = Settings.instance.Layer_Obstacle;

            //Debug.Log(" layerObstacle :" + (int)layerObstacle + "layerHideSpot:" + (int)layerHideSpot);
        }

        public void DetectNeighbors()
        {
            //Reset the list collections of different neighbors
            Predators = new List<Transform>();
            SameFlock = new List<Transform>();
            Preys = new List<Lifeform>();
            Obstacles = new List<Transform>();
            HideSpots = new List<Transform>();

            //Get all objects that are near the lifeform
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, neighborRadius);

            //Loop through the collided objects
            foreach (Collider2D c in overlaps)
            {
                //If the collider is not the lifeform itself,
                if (c != collider)
                {
                    //Try to see if there is a lifeformbase script on the target object
                    Lifeform neighbor = c.GetComponent<Lifeform>();
                    //If the neighbor is a lifeform
                    if (neighbor != null)
                    {
                        //Check if the neighbor is a predator, same flock, or a prey
                        int neighborType = (int)neighbor.LifeFormType;

                        if (neighborType == lifeformType)
                        {
                            SameFlock.Add(neighbor.transform);
                        }
                        else
                        {
                            if (neighborType - lifeformType == 1)
                            {
                                Predators.Add(neighbor.transform);
                            }
                            else if (neighborType - lifeformType == -1 && !neighbor.inHidingArea)
                            {
                                Preys.Add(neighbor);
                            }
                        }
                    }
                    //if the neighbor is not a lifeform...
                    else
                    {
                        //...check if it is a hide spot or an obstacle
                        if (layerObstacle == (layerObstacle | 1 << c.gameObject.layer))
                        {
                            Obstacles.Add(c.transform);
                        }
                        else if (layerHideSpot == (layerHideSpot | 1 << c.gameObject.layer))
                        {
                            HideSpots.Add(c.transform);
                        }
                    }
                }
            }
        }

        public bool HasPredator() => Predators.Count > 0;
        public Transform GetClosestPredator()
        {
            //Get the closest predator to this lifeform
            Transform closest = null;
            //When there is only 1 in the collection, just return the first indexed object
            if (Predators.Count == 1)
            {
                closest = Predators[0];
            }
            else
            {
                //Find the closest by comparing distance
                float closestDist = float.MaxValue;
                foreach (Transform predator in Predators)
                {
                    float dist = Vector2.SqrMagnitude(predator.position - transform.position);
                    if (dist < closestDist)
                    {
                        closest = predator;
                        closestDist = dist;
                    }
                }
            }
            return closest;
        }

        public bool HasPrey() => Preys.Count > 0;
        public Transform GetClosestPrey()
        {
            //Get the closest prey to this lifeform
            Transform closest = null;
            //When there is only 1 in the collection, just return the first indexed object
            if (Preys.Count == 1)
            {
                closest = Preys[0].transform;
            }
            else
            {
                //Find the closest by comparing distance
                float closestDist = float.MaxValue;
                foreach (Lifeform prey in Preys)
                {
                    float dist = Vector2.SqrMagnitude(prey.transform.position - transform.position);
                    if (dist < closestDist)
                    {
                        closest = prey.transform;
                        closestDist = dist;
                    }
                }
            }
            return closest;
        }


        public bool HasHideSpot() => HideSpots.Count > 0;
        public Transform GetClosestHideSpot()
        {
            //Get the closest hide spot to this lifeform
            Transform closest = null;
            //When there is only 1 in the collection, just return the first indexed object
            if (HideSpots.Count == 1)
            {
                closest = HideSpots[0];
            }
            else
            {
                //Find the closest by comparing distance
                float closestDist = float.MaxValue;
                foreach (Transform hideSpot in HideSpots)
                {
                    float dist = Vector2.SqrMagnitude(hideSpot.position - transform.position);
                    if (dist < closestDist)
                    {
                        closest = hideSpot;
                        closestDist = dist;
                    }
                }
            }

            return closest;
        }
    }

}