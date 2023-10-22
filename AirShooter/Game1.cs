using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AirShooter.Classes;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using AirShooter.Classes.Components;

namespace AirShooter
{
    public class Game1 : Game
    {
        private GameMode gameMode = GameMode.Menu;
        int screenHeight = 480;
        int screenWidth = 800;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Sky sky;
        private List<Bomb> bombs;
        private List<Explosion> explosions;
        private HUD hud;
        private GameOver gameOver;
        private MainMenu mainMenu;
        private int score = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            sky = new Sky();
            bombs = new List<Bomb>();
            explosions = new List<Explosion>();
            hud = new HUD();
            gameOver = new GameOver();
            mainMenu = new MainMenu();

            player.Dammage += hud.OnPlayerTakeDammage;
            player.Died += OnPlayerDeath;
            mainMenu.MenuClickExit += MainMenu_MenuClickExit;
            mainMenu.MenuClickPlay += MainMenu_MenuClickPlay;
            gameOver.ClickExit += MainMenu_MenuClickExit;
            gameOver.ClickAgain += GameOver_ClickAgain;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player.LoadContent(Content);
            sky.LoadContent(Content);
            LoadBomb();
            hud.LoadContent(Content);
            gameOver.LoadContent(Content);
            mainMenu.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (gameMode)
            {
                case GameMode.Menu:
                    mainMenu.Update();
                    break;

                case GameMode.Playing:
                    player.Update(Content, gameTime);
                    sky.Update();
                    CheckCollision();
                    UpdateBomb();
                    UpdateExplosions(gameTime);
                    hud.Update(score);
                    base.Update(gameTime);
                    break;

                case GameMode.GameOver:
                    gameOver.Update();
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (gameMode)
            {
                case GameMode.Menu:
                    mainMenu.Draw(_spriteBatch);
                    break;

                case GameMode.Playing:
                    sky.Draw(_spriteBatch);
                    player.Draw(_spriteBatch);
                    foreach (Bomb bomb in bombs)
                    {
                        bomb.Draw(_spriteBatch);
                    }
                    foreach (Explosion explosion in explosions)
                    {
                        explosion.Draw(_spriteBatch);
                    }
                    hud.Draw(_spriteBatch);
                    Debug.WriteLine(bombs.Count);
                    break;

                case GameMode.GameOver:
                    gameOver.Draw(_spriteBatch, score);
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void UpdateBomb()
        {
            for(int i = 0; i < bombs.Count  ; i ++)
            {
                Bomb bomb = bombs[i];
                bomb.Update();
                //teleport
                if (bomb.Position.X < 0 - bomb.Width)
                {
                    Random random = new Random();
                    int x = random.Next(screenWidth, screenWidth * 2 - bomb.Width);
                    int y = random.Next(0, screenHeight - bomb.Height);
                    bomb.Position = new Vector2(x, y);
                }
                if (!bomb.IsAlive)
                {
                    bombs.RemoveAt(i);
                    i--;
                }
                //check count
                if(bombs.Count < 10)
                {
                    LoadBomb();
                }
            }
        }
        private void LoadBomb()
        {
                Vector2 position = new Vector2();
                Bomb bomb = new Bomb(position);
                bomb.LoadContent(Content);

                int rectWidth = screenWidth;
                int rectHeight = screenHeight;

                Random random = new Random();
                int x = random.Next(rectWidth, rectWidth * 2 - bomb.Width);
                int y = random.Next(0, rectHeight - bomb.Height);

                bomb.Position = new Vector2(x, y);
                bombs.Add(bomb);
        }
        private void CheckCollision()
        {
            foreach(Bomb bomb in bombs)
            {
                if (bomb.Collision.Intersects(player.Collision))
                {
                    player.OnDammage();
                    bomb.IsAlive = false;
                    Explosion explosion = new Explosion(bomb.Position);
                    explosion.LoadContent(Content);
                    explosions.Add(explosion);
                }
                foreach(Laser laser in player.LaserList)
                {
                    if (bomb.Collision.Intersects(laser.Collision))
                    {
                        bomb.IsAlive = false;
                        laser.IsAlive = false;

                        Explosion explosion = new Explosion(bomb.Position);
                        explosion.LoadContent(Content);
                        explosions.Add(explosion);
                        score++;
                    }
                }
            }
        }
        private void UpdateExplosions(GameTime gameTime)
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].Update(gameTime);

                if (!explosions[i].IsAlive)
                {
                    explosions.RemoveAt(i);
                    i--;
                }
            }
        }

        private void OnPlayerDeath()
        {
            gameMode = GameMode.GameOver;
        }
        private void MainMenu_MenuClickExit()
        {
            Exit();
        }
        private void MainMenu_MenuClickPlay()
        {
            gameMode = GameMode.Playing;
            if(bombs.Count == 0)
            {
                LoadBomb();
            }
        }
        private void GameOver_ClickAgain()
        {
            Reset();
            gameMode = GameMode.Menu;
        }
        public void Reset()
        {
            player.Reset();
            score = 0;
            bombs.Clear();
            explosions.Clear();
            hud.Reset();
        }
    }
}