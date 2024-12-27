using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamMilkGame.Physics;
using TeamMilkGame.PlayerStateMachine.MarioStates;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class BrickBlock : IBlocks, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }
        public ISprite sprite { get; }
        public int width { get; }
        public int height { get; }
        public IPhysics physics { get; set; }
        public List<BlockPiece> blockPieces { get; set; } // four corners of broken block

        private bool isBroken;
        private int timer = BlockUtility.Instance.DEBRIS_TIMER;

        public BrickBlock(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = true;
            sprite = SpriteFactory.Instance.CreateBrickBlockDefaultSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, width, height);
            physics = new PhysicsHandler(this, false);
            blockPieces = new List<BlockPiece>();
        }

        public void FillList()
        {
            blockPieces.Add(new BlockPiece((int)Position.X, (int)Position.Y, BlockUtility.Instance.UP_LEFT_CORNER));
            blockPieces.Add(new BlockPiece((int)Position.X + BlockUtility.Instance.DEBRIS_OFFSET, (int)Position.Y, BlockUtility.Instance.UP_RIGHT_CORNER));
            blockPieces.Add(new BlockPiece((int)Position.X, (int)Position.Y + BlockUtility.Instance.DEBRIS_OFFSET, BlockUtility.Instance.DOWN_LEFT_CORNER));
            blockPieces.Add(new BlockPiece((int)Position.X + BlockUtility.Instance.DEBRIS_OFFSET, (int)Position.Y + BlockUtility.Instance.DEBRIS_OFFSET, BlockUtility.Instance.DOWN_RIGHT_CORNER));
        }

        public void Update(GameTime gameTime)
        {
            // Mario only interacts from below, and shells can break from both sides
            /*
             * If Mario collides from below and Mario != BigMario or FireMario:
             *      BrickBlock plays short "bounce up" animation (same as when QuestionBlock is activated)
             *      Returns to initial position, nothing else changes
             *
             * If Mario collides from below and Mario == BigMario or FireMario:
             *      Call BreakBlock()
             *
             * Note: in either instance of Mario colliding from below, any enemy (not Bowser) on top of the block are killed
             *      Koopa doesn't drop shell in this case (not really a problem for our implementation)
             *
             * If Koopa shell collides with either Left or Right Side:
             *      Call BreakBlock()
             *      Note: only one Koopa in 1-1, and there isn't really any possible blocks for the shell to hit, so only implement if we have time?
             */
            if (isBroken)
            {
                timer--;
                blockPieces[0].Update(gameTime);
                blockPieces[1].Update(gameTime);
                blockPieces[2].Update(gameTime);
                blockPieces[3].Update(gameTime);
            }
            if (timer <= 0)
            {
                GameObjectManager.Instance.ReqRemove(this);
            }
            physics.Update(gameTime);
        }

        public CollisionType GetCollisionType()
        {
            return CollisionType.Block;
        }

        public void Bounce()
        {
            if ((GameObjectManager.Instance.Players[0] as IMario)?.state is not RegMarioStateMachine)
            {
                BreakBlock();
            }
        }

        private void BreakBlock()
        {
            /*
             * Plays breaking animation, removes block from level entirely
             * Any enemies on top of block are killed
             * Increment score by 50 points but don't display indicator like when enemy is killed
             * Stop Mario's momentum and push him downwards
             */

            // TODO: breaking animation + remove from game completely (?)
            // TODO: check for enemies on top
            // TODO: score += 50
            SoundFXStorage.Instance.PlaySoundEffect("blockBreak");
            Interactable = false;
            isBroken = true;
            BoundingBox = default; // get rid of the old bounding box so mario can't collide with the air
            FillList();
            foreach (BlockPiece corner in blockPieces)
            {
                corner.physics.AddForce(corner.accelAmount);

            }
        }

        public static void BounceBlock(IMario mario)
        {
            /*
             * Only if Mario is small and collides from below
             * Play bouncing animation (same as QuestionBlock activating but BrickBlock sprite is unchanged)
             * Any enemies on top of block are killed
             * Stop Mario's momentum and push him downwards
             */

            SoundFXStorage.Instance.PlaySoundEffect("blockBump");

            // TODO: bounce animation
            // TODO: check for enemies
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = BoundingBox;
            temp.X = (int)Position.X;
            temp.Y = (int)Position.Y;
            BoundingBox = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!isBroken)
            {
                sprite.Draw(spriteBatch, Position);
                return;
            }
            foreach (BlockPiece corner in blockPieces)
            {
                corner.Draw(spriteBatch);
            }
        }
        public IGameObject Clone(Vector2 location)
        {
            return new BrickBlock((int)location.X, (int)location.Y);
        }
    }
}