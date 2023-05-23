using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace JengaTask
{
    public class JengaTower : MonoBehaviour
    {
        [SerializeField] JengaPiece jengaPiecePrefab;
        [SerializeField] TMP_Text labelText;

        private List<JengaPiece> jengaPieces = new List<JengaPiece>();

        public void BuildTower(string label, Stack stack, Transform parent)
        {
            labelText.text = label;

            DestroyOldJengaPieces();

            bool horizontal = false;

            for (int i = 0; i < stack.Blocks.Count; i++)
            {
                JengaPiece piece = Instantiate(jengaPiecePrefab, parent);

                float spacingBetweenAdjacentPieces = (jengaPiecePrefab.Depth - 3 * jengaPiecePrefab.Width) / 2;

                float horizontalDisplacement = ((i % 3) - 1) * (jengaPiecePrefab.Width + spacingBetweenAdjacentPieces);

                horizontal = i % 3 == 0 ? !horizontal : horizontal;
                if (horizontal)
                {
                    piece.transform.Translate(piece.transform.forward * horizontalDisplacement);
                    piece.transform.Rotate(parent.transform.up, -90f);
                }
                else
                {
                    piece.transform.Translate(piece.transform.right * horizontalDisplacement);
                    piece.transform.Rotate(parent.transform.up, 0f);
                }

                float verticalDisplacement = (i / 3) * jengaPiecePrefab.Height;
                piece.transform.Translate(piece.transform.up * verticalDisplacement);

                piece.Initialize(stack.Blocks[i]);
                jengaPieces.Add(piece);
            }
        }

        public Vector3 GetTowerCenterPoint()
        {
            if (jengaPieces == null || jengaPieces.Count == 0) return transform.position;

            float towerHeightInJengaPieces = jengaPieces.Count / 3;

            return transform.position + new Vector3(0, towerHeightInJengaPieces * jengaPiecePrefab.Height / 2, 0);
        }

        public void TestStack()
        {
            foreach (var piece in jengaPieces)
            {
                if (piece.Block.Mastery == Block.MasteryType.GLASS) 
                {
                    Destroy(piece.gameObject);
                }
                else
                {
                    piece.EnablePhysics();
                }
            }
        }

        private void DestroyOldJengaPieces()
        {
            if (jengaPieces == null) return;

            foreach (var piece in jengaPieces)
            {
                Destroy(piece.gameObject);
            }
            jengaPieces.Clear();
        }
    }
}