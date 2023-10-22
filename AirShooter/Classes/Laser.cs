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
    public class Laser
    {
        private Texture2D texture;
        private Rectangle destinationRectangle;
        private int width = 46;
        private int height = 16;
        private int speed = 8;
        private bool isAlive = true;
        #region Constructors
        public Laser()
        {
            texture = null;
            destinationRectangle = new Rectangle(0, 0, 46, 16);
        }
        public Laser(Vector2 position)
        {
            texture = null;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 46, 16);
        }
        #endregion
        #region Properties
        public int Width
        {get { return width; }}

        public int Height
        { get { return height; }}

        public Vector2 Position
        {
            get { return new Vector2(destinationRectangle.X, destinationRectangle.Y); }
            set
            {
                destinationRectangle.X = (int)value.X;
                destinationRectangle.Y = (int)value.Y;
            }
        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public Rectangle Collision
        {
            get { return destinationRectangle; }
        }
        #endregion
        public void Update()
        {
            destinationRectangle.X += speed;
            if(Position.X - width > 800)
            {
                isAlive = false;
            }
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("laser");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, Color.White);
        }
    }
}
