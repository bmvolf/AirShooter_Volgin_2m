using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace AirShooter.Classes
{
    internal class Player
    {
        public event Action Dammage;
        public event Action Died;

        private Vector2 position;
        private Texture2D texture;
        private float speed;
        private Rectangle collision;
        private int time;
        private int maxTime;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private int frameHeight = 69;
        private int frameWidth = 115;
        private int frameNumber = 1;
        private double timeTotalSeconds = 0;
        private double duration = 0.08;
        private int health = 10;
        //weapon
        private List<Laser> laserList = new List<Laser>();
        #region Constructs
        public Player()
        {
            position = new Vector2(40, 195);
            texture = null;
            maxTime = 40;
            speed = 6;
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, frameWidth, frameHeight);
        }
        #endregion
        #region Properties
        public Rectangle Collision { get { return collision; } }
        public List<Laser> LaserList { get { return laserList; } set { laserList = value; } }
        #endregion
        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("shipAnimation");
        }
        public void Draw (SpriteBatch spriteBatch)
        {
            foreach(Laser laser in laserList)
            {
                laser.Draw(spriteBatch);
            }
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update(ContentManager manager, GameTime gameTime)
        {
            #region Move
            KeyboardState keyboardState = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                position.X -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                position.X += speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed;
            }
            #endregion
            #region Bounds
            if (position.X < 0)
            {
                position.X = 0;
            }
            if(position.Y < 0)
            {
                position.Y = 0;
            }
            if(position.X > 800 - frameWidth)
            {
                position.X = 800 - frameWidth;
            }
            if(position.Y > 480 - frameHeight)
            {
                position.Y = 480 - frameHeight;
            }
            #endregion
            #region Collision
            collision = new Rectangle((int)position.X, (int)position.Y,
                frameWidth, frameHeight);
            #endregion
            #region Shoot
            time++;
            if(time > maxTime)
            {
                Laser laser = new Laser();
                laser.Position = new Vector2(position.X + frameWidth - laser.Width/2,
                    position.Y + frameHeight/ 2 - laser.Height/2);
                laser.LoadContent(manager);
                laserList.Add(laser);
                time = 0;
            }
            for(int i = 0; i < laserList.Count; i++)
            {
                Laser laser = laserList[i];
                laser.Update();
            }
            for(int i = 0; i < laserList.Count; i++)
            {
                if (!laserList[i].IsAlive)
                {
                    laserList.RemoveAt(i);
                    i--;
                }
            }
            #endregion
            #region Animation
            timeTotalSeconds += gameTime.ElapsedGameTime.TotalSeconds;
            if(timeTotalSeconds > duration)
            {
                frameNumber++;
                timeTotalSeconds = 0;
            }
            if(frameNumber == 8)
            {
                frameNumber = 1;
            }
            sourceRectangle = new Rectangle(frameNumber * frameWidth, 0, frameWidth, frameHeight);
            destinationRectangle = collision;
            #endregion
            if(health == 0)
            {
                if(Died != null)
                {
                    Died();
                }
            }
        }
        public void OnDammage()
        {
            health--;
            if(Dammage != null)
            {
                Dammage();
            }
        }
        public void Reset()
        {
            health = 10;
            position = new Vector2(40, 195);
            laserList.Clear();
        }
    }
}
