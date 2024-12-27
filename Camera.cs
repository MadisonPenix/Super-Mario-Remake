using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.ComponentModel;
using System.Diagnostics;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame
{
    public class Camera
    {
        public Vector2 Position { get; private set; }
        public Vector2 Origin { get; private set; }
        public float Zoom { get; private set; }
        public Viewport Viewport { get; private set; }
        public Matrix TransformCamera { get; private set; }
        public bool disableCamera { get; set; }

        private bool canMoveRight, canMoveLeft;

        public Camera(Viewport viewport)
        {
            Origin = new Vector2((viewport.Width / 2f) - CameraUtility.Instance.X_OFFSET, (viewport.Height / 2f) - CameraUtility.Instance.Y_OFFSET);
            Zoom = CameraUtility.Instance.ZOOM;
            Viewport = viewport;
            disableCamera = false;
            this.canMoveLeft = true;
            this.canMoveRight = true;
        }

        public void LookAt(float xPosition)
        {
            if (!disableCamera)
            {
                CheckMoving();
                Vector2 storePos = Position; // camera's current position
                storePos.X = xPosition - Viewport.Width / 2f;
                if (storePos.X > Position.X && canMoveRight)
                {
                    Position = storePos;
                }
                if (storePos.X < Position.X && canMoveLeft)
                {
                    Position = storePos;
                }
            }
        }

        public void CheckMoving()
        {
            bool canGoLeft = true;
            bool canGoRight = true;
            foreach (IMario player in GameObjectManager.Instance.Players)
            {
                if(player.Position.X - (Viewport.Width / 2f)  > Position.X)
                {
                    canGoLeft = false;
                }
                else if (player.Position.X - (Viewport.Width / 2f)  < Position.X)
                {
                    canGoRight = false;
                }

                //makes mario not be able to go outside camera field of view
                if (player.Position.X  < (Position.X))
                {
                    player.Position = new Vector2(Position.X, player.Position.Y);
                    player.physics.Xvelocity = 0;
                }
                else if (player.Position.X > Position.X + (GameUtility.Instance.GRAPHICS_BUFFER_WIDTH - player.sprite.spriteWidth) / Zoom)
                {
                    //player.Position = new Vector2(Position.X + (GameUtility.Instance.GRAPHICS_BUFFER_WIDTH - player.sprite.spriteWidth) / Zoom, player.Position.Y);
                    player.physics.Xvelocity = 0;
                }
            }
            this.canMoveLeft = canGoLeft;
            this.canMoveRight = canGoRight;
        }

        //hard resets camera with no checks for game
        public void ResetCamera(float xPosition)
        {
            Vector2 storePos = Position; // camera's current position
            storePos.X = xPosition - (Viewport.Width / 2f);
            Position = storePos;
        }

        public void Update()
        {
            IMario CenterMostMario = (IMario)GameObjectManager.Instance.Players[0];
            foreach (IMario Mario in GameObjectManager.Instance.Players)
            {
                //gets center most mario
                if (Math.Abs(Mario.Position.X - Position.X) < Math.Abs(CenterMostMario.Position.X - Position.X))
                {
                    CenterMostMario = Mario;
                }
            }
            if (CenterMostMario.Position.X > GameUtility.Instance.INIT_CAMERA_POS)
            {
                LookAt(CenterMostMario.Position.X);
            }
            else
            {
                LookAt(GameUtility.Instance.INIT_CAMERA_POS);
            }
        }

        /*
		 * CreateTranslation() moves Matrix (Camera) given certain parameters (coordinates, parallax)
		 * 
		 * CreateTranslation: returns translation Matrix. Vector3 parameter represents X/Y/Z coordinates of translation
		 *      Vector3 param. created using pre-existing Vector2 (Position/Origin) and float value for the "Z" (2D game = 0 for Z)
		 *      
		 * CreateScale: returns scaling Matrix. Three parameters are floats representing X/Y/Z scale factors, respectively (2D game = no Z scale = 1). 
		 *      Just scales both X/Y based on whatever Zoom is
		 */
        public Matrix TransformViewMatrix(Vector2 parallax)
        {
            TransformCamera = Matrix.CreateTranslation(new Vector3(-Position * parallax, 0)) *
                Matrix.CreateTranslation(new Vector3(-Origin, 0)) *
                Matrix.CreateScale(Zoom, Zoom, CameraUtility.Instance.SCALE) *
                Matrix.CreateTranslation(new Vector3(Origin, 0));
            return TransformCamera;
        }
    }
}