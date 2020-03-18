using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.IO.Compression;

namespace Blazar_Installer
{
    public partial class Form1 : Form
    {

        private bool mouseDown;
        private Point lastLocation;

        public Form1()
        {

            InitializeComponent();
            richTextBox1.ReadOnly = true;
            button1.FlatStyle = FlatStyle.Flat;
            button2.FlatStyle = FlatStyle.Flat;

        }

        private void button2_Click(object sender, EventArgs e)
        {

            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String mcDir = Path.Combine(appdata, ".minecraft/");
            String modDir = Path.Combine(mcDir, "mods/");
            String versionsDir = Path.Combine(mcDir, "versions/paladium/");
            String libsDir = Path.Combine(mcDir, "libraries/net/minecraftforge/forge/paladium");

            if (Directory.Exists(modDir))
            {

                DirectoryInfo di = new DirectoryInfo(modDir);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

            }

            if (Directory.Exists(versionsDir))
            {

                DirectoryInfo di = new DirectoryInfo(versionsDir);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(versionsDir);

            }

            if (Directory.Exists(libsDir))
            {

                DirectoryInfo di = new DirectoryInfo(libsDir);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(libsDir);

            }

            MessageBox.Show("Blazar a bien été désinstallé !", "Blazar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void addInfoRTB(String info)
        {

            richTextBox1.Text = richTextBox1.Text + info + "\n";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.FlatStyle = FlatStyle.Flat;

            String cDir = Directory.GetCurrentDirectory();
            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String cLibs = Path.Combine(cDir, "libs.zip");
            String cMods = Path.Combine(cDir, "mods.zip");
            String cVersions = Path.Combine(cDir, "versions.zip");
          
            String mcDir = Path.Combine(appdata, ".minecraft/");
            String modDir = Path.Combine(mcDir, "mods/");
            String versionsDir = Path.Combine(mcDir, "versions/");
            String libsDir = Path.Combine(mcDir, "libraries/net/minecraftforge/forge/");

            String palaLibsZip = Path.Combine(libsDir, "libs.zip");
            String palaVersionZip = Path.Combine(versionsDir, "versions.zip");
            String palaModsZip = Path.Combine(modDir, "mods.zip");

            addInfoRTB("Initializing...");

            //----------------------------------------------------------------------------------------------------------------------//

            if (File.Exists(palaLibsZip)){File.Delete(palaLibsZip);}
            if (File.Exists(palaVersionZip)) { File.Delete(palaVersionZip); }
            if (File.Exists(palaModsZip)) { File.Delete(palaModsZip); }

            //----------------------------------------------------------------------------------------------------------------------//

            WebClient webClient = new WebClient();
            String downloadString = webClient.DownloadString("https://pastebin.com/raw/PBXwn0Pc");

            //----------------------------------------------------------------------------------------------------------------------//

            string remoteUri = downloadString;
            string fileName = "libs.zip", myStringWebResource = null;
            WebClient myWebClient = new WebClient();
            myStringWebResource = remoteUri + fileName;
            myWebClient.DownloadFile(myStringWebResource, fileName);

            addInfoRTB("Downloading libs...");

            File.Move(cLibs, Path.Combine(libsDir, "libs.zip"));

            addInfoRTB("Moveing libs...");

            Directory.CreateDirectory(Path.Combine(libsDir, "paladium/"));
            addInfoRTB("Creating libs dir...");
            ZipFile.ExtractToDirectory(palaLibsZip, Path.Combine(libsDir, "paladium/"));
            addInfoRTB("Extracting...");
            File.Delete(palaLibsZip);
            addInfoRTB("Deleting libs zip...");

            //----------------------------------------------------------------------------------------------------------------------//

            string remoteUrid = downloadString;
            string fileNamed = "mods.zip", myStringWebResourced = null;
            WebClient myWebClientd = new WebClient();
            myStringWebResourced = remoteUrid + fileNamed;
            myWebClientd.DownloadFile(myStringWebResourced, fileNamed);
            addInfoRTB("Downloading mods...");

            if (File.Exists(cMods))
            {

                File.Move(cMods, Path.Combine(modDir, "mods.zip"));
                addInfoRTB("Moving mods...");

            }

            ZipFile.ExtractToDirectory(palaModsZip, modDir);
            addInfoRTB("Extracting...");
            File.Delete(palaModsZip);
            addInfoRTB("Deleting mods zip...");

            //----------------------------------------------------------------------------------------------------------------------//

            string remoteUrit = downloadString;
            string fileNamet = "versions.zip", myStringWebResourcet = null;
            WebClient myWebClientt = new WebClient();
            myStringWebResourcet = remoteUrit + fileNamet;
            myWebClientt.DownloadFile(myStringWebResourcet, fileNamet);
            addInfoRTB("Downloading vers...");

            if (File.Exists(cVersions))
            {

                File.Move(cVersions, Path.Combine(versionsDir, "versions.zip"));
                addInfoRTB("Moving vers...");

            }

            ZipFile.ExtractToDirectory(palaVersionZip, Path.Combine(versionsDir, "paladium/"));
            addInfoRTB("Creating vers dir...");
            File.Delete(palaVersionZip);
            addInfoRTB("Deleting vers zip...");

            addInfoRTB("Finish !");

            MessageBox.Show("Blazar a été installé !", "Blazar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void panel1_mouseDown(object sender, MouseEventArgs e)
        {

            mouseDown = true;
            lastLocation = e.Location;

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {

            mouseDown = false;

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {

            mouseDown = true;
            lastLocation = e.Location;

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {

            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }

        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {

            mouseDown = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            WebClient webClient = new WebClient();
            if (!webClient.DownloadString("https://pastebin.com/raw/LFvjNEEP").Contains("1.6"))
            {

                MessageBox.Show("Une mise à jour est disponible, allez sur le discord pour la télécharger !", "OUAH", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Pour télécharger Blazar il faut avoir déjà avoir lancé une foi Forge 1.7.10.", "Aide", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Si vous ne l'avais jamais installer alors installer le (forge 1.7.10)", "Aide", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Ensuite pour download le cheat, il vous suffit d'appuyer sur Uninstall et Install", "Aide", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("Si on vous demande sur le cheat de faire une mise à jour, il faut faire Uninstall et Install", "Aide", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
