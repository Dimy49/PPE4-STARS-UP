﻿using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CColor = System.Drawing.Color;

namespace PPE4_Stars_up
{
    public partial class FormParametre : Form
    {

        int A = 0;
        int R = 0;
        int G = 0;
        int B = 0;

        string couleur = "";

        double opa = 1;
        int rentre = 0;
        string FichierLangue = "";
        List<string> LangueElement = new List<string>();

        string fileName2 = @"C:\PPE4_DR\Preferences_PPE4_DR.txt";

        public FormParametre()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FormParametre_FormClosed(object sender, FormClosedEventArgs e)
        {
            // On récupère tout
            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();


            if (AffichageInputBox() == "Oui")
            {
                InputBox(LangueElement[122], "");
            }

            // On met à jour

            listeElement[3] = opa.ToString();

            if (cbBoiteInputOui.Checked == true)
            {
                listeElement[4] = "Oui";
            }

            if (cbBoiteInputNon.Checked == true)
            {
                listeElement[4] = "Non";
            }

            if (cbTutorielOui.Checked == true)
            {
                listeElement[5] = "Oui";
            }

            if (cbTutorielNon.Checked == true)
            {
                listeElement[5] = "Non";
            }

            if(lblCouleur.Text != LangueElement[123])
            {
                listeElement[6] = couleur;
            }
            else
            {
                listeElement[6] = lblCouleur.Text;
            }

            listeElement[1] = combobLangue.SelectedItem.ToString();

            StreamWriter writer = new StreamWriter(fileName2);

            foreach (var item in listeElement)
            {
                writer.WriteLine(item);
            }
            writer.Close();
        }

        private void FormParametre_Load(object sender, EventArgs e)
        {
            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();

            if (listeElement[1] == "Francais")
            {
                FichierLangue = "Francais.txt";
            }

            if (listeElement[1] == "Anglais")
            {
                FichierLangue = "Anglais.txt";
            }

            if (listeElement[1] == "Allemand")
            {
                FichierLangue = "Allemand.txt";
            }

            if (listeElement[1] == "Espagnol")
            {
                FichierLangue = "Espagne.txt";
            }

            StreamReader reader2 = File.OpenText(FichierLangue);
            string ligne2;

            while (!reader2.EndOfStream)
            {
                ligne2 = reader2.ReadLine();
                LangueElement.Add(ligne2);
            }
            reader.Close();

            this.Text = LangueElement[113];
            lblTitrePara.Text = LangueElement[113];
            lblTransparence.Text = LangueElement[114];
            lblRedemRequis.Text = LangueElement[115];
            lblInputBox.Text = LangueElement[116];
            cbBoiteInputOui.Text = LangueElement[117];
            cbTutorielOui.Text = LangueElement[117];
            cbBoiteInputNon.Text = LangueElement[118];
            cbTutorielNon.Text = LangueElement[118];
            lblTutoriel.Text = LangueElement[119];
            lblLangue.Text = LangueElement[120];
            lblCouleurAppli.Text = LangueElement[121];
            lblRedemRequis2.Text = LangueElement[125];

            // Gestion transparence
            if (listeElement[3] != "")
            {
                Opacity = Convert.ToDouble(listeElement[3]);
                hsTransparence.Value = Convert.ToInt32(Opacity * 100);
            }

            // Gestion Afficher fenêtre
            if (listeElement[4] == "Oui")
            {
                cbBoiteInputOui.Checked = true;
                cbBoiteInputNon.Checked = false;
            }
            else if (listeElement[4] == "Non")
            {
                cbBoiteInputOui.Checked = false;
                cbBoiteInputNon.Checked = true;
            }
            else
            {
                cbBoiteInputOui.Checked = true;
                cbBoiteInputNon.Checked = false;
            }

            // Gestion Afficher Tutoriel
            if (listeElement[5] == "Oui")
            {
                cbTutorielOui.Checked = true;
                cbTutorielNon.Checked = false;
            }
            else if (listeElement[5] == "Non")
            {
                cbTutorielOui.Checked = false;
                cbTutorielNon.Checked = true;
            }
            else
            {
                cbTutorielOui.Checked = true;
                cbTutorielNon.Checked = false;
            }

            foreach(var i in combobLangue.Items)
            {
                if(listeElement[1] == i.ToString())
                {
                    combobLangue.SelectedItem = i.ToString();
                }
            }

            // Gestion couleur background

            lblCouleur.Text = listeElement[6];

            if (lblCouleur.Text != "Par défaut")
            {
                int AA = 0;
                int RR = 0;
                int GG = 0;
                int BB = 0;

                // Get first match.
                Match match = Regex.Match(lblCouleur.Text, @"\d+");
                if (match.Success)
                {
                    AA = Convert.ToInt32(match.Value);
                }

                // Get second match.
                match = match.NextMatch();
                if (match.Success)
                {
                    RR = Convert.ToInt32(match.Value);
                }

                // Get 3 match.
                match = match.NextMatch();
                if (match.Success)
                {
                    GG = Convert.ToInt32(match.Value);
                }

                // Get 4 match.
                match = match.NextMatch();
                if (match.Success)
                {
                    BB = Convert.ToInt32(match.Value); ;
                }
                
                Color c = Color.FromArgb(AA, RR, GG, BB);
                this.BackColor = c;
            }
        }

        private void pbPalette_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblCouleur.Text = colorDialog.Color.ToString();
                A = colorDialog.Color.A;
                R = colorDialog.Color.R;
                G = colorDialog.Color.G;
                B = colorDialog.Color.B;

                Color myRgbColor = new Color();
                myRgbColor = Color.FromArgb(A, R, G, B);

                couleur = A + ", " + R + ", " + G + ", " + B;

                // lblCouleur.Text = colorDialog.Color.ToArgb().ToString()
                // lblCouleur.Text = couleur.ToString();

                if (lblCouleur.Text != "Par défaut")
                {
                    // MessageBox.Show(colorDialog.Color.ToString());
                    this.BackColor = colorDialog.Color;
                }
            }
        }

        private void hsTransparence_ValueChanged(object sender, EventArgs e)
        {
            lblNbTransparence.Text = hsTransparence.Value.ToString();
            opa = Convert.ToDouble(Convert.ToDouble((lblNbTransparence.Text)) / 100);
            Opacity = opa;

            FormIndex.ActiveForm.Opacity = opa;
        }

        private void cbBoiteInputOui_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbBoiteInputOui_Click(object sender, EventArgs e)
        {
            cbBoiteInputNon.Checked = false;

            if (cbBoiteInputNon.Checked == false)
            {
                cbBoiteInputOui.Checked = true;
            }
        }

        private void cbBoiteInputNon_Click(object sender, EventArgs e)
        {
            cbBoiteInputOui.Checked = false;

            if (cbBoiteInputOui.Checked == false)
            {
                cbBoiteInputNon.Checked = true;
            }
        }

        private void cbTutorielOui_Click(object sender, EventArgs e)
        {
            cbTutorielNon.Checked = false;

            if (cbTutorielNon.Checked == false)
            {
                cbTutorielOui.Checked = true;
            }
        }

        private void cbTutorielNon_Click(object sender, EventArgs e)
        {
            cbTutorielOui.Checked = false;

            if (cbTutorielOui.Checked == false)
            {
                cbTutorielNon.Checked = true;
            }
        }

        private void combobLangue_SelectedIndexChanged(object sender, EventArgs e)
        {
            rentre++;

            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();

            if (rentre != 1)
            {
                MessageBox.Show(LangueElement[47], LangueElement[42], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static int InputBox(string title, string promptText)
        {
            Form form = new Form();
            LinkLabel texte = new LinkLabel();
            ProgressBar Progress = new ProgressBar();

            Progress.Minimum = 0;
            Progress.Maximum = 100;

            form.Text = title;
            texte.Text = promptText;
            texte.SetBounds(9, 20, 372, 13);
            Progress.SetBounds(9, 30, 372, 20);

            texte.AutoSize = true;
            Progress.Anchor = Progress.Anchor | AnchorStyles.Right;

            form.ClientSize = new Size(396, 91);
            form.Controls.AddRange(new Control[] { texte, Progress });
            form.ClientSize = new Size(Math.Max(300, texte.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            Progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;

            form.Show();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(10); // --> Timer au tick

                Progress.Value += 1;
                form.Show();
            }

            int Res = Progress.Value;
            if (Res == 100)
                form.Close();

            return Res;
        }

        public string AffichageInputBox()
        {
            string resultat = "";

            StreamReader reader = File.OpenText(fileName2);
            string ligne;

            List<string> listeElement = new List<string>();
            while (!reader.EndOfStream)
            {
                ligne = reader.ReadLine();
                listeElement.Add(ligne);
            }
            reader.Close();

            // Gestion Affichage InputBox
            if (listeElement[4] == "Oui")
            {
                resultat = "Oui";
            }
            else if (listeElement[4] == "Non")
            {
                resultat = "Non";
            }

            return resultat;
        }

        private void btnDefaut_Click(object sender, EventArgs e)
        {
            if(this.BackColor == SystemColors.Control)
            {
                MessageBox.Show(LangueElement[124], LangueElement[105], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.BackColor = SystemColors.Control;
                lblCouleur.Text = LangueElement[123];
            }            
        }
    }
}
