using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class QuestionBlock : IBlocks, ICollidable
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }

        public IGameItems Item { get; set; }
        public ISprite sprite { get; private set; }

        public int width { get; }
        public int height { get; }

        public QuestionBlock(int x, int y, IGameItems item)
        {
            Position = new Vector2(x, y);
            Interactable = true; // Question Block is active until it spawns item
            Item = item; // item stored inside, null = no item
            sprite = SpriteFactory.Instance.CreateQuestionBlockActiveSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }
        public QuestionBlock(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = false;
            sprite = SpriteFactory.Instance.CreateQuestionBlockActiveSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public void Bounce()
        {
            if (Interactable && Item is not null)
            {
                Item.Position = Item.Position + new Vector2(0, -this.height);
                GameObjectManager.Instance.ReqAdd("MovingCollidables", Item);
            }
            Activate();
        }

        public void Update(GameTime gameTime)
        {
            /*
             * If Mario collides with block from below, call Activate()
             * Otherwise, act like normal block
             * Ensure that only Mario colliding from below will call Activate()
             */
            sprite.Update(gameTime);
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            this.BoundingBox = temp;
        }

        public void Activate()
        {
            /*
             * Play short "bounce up" block animation and move Mario downwards
             * If block has an item, dispense it during animation (maybe write a new method to handle this specifically)
             * Any enemy besides Bowser on top of block is killed (Koopas don't turn into shell in this case)
             * Change sprite to QuestionBlockInactive and make non-interactable
             */
            // TODO: bounce animation
            // TODO: dispense item (if any)
            // TODO: check for enemies on top

            Interactable = false;
            sprite = SpriteFactory.Instance.CreateQuestionBlockInctiveSprite();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new QuestionBlock((int)location.X, (int)location.Y);
        }
    }
}
