using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AirShooter.Classes
{
    public class Bomb
    {
        private Vector2 position;
        private Texture2D texture;
        private Rectangle collision;
        private bool isAlive = true;
        #region Constuctors
        public Bomb()
        {
            texture = null;
            position = Vector2.Zero;
        }

        public Bomb(Vector2 position)
        {
            texture = null;
            this.position = position;
        }
        #endregion
        #region Properties
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Height
        {
            get { return texture.Height; }
        }
        public int Width
        {
            get { return texture.Width; }
        }
        public Rectangle Collision
        {
            get { return collision; }
            set { collision = value; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        #endregion
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("mine");
        }

        public void Update()
        {
            position.X -= 5;
            Collision = new Rectangle((int)position.X, (int)position.Y,
                texture.Width, texture.Height);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
