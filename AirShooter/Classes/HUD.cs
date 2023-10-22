using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes.Components;


namespace AirShooter.Classes
{
    public class HUD
    {
        private Label scoreLabel;
        private HealthBar healthBar;

        public HUD()
        {
            Vector2 position = new Vector2(20, 20);
            healthBar = new HealthBar(position, 10, 300, 25);
            scoreLabel = new Label("Score: ", new Vector2(500, 25), Color.White);
        }
        public void LoadContent(ContentManager content)
        {
            healthBar.LoadContent(content);
            scoreLabel.LoadContent(content);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            healthBar.Draw(spriteBatch);
            scoreLabel.Draw(spriteBatch, Color.White);
        }
        public void OnPlayerTakeDammage()
        {
            healthBar.NumParts--;
        }
        public void Update(int score)
        {
            healthBar.Update();
            scoreLabel.Text = $"Score: {score}";
        }
        public void Reset()
        {
            healthBar.NumParts = 10;
            scoreLabel.Text = $"Score: 0";
        }
    }
}
