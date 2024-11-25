using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Win32;

namespace LoginScreenEditor
{
    public class LoginEditorManager
    {
        private string _Caption = "";
        private string _Txt = "";
        private string _ImageFilepath = "";
        public LoginEditorManager(string caption,string txt,string imageFilepath)
        {
            if (imageFilepath != string.Empty)
            {
                if (!System.IO.File.Exists(imageFilepath))
                {
                    throw new Exception("Login Image does not exist !");
                }

                Image img = null;

                try
                {
                    img = Image.FromFile(imageFilepath);
                }
                catch
                {
                    throw new Exception("Invalid Login Image !");
                }
            }

            _ImageFilepath = imageFilepath;
            _Caption = caption;
            _Txt = txt;
        }

        public void Apply()
        {
            RegistryKey reg = null;
            RegistryKey reg2 = null;
            RegistryKey reg3 = null;
            RegistryKey reg4 = null;
            RegistryKey reg5 = null;
            RegistryKey reg6 = null;

            try
            {
                reg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion");

                reg2 = reg.OpenSubKey("Policies", true);

                if (reg2 == null)
                {
                    reg2 = reg.CreateSubKey("Policies");
                }

                reg3 = reg2.OpenSubKey("System", true);

                if (reg3 == null)
                {
                    reg3 = reg2.CreateSubKey("System");
                }


                if (_Caption != string.Empty)
                {
                    reg3.SetValue("legalnoticecaption", _Caption);
                }
                else
                {
                    try
                    {
                        reg3.DeleteValue("legalnoticecaption");
                    }
                    catch { }
                }

                if (_Txt != string.Empty)
                {
                    reg3.SetValue("legalnoticetext", _Txt);
                }
                else
                {
                    try
                    {
                        reg3.DeleteValue("legalnoticetext");
                    }
                    catch { }
                }

                reg4 = reg.OpenSubKey("PersonalizationCSP", true);

                if (reg4 == null)
                {
                    reg4 = reg.CreateSubKey("PersonalizationCSP");
                }

                reg5 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows", true);

                if (reg5==null)
                {
                    reg5 = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\Windows");
                }

                reg6 = reg5.OpenSubKey("Personalization", true);

                if (reg6 == null)
                {
                    reg6 = reg5.CreateSubKey("Personalization");
                }                

                if (_ImageFilepath!=string.Empty)
                {
                    reg4.SetValue("LockScreenImagePath", _ImageFilepath);
                    reg4.SetValue("LockScreenImageUrl", _ImageFilepath);
                    reg4.SetValue("LockScreenImageStatus", 1);

                    reg6.SetValue("LockScreenImage", _ImageFilepath);
                    reg6.SetValue("NoChangingLockScreen", 1);

                }
                else
                {
                    try
                    {
                        reg4.DeleteValue("LockScreenImagePath");
                        reg4.DeleteValue("LockScreenImageUrl");
                        reg4.DeleteValue("LockScreenImageStatus");

                        reg6.DeleteValue("LockScreenImage");
                        reg6.DeleteValue("NoChangingLockScreen");
                    }
                    catch { }
                }
            }
            finally
            {
                if (reg != null) reg.Close();
                if (reg2 != null) reg2.Close();
                if (reg3 != null) reg3.Close();

                if (reg4 != null) reg2.Close();
                if (reg5 != null) reg3.Close();                
                if (reg6 != null) reg3.Close();
            }

            
        }
    }
}
