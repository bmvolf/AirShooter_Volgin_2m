using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace AirShooter.Classes
{
    public class Explosion
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private int frameHeight = 134;
        private int frameWidth = 134;
        private int frameNumber;
        private double timeTotalSeconds = 0;
        private double duration = 0.035;
        private bool isAlive;
        #region Constuctors
        public Explosion(Vector2 position)
        {
            texture = null;
            this.position = position;
            isAlive = true;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, 80, 80);
        }
        #endregion
        #region Properties
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        #endregion
        #region Methods
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Explosion");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update(GameTime gameTime)
        {
            timeTotalSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            if(timeTotalSeconds > duration)
            {
                frameNumber++;
                timeTotalSeconds = 0;
            }
            if(frameNumber == 12)
            {
                isAlive = false;
            }
            sourceRectangle = new Rectangle(frameNumber * frameWidth, 0, frameWidth, frameHeight);
        }
        #endregion
    }
}
