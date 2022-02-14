using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Imaging;

namespace ArcanaHelper
{
    public partial class ArcanaHelper : Form
    {

        bool full = true;
        //bool add = true;
        int currentPersonaGame = 2;

        float opacityChangeSpeed = 0.015f;

        String[] basicArcana = {
            "The Fool", "The Magician", "The Priestess",
            "The Empress", "The Empreror", "The Hierophant",
            "The Lovers", "The Chariot", "Justice",
            "The Hermit", "Fortune", "Strength",
            "The Hanged Man", "Death", "Temperance",
            "The Devil", "The Tower", "The Star",
            "The Moon", "The Sun", "Judgement",
            "The World",
        };

        String[,] specificArcana = {
            {"Aeon", ""},{"Jester", "Aeon" },{"Faith", "The Councillor"} 
        };

        String[] arcanaName;

        int[,,] arcanaRes = {
           //                                                                                
           // 0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15  16  17  18  19  20  21  22  23
           {
            { 0, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 0 the fool
            { 5,  1, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 1 the magician
            { 8,  6,  2, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 2 the high priestess
            {10, 12,  6,  3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 3 the empress
            { 7, 14,  8,  6,  4, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 4 the empreror
            { 9,  9,  7,  2,  7,  5, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 5 the hierophant
            { 2,  4,  1, 10,  7,  1,  6, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 6 the lovers
            { 4, 15,  1, 15,  9,  8,  4,  7, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 7 the chariot
            { 6,  5,  6,  4, 15,  7,  7,  1,  8, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 8 justice
            { 2,  7, 11,  6, 11,  7,  8, 14,  2,  9, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 9 the hermit
            { 8,  4,  1, 11, -1,  3,  1, 11,  7,  4, 10, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 10 wheel of fortune
            {12, -1,  9,  7, 12,  2,  5,  8, 14, 10, -1, 11, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 11 strength
            { 1, 15, 11,  7,  9,  6,  9, 10,  2, 10, 11,  9, 12, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 12 the hanged man
            {11, -1,  4, 15, 18,  4, 15, -1, 18, -1, -1, 12, 15, 13, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 13 death
            { 5, 13,  3,  6, 12, 11,  2, 13, 18, 12,  6, 18,  5, -1, 14, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 14 temperance
            { 9, 14, -1,  6, -1, -1, 11, 12, -1, 13, 18, 10, 18, -1, 13, 15, -3, -3, -3, -3, -3, -3, -3, -3}, // 15 the devil
            {18,  3, -1,  7, -1, 14, 17, 18, 17, -1, 18, 15, 13, -1, 15, -1, 16, -3, -3, -3, -3, -3, -3, -3}, // 16 the tower
            {22,  3,  8, 14,  8,  2,  5, -1,  4,  7, 18,  2, 11, -1, 18, -1, -1, 17, -3, -3, -3, -3, -3, -3}, // 17 the star
            {10,  2, 17,  6, -1, 14,  3, 10, -1,  1,  7, 12,  3, 17,  3, -1, 10, 13, 18, -3, -3, -3, -3, -3}, // 18 the moon
            { 3,  6, 17,  6,  3, 14,  5, -1,  4, -1, 14,  2, -1, -1, 17, -1, -1,  8, 14, 19, -3, -3, -3, -3}, // 19 the sun
            {17, -1,  3, -1,  5,  6, -1, -1, 22, -1, -1, 12, -1, -1, 18, -1, 22, 14, -1, 17, 20, -3, -3, -3}, // 20 judgement
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 21, -3, -3}, // 21 the world
            {13, -1,  3, 18, -1, -1, 12, 13, -1, 17, 15, -1, 14, -1, 17,  6, 18, 15, -1,  3, 17, -1, 22, -3}, // 22 aeon
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,-1, -1, -3}   // 23 --
            },
           {
            { 0, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 0 the fool
            {13,  1, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 1 the magician
            {18, 14,  2, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 2 the high priestess
            {12,  8,  4,  3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 3 the empress
            {14, 23,  3,  8,  4, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 4 the empreror
            { 9, 13,  1,  0, 10,  5, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 5 the hierophant
            { 7, 15, 10, 20,  0, 11,  6, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 6 the lovers
            {18,  2,  5, 17, 22, 17, 14,  7, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 7 the chariot
            {17,  4, 13,  6,  7, 12, 20, 18,  8, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 8 justice
            { 2,  6, 14, 11,  5, 23,  7, 15,  1,  9, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 9 the hermit
            {22,  8,  1,  9, 19,  8, 11, 23,  4, 17, 10, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 10 wheel of fortune
            {13,  0, 15, 22, 16,  0, 13,  9, 23,  5, 22, 11, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 11 strength
            {16,  3, 13,  2, 15, 19, 23,  0,  6, 17,  4, 14, 12, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 12 the hanged man
            {11,  9,  1,  0,  9,  7, 14, 15,  0, 11, 17,  5, 18, 13, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 13 death
            { 5,  7, 15, 22, 15, 13, 11, 11,  4, 11,  3,  7, 13, 12, 14, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 14 temperance
            {14, 14, 18, 19,  8, 12, 18, 14,  0,  2,  5, 13, 10,  7,  0, 15, -3, -3, -3, -3, -3, -3, -3, -3}, // 15 the devil
            { 3, 14, 12,  4, 17, 20,  3, 10, 19, 20, 12, 22,  9, 19, 10,  1, 16, -3, -3, -3, -3, -3, -3, -3}, // 16 the tower
            { 1,  2,  9,  6,  6, 16, 22, 18,  3, 11, 15, 18,  8, 23, 19, 11, 23, 17, -3, -3, -3, -3, -3, -3}, // 17 the star
            { 8,  6,  5, 10, 16,  2,  1,  6, 14,  2, 19,  1, 23,  5, 23,  7,  9, 14, 18, -3, -3, -3, -3, -3}, // 18 the moon
            { 8,  5,  7, 16, 20,  6,  3,  2, 12, 15, 17, 18,  5,  2,  1,  9,  4, 20,  3, 19, -3, -3, -3, -3}, // 19 the sun
            {19, 11,  8,  4,  2, 22, 12, -4, -4,  4, 16, -4, 17, -4,  9,  6, 18, 10,  0, 13, 20, -3, -3, -3}, // 20 judgement
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 21, -3, -3}, // 21 the world
            {23, 11,  8,  1,  2,  3, 16,  6, 12, 20, 23, 17, 15,  0,  9,  7, 13, 14, 19,  4, 10, -1, 22, -3}, // 22 jester
            { 5, 18, 22, 12,  6,  8, 16, 19,  4, 22, 20,  3, 17,  1,  0,  7, 13, 19, 14, 10, 15, -1,  2, 23}  // 23 aeon
            },
           {

            { 0, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 0 the fool
            {13,  1, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 1 the magician
            {18, 14,  2, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 2 the high priestess
            {12,  8,  4,  3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 3 the empress
            {14, 23,  3,  8,  4, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 4 the empreror
            { 9, 13,  1,  0, 10,  5, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 5 the hierophant
            { 7, 15, 10, 20,  0, 11,  6, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 6 the lovers
            {18,  2,  5, 17, 22, 17, 14,  7, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 7 the chariot
            {17,  4, 13,  6,  7, 12, 20, 18,  8, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 8 justice
            { 2,  6, 14, 11,  5, 23,  7, 15,  1,  9, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 9 the hermit
            {22,  8,  1,  9, 19,  8, 11, 23,  4, 17, 10, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 10 wheel of fortune
            {13,  0, 15, 22, 16,  0, 13,  9, 23,  5, 22, 11, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 11 strength
            {16,  3, 13,  2, 15, 19, 23,  0,  6, 17,  4, 14, 12, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 12 the hanged man
            {11,  9,  1,  0,  9,  7, 14, 15,  0, 11, 17,  5, 18, 13, -3, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 13 death
            { 5,  7, 15, 22, 15, 13, 11, 11,  4, 11,  3,  7, 13, 12, 14, -3, -3, -3, -3, -3, -3, -3, -3, -3}, // 14 temperance
            {14, 14, 18, 19,  8, 12, 18, 14,  0,  2,  5, 13, 10,  7,  0, 15, -3, -3, -3, -3, -3, -3, -3, -3}, // 15 the devil
            { 3, 14, 12,  4, 17, 20,  3, 10, 19, 20, 12, 22,  9, 19, 10,  1, 16, -3, -3, -3, -3, -3, -3, -3}, // 16 the tower
            { 1,  2,  9,  6,  6, 16, 22, 18,  3, 11, 15, 18,  8, 23, 19, 11, 23, 17, -3, -3, -3, -3, -3, -3}, // 17 the star
            { 8,  6,  5, 10, 16,  2,  1,  6, 14,  2, 19,  1, 23,  5, 23,  7,  9, 14, 18, -3, -3, -3, -3, -3}, // 18 the moon
            { 8,  5,  7, 16, 20,  6,  3,  2, 12, 15, 17, 18,  5,  2,  1,  9,  4, 20,  3, 19, -3, -3, -3, -3}, // 19 the sun
            {19, 11,  8,  4,  2, 22, 12, -4, -4,  4, 16, -4, 17, -4,  9,  6, 18, 10,  0, 13, 20, -3, -3, -3}, // 20 judgement
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 21, -3, -3}, // 21 the world
            {23, 11,  8,  1,  2,  3, 16,  6, 12, 20, 23, 17, 15,  0,  9,  7, 13, 14, 19,  4, 10, -1, 22, -3}, // 22 faith
            { 5, 18, 22, 12,  6,  8, 16, 19,  4, 22, 20,  3, 17,  1,  0,  7, 13, 19, 14, 10, 15, -1,  2, 23}  // 23 councillor
            
            }
        };

        Random rand = new Random();

        Color[] gameColor;
        String[] gameIm = {"p3f", "p4", "p5r" };
        String[] fontArr = { "optima", "optima", "p5hatty" };
        int[,] fontSize = { { 18, 14, 12 }, { 18, 14, 12 }, { 22, 18, 16 } };

        Image back;
        Image unknown;

        Image line;
        PictureBox[] lineBoxes;
        Label[] lineLabels;

        String[] resFadeText;
        int resFadeCount;

        Image[] quit;
        PictureBox quitBox;

        Image[] images;
        PictureBox[] arcanaPic;
        PictureBox[] animArcana;
        bool[] arcanaPick;
        Label[] arcanaLabels;

        Label[] arcanaPickLabels;

        Image imp;

        Timer updown;
        Timer lineTimer;
        Timer resFadeTimer;
        Timer arrowTimer;
        Timer realArrowTimer;

        SoundPlayer pickS;
        SoundPlayer switchS;

        Image[] arrowPic;
        PictureBox[] arrowBox;

        Image[] personaImages;
        PictureBox[] personaBox;

        int[] memAnim = { -1, -1, -1 };
        int[] memOpacity = { -1, -1, -1 };
        int[] changeAnim = { 0, 0, 0 };
        float[] opacityValue = { 0, 0, 0 };

        int countLines = 0;
        string[] resArcanaComb;
        int resArcanaIndex;
        int foundResArcana;
        int resArcanaCombMax = 100;
        void realQuitAnim(object sender, EventArgs e) {
        }

        void quitAnim(object sender, EventArgs e) {
            //Console.WriteLine("lol");
            quitBox.Image = quit[1];
            /*
            if (quitTimer == null){
                quitTimer = new Timer();
                quitTimer.Interval = 1;
                quitTimer.Tick += new EventHandler(realQuitAnim);
                quitTimer.Start();
            }
            */
        }

        void antiQuitAnim(object sender, EventArgs e) {
            quitBox.Image = quit[0];
        }

        void quitButton(object sender, EventArgs e) {
            this.Close();
        }
        string randText() {
            string alph = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            string res = "";
            int am = rand.Next() % 15 + 10;

            for (int i = 0; i < am; i++) res += alph[rand.Next() % alph.Length];

            return res;
        }
        void realArrowClockAnim(object sender, EventArgs e) {
            for (int i = 0; i < 5; i++){
                lineLabels[i % 5].Text = randText();
            }
        }
        void arrowClickAnim(object sender, EventArgs e) {
            arrowTimer = new Timer();
            arrowTimer.Interval = 10;
            arrowTimer.Tick += new EventHandler(realArrowClockAnim);
            arrowTimer.Start();

            realArrowTimer = new Timer();
            realArrowTimer.Interval = 400;
            realArrowTimer.Tick += new EventHandler(arrowClick);

            if (sender == arrowBox[0]) realArrowTimer.Tag = 0;
            else realArrowTimer.Tag = 1;

            realArrowTimer.Start();
            switchS.Play();
        }
        void arrowClick(object sender, EventArgs e) {
            arrowTimer.Stop();
            realArrowTimer.Stop();

            //Console.WriteLine(resArcanaIndex);
            if ((int)realArrowTimer.Tag == 0) resArcanaIndex -= 10;

            for (int i = 0; i < 5; i++) {
                lineLabels[i].Text = resArcanaComb[++resArcanaIndex];
            }

            //Console.WriteLine(resArcanaIndex);
            visArrows(-1);
            if (resArcanaIndex < foundResArcana) visArrows(1);
            if (resArcanaIndex != 5) visArrows(0);
        }

        void visArrows(int mode) {
            if (mode == -1) {
                arrowBox[0].Visible = false;
                arrowBox[1].Visible = false;
            }
            if (mode == 0 || mode == 2) arrowBox[0].Visible = true;
            if (mode == 1 || mode == 2) arrowBox[1].Visible = true;
        }

        void arrowAnim(object sender, EventArgs e) {
            if (sender == arrowBox[0] && arrowBox[0].Visible){
                arrowBox[0].Image = arrowPic[1];
            }
            else {
                arrowBox[1].Image = arrowPic[3];
            }

        }

        void backArrowAnim(object sender, EventArgs e) {
            arrowBox[0].Image = arrowPic[0];
            arrowBox[1].Image = arrowPic[2];
        }

        void pickGame(object sender, MouseEventArgs e) {
            for (int i = 0; i < 3; i++) {
                if (sender.Equals(personaBox[i]) && i != currentPersonaGame) {
                    currentPersonaGame = i;
                    for (int j = 0; j < 3; j++) {
                        personaBox[j].BackColor = Color.Transparent;
                    }
                    personaBox[currentPersonaGame].BackColor = Color.White;
                    this.BackColor = gameColor[currentPersonaGame];

                    disposeStuff();
                    initSound();
                    initArcanaAmount();
                    initArcana();
                    initImages();
                    initFonts();
                    initBoxImages();
                    break;
                }
            }
        }

        void disposeStuff() {
            for (int i = 0; i < 3; i++) {
                animArcana[i].Dispose();
            }
            for (int i = 0; i < arcanaName.Length; i++) {
                arcanaPic[i].Dispose();
                arcanaLabels[i].Dispose();
            }
        }

        void initBoxImages() {
            for (int i = 0; i < 2; i++) {
                arrowBox[i].Image = arrowPic[0 + (2 * i)];
            }
            quitBox.Image = quit[0];
            for (int i = 0; i < 3; i++) {
                personaBox[i].Image = personaImages[i];
            }
            for (int i = 0; i < arcanaName.Length; i++) {
                arcanaPic[i].Image = null;
                //arcanaPic[i].Refresh();
                arcanaPic[i].Image = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\arcana_back.png");
            }
        }

        void initFonts() {
            for (int i = 0; i < 3; i++) {
                arcanaPickLabels[i].Font = new Font(fontArr[currentPersonaGame], fontSize[currentPersonaGame, 0]);
            }
            for(int i = 0; i < 5; i++){
                lineLabels[i].Font = new Font(fontArr[currentPersonaGame], fontSize[currentPersonaGame, 1]);
            }
            for (int i = 0; i < arcanaName.Length; i++) {
                arcanaLabels[i].Font = new Font(fontArr[currentPersonaGame], fontSize[currentPersonaGame, 2]);
            }
        }
        void initAll() {

            gameColor = new Color[3];
            gameColor[0] = Color.Blue;
            gameColor[1] = Color.Yellow;
            gameColor[2] = Color.Red;

            this.BackColor = gameColor[currentPersonaGame];

            initArcanaAmount();

            arrowPic = new Image[4];
            personaImages = new Image[3];

            images = new Image[arcanaName.Length]; // change
            arcanaPic = new PictureBox[arcanaName.Length];
            animArcana = new PictureBox[3];
            arcanaPick = new bool[arcanaName.Length];
            arcanaLabels = new Label[arcanaName.Length];
            arcanaPickLabels = new Label[3];
            lineBoxes = new PictureBox[5];
            lineLabels = new Label[5];
            resArcanaComb = new string[100];
            arrowBox = new PictureBox[2];
            personaBox = new PictureBox[3];

            initSound();
            initArcana();
            initImages();

            for (int i = 0; i < 2; i++){
                arrowBox[i] = new PictureBox();
                arrowBox[i].Name = (2 * i).ToString();
                arrowBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                arrowBox[i].Location = new Point(1720, 350 + (i * 470));
                arrowBox[i].Visible = false;
                arrowBox[i].MouseMove += new MouseEventHandler(arrowAnim);
                arrowBox[i].MouseLeave += new EventHandler(backArrowAnim);
                arrowBox[i].MouseClick += new MouseEventHandler(arrowClickAnim);
                Controls.Add(arrowBox[i]);
            }

            for (int i = 0; i < 3; i++){
                arcanaPickLabels[i] = new Label();
                arcanaPickLabels[i].AutoSize = true;
                arcanaPickLabels[i].Text = "placeholder";
                arcanaPickLabels[i].Visible = false;
                Controls.Add(arcanaPickLabels[i]);
            }

            for (int i = 0; i < 5; i++){
                lineBoxes[i] = new PictureBox();
                lineBoxes[i].Image = line;
                lineBoxes[i].SizeMode = PictureBoxSizeMode.Zoom;
                lineBoxes[i].Location = new Point(-500, 400 + (i * 80));
                lineBoxes[i].Size = new Size(500, 100);
                Controls.Add(lineBoxes[i]);

                lineLabels[i] = new Label();
                lineLabels[i].AutoSize = true;
                Controls.Add(lineLabels[i]);
            }

            for (int i = 0; i < resArcanaCombMax; i++) resArcanaComb[i] = "";

            quitBox = new PictureBox();
            quitBox.Location = new Point(1720, 900);
            quitBox.Size = new Size(180, 180);
            quitBox.SizeMode = PictureBoxSizeMode.Zoom;

            quitBox.MouseMove += new MouseEventHandler(quitAnim);
            quitBox.MouseLeave += new EventHandler(antiQuitAnim);
            quitBox.MouseClick += new MouseEventHandler(quitButton);
            Controls.Add(quitBox);

            int[] personaSizes = { 90, 80, 100 };
            for (int i = 0; i < 3; i++){
                personaBox[i] = new PictureBox();
                personaBox[i].Location = new Point(1150 + i * 150, 820);
                personaBox[i].Size = new Size(personaSizes[i], personaSizes[i]);
                personaBox[i].SizeMode = PictureBoxSizeMode.Zoom;
                personaBox[i].MouseClick += new MouseEventHandler(pickGame);
                Controls.Add(personaBox[i]);
            }
            personaBox[currentPersonaGame].BackColor = Color.White;

            initFonts();
            initBoxImages();

            if (full){
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }

            updown = null;
            lineTimer = null;
            resFadeTimer = null;
        }
        public ArcanaHelper(){

            InitializeComponent();
            initAll();
        }

        private void ArcanaHelper_MouseHover(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void initSound() {
            pickS = new SoundPlayer("arcana\\" + gameIm[currentPersonaGame] + "\\pick.wav");
            switchS = new SoundPlayer("arcana\\" + gameIm[currentPersonaGame] + "\\switch.wav");
        }

        void initImages() {

            back = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\arcana_back.png");
            unknown = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\unknown.png");
            line = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\line.png");
            imp = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\arcana_imp.png");

            quit = new Image[2];
            quit[0] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\quit.png");
            quit[1] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\quit_inv.png");
            arrowPic[0] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\up.png");
            arrowPic[1] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\up_inv.png");
            arrowPic[2] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\down.png");
            arrowPic[3] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\down_inv.png");

            personaImages[0] = Image.FromFile("arcana\\p3f.png");
            personaImages[1] = Image.FromFile("arcana\\p4.png");
            personaImages[2] = Image.FromFile("arcana\\p5r.png");

            for (int i = 0; i < arcanaName.Length; i++) {
                images[i] = Image.FromFile("arcana\\" + gameIm[currentPersonaGame] + "\\arcana_" + i.ToString() + ".png");
            }
        }

        void initArcanaAmount() {
            int amountOfCards = basicArcana.Length;
            int amountOfCardsPlus = 0;
            for (int i = 0; i < 2; i++){
                if (specificArcana[currentPersonaGame, i] != ""){
                    amountOfCardsPlus++;
                }
            }
            arcanaName = new String[amountOfCards + amountOfCardsPlus];

            for (int i = 0; i < amountOfCards; i++){
                arcanaName[i] = basicArcana[i];
            }
            for (int i = 0; i < amountOfCardsPlus; i++){
                arcanaName[amountOfCards + i] = specificArcana[currentPersonaGame, i];
            }
        }

        void initArcana() {

            int x = 62;
            int y = 12;

            for (int i = 0; i < 3; i++) {
                animArcana[i] = new PictureBox();
                animArcana[i].SizeMode = PictureBoxSizeMode.Zoom;
                animArcana[i].Location = new Point(1000 + (i * 250), 1000);
                animArcana[i].Size = new Size(160, 320);
                Controls.Add(animArcana[i]);
            }

            for (int i = 0; i < arcanaName.Length; i++) {
                if (i != 0 && i % 5 == 0) {
                    x = 62;
                    y += 210;
                }

                arcanaPic[i] = new PictureBox();
                arcanaPic[i].Name = i.ToString();
                arcanaPic[i].SizeMode = PictureBoxSizeMode.Zoom;
                arcanaPic[i].Location = new Point(x, y);
                arcanaPic[i].Size = new Size(80, 160);

                arcanaLabels[i] = new Label();
                arcanaLabels[i].AutoSize = true;
                arcanaLabels[i].Text = arcanaName[i];
                arcanaLabels[i].Location = new Point(x + 20 - (2 * arcanaName[i].Length), y + 180);

                Controls.Add(arcanaLabels[i]);
                Controls.Add(arcanaPic[i]);

                arcanaPic[i].Click += ArcanaHelper_Click;

                x += 180;
            }
        }

        void getCombs() {
            int used;

            if (memAnim[0] != -1) used = memAnim[0];
            else used = memAnim[1];

            for (int i = 0; i < arcanaName.Length; i++) {
                for (int j = i; j < arcanaName.Length; j++) {
                    if (arcanaRes[currentPersonaGame, j, i] == used) {
                        resArcanaComb[++resArcanaIndex] = arcanaName[i] + " + " + arcanaName[j];
                        //Console.WriteLine(resArcanaComb[resArcanaIndex - 1]);
                    }
                }
            }

            foundResArcana = resArcanaIndex;
            resArcanaIndex = 0;
        }

        void drawOnLines(int endLocation) {
            lineLabels[countLines].BringToFront();
            lineLabels[countLines].BackColor = Color.White;
            lineLabels[countLines].Text = resArcanaComb[++resArcanaIndex];
            lineLabels[countLines].Location = new Point(endLocation + 260 - (3 * lineLabels[countLines].Text.Length), 440 + (countLines * 80));
        }

        void lineAnim(object sender, EventArgs e) {
            //Console.WriteLine(lineBoxes[countLines].Location.X);

            int endLocation = 1450 + rand.Next() % 15;

            if (lineTimer != null){
                if (lineBoxes[countLines].Location.X < endLocation){
                    lineBoxes[countLines].Location = new Point(lineBoxes[countLines].Location.X + 150 + rand.Next() % 20, lineBoxes[countLines].Location.Y);
                }
                else if (countLines < 5){
                    drawOnLines(endLocation);
                    countLines++;
                }


                if (countLines == 5){
                    lineTimer.Stop();
                    lineTimer = null;

                    if (foundResArcana > 5) visArrows(1);
                }
            }
        }

        void resFadeAnim(object sender, EventArgs e) {
           bool done = true;

           for (int i = 0; i < 5; i++){
                if (i >= foundResArcana) continue;
                if (resFadeCount < resFadeText[i].Length){
                    done = false;
                    lineLabels[i].BringToFront();
                    lineLabels[i].BackColor = Color.Blue;
                    lineLabels[i].Text += resFadeText[i][resFadeCount].ToString();
                    lineLabels[i].Location = new Point(1200, 440 + (i * 80));
                } 
           }

           if (done){
                resFadeTimer.Stop();
                resFadeTimer = null;
           }
           resFadeCount++;
        }

        void protoResFadeAnim() {
            resFadeCount = 0;
            resFadeText = new String[5];
            for (int i = 0; i < 5; i++){
                lineLabels[i].ForeColor = Color.Black;
                resFadeText[i] = resArcanaComb[++resArcanaIndex];
                lineLabels[i].Text = "";
            }
            resFadeTimer = new Timer();
            resFadeTimer.Interval = 15;
            resFadeTimer.Tick += new EventHandler(resFadeAnim);
            resFadeTimer.Start();
        }

        void resArcana() {
            foundResArcana = 0;
            countLines = 0;
            resArcanaIndex = 0;
            for (int i = 0; i < resArcanaCombMax; i++) resArcanaComb[i] = "";
            
            getCombs();
            if (currentPersonaGame == 0) {
                protoResFadeAnim();
            }
            else if (currentPersonaGame == 2){
                for (int i = 0; i < 5; i++) {
                    lineLabels[i].BackColor = Color.White;
                    lineLabels[i].ForeColor = Color.Black;
                }

                lineTimer = new Timer();
                lineTimer.Interval = 1;
                lineTimer.Tick += new EventHandler(lineAnim);
                lineTimer.Start();
            }
  
        }

        void removeLines(object sender, EventArgs e) {
            visArrows(-1);

            if (currentPersonaGame == 0) {
                for (int i = 0; i < 5; i++) {
                    int b = lineLabels[i].ForeColor.B + 5;
                    if(b < 255) lineLabels[i].ForeColor = Color.FromArgb(0, 0, b);
                }
                if (lineLabels[0].ForeColor.B >= 250){
                    for (int i = 0; i < 5; i++) {
                        lineLabels[i].Location = new Point(2000, 2000);
                    }
                    lineTimer.Stop();
                    lineTimer = null;
                }
            }
            else if (currentPersonaGame == 2){
                for (int i = 0; i < 5; i++){
                    lineLabels[i].Location = new Point(lineLabels[i].Location.X + 90, lineLabels[i].Location.Y);
                    lineBoxes[i].Location = new Point(lineBoxes[i].Location.X + 90, lineBoxes[i].Location.Y);
                }
                if (lineBoxes[0].Location.X > 2300){
                    lineTimer.Stop();
                    lineTimer = null;
                    for (int i = 0; i < 5; i++) lineBoxes[i].Location = new Point(-500, 400 + (i * 80));
                }
            }
        }

        void antiResArcana() {
            lineTimer = new Timer();
            lineTimer.Interval = 1;
            lineTimer.Tick += new EventHandler(removeLines);
            lineTimer.Start();
        }

        void sumArcana(int first, int second) {
            int buff;
            if (arcanaRes[currentPersonaGame, first, second] == -3){
                buff = first;
                first = second;
                second = buff;
            }

            if (arcanaRes[currentPersonaGame, first, second] == -1) {
                animArcana[2].Image = imp;
                memAnim[2] = -2;
            }
            else if (arcanaRes[currentPersonaGame, first, second] != -1){
                animArcana[2].Image = images[arcanaRes[currentPersonaGame, first, second]];
                memAnim[2] = arcanaRes[currentPersonaGame, first, second];
                memOpacity[2] = arcanaRes[currentPersonaGame, first, second];
            }
            else{
                animArcana[2].Image = unknown;
                memAnim[2] = -2;
            }
            changeAnim[2] = 1;
            
            updown = new Timer();
            updown.Interval = 1;
            updown.Tick += new EventHandler(upAnimation);
            updown.Start();
        }

        void downSumArcana() {
            memAnim[2] = -1;
            changeAnim[2] = 1;
            updown = new Timer();
            updown.Interval = 1;
            updown.Tick += new EventHandler(downAnimation);
            updown.Start();
        }

        private void ArcanaHelper_Click(object sender, EventArgs e) {
            int i;
            int count = 0;

            for (i = 0; i < arcanaName.Length; i++) {
                if (sender.Equals(arcanaPic[i])) {
                    arcanaPick[i] = !arcanaPick[i];
                    break;
                }
            }
            for (int li = 0; li < arcanaName.Length; li++) {
                if (arcanaPick[li]) count++;
            }

            if (count < 3 && updown == null && lineTimer == null && resFadeTimer == null) {
                pickS.Play();

                updown = new Timer();
                updown.Interval = 1;

                if (arcanaPick[i]) {
                    arcanaPic[i].Image = images[i];
                    arcanaLabels[i].ForeColor = Color.White;

                    updown.Tick += new EventHandler(upAnimation);

                    if (memAnim[0] == -1){
                        memAnim[0] = i;
                        memOpacity[0] = i;
                        changeAnim[0] = 1;
                        animArcana[0].Image = images[i];
                    }
                    else{
                        memAnim[1] = i;
                        memOpacity[1] = i;
                        changeAnim[1] = 1;
                        animArcana[1].Image = images[i];
                    }
                }
                else {
                    arcanaPic[i].Image = back;
                    arcanaLabels[i].ForeColor = Color.Black;

                    updown.Tick += new EventHandler(downAnimation);

                    if (memAnim[0] == i){
                        memAnim[0] = -1;
                        changeAnim[0] = 1;
                        animArcana[0].Image = images[i];
                    }
                    else{
                        memAnim[1] = -1;
                        changeAnim[1] = 1;
                        animArcana[1].Image = images[i];
                    }
                }
                updown.Start();
            }
            else arcanaPick[i] = !arcanaPick[i];

            if (!(memAnim[0] == -1 ^ memAnim[1] == -1)){
                antiResArcana();
            }
        }

        Image SetImageOpacity(Image image, float opacity) {
            Bitmap bmp = new Bitmap(image.Width, image.Height);

            using (Graphics g = Graphics.FromImage(bmp)) {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributrs = new ImageAttributes();
                attributrs.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0,
                    image.Width, image.Height, GraphicsUnit.Pixel, attributrs);
                
            }
            return bmp;
        }

        void upAnimation(object sender, EventArgs e) {
            //Console.WriteLine(animArcana.Location.Y);
            int change;

            for (change = 0; change < 3; change++) {
                if (changeAnim[change] == 1) break;
            }
            //Console.WriteLine(change);

            if (currentPersonaGame == 0 && opacityValue[change] < 1) {
                animArcana[change].Location = new Point(animArcana[change].Location.X, 20);
                opacityValue[change] += opacityChangeSpeed;
                animArcana[change].Image = SetImageOpacity(images[memOpacity[change]], opacityValue[change]);
            }
            else if (currentPersonaGame == 2 && animArcana[change].Location.Y > 60){
                animArcana[change].Location = new Point(animArcana[change].Location.X, animArcana[change].Location.Y - 90);
            }
            else{
                updown.Stop();
                updown = null;

                changeAnim[change] = 0;
                if (change != 2 && (memAnim[0] != -1 && memAnim[1] != -1)){
                    sumArcana(memAnim[0], memAnim[1]);
                }
                else if (change != 2 && (memAnim[0] == -1 ^ memAnim[1] == -1)){  // a real fucking xor that is used not for fucking bitwise operation. Fuck me
                    resArcana();

                }

                if (memAnim[change] != -1){
                    if (memAnim[change] != -2 && memAnim[change] != -4){
                        arcanaPickLabels[change].Text = arcanaName[memAnim[change]];
                        arcanaPickLabels[change].Location = new Point(1050 + (change * 250) - (3 * arcanaName[memAnim[change]].Length), 350);
                    }
                    else{
                        arcanaPickLabels[change].Text = "???";
                        arcanaPickLabels[change].Location = new Point(1050 + (change * 250) - 9, 350);
                    }

                    arcanaPickLabels[change].Visible = true;
                }
            }
        }

        void downAnimation(object sender, EventArgs e) {

            int change;
            //Console.WriteLine("lol");

            for (change = 0; change < 3; change++) if (changeAnim[change] == 1) break;

            arcanaPickLabels[change].Visible = false;

            if (currentPersonaGame == 0 && opacityValue[change] > 0) {
                opacityValue[change] -= opacityChangeSpeed;
                Console.WriteLine(memAnim[change]);
                animArcana[change].Image = SetImageOpacity(images[memOpacity[change]], opacityValue[change]);
            }
            else if (currentPersonaGame == 2 && animArcana[change].Location.Y < 1200){
                animArcana[change].Location = new Point(animArcana[change].Location.X, animArcana[change].Location.Y + 90);
            }
            else{
                updown.Stop();
                updown = null;
                //memAnim[change] = -1;
                changeAnim[change] = 0;
                if (memAnim[2] != -1) downSumArcana();

                if (change != 2 && (memAnim[0] == -1 ^ memAnim[1] == -1)){
                    resArcana();
                }
            }
        }
    }
}
