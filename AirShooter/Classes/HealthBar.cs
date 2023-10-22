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
    public class HealthBar
    {
        private Texture2D texture;
        private Vector2 position;
        private int height;
        private int numParts;
        private int partWidth;

        public HealthBar(Vector2 position, int numParts, int width, int height)
        {
            this.position = position;  
            this.height = height;
            this.numParts = numParts;
            partWidth = width / numParts;
            texture = null;
        }
        public int NumParts { get { return numParts; } set { numParts = value; } }

        public Rectangle DestinationRecatngle
        {
            get { return new Rectangle((int)position.X, (int)position.Y, partWidth * numParts, height); }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("healthbar");
        }
        public void Update() { }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRecatngle, Color.White);
        }
    }
}
