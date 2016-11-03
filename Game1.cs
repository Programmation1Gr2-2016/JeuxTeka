using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;

namespace Teka
{

    // ----------------------------------------------------------------------------------INTRO------------------------------------------------------------------------------------------------
    // Alexandre Dufresne et Samuel Joyal
    // Projet MonoGame
    // TEKA
    // "The enchanted kingdom of Astria"
    //-----------------------------------------------------------------------------------INTRO------------------------------------------------------------------------------------------------

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        KeyboardState keys = new KeyboardState();

        KeyboardState previousKeys = new KeyboardState();

        Rectangle fenetre;

        GameObjectAnime persoPrincipal;

        GameObjectAnime slime;

        GameObjectAnime gobelin;

        GameObject mapIntro;
        Vector2 mapIntroPosition;

        GameObject maisonPrincipal;
        GameObject maisonPrincipalOuverte;

        GameObject interieurMaison;

        GameObject transparente;

        SoundEffect sonAlex;
        SoundEffectInstance alexSong;

        SoundEffect sonPorte;
        SoundEffectInstance bruitPorte;

        GameObject curseur;
        Vector2 m_mousePos;

        GameObject buisson;

        GameObject sceneBataille;


        Vector2 positionProjectile;



        GameObject boiteTexteBas;
        GameObject boiteTexteHaut;


        Random rnd = new Random();

        GameObject introWall;

        GameObject flecheDroite1;

        GameObject flecheDroite2;

        GameObject flecheGauche1;

        GameObject flecheGauche2;

        bool intro = true;
        bool jeu = false;
        int choix = 1;





        GameObject boutonFight;

        GameObject boutonAttack;

        GameObject boutonDefend;

        GameObject boutonBack;

        GameObject boutonBag;

        int chancesBataille =0;

        bool battle = false;

        int phaseBataille = 1;

        string click = "off";








        int monde = 0;

        int compteur = 0;

        int direction = 0;

        string changementPiece = "off";

        int time = 0;

        int intersection;

        int temps;

        string choixIntro = "off";


        // ---------------------------------------- Variable caractéristiques perso principal ---------------------------------------------

        int niveau = 1;
        int exp = 0;
        int expGagne = 0;
        int expProchainNiveau = 30;

        int hpMax = 0;
        int hp = 0;
        int force = 0;
        int defense = 0;
        string nomPersonnage;
        int niveauSuperieur = 0;

        // ---------------------------------------- Variable caractéristiques ennemies ---------------------------------------------


        int niveauEnnemie = 0;
        int hpEnnemie = 0;
        int forceEnnemie = 0;
        int defenseEnnemie = 0;
        int difficulteEnnemie = 1;

        int damageEnnemie = 0;
        int damageMinEnnemie = 0;
        int damageMaxEnnemie = 0;



        SpriteFont classes;

        


        string bataille = "off";



        public Game1()
        {
              graphics = new GraphicsDeviceManager(this);
              Content.RootDirectory = "Content";
             fenetre.Width = graphics.PreferredBackBufferWidth = 1920;
             fenetre.Height = graphics.PreferredBackBufferHeight = 1080;
              graphics.ApplyChanges(); 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            persoPrincipal = new GameObjectAnime();


            persoPrincipal.estVivant = true;  //seulement pour l'instant
            persoPrincipal.vitesse = 0;   //c'est la map qui bouge
            persoPrincipal.sprite = Content.Load<Texture2D>("SpriteSheet.png"); // SpriteSheet
            persoPrincipal.position = new Rectangle(0, 0, 50, 72);
            persoPrincipal.direction = Vector2.Zero;
            persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
            persoPrincipal.position.Offset(fenetre.Width / 2 - 26, fenetre.Height / 2 - 36);

            slime = new GameObjectAnime();
            slime.estVivant = true;  //seulement pour l'instant
            slime.vitesse = 0;   //c'est la map qui bouge
            slime.sprite = Content.Load<Texture2D>("SlimeSpriteSheet.png"); // SpriteSheet
            slime.position = new Rectangle(0, 0, 80, 60);
            slime.direction = Vector2.Zero;
            slime.slimeEtat = GameObjectAnime.SlimeEtat.AttenteBasSlime;
            slime.position.Offset(fenetre.Width / 2 + 26, fenetre.Height / 2 + 36);

            gobelin = new GameObjectAnime();
            gobelin.estVivant = false;  //seulement pour l'instant
            gobelin.vitesse = 0;   //c'est la map qui bouge
            gobelin.sprite = Content.Load<Texture2D>("SpriteSheetGobelin.png"); // SpriteSheet
            gobelin.position = new Rectangle(0, 0, 75, 75);
            gobelin.direction = Vector2.Zero;
            gobelin.gobelinEtat = GameObjectAnime.GobelinEtat.AttenteDroiteGobelin;
            gobelin.position.Offset(0,0);

            mapIntro = new GameObject();
            mapIntro.estVivant = true;
            mapIntro.vitesse = 5;
            mapIntro.sprite = Content.Load<Texture2D>("map_village2.png");
            mapIntro.position = mapIntro.sprite.Bounds;
            mapIntro.position.Offset(0,0);

            maisonPrincipal = new GameObject();
            maisonPrincipal.estVivant = true;
            maisonPrincipal.vitesse = 0;
            maisonPrincipal.sprite = Content.Load<Texture2D>("MaisonPrincipale.png");
            maisonPrincipal.position = maisonPrincipal.sprite.Bounds;
            maisonPrincipal.position.Offset(mapIntro.position.X +300, mapIntro.position.Y + 1150);

            maisonPrincipalOuverte = new GameObject();
            maisonPrincipalOuverte.estVivant = true;
            maisonPrincipalOuverte.vitesse = 0;
            maisonPrincipalOuverte.sprite = Content.Load<Texture2D>("MaisonPrincipaleOuverte.png");
            maisonPrincipalOuverte.position = maisonPrincipalOuverte.sprite.Bounds;
            maisonPrincipalOuverte.position.Offset(mapIntro.position.X + 300, mapIntro.position.Y + 1150);

            interieurMaison = new GameObject();
            interieurMaison.estVivant = true;
            interieurMaison.vitesse = 0;
            interieurMaison.sprite = Content.Load<Texture2D>("InterieurMaison.png");
            interieurMaison.position = interieurMaison.sprite.Bounds;
            interieurMaison.position.Offset(mapIntro.position.X + 310, mapIntro.position.Y + 620);

            transparente = new GameObject();
            transparente.estVivant = true;
            transparente.vitesse = 0;
            transparente.sprite = Content.Load<Texture2D>("Transparente.png");
            transparente.position = transparente.sprite.Bounds;
            transparente.position.Offset(mapIntro.position.X + 560, mapIntro.position.Y + 1680);

            sonAlex = Content.Load<SoundEffect>("SongTeka");
            alexSong = sonAlex.CreateInstance();

            sonPorte = Content.Load<SoundEffect>("Click");
            bruitPorte = sonPorte.CreateInstance();

            curseur = new GameObject();
            curseur.sprite = Content.Load<Texture2D>("SwordCursor.png");
            curseur.position = curseur.sprite.Bounds;
            curseur.position.Offset(-760, 235);

            buisson = new GameObject();
            buisson.estVivant = true;
            buisson.sprite = Content.Load<Texture2D>("Bushes.png");
            buisson.position = buisson.sprite.Bounds;
            buisson.position.Offset(mapIntro.position.X + 1350, mapIntro.position.Y + 25);

            sceneBataille = new GameObject();
            sceneBataille.estVivant = true;
            sceneBataille.sprite = Content.Load<Texture2D>("BattleScene.png");
            sceneBataille.position = sceneBataille.sprite.Bounds;
            sceneBataille.position.Offset(0,0);

            flecheDroite1 = new GameObject();
            flecheDroite1.estVivant = true;
            flecheDroite1.sprite = Content.Load<Texture2D>("FlecheDroiteUp");
            flecheDroite1.position = flecheDroite1.sprite.Bounds;
            flecheDroite1.position.Offset(persoPrincipal.position.X + flecheDroite1.sprite.Width, persoPrincipal.position.Y);

            flecheDroite2 = new GameObject();
            flecheDroite2.estVivant = false;
            flecheDroite2.sprite = Content.Load<Texture2D>("FlecheDroiteDown");
            flecheDroite2.position = flecheDroite2.sprite.Bounds;
            flecheDroite2.position.Offset(persoPrincipal.position.X + flecheDroite2.sprite.Width, persoPrincipal.position.Y);

            flecheGauche1 = new GameObject();
            flecheGauche1.estVivant = true;
            flecheGauche1.sprite = Content.Load<Texture2D>("FlecheGaucheUp");
            flecheGauche1.position = flecheGauche1.sprite.Bounds;
            flecheGauche1.position.Offset(persoPrincipal.position.X - flecheGauche1.sprite.Width - 55, persoPrincipal.position.Y);

            flecheGauche2 = new GameObject();
            flecheGauche2.estVivant = false;
            flecheGauche2.sprite = Content.Load<Texture2D>("FlecheGaucheDown");
            flecheGauche2.position = flecheGauche2.sprite.Bounds;
            flecheGauche2.position.Offset(persoPrincipal.position.X - flecheGauche2.sprite.Width - 55, persoPrincipal.position.Y);

            boutonFight = new GameObject();
            boutonFight.estVivant = true;
            boutonFight.sprite = Content.Load<Texture2D>("FightButton.png");
            boutonFight.position = boutonFight.sprite.Bounds;
            boutonFight.position.Offset(sceneBataille.position.X + sceneBataille.sprite.Width - boutonFight.sprite.Width, sceneBataille.position.Y + sceneBataille.sprite.Height - boutonFight.sprite.Height * 8);

            boutonAttack = new GameObject();
            boutonAttack.estVivant = true;
            boutonAttack.sprite = Content.Load<Texture2D>("AttackButton.png");
            boutonAttack.position = boutonAttack.sprite.Bounds;
            boutonAttack.position.Offset(sceneBataille.position.X + sceneBataille.sprite.Width - boutonFight.sprite.Width, sceneBataille.position.Y + sceneBataille.sprite.Height - boutonFight.sprite.Height * 7);

            boutonDefend = new GameObject();
            boutonDefend.estVivant = true;
            boutonDefend.sprite = Content.Load<Texture2D>("DefendButton.png");
            boutonDefend.position = boutonDefend.sprite.Bounds;
            boutonDefend.position.Offset(sceneBataille.position.X + sceneBataille.sprite.Width - boutonFight.sprite.Width, sceneBataille.position.Y + sceneBataille.sprite.Height - boutonFight.sprite.Height * 6);

            boutonBag = new GameObject();
            boutonBag.estVivant = true;
            boutonBag.sprite = Content.Load<Texture2D>("BagButton.png");
            boutonBag.position = boutonBag.sprite.Bounds;
            boutonBag.position.Offset(sceneBataille.position.X + sceneBataille.sprite.Width - boutonFight.sprite.Width, sceneBataille.position.Y + sceneBataille.sprite.Height - boutonFight.sprite.Height * 7);

            boutonBack = new GameObject();
            boutonBack.estVivant = true;
            boutonBack.sprite = Content.Load<Texture2D>("BackButton.png");
            boutonBack.position = boutonBack.sprite.Bounds;
            boutonBack.position.Offset(sceneBataille.position.X + sceneBataille.sprite.Width - boutonFight.sprite.Width, sceneBataille.position.Y + sceneBataille.sprite.Height - boutonFight.sprite.Height * 5);

            introWall = new GameObject();
            introWall.sprite = Content.Load<Texture2D>("IntroWall");
            introWall.position = introWall.sprite.Bounds;
            introWall.position.Offset(fenetre.X, fenetre.Y);

            boiteTexteBas = new GameObject();
            boiteTexteBas.estVivant = false;
            boiteTexteBas.sprite = Content.Load<Texture2D>("BoiteTexteBas.png");
            boiteTexteBas.position = boiteTexteBas.sprite.Bounds;
            boiteTexteBas.position.Offset(960 - boiteTexteBas.sprite.Width / 2, 1080 - boiteTexteBas.sprite.Height - 20);

            boiteTexteHaut = new GameObject();
            boiteTexteHaut.estVivant = false;
            boiteTexteHaut.sprite = Content.Load<Texture2D>("BoiteTexteHaut.png");
            boiteTexteHaut.position = boiteTexteHaut.sprite.Bounds;
            boiteTexteHaut.position.Offset(960 - boiteTexteHaut.sprite.Width / 2, 25);


            classes = Content.Load<SpriteFont>("Classe");





            // TODO: use this.Content to load your game content here
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

  /*---------------------------------------------------------------------------------------------------------------------------------------------------------------
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   *                                                                Update --- Update --- Update
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * 
   * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        protected override void Update(GameTime gameTime)
        {


            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

            * 
            *                                                         Intro --- Intro --- Intro
            *                                                         
            *                                                         
            * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



            if (intro == true)
            {
                flecheDroite1.estVivant = true;
                flecheDroite2.estVivant = false;
                flecheGauche1.estVivant = true;
                flecheGauche2.estVivant = false;
                persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
                if (Keyboard.GetState().IsKeyDown(Keys.A) && choixIntro == "off")
                {
                    flecheGauche2.estVivant = true;
                    flecheGauche1.estVivant = false;
                    if (choix != 0)
                    {
                        choix--;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && choixIntro == "off")
                {
                    flecheDroite2.estVivant = true;
                    flecheDroite1.estVivant = false;
                    if (choix != 3) // 3 quand il vas y avoir le juggertnaut
                    {
                        choix++;
                    }
                }
                if (choix == 1)
                {
                    persoPrincipal.sprite = Content.Load<Texture2D>("SpriteSheet.png");
                    //aussi ajouter ici quand on vas avoir la vie l attaque defense et tout le kit
                    choixIntro = "on";
                }
                if (choix == 2)
                {
                    persoPrincipal.sprite = Content.Load<Texture2D>("WarriorPersoPrincipal.png");
                    choixIntro = "on";
                }
                if (choix == 3)
                {
                    persoPrincipal.sprite = Content.Load<Texture2D>("JuggernautSpriteSheet.png");
                    choixIntro = "on";
                }

                slime.position.X = persoPrincipal.position.X;
                slime.position.Y = persoPrincipal.position.Y + 100;
                slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpBasSlime;

                switch (choix)
                {

                    case 1:

                        persoPrincipal.hpMax = 75;
                        persoPrincipal.hp = hpMax;
                        persoPrincipal.force = 3;
                        persoPrincipal.defense = 1;

                        break;

                    case 2:

                        persoPrincipal.hpMax = 50;
                        persoPrincipal.hp = hpMax;
                        persoPrincipal.force = 3;
                        persoPrincipal.defense = 3;

                        break;

                    case 3:

                        persoPrincipal.hpMax = 150;
                        persoPrincipal.hp = hpMax;
                        persoPrincipal.force = 1;
                        persoPrincipal.defense = 3;

                        break;



                }

                if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                {

                    choixIntro = "off";
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    intro = false;
                    jeu = true;
                }



            }

           if (jeu == true && bataille != "on")
            {

                /*
                            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                            {

                                projectile.estVivant = true;

                                projectile.position.X = persoPrincipal.position.X;
                                projectile.position.Y = persoPrincipal.position.Y;

                            }else if (Keyboard.GetState().IsKeyUp(Keys.Space))
                            {

                                positionProjectile = m_mousePos;

                                projectile.position.X += curseur.position.X;
                                projectile.position.Y -= curseur.position.Y;
                            }

                            */





                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Chanson --- Musique --- Son
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                alexSong.Play();



                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Exit --- Exit --- Exit
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                              Animation Personnage --- Animation Personnage --- Animation personnage
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                
                                if (changementPiece != "on" && bataille != "on")
                                {
                                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                                    {

                                        persoPrincipal.etat = GameObjectAnime.Etat.RunHaut;
                                        slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpHautSlime;
                                        direction = 1;
                                    }

                                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                                    {

                                        persoPrincipal.etat = GameObjectAnime.Etat.RunBas;
                                        slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpBasSlime;
                                        direction = 2;
                                    }

                                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                                    {

                                        persoPrincipal.etat = GameObjectAnime.Etat.RunGauche;
                                        slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpGaucheSlime;
                                        direction = 3;
                                    }

                                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                                    {

                                        persoPrincipal.etat = GameObjectAnime.Etat.RunDroite;
                                        slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpDroiteSlime;
                                        direction = 4;
                                    }
                                    if (Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
                                    {
                                        direction = 0;

                                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunHaut)
                                        {
                                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteHaut;
                                            slime.slimeEtat = GameObjectAnime.SlimeEtat.AttenteHautSlime;
                                        }
                                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunBas)
                                        {
                                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteBas;
                                            slime.slimeEtat = GameObjectAnime.SlimeEtat.AttenteBasSlime;
                                        }
                                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunGauche)
                                        {
                                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteGauche;
                                            slime.slimeEtat = GameObjectAnime.SlimeEtat.AttenteGaucheSlime;
                                        }
                                        if (persoPrincipal.etat == GameObjectAnime.Etat.RunDroite)
                                        {
                                            persoPrincipal.etat = GameObjectAnime.Etat.AttenteDroite;
                                            slime.slimeEtat = GameObjectAnime.SlimeEtat.AttenteDroiteSlime;
                                        }
                                    }
                                }

                                keys = Keyboard.GetState();

                                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                                {

                                    mapIntro.vitesse = 10;

                                }





                                
/*
                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

                mapIntroPosition = gamepadState.ThumbSticks.Left * mapIntro.vitesse;

                mapIntro.position.X -= (int)mapIntroPosition.X;
                mapIntro.position.Y += (int)mapIntroPosition.Y;
                
                */



                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

               * 
               *                                   Changement de piece --- Changement de piece --- Changement de piece
               *                                                         
               *                                                         
               * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (persoPrincipal.position.Intersects(transparente.position) && compteur == 0 && Keyboard.GetState().IsKeyDown(Keys.W))
                {

                    changementPiece = "on";


                    direction = 1;
                    persoPrincipal.etat = GameObjectAnime.Etat.RunHaut;
                    monde = 1;



                }



                if (persoPrincipal.position.Intersects(transparente.position) && compteur == 1 && Keyboard.GetState().IsKeyDown(Keys.S))
                {

                    changementPiece = "on";


                    direction = 2;
                    persoPrincipal.etat = GameObjectAnime.Etat.RunBas;
                    monde = 1;



                }



                if (persoPrincipal.position.Intersects(transparente.position))
                {


                }
                else if (changementPiece == "on")
                {

                    if (compteur == 1)
                    {
                        monde = 0;
                        compteur = 0;
                        bruitPorte.Play();
                    }


                    else
                    {
                        monde = 2;
                        compteur = 1;
                        bruitPorte.Play();
                    }

                    changementPiece = "off";
                }

                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                *                                                                                      ___
                *                                              Rince --- Wash --- Repeat --- VICTORY  | 0 |  ------  (Bataille)
                *                                                                                     |_-_|                     
                *                                                                                     |_|_|
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (persoPrincipal.position.Intersects(buisson.position))
                {



                    chancesBataille = rnd.Next(1, 700);


                    if (chancesBataille == 5)
                    {

                        hpEnnemie = 30;
                        forceEnnemie = 2;
                        defense = 1;
                        niveauEnnemie = 1;
                        damageMinEnnemie = 1;
                        damageMaxEnnemie = 3;

                        phaseBataille = 1;
                        bataille = "on";
                        monde = 3;



                    }


                }









                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Direction --- Direction --- Direction
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                switch (direction)
                {

                    case 1:

                        mapIntro.position.Y += mapIntro.vitesse;
                        maisonPrincipal.position.Y += mapIntro.vitesse;
                        transparente.position.Y += mapIntro.vitesse;
                        maisonPrincipalOuverte.position.Y += mapIntro.vitesse;
                        interieurMaison.position.Y += mapIntro.vitesse;
                        buisson.position.Y += mapIntro.vitesse;
                        slime.position.Y = persoPrincipal.position.Y + slime.spriteAfficheSlime.Height;
                        slime.position.X = persoPrincipal.position.X;


                        break;

                    case 2:
                        mapIntro.position.Y -= mapIntro.vitesse;
                        maisonPrincipal.position.Y -= mapIntro.vitesse;
                        transparente.position.Y -= mapIntro.vitesse;
                        maisonPrincipalOuverte.position.Y -= mapIntro.vitesse;
                        interieurMaison.position.Y -= mapIntro.vitesse;
                        buisson.position.Y -= mapIntro.vitesse;
                        slime.position.Y = persoPrincipal.position.Y - slime.spriteAfficheSlime.Height + 20;
                        slime.position.X = persoPrincipal.position.X;


                        break;

                    case 3:
                        mapIntro.position.X += mapIntro.vitesse;
                        maisonPrincipal.position.X += mapIntro.vitesse;
                        transparente.position.X += mapIntro.vitesse;
                        maisonPrincipalOuverte.position.X += mapIntro.vitesse;
                        interieurMaison.position.X += mapIntro.vitesse;
                        buisson.position.X += mapIntro.vitesse;
                        slime.position.Y = persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height - slime.spriteAfficheSlime.Height + 20;
                        slime.position.X = persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width;


                        break;

                    case 4:
                        mapIntro.position.X -= mapIntro.vitesse;
                        maisonPrincipal.position.X -= mapIntro.vitesse;
                        transparente.position.X -= mapIntro.vitesse;
                        maisonPrincipalOuverte.position.X -= mapIntro.vitesse;
                        interieurMaison.position.X -= mapIntro.vitesse;
                        buisson.position.X -= mapIntro.vitesse;
                        slime.position.Y = persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height - slime.spriteAfficheSlime.Height + 20;
                        slime.position.X = persoPrincipal.position.X - slime.spriteAfficheSlime.Width + 20;


                        break;

                }

                if (changementPiece != "on")
                {

                    BoundsMaisonPersoPrincipal();
                }





            }

/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Souris --- Souris --- Souris
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            MouseState mouseState = Mouse.GetState();
            m_mousePos.X = mouseState.X;
            m_mousePos.Y = mouseState.Y;

            curseur.position.X = mouseState.X;
            curseur.position.Y = mouseState.Y;


/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Updates --- Fin --- Update
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            if (bataille == "on")
            {

                Battle();

                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || hpEnnemie <= 0)
                {
                    gobelin.estVivant = false;
                    monde = 0;
                    bataille = "off";
                    persoPrincipal.position.X = fenetre.Width / 2 - 26;
                    persoPrincipal.position.Y = fenetre.Height / 2 - 36;
                    slime.position.X = persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width;
                    slime.position.Y = persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height / 2;
                }


              }

// ------------------------------- -------------- ------------- ------------ --------- ----- -- Fin Bataille -- ----- --------- ----------- ---------------- --------------------------------

            ChangementDeMonde();
            gobelin.Update(gameTime);
            persoPrincipal.Update(gameTime);
            slime.Update(gameTime);
            base.Update(gameTime);
        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        *                                                                Fonction --- Fonction --- Fonction
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        * 
        * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

        * 
        *                                                         Experience --- Gold --- Fin de la bataille
        *                                                         
        *                                                         
        * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        void Experience()
        {

            persoPrincipal.expGagne = niveauEnnemie * 7 * difficulteEnnemie;
            persoPrincipal.exp = persoPrincipal.exp + persoPrincipal.expGagne;

            //   cout << "You gained " << gainedxp << " xp." << endl << endl;
            //    if (cin.get() == '\n')


            if (persoPrincipal.exp >= persoPrincipal.expProchainNiveau)
            {

                //     cout << "You level up !!!" << endl << endl;
                //     if (cin.get() == '\n')

                persoPrincipal.niveau += 1;
                persoPrincipal.exp = persoPrincipal.exp - persoPrincipal.expProchainNiveau;
                persoPrincipal.expProchainNiveau = persoPrincipal.expProchainNiveau * 2;

                //     srand(time(0));

                //    niveauSuperieur = 1 + (rand() % 7);

                switch (persoPrincipal.niveauSuperieur)
                {

                    case 1:

                        //    cout << "Your strenght increase !" << endl << endl;
                        //    if (cin.get() == '\n')
                        persoPrincipal.force += niveau;

                        break;

                    case 2:

                        //    cout << "Your defence increase !" << endl << endl;
                        //    if (cin.get() == '\n')
                        persoPrincipal.defense += niveau;

                        break;

                    case 3:

                        //     cout << "Your max hp increase !" << endl << endl;
                        //     if (cin.get() == '\n')
                        persoPrincipal.hpMax += niveau * 25;

                        break;

                    case 4:

                        //    cout << "Your max hp and your defense increase !" << endl << endl;
                        //    if (cin.get() == '\n')
                        persoPrincipal.hpMax += niveau * 25;
                        persoPrincipal.defense += niveau;

                        break;


                    case 5:

                        //   cout << "Your Defense and your strength increase !" << endl << endl;
                        //   if (cin.get() == '\n')
                        persoPrincipal.defense += niveau;
                        persoPrincipal.force += niveau;

                        break;


                    case 6:

                        //    cout << "Your Maxhp and your strength increase !" << endl << endl;
                        //    if (cin.get() == '\n')
                        persoPrincipal.force += niveau;
                        persoPrincipal.hpMax += niveau * 25;


                        break;


                    case 7:

                        //   cout << "ALL your stats increase !" << endl << endl;
                        //   if (cin.get() == '\n')
                        persoPrincipal.force += niveau;
                        persoPrincipal.hpMax += niveau * 25;
                        persoPrincipal.defense += niveau;

                        break;



                }





            }


        }



            


/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Battle --- Fonction --- Bataille
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        protected void Battle()
        {

            gobelin.estVivant = true;
            persoPrincipal.etat = GameObjectAnime.Etat.AttenteGauche;
            slime.slimeEtat = GameObjectAnime.SlimeEtat.JumpGaucheSlime;
            gobelin.gobelinEtat = GameObjectAnime.GobelinEtat.AttenteDroiteGobelin;
            gobelin.position.X = sceneBataille.position.X + 200;
            gobelin.position.Y = sceneBataille.position.Y + 900;
            persoPrincipal.position.X = sceneBataille.position.X + 1600;
            persoPrincipal.position.Y = sceneBataille.position.Y + 800;
            slime.position.X = sceneBataille.position.X + 1600;
            slime.position.Y = sceneBataille.position.Y + 800 + persoPrincipal.spriteAffiche.Height + slime.spriteAfficheSlime.Height;

            MouseState mouseState = Mouse.GetState();


            switch (phaseBataille)
        {

                case 1: // Fight | Bag | Run


                    if (mouseState.LeftButton == ButtonState.Released)
                    {

                        click = "off";
                    }

                    if (curseur.position.Intersects(boutonFight.position) && mouseState.LeftButton == ButtonState.Pressed && click == "off")
                    {
                        phaseBataille = 2;
                        click = "on";

                    }

                    if(curseur.position.Intersects(boutonBag.position) && mouseState.LeftButton == ButtonState.Pressed && click == "off")
                    {
                        phaseBataille = 4;
                        click = "on";
                    }

                    break;

                case 2: // Fight --> Attack | Defend | Return

                    if (mouseState.LeftButton == ButtonState.Released)
                    {

                        click = "off";
                    }


                    if (curseur.position.Intersects(boutonAttack.position) && mouseState.LeftButton == ButtonState.Pressed && click == "off")
                    {
                        phaseBataille = 3;
                        click = "on";
                    }


                    if (curseur.position.Intersects(boutonDefend.position) && mouseState.LeftButton == ButtonState.Pressed && click == "off")
                    {
                        phaseBataille = 4;
                        click = "on";
                    }


                    if (curseur.position.Intersects(boutonBack.position) && mouseState.LeftButton == ButtonState.Pressed && click == "off")
                    {
                        phaseBataille = 1;
                        click = "on";
                    }

                    break;

                case 3: // Attack

                    DamagePersoPrincipal();
                    hpEnnemie -= persoPrincipal.damage;


                    phaseBataille = 1;

                    break;

                case 4: // defense

                    break;

                case 5: // Attack Ennemie

                    break;


            }






        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

        * 
        *                                                         Damage --- Perso Principal --- Combat
        *                                                         
        *                                                         
        * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


            protected void DamagePersoPrincipal()
        {
            persoPrincipal.damage = rnd.Next(persoPrincipal.damageMin, persoPrincipal.damageMax + 1) * persoPrincipal.force;


        }

        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

        * 
        *                                                         Damage --- Ennemie --- Combat
        *                                                         
        *                                                         
        * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        protected void DamageEnnemie()
        {
            damageEnnemie = rnd.Next(damageMinEnnemie, damageMaxEnnemie + 1) * forceEnnemie;


        }


        /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

        * 
        *                                                         Bounds --- Maison Principal --- Repositionnement des images
        *                                                         
        *                                                         
        * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



        protected void BoundsMaisonPersoPrincipal()
        {
            if (mapIntro.estVivant == true)
            {


                if (persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width > maisonPrincipal.position.Left && persoPrincipal.position.X < maisonPrincipal.position.Right && persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && persoPrincipal.position.Y < maisonPrincipal.position.Bottom)
                {

                    mapIntro.vitesse = 0;
                    if (persoPrincipal.position.X > maisonPrincipal.position.Left && direction == 3)
                    {
                        maisonPrincipal.position.X = persoPrincipal.position.X - maisonPrincipal.position.Width;
                        mapIntro.position.X = maisonPrincipal.position.X - 300;
                        transparente.position.X = mapIntro.position.X + 560;
                        interieurMaison.position.X = mapIntro.position.X + 310;
                        maisonPrincipalOuverte.position.X = mapIntro.position.X + 300;
                        buisson.position.X = mapIntro.position.X + 1350;
                    }
                    if (persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width < maisonPrincipal.position.Right && direction == 4)
                    {

                        maisonPrincipal.position.X = persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width;
                        mapIntro.position.X = maisonPrincipal.position.X - 300;
                        transparente.position.X = mapIntro.position.X + 560;
                        interieurMaison.position.X = mapIntro.position.X + 310;
                        maisonPrincipalOuverte.position.X = mapIntro.position.X + 300;
                        buisson.position.X = mapIntro.position.X + 1350;
                    }
                }
                if (persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && persoPrincipal.position.Y < maisonPrincipal.position.Bottom && persoPrincipal.position.X + persoPrincipal.spriteAffiche.Width > maisonPrincipal.position.Left && persoPrincipal.position.X < maisonPrincipal.position.Right)
                {

                    mapIntro.vitesse = 0;
                    if (persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height > maisonPrincipal.position.Top && direction == 2)
                    {

                        maisonPrincipal.position.Y = persoPrincipal.position.Y + persoPrincipal.spriteAffiche.Height;
                        mapIntro.position.Y = maisonPrincipal.position.Y - 1150;
                        transparente.position.Y = mapIntro.position.Y + 1680;
                        interieurMaison.position.Y = mapIntro.position.Y + 620;
                        maisonPrincipalOuverte.position.Y = mapIntro.position.Y + 1150;
                        buisson.position.Y = mapIntro.position.Y + 25;
                    }
                    if (persoPrincipal.position.Y < maisonPrincipal.position.Bottom && direction == 1)
                    {

                        maisonPrincipal.position.Y = persoPrincipal.position.Y - maisonPrincipal.position.Height;
                        mapIntro.position.Y = maisonPrincipal.position.Y - 1150;
                        transparente.position.Y = mapIntro.position.Y + 1680;
                        interieurMaison.position.Y = mapIntro.position.Y + 620;
                        maisonPrincipalOuverte.position.Y = mapIntro.position.Y + 1150;
                        buisson.position.Y = mapIntro.position.Y + 25;
                    }
                }


                mapIntro.vitesse = 5;



            }


        }



/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                     Changement de monde --- Differentes Places --- Differents Mondes
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/



        protected void ChangementDeMonde()
        {

            mapIntro.estVivant = false;
            maisonPrincipal.estVivant = false;
            transparente.estVivant = true;
            maisonPrincipalOuverte.estVivant = false;
            interieurMaison.estVivant = false;
            buisson.estVivant = false;
            sceneBataille.estVivant = false;
            boutonFight.estVivant = false;
            boutonAttack.estVivant = false;
            boutonDefend.estVivant = false;
            boutonBack.estVivant = false;
            boutonBag.estVivant = false;
            boiteTexteBas.estVivant = false;
            boiteTexteHaut.estVivant = false;


            switch (monde)
            {

                case 0: // la map / monde d'origine

                    mapIntro.estVivant = true;
                    maisonPrincipal.estVivant = true;
                    transparente.estVivant = true;
                    buisson.estVivant = true;
              //      boiteTexteBas.estVivant = true;
              //      boiteTexteHaut.estVivant = true;

                    break;

                case 1: // interlude quand on entre dans la maison principal

                    maisonPrincipalOuverte.estVivant = true;
                    mapIntro.estVivant = true; 

                    break;

                case 2: // dans la maison principal

                    interieurMaison.estVivant = true;

                    break;

                case 3: // bataille

                    sceneBataille.estVivant = true;

                    switch (phaseBataille)
                    {
                        case 1: // Fight | Bag | Run

                            boutonFight.estVivant = true;
                            boutonBag.estVivant = true;

                            break;

                        case 2: // Fight --> Attack | Defend | Return

                            boutonAttack.estVivant = true;
                            boutonDefend.estVivant = true;
                            boutonBack.estVivant = true;

                            break;

                        case 3://Attaque

                            boiteTexteHaut.estVivant = true;

                            break;

                        case 4://Defense

                            boiteTexteBas.estVivant = true;

                            break;

                        case 5://Attaque ennemie

                            break;

                    }
                    


                    break;
            }



        }



/*---------------------------------------------------------------------------------------------------------------------------------------------------------------
* 
* 
* 
* 
* 
* 
* 
* 
*
* 
*                                                                Drawing --- des beaux --- Dessins
* 
* 
* 
* 
* 
* 
* 
* 
* 
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if(jeu == true)
            {


/*---------------------------------------------------------------------------------------------------------------------------------------------------------------

* 
*                                                         Le --- Monde --- Zero
*                                                         
*                                                         
* -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

                if (mapIntro.estVivant == true)
                {
                    spriteBatch.Draw(mapIntro.sprite, mapIntro.position, Color.White);

                }


                if (transparente.estVivant == true)
                {

                    spriteBatch.Draw(transparente.sprite, transparente.position, Color.White);

                }

                if (buisson.estVivant == true)
                {

                    spriteBatch.Draw(buisson.sprite, buisson.position, Color.White);
                }


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Maison --- et ses --- Composantes
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                if (interieurMaison.estVivant == true)
                {
                    spriteBatch.Draw(interieurMaison.sprite, interieurMaison.position, Color.White);

                }

                if (maisonPrincipalOuverte.estVivant == true)
                {
                    spriteBatch.Draw(maisonPrincipalOuverte.sprite, maisonPrincipalOuverte.position, Color.White);

                }


                if (maisonPrincipal.estVivant == true)
                {

                    spriteBatch.Draw(maisonPrincipal.sprite, maisonPrincipal.position, Color.White);
                }


               
            



                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Scene --- de --- Batailles
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/
                if (sceneBataille.estVivant == true)
                {

                    spriteBatch.Draw(sceneBataille.sprite, sceneBataille.position, Color.White);

                }



                if (boutonFight.estVivant == true)
                {

                    spriteBatch.Draw(boutonFight.sprite, boutonFight.position, Color.White);
                }

                if (boutonAttack.estVivant == true)
                {

                    spriteBatch.Draw(boutonAttack.sprite, boutonAttack.position, Color.White);
                }

                if (boutonDefend.estVivant == true)
                {

                    spriteBatch.Draw(boutonDefend.sprite, boutonDefend.position, Color.White);
                }

                if (boutonBack.estVivant == true)
                {

                    spriteBatch.Draw(boutonBack.sprite, boutonBack.position, Color.White);
                }

                if (boutonBag.estVivant == true)
                {

                    spriteBatch.Draw(boutonBag.sprite, boutonBag.position, Color.White);
                }

                if (boiteTexteBas.estVivant == true)
                {

                    spriteBatch.Draw(boiteTexteBas.sprite, boiteTexteBas.position, Color.White);
                }

                if (boiteTexteHaut.estVivant == true)
                {

                    spriteBatch.Draw(boiteTexteHaut.sprite, boiteTexteHaut.position, Color.White);
                }


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Personnages --- tant gentil --- que mechant
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

                if (slime.estVivant == true)
                {

                    spriteBatch.Draw(slime.sprite, slime.position, slime.spriteAfficheSlime, Color.White);
                }


                if (gobelin.estVivant == true)
                {

                    spriteBatch.Draw(gobelin.sprite, gobelin.position, gobelin.spriteAfficheGobelin, Color.White);
                }

                if (persoPrincipal.estVivant == true)
                {

                    spriteBatch.Draw(persoPrincipal.sprite, persoPrincipal.position, persoPrincipal.spriteAffiche, Color.White);
                }


                /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

                * 
                *                                                         Curseur --- epee --- qui se deplace
                *                                                         
                *                                                         
                * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/


                spriteBatch.Draw(curseur.sprite, curseur.position, Color.White);
            }


            /*---------------------------------------------------------------------------------------------------------------------------------------------------------------

            * 
            *                                                         Intro --- dessin --- bruhh
            *                                                         
            *                                                         
            * -------------------------------------------------------------------------------------------------------------------------------------------------------------*/

            if (intro == true)
            {
                spriteBatch.Draw(introWall.sprite, introWall.position, Color.White);
                if (flecheDroite1.estVivant == true)
                {
                    spriteBatch.Draw(flecheDroite1.sprite, flecheDroite1.position, Color.White);
                }
                if (flecheDroite2.estVivant == true)
                {
                    spriteBatch.Draw(flecheDroite2.sprite, flecheDroite2.position, Color.White);
                }
                if (flecheGauche1.estVivant == true)
                {
                    spriteBatch.Draw(flecheGauche1.sprite, flecheGauche1.position, Color.White);
                }
                if (flecheGauche2.estVivant == true)
                {
                    spriteBatch.Draw(flecheGauche2.sprite, flecheGauche2.position, Color.White);
                }
                spriteBatch.Draw(persoPrincipal.sprite, persoPrincipal.position, persoPrincipal.spriteAffiche, Color.White);

                spriteBatch.Draw(slime.sprite, slime.position, slime.spriteAfficheSlime, Color.White);

                spriteBatch.DrawString(classes, "Rogue", new Vector2(fenetre.Width / 2, 0), Color.White);



            }
            

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}




/*   if (click de la souris sur un Intersect image de qqu chose dans linventaire){
 *   
 *   limage de la chose sur la quelle on a clique est egale a la position de la souris jusqua ce que
 *   le bouton de la souris == Unpressed.}
 *   
 *   if (souris Intersect avec un image)
 *   {
 *   afficher les detaille de limage
 *   }
 *   
 *   */