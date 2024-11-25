using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace LoginScreenEditor
{
    public partial class frmMain : CustomForm
    {
        public static frmMain Instance = null;
        public frmMain()
        {
            InitializeComponent();

            Instance = this;

            //3Properties.Settings.Default.Password = "";            
        }                        

        #region Share

        private void tsiFacebook_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareFacebook();
        }

        private void tsiTwitter_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareTwitter();
        }

        private void tsiGooglePlus_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareGooglePlus();
        }

        private void tsiLinkedIn_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareLinkedIn();
        }

        private void tsiEmail_Click(object sender, EventArgs e)
        {
            ShareHelper.ShareEmail();
        }

        #endregion        

        bool FreeForPersonalUse = false;
        bool FreeForPersonalAndCommercialUse = true;

        private void SetTitle()
        {
            string str = "";
                        
            if (FreeForPersonalUse)
            {
                str += " - " + TranslateHelper.Translate("Free for Personal Use Only - Please Donate !");
            }
            else if (FreeForPersonalAndCommercialUse)
            {
                str += " - " + TranslateHelper.Translate("Free for Personal and Commercial Use - Please Donate !");
            }

            this.Text = Module.ApplicationTitle + str.ToUpper();
        }
        public void SetupOnLoad()
        {            
            //3this.Icon = Properties.Resources.pdf_compress_48;

            this.Text = Module.ApplicationTitle;

            SetTitle();

            //this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            //this.Left = 0;
            //12AddLanguageMenuItems();

            AdjustSizeLocation();            

            ResizeControls();

            //12checkForNewVersionEachWeekToolStripMenuItem.Checked = Properties.Settings.Default.CheckWeek;                        

            txtLoginCaption.Text = Properties.Settings.Default.Caption;
            txtLoginText.Text = Properties.Settings.Default.Text;
            txtLoginImage.Text = Properties.Settings.Default.ImageFilepath;
        }        
        private void AdjustSizeLocation()
        {
            if (Properties.Settings.Default.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {

                if (Properties.Settings.Default.Width == -1)
                {
                    this.CenterToScreen();
                    return;
                }
                else
                {
                    this.Width = Properties.Settings.Default.Width;
                }
                if (Properties.Settings.Default.Height != 0)
                {
                    this.Height = Properties.Settings.Default.Height;
                }

                if (Properties.Settings.Default.Left != -1)
                {
                    this.Left = Properties.Settings.Default.Left;
                }

                if (Properties.Settings.Default.Top != -1)
                {
                    this.Top = Properties.Settings.Default.Top;
                }

                if (this.Width < 300)
                {
                    this.Width = 300;
                }

                if (this.Height < 300)
                {
                    this.Height = 300;
                }

                if (this.Left < 0)
                {
                    this.Left = 0;
                }

                if (this.Top < 0)
                {
                    this.Top = 0;
                }
            }

        }

        private void SaveSizeLocation()
        {
            Properties.Settings.Default.Maximized = (this.WindowState == FormWindowState.Maximized);

            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized) return;

            if (this.WindowState == System.Windows.Forms.FormWindowState.Maximized)
            {
                Properties.Settings.Default.Save();
                return;
            }

            Properties.Settings.Default.Left = this.Left;
            Properties.Settings.Default.Top = this.Top;
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Save();

        }
        /*12
        #region Localization

        private void AddLanguageMenuItems()
        {
            for (int k = 0; k < frmLanguage.LangCodes.Count; k++)
            {
                ToolStripMenuItem ti = new ToolStripMenuItem();
                ti.Text = frmLanguage.LangDesc[k];
                ti.Tag = frmLanguage.LangCodes[k];
                ti.Image = frmLanguage.LangImg[k];

                if (Properties.Settings.Default.Language == frmLanguage.LangCodes[k])
                {
                    ti.Checked = true;
                }

                ti.Click += new EventHandler(tiLang_Click);

                if (k < 25)
                {
                    languages1ToolStripMenuItem.DropDownItems.Add(ti);
                }
                else
                {
                    languages2ToolStripMenuItem.DropDownItems.Add(ti);
                }

                //languageToolStripMenuItem.DropDownItems.Add(ti);
            }
        }

        void tiLang_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ti = (ToolStripMenuItem)sender;
            string langcode = ti.Tag.ToString();
            ChangeLanguage(langcode);

            //for (int k = 0; k < languageToolStripMenuItem.DropDownItems.Count; k++)
            for (int k = 0; k < languages1ToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem til = (ToolStripMenuItem)languages1ToolStripMenuItem.DropDownItems[k];
                if (til == ti)
                {
                    til.Checked = true;
                }
                else
                {
                    til.Checked = false;
                }
            }

            for (int k = 0; k < languages2ToolStripMenuItem.DropDownItems.Count; k++)
            {
                ToolStripMenuItem til = (ToolStripMenuItem)languages2ToolStripMenuItem.DropDownItems[k];
                if (til == ti)
                {
                    til.Checked = true;
                }
                else
                {
                    til.Checked = false;
                }
            }
        }

        private bool InChangeLanguage = false;

        private void ChangeLanguage(string language_code)
        {
            try
            {
                InChangeLanguage = true;

                Properties.Settings.Default.Language = language_code;
                frmLanguage.SetLanguage();

                Module.ShowMessage("Please restart the application !");
                Application.Exit();

                return;

                bool maximized = (this.WindowState == FormWindowState.Maximized);
                this.WindowState = FormWindowState.Normal;

                /*
                RegistryKey key = Registry.CurrentUser;
                RegistryKey key2 = Registry.CurrentUser;

                try
                {
                    key = key.OpenSubKey("Software\\softpcapps Software", true);

                    if (key == null)
                    {
                        key = Registry.CurrentUser.CreateSubKey("SOFTWARE\\softpcapps Software");
                    }

                    key2 = key.OpenSubKey(frmLanguage.RegKeyName, true);

                    if (key2 == null)
                    {
                        key2 = key.CreateSubKey(frmLanguage.RegKeyName);
                    }

                    key = key2;

                    //key.SetValue("Language", language_code);
                    key.SetValue("Menu Item Caption", TranslateHelper.Translate("Change PDF Properties"));
                }
                catch (Exception ex)
                {
                    Module.ShowError(ex);
                    return;
                }
                finally
                {
                    key.Close();
                    key2.Close();
                }
                */
        //1SaveSizeLocation();

        //3SavePositionSize();\
        /*12

        this.Controls.Clear();

        InitializeComponent();

        SetupOnLoad();

        if (maximized)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        this.ResumeLayout(true);
    }
    finally
    {
        InChangeLanguage = false;
    }
}

#endregion
*/

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {


                SetupOnLoad();

                /*12
                if (Properties.Settings.Default.CheckWeek)
                {
                    UpdateHelper.InitializeCheckVersionWeek();
                }
                */

                if (txtLoginImage.Text.Trim()!=string.Empty)
                {
                   if (!System.IO.File.Exists(txtLoginImage.Text))
                    {
                        Module.ShowMessage("Login Image does not exist !");
                    }
                   else
                    {
                        try {

                            Image img = Image.FromFile(txtLoginImage.Text);
                            picImage.Image = img;
                        }
                        catch
                        {
                            Module.ShowMessage("Invalid Login Image !");
                        }
                    }
                }
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        #region Help

        private void helpGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(Application.StartupPath + "\\Video Cutter Joiner Expert - User's Manual.chm");
            System.Diagnostics.Process.Start(Module.HelpURL);
        }

        private void pleaseDonateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://softpcapps.com/donate.php");
        }

        private void dotsSoftwarePRODUCTCATALOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://softpcapps.com/downloads/4dots-Software-PRODUCT-CATALOG.pdf");
        }        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void tiHelpFeedback_Click(object sender, EventArgs e)
        {
            /*
            frmUninstallQuestionnaire f = new frmUninstallQuestionnaire(false);
            f.ShowDialog();
            */

            System.Diagnostics.Process.Start("https://softpcapps.com/support/bugfeature.php?app=" + System.Web.HttpUtility.UrlEncode(Module.ShortApplicationTitle));
        }

        private void followUsOnTwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.twitter.com/4dotsSoftware");
        }

        private void visit4dotsSoftwareWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://softpcapps.com");
        }

        private void checkForNewVersionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateHelper.CheckVersion(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch { }
        }

        #endregion

        public void SaveOptions()
        {
            Properties.Settings.Default.CheckWeek = checkForNewVersionEachWeekToolStripMenuItem.Checked;

            Properties.Settings.Default.Caption = txtLoginCaption.Text;
            Properties.Settings.Default.Text = txtLoginText.Text;
            Properties.Settings.Default.ImageFilepath = txtLoginImage.Text;

            Properties.Settings.Default.Save();
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                DialogResult dres = Module.ShowQuestionDialog(TranslateHelper.Translate("Are you sure that you want to exit") + " " + Module.ApplicationName + " ?"
                    , TranslateHelper.Translate("Exit") + " " + Module.ApplicationName + " ?");

                if (dres != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }            

            SaveOptions();
            SaveSizeLocation();

            Properties.Settings.Default.Save();
        }                
        
        private void youtubeChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            FileShower fs = new FileShower(@"c:\1\03\con.a.jpg");
            fs.Show();

            Module.ShowError(fs.Err);
            */

            System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCovA-lld9Q79l08K-V1QEng");
        }        
        
        private void frmMain_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Refresh();
        }       

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            
            ofd.Filter = "Login Screen Editor Softpcapps Project (*.lep)|*.lep";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Project pr = new Project(ofd.FileName);
                pr.Load();
            }
        }        
        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            SaveFileDialog ofd = new SaveFileDialog();

            ofd.Filter = "Login Screen Editor Softpcapps Project (*.lep)|*.lep";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Project pr = new Project(ofd.FileName);
                pr.Save();
            }
        }

        private void tsbApply2_Click(object sender, EventArgs e)
        {
            LoginEditorManager lm = new LoginEditorManager(txtLoginCaption.Text, txtLoginText.Text, txtLoginImage.Text);
            lm.Apply();

            Module.ShowMessage("Operation completed successfully !");
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = Module.OpenImagesFilter;

            if (ofd.ShowDialog()==DialogResult.OK)
            {
                if (Module.IsImage(ofd.FileName))
                {
                    txtLoginImage.Text = ofd.FileName;

                    Image img = Image.FromFile(ofd.FileName);

                    picImage.Image = img;
                }
                else
                {
                    Module.ShowMessage("Invalid Image File !");
                }
            }
        }

        private void tsbClear_Click(object sender, EventArgs e)
        {
            DialogResult dres = Module.ShowQuestionDialog(TranslateHelper.Translate("Clear settings. Are you sure ?"), TranslateHelper.Translate("Clear Settings ?"));

            if (dres==DialogResult.Yes)
            {
                txtLoginCaption.Text = "";
                txtLoginImage.Text = "";
                txtLoginText.Text = "";

                picImage.Image = null;
            }


        }
    }
}
