using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JengaTask
{
    public class JengaPiece : MonoBehaviour, IClickable
    {
        public Block Block { get; private set; }

        [Header("Size Parameters")]
        [Range(0.1f,2f)]
        [SerializeField] private float width = 0.75f;
        public float Width => width;
        [Range(0.1f, 2f)]
        [SerializeField] private float height = 0.5f;
        public float Height => height;
        [Range(0.1f, 3f)]
        [SerializeField] private float depth = 1.5f;
        public float Depth => depth;

        [Space(20f)]
        [Header("Additional References")]
        [SerializeField] MeshRenderer meshRenderer;
        [SerializeField] TMP_Text masteryText;

        [Space(20f)]
        [Header("Mastery Materials")]
        [SerializeField] Material glassMaterial;
        [SerializeField] Material woodMaterial;
        [SerializeField] Material stoneMaterial;

        [Space(20f)]
        [Header("HighLight Material")]
        [SerializeField] Material highlightMaterial;

        public void Initialize(Block block)
        {
            Block = block;
            ChangeMasteryText();
            ChangeBlockMaterial();
        }

        public void EnablePhysics()
        {
            GetComponentInChildren<Rigidbody>().isKinematic = false;
        }

        public void OnClick()
        {
            StartCoroutine(HighlightForDuration(0.25f));
            StartCoroutine(ShowDetailsAfterDuration(0.25f));
        }

        private void ChangeMasteryText()
        {
            switch (Block.Mastery)
            {
                case Block.MasteryType.GLASS:
                    masteryText.text = "MISSING";
                    break;
                case Block.MasteryType.WOOD:
                    masteryText.text = "LEARNED";
                    break;
                case Block.MasteryType.STONE:
                    masteryText.text = "MASTERED";
                    break;
                default:
                    masteryText.text = "MISSING";
                    break;
            }
        }

        private void ChangeBlockMaterial()
        {
            switch (Block.Mastery)
            {
                case Block.MasteryType.GLASS:
                    meshRenderer.material = glassMaterial;
                    break;
                case Block.MasteryType.WOOD:
                    meshRenderer.material = woodMaterial;
                    break;
                case Block.MasteryType.STONE:
                    meshRenderer.material = stoneMaterial;
                    break;
                default:
                    meshRenderer.material = glassMaterial;
                    break;
            }
        }

        private void OnValidate()
        {
            meshRenderer.transform.localScale = new Vector3(Width, Height, Depth);
            meshRenderer.transform.position = new Vector3(0, Height/2f, 0);
        }

        IEnumerator HighlightForDuration(float durationInSeconds)
        {
            meshRenderer.material = highlightMaterial;
            yield return new WaitForSeconds(durationInSeconds);
            ChangeBlockMaterial();
        }

        IEnumerator ShowDetailsAfterDuration(float durationInSeconds)
        {
            yield return new WaitForSeconds(durationInSeconds);
            GameManager.Instance.ShowBlockDetails(Block);
        }
    }
}