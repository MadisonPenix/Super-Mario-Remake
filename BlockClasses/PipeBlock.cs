using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class PipeBlock : IBlocks, ICollidable
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 TeleportLoc { get; private set; }
        public bool Interactable { get; set; }

        public ISprite sprite { get; }
        public int width { get; }
        public int height { get; }
        private string ActiveSide;

        private string xmlFile;

        public PipeBlock(int x, int y, string ActiveSide, string xmlFile, Vector2 TeleportLoc)
        {
            Position = new Vector2(x, y);
            Interactable = true;
            sprite = SpriteFactory.Instance.CreatePipeBlockSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            this.TeleportLoc = TeleportLoc;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
            this.ActiveSide = ActiveSide;
            this.xmlFile = xmlFile;
        }

        public PipeBlock(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = false;
            sprite = SpriteFactory.Instance.CreatePipeBlockSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            TeleportLoc = new Vector2();
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Pipe;
        }

        public void Bounce()
        {
            if (!Interactable)
            {
                return;
            }

            foreach (IMario Mario in GameObjectManager.Instance.Players)
            {
                if (ActiveSide == "Top" && Mario.IsCrouched)
                {
                    Activate();
                    break;
                }
                /*else if (ActiveSide == "Left" || ActiveSide == "Right")
                {
                    Activate();
                    break;
                }*/
            }
        }

        public void Update(GameTime gameTime)
        {
            /*
             * If Mario collides with pipe, check if collision is with "open" side of pipe
             *      If yes, check if there are any directional/movement inputs
             *              1. Movement inputs are being sent and are in the direction "towards" the open part of pipe --> call Activate()
             *              2. Movement inputs are being sent but not in the right direction --> do nothing
             *              3. No movement inputs --> do nothing
             *      If no, just act as solid impassable object
             */

        }

        private void Activate()
        {
            /*
             * Plays Mario entering pipe animation (note: Mario slides towards center of pipe while animation 
             *      plays if not already centered)
             * Transition level: unload current level and load new
             * Freeze Mario's inputs until finished
             */

            SoundFXStorage.Instance.PlaySoundEffect("pipe");

            /*foreach(IMario Mario in GameObjectManager.Instance.Players)
            {
                Mario.Position = TeleportLoc;
            }*/
            LevelManager.Instance.Load(xmlFile, TeleportLoc);
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            this.BoundingBox = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new PipeBlock((int)location.X, (int)location.Y);
        }
    }
}
