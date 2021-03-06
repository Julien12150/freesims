﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Technochips.FreeSims.Game.Activity;
using Technochips.FreeSims.Game.Entity.Item;
using Technochips.FreeSims.Game;

namespace Technochips.FreeSims.Game.Entity
{
	public class Human : Entity
    {
        const float TIMER = 5;
        float timer = TIMER;

        const float BLINKTIMER = 125;
        float blinktimer = BLINKTIMER;

        public bool selected;

        public int finalPosX, finalPosY;

        Control control;
        Cursor cursor;
        SpriteBatch spriteBatch;

        Sprite sprites;

        public int Social;
        public int Fun;
        public int Hunger;

        public bool cannotDie;
        public bool blinking = false;

        public Activity.Activity activity = null;

        Random rnd;

        public HumanStyle style;

        public Texture2D shadow;
		public Shadow shadowClass;

		public Item.Item[] itemList;

		public Human(float posX, float posY, float posZ, int angle, int Social, int Fun, int Hunger, bool cannotDie, Control control, Cursor cursor, Sprite sprites, SpriteBatch spriteBatch, HumanStyle style, int num, Item.Item[] itemList, GraphicsDevice gd)
        {
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
            finalPosX = (int)posX;
            finalPosY = (int)posY;

            this.cannotDie = cannotDie;

            this.angle = angle;
            this.sprites = sprites;

            this.control = control;
            this.cursor = cursor;
            this.spriteBatch = spriteBatch;

            this.Social = Social;
            this.Fun = Fun;
            this.Hunger = Hunger;

            this.style = style;

			this.itemList = itemList;
			this.shadowClass = new Shadow(gd);

            rnd = new Random(((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - int.MaxValue) + num);
        }

        private bool FindHuman(Human obj)
        {
            if (obj == this)
                return true;
            return false;
        }

		public override void Update(GameTime gameTime, Vector2 camera)
        {
			base.Update(gameTime, camera);

            int bn = rnd.Next(int.MinValue, int.MaxValue);
            if (!blinking)
            {
                if (bn < (int.MinValue + 50000000))
                {
                    blinking = true;
                }
            }
            else
            {
                if(blinktimer < 0)
                {
                    blinking = false;
                    blinktimer = BLINKTIMER;
                }
                blinktimer -= (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

			if (Hunger < 50)
			{
				if (rnd.Next() < int.MaxValue / 1000)
				{
					Item.Item f = null;
					foreach (Item.Item i in itemList)
					{
						if (i.type == "Fridge")
						{
							f = i;
							break;
						}
					}
					if (f != null)
					{
						activity = new Eat(this, (Fridge)f);
						activity.Start(gameTime);
					}
				}
			}

            Predicate<Human> ph = FindHuman;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;

			if (activity != null)
			{
				activity.Update(gameTime);
				if (activity != null)
				{
					if (activity.type == "Talk")
					{
						if (timer < 0)
						{
							Fun--;
							Hunger--;
						}
						if (activity.targetH.activity == null)
						{
							activity = null;
						}
					}
					else if (activity.type == "TVWatch")
					{
						if (timer < 0)
						{
							Social--;
							Hunger--;
						}
					}
					else if (activity.type == "Eat")
					{
						if (timer < 0)
						{
							if (posX == finalPosX && posY == finalPosY)
							{
								Hunger = 100;
								activity = null;
							}
						}
					}
					else
					{
						if (timer < 0)
						{
							Social--;
							Fun--;
							Hunger--;
						}
					}

					if (timer < 0)
						timer = TIMER;
				}
			}
			else
			{
				if (timer < 0)
				{
					timer = TIMER;
					Social--;
					Hunger--;
					Fun--;
				}
			}

            if (Social > 100)
                Social = 100;
            else if (Social < 0)
                Social = 0;

            if (Fun > 100)
                Fun = 100;
            else if (Fun < 0)
                Fun = 0;

            if (Hunger > 100)
                Hunger = 100;
            else if (Hunger < 0)
                Hunger = 0;

            if (selected)
            {
                if(control.A && control.isControllerMode)
				{
					finalPosX = (int)cursor.posX;
					finalPosY = (int)cursor.posY;
                    if (activity != null)
                    {
                        if (activity.type == "Talk")
                            activity.targetH.activity = null;
                        else if (activity.type == "TVWatch" || activity.type == "Eat")
                            activity.targetI.humanList.RemoveAll(ph);
                        else if (activity.type == "SitChair")
                            activity.targetI.humanList.Remove(this);
                        activity = null;
                    }
                }
                else if(control.LeftMouseClick && !control.isControllerMode)
                {
					finalPosX = (int)cursor.posX;
					finalPosY = (int)cursor.posY;
                    if (activity != null)
                    {
                        if (activity.type == "Talk")
                            activity.targetH.activity = null;
                        else if (activity.type == "TVWatch" || activity.type == "Eat")
                            activity.targetI.humanList.RemoveAll(ph);
                        else if (activity.type == "SitChair")
                            activity.targetI.humanList.Remove(this);

                        activity = null;
                    }
                }
            }

			var distance = Math.Pow(posX - finalPosX, 2) + Math.Pow(posY - finalPosY, 2);
			if (distance < style.walkSpeed)
			{
				posX = finalPosX;
				posY = finalPosY;
			}
			else
			{
				var deltaX = finalPosX - posX;
				var deltaY = finalPosY - posY;
				var angleT = Math.Atan2(deltaX, deltaY);
				var angleD = angleT * (180 / Math.PI);
				if (angleD < 0)
					angleD += 360;
				//Console.WriteLine(angleD);
				posX += (float)Math.Sin(angleT) * style.walkSpeed;
				posY += (float)Math.Cos(angleT) * style.walkSpeed;
				if (angleD < 22.5 || angleD > 337.5)
					angle = 0;
				else if (angleD > 22.5 && angleD < 67.5)
					angle = 1;
				else if (angleD > 67.5 && angleD < 112.5)
					angle = 2;
				else if (angleD > 112.5 && angleD < 157.5)
					angle = 3;
				else if (angleD > 157.5 && angleD < 202.5)
					angle = 4;
				else if (angleD > 202.5 && angleD < 247.5)
					angle = 5;
				else if (angleD > 247.5 && angleD < 292.5)
					angle = 6;
				else if (angleD > 292.5 && angleD < 337.5)
					angle = 7;
			}
			/*
            if(posX > finalPosX)
            {
                if(posY > finalPosY)
                {
                    posX--;
                    posY--;
                    angle = 5;
                }
                else if (posY == finalPosY)
                {
                    posX--;
                    angle = 6;
                }
                else if (posY < finalPosY)
                {
                    posX--;
                    posY++;
                    angle = 7;
                }
            }
            else if(posX < finalPosX)
            {
                if (posY > finalPosY)
                {
                    posX++;
                    posY--;
                    angle = 3;
                }
                else if (posY == finalPosY)
                {
                    posX++;
                    angle = 2;
                }
                else if (posY < finalPosY)
                {
                    posX++;
                    posY++;
                    angle = 1;
                }
            }
            else if (posX == finalPosX)
            {
                if (posY > finalPosY)
                {
                    posY--;
                    angle = 4;
                }
                else if (posY < finalPosY)
                {
                    posY++;
                    angle = 0;
                }
            }*/
        }

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 camera)
		{
			if (shadow == null)
			{
				shadow = shadowClass.GenerateShadow((int)(((double)sprites.humanSprites.mNoColor.Width / 8) / 1.5), (sprites.humanSprites.mNoColor.Width / 8));
			}
			
            //spriteBatch.Draw(sprites.humanSprite, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (int)posY - sprites.humanSprites.mNoColor.Height + 16, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), new Rectangle((sprites.humanSprites.mNoColor.Width / 8 ) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
			if(shadow != null)
				spriteBatch.Draw(shadow, new Vector2(((int)posX - ((sprites.humanSprites.mNoColor.Width / 8) / 3)) - (int)camera.X, ((int)posY - ((sprites.humanSprites.mNoColor.Width / 8) / 4)) - (int)camera.Y), Color.White * 0.5f);
            if (!style.female)
            {
                if (!blinking)
                {
					spriteBatch.Draw(sprites.humanSprites.mEyes, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int) posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.eyes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mPants, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.pants, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mShirt, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.shirt, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mShoes, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.shoes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mSkin, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.skin, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mNoColor, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                    if (style.hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.hair, 0, Vector2.Zero, SpriteEffects.None, 0);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, 0, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    spriteBatch.Draw(sprites.humanSprites.mEyes, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.eyes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mPants, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.pants, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mShirt, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.shirt, 0, Vector2.Zero, SpriteEffects.None, 0);
					spriteBatch.Draw(sprites.humanSprites.mShoes, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.shoes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mSkin, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.skin, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.mNoColor, new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                    if (style.hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), style.hair, 0, Vector2.Zero, SpriteEffects.None, 0);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.mNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.mNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.mNoColor.Width / 8) * angle, sprites.humanSprites.mNoColor.Height / 2, sprites.humanSprites.mNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
            }
            else
            {
                if (!blinking)
                {
                    spriteBatch.Draw(sprites.humanSprites.fEyes, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.eyes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fPants, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.pants, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fShirt, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.shirt, 0, Vector2.Zero, SpriteEffects.None, 0);
					spriteBatch.Draw(sprites.humanSprites.fShoes, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.mNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.shoes, 0, Vector2.Zero, SpriteEffects.None, 0);
					spriteBatch.Draw(sprites.humanSprites.fSkin, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.skin, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fNoColor, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                    if (style.hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.hair, 0, Vector2.Zero, SpriteEffects.None, 0);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, 0, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
                else
                {
                    spriteBatch.Draw(sprites.humanSprites.fEyes, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.eyes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fPants, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.pants, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fShirt, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.shirt, 0, Vector2.Zero, SpriteEffects.None, 0);
					spriteBatch.Draw(sprites.humanSprites.fShoes, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.shoes, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fSkin, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.skin, 0, Vector2.Zero, SpriteEffects.None, 0);
                    spriteBatch.Draw(sprites.humanSprites.fNoColor, new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);

                    if (style.hairStyle != 0)
                    {
                        spriteBatch.Draw(sprites.humanSprites.GetHair(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), style.hair, 0, Vector2.Zero, SpriteEffects.None, 0);
                        spriteBatch.Draw(sprites.humanSprites.GetHairNoColor(style.hairStyle), new Rectangle((int)(posX - ((sprites.humanSprites.fNoColor.Width / 8) / 2) - camera.X), (((int)posY - (int)posZ) - (sprites.humanSprites.fNoColor.Height / 2) + 16) - (int)camera.Y, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), new Rectangle((sprites.humanSprites.fNoColor.Width / 8) * angle, sprites.humanSprites.fNoColor.Height / 2, sprites.humanSprites.fNoColor.Width / 8, sprites.humanSprites.fNoColor.Height / 2), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
                    }
                }
            }
            if (activity != null)
            {
				activity.Draw(gameTime, spriteBatch, sprites, camera);
            }
        }
    }
}
