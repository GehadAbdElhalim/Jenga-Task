using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JengaTask
{
    public class JengaTowersBuilder : MonoBehaviour
    {
        [SerializeField] private float spacingBetweenTowers;
        [SerializeField] private JengaTower jengaTowerPrefab;
        private float currentXDisplacement;

        public List<JengaTower> BuildJengaTowers(Dictionary<string, Stack> labeledStacks)
        {
            List<JengaTower> jengaTowers = new List<JengaTower>();
            foreach (var (label, stack) in labeledStacks)
            {
                JengaTower jengaTower = Instantiate(jengaTowerPrefab, transform);
                jengaTower.transform.Translate(Vector3.right * currentXDisplacement);
                jengaTower.BuildTower(label, stack, jengaTower.transform);
                currentXDisplacement += spacingBetweenTowers;
                jengaTowers.Add(jengaTower);
            }
            return jengaTowers;
        }
    }
}