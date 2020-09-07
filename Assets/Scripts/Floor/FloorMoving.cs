using System;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Floor
{
    public class FloorMoving : MonoBehaviour
    {
        private static readonly string[] textureNames =
        {
            "_MainTex", 
            "_SpecTex", 
            "_NormalTex", 
            "_EmissionTex"
        };

        private Renderer meshRenderer;

        public float Speed;

        void Start()
        {
            meshRenderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.Play)
            {
                return;
            }

            var currentSpeed = Speed;
            if (Input.GetKey(KeyCode.Space))
            {
                currentSpeed *= GameManager.Instance.SpeedModifier;
            }

            var offset = Mathf.Repeat(Time.fixedDeltaTime * currentSpeed, 1f);

            var textureOffset = new Vector2(0, -offset);
            foreach (var textureName in textureNames)
            {
                var currentOffset = meshRenderer.material.GetTextureOffset(textureName);
                meshRenderer.material.SetTextureOffset(textureName, currentOffset + textureOffset);
            }
        }
    }
}