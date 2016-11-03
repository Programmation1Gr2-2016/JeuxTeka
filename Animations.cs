using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teka
{
    class GameObjectAnime
    {
        public Rectangle position;
        public int vitesse;
        public Texture2D sprite;
        public bool estVivant;
        public Vector2 direction;
        public Rectangle spriteAffiche; // Le rectangle affiché à l'écran
        public Rectangle spriteAfficheSlime;
        public Rectangle spriteAfficheGobelin;

        public int niveau = 1;
        public int exp = 0;
        public int expGagne = 0;
        public int expProchainNiveau = 30;

        public int hpMax = 0;
        public int hp = 0;
        public int force = 0;
        public int defense = 0;
        public string nomPersonnage;
        public int niveauSuperieur = 0;
        public int damage = 0;
        public int damageMin = 1;
        public int damageMax = 3;


        public enum Etat { AttenteHaut, AttenteBas, AttenteGauche, AttenteDroite, RunDroite, RunGauche, RunHaut, RunBas };
        public enum SlimeEtat { AttenteHautSlime, AttenteBasSlime, AttenteGaucheSlime, AttenteDroiteSlime, JumpDroiteSlime, JumpGaucheSlime, JumpHautSlime, JumpBasSlime };
        public enum GobelinEtat { AttenteHautGobelin, AttenteBasGobelin, AttenteGaucheGobelin, AttenteDroiteGobelin, RunDroiteGobelin, RunGaucheGobelin, RunHautGobelin, RunBasGobelin };
        public Etat etat;
        public SlimeEtat slimeEtat;
        public GobelinEtat gobelinEtat;


        //competuer qui chargera le sprite afficher
        private int compteur = 0;
        private int compteurSlime = 0;
        private int compteurGobelin = 0;

        //gestion tableau sprite
        int runState = 0; // etat de depart
        int nbEtatsRun = 4; // nb d etat de run (nb de rectangles)

        int jumpStateSlime = 0;
        int nbEtatJumpSlime = 4;

        int runStateGobelin = 0;
        int nbEtatsRunGobelin = 3;

        //---------------------------------------- Perso principal rectangles -----------------------------------------------

        public Rectangle[] tabRunDroite =
        {
            new Rectangle(0,144,55,72),
            new Rectangle(55,144,55,72),
            new Rectangle(110,144,55,72),
            new Rectangle(165,144,55,72)
        };

        public Rectangle[] tabRunGauche =
        {
            new Rectangle(0,72,55,72),
            new Rectangle(55,72,55,72),
            new Rectangle(110,72,55,72),
            new Rectangle(165,72,55,72)
        };

        public Rectangle[] tabRunHaut =
        {
            new Rectangle(0,216,55,72),
            new Rectangle(55,216,55,72),
            new Rectangle(110,216,55,72),
            new Rectangle(165,216,55,72)
        };

        public Rectangle[] tabRunBas =
        {
            new Rectangle(0,0,55,72),
            new Rectangle(55,0,55,72),
            new Rectangle(110,0,55,72),
            new Rectangle(165,0,55,72)
        };

        int waitState = 0;

        public Rectangle[] attenteBas =
        {
            new Rectangle(0,0,55,72)
        };
        public Rectangle[] attenteHaut =
{
            new Rectangle(0,216,55,72)
        };
        public Rectangle[] attenteGauche =
{
            new Rectangle(0,72,55,72)
        };
        public Rectangle[] attenteDroite =
{
            new Rectangle(0,144,55,72)
        };



        //----------------------------------------------------- Slime Rectangles ---------------------------------------------------

        public Rectangle[] tabJumpBasSlime =
        {
            new Rectangle(40,20,68,60),
            new Rectangle(105,20,68,60),
            new Rectangle(167,20,68,60),
            new Rectangle(230,20,68,60)
        };

        public Rectangle[] tabJumpGaucheSlime =
        {
            new Rectangle(40,85,70,60),
            new Rectangle(105,85,70,60),
            new Rectangle(167,85,70,60),
            new Rectangle(230,85,70,60)
        };

        public Rectangle[] tabJumpDroiteSlime =
        {
            new Rectangle(40,150,70,60),
            new Rectangle(105,150,70,60),
            new Rectangle(167,150,70,60),
            new Rectangle(230,150,70,60)
        };

        public Rectangle[] tabJumpHautSlime =
        {
            new Rectangle(40,215,68,60),
            new Rectangle(105,215,68,60),
            new Rectangle(167,215,68,60),
            new Rectangle(230,215,68,60)
        };

        int waitStateSlime = 0;

        public Rectangle[] attenteBasSlime =
        {
            new Rectangle(105,20,68,60)
        };
        public Rectangle[] attenteHautSlime =
{
            new Rectangle(105,215,68,60)
        };
        public Rectangle[] attenteGaucheSlime =
{
            new Rectangle(105,85,70,60)
        };
        public Rectangle[] attenteDroiteSlime =
{
            new Rectangle(105,150,70,60)             
        };

        //------------------------------------------------------------- Gobelin Rectangles -------------------------------------

        public Rectangle[] tabRunBasGobelin =
{
            new Rectangle(0,0,70,63),
            new Rectangle(70,0,70,63),
            new Rectangle(140,0,70,63),
        };

        public Rectangle[] tabRunGaucheGobelin =
        {
            new Rectangle(0,63,70,63),
            new Rectangle(70,63,70,63),
            new Rectangle(140,63,70,63),
        };

        public Rectangle[] tabRunDroiteGobelin =
        {
            new Rectangle(0,126,70,63),
            new Rectangle(70,126, 70,63),
            new Rectangle(140,126,70,63),
        };

        public Rectangle[] tabRunHautGobelin =
        {
            new Rectangle(0,189,70,63),
            new Rectangle(70,189,70,63),
            new Rectangle(140,189,70,63),
        };

        int waitStateGobelin = 0;

        public Rectangle[] attenteBasGobelin =
        {
            new Rectangle(70,0,70,63)
        };
        public Rectangle[] attenteHautGobelin =
        {
            new Rectangle(75,189,70,63)
        };
        public Rectangle[] attenteGaucheGobelin =
        {
            new Rectangle(70,63,70,63)
        };
        public Rectangle[] attenteDroiteGobelin =
        {
            new Rectangle(70,126,70,63)
        };

        // --------------------------------------------------------------- Fonction -----------------------------------------------------------


        public virtual void Update(GameTime gameTime)
        {

            if (etat == Etat.RunHaut)
            {
                spriteAffiche = tabRunHaut[runState];
            }
            if (etat == Etat.RunBas)
            {
                spriteAffiche = tabRunBas[runState];
            }
            if (etat == Etat.RunDroite)
            {
                spriteAffiche = tabRunDroite[runState];
            }
            if (etat == Etat.RunGauche)
            {
                spriteAffiche = tabRunGauche[runState];
            }
            if (etat == Etat.AttenteHaut)
            {
                spriteAffiche = attenteHaut[waitState];
            }
            if (etat == Etat.AttenteBas)
            {
                spriteAffiche = attenteBas[waitState];
            }
            if (etat == Etat.AttenteGauche)
            {
                spriteAffiche = attenteGauche[waitState];
            }
            if (etat == Etat.AttenteDroite)
            {
                spriteAffiche = attenteDroite[waitState];
            }




            //compteur permettant de generer le changement d images
            compteur++;
            if (compteur == 15)//vitesse de defoulement
            {
                runState++;
                if (runState == nbEtatsRun)
                {
                    runState = 0;
                }
                compteur = 0;
            }


            //---------------------------------------------------------------------------- Slime -------------------------------------------------------------

            if (slimeEtat == SlimeEtat.JumpHautSlime)
            {
                spriteAfficheSlime = tabJumpHautSlime[jumpStateSlime];
            }
            if (slimeEtat == SlimeEtat.JumpBasSlime)
            {
                spriteAfficheSlime = tabJumpBasSlime[jumpStateSlime];
            }
            if (slimeEtat == SlimeEtat.JumpDroiteSlime)
            {
                spriteAfficheSlime = tabJumpDroiteSlime[jumpStateSlime];
            }
            if (slimeEtat == SlimeEtat.JumpGaucheSlime)
            {
                spriteAfficheSlime = tabJumpGaucheSlime[jumpStateSlime];
            }
            if (slimeEtat == SlimeEtat.AttenteHautSlime)
            {
                spriteAfficheSlime = attenteHautSlime[waitStateSlime];
            }
            if (slimeEtat == SlimeEtat.AttenteBasSlime)
            {
                spriteAfficheSlime = attenteBasSlime[waitStateSlime];
            }
            if (slimeEtat == SlimeEtat.AttenteGaucheSlime)
            {
                spriteAfficheSlime = attenteGaucheSlime[waitStateSlime];
            }
            if (slimeEtat == SlimeEtat.AttenteDroiteSlime)
            {
                spriteAfficheSlime = attenteDroiteSlime[waitStateSlime];
            }




            //compteur permettant de generer le changement d images
            compteurSlime++;
            if (compteurSlime == 15)//vitesse de defoulement
            {
                jumpStateSlime++;
                if (jumpStateSlime == nbEtatJumpSlime)
                {
                    jumpStateSlime = 0;
                }
                compteurSlime = 0;
            }

            //---------------------------------------------------------- Gobelin ----------------------------------------------------------------------


            if (gobelinEtat == GobelinEtat.RunHautGobelin)
            {
                spriteAfficheGobelin = tabRunHautGobelin[runStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.RunBasGobelin)
            {
                spriteAfficheGobelin = tabRunBasGobelin[runStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.RunDroiteGobelin)
            {
                spriteAfficheGobelin = tabRunDroiteGobelin[runStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.RunGaucheGobelin)
            {
                spriteAfficheGobelin = tabRunGaucheGobelin[runStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.AttenteHautGobelin)
            {
                spriteAfficheGobelin = attenteHautGobelin[waitStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.AttenteBasGobelin)
            {
                spriteAfficheGobelin = attenteBasGobelin[waitStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.AttenteGaucheGobelin)
            {
                spriteAfficheGobelin = attenteGaucheGobelin[waitStateGobelin];
            }
            if (gobelinEtat == GobelinEtat.AttenteDroiteGobelin)
            {
                spriteAfficheGobelin = attenteDroiteGobelin[waitStateGobelin];
            }




            //compteur permettant de generer le changement d images
            compteurGobelin++;
            if (compteurGobelin == 15)//vitesse de defoulement
            {
                runStateGobelin++;
                if (runStateGobelin == nbEtatsRunGobelin)
                {
                    runStateGobelin = 0;
                }
                compteurGobelin = 0;
            }






        }
    }
}