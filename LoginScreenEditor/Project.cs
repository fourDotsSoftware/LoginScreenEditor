using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Security;

namespace LoginScreenEditor
{
    public class Project
    {
        string _Filepath = "";

        public Project(string filepath)
        {
            _Filepath = filepath;
        }

        public bool Load()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                
                doc.LoadXml(System.IO.File.ReadAllText(_Filepath));

                XmlNode no = doc.SelectSingleNode("//Project");

                string caption = no.Attributes.GetNamedItem("Caption").Value;

                string txt = no.Attributes.GetNamedItem("Txt").Value;

                string image = no.Attributes.GetNamedItem("Image").Value;

                frmMain.Instance.txtLoginCaption.Text = caption;
                frmMain.Instance.txtLoginText.Text = txt;
                
                if (image!=string.Empty)
                {
                    if (!System.IO.File.Exists(image))
                    {
                        Module.ShowMessage("Project's Login Image does not exist !");
                        return false;
                    }

                    try
                    {
                        Image img = Image.FromFile(image);

                        frmMain.Instance.picImage.Image = img;

                        frmMain.Instance.txtLoginImage.Text = image;

                    }
                    catch
                    {
                        Module.ShowMessage("Invalid Login Image !");
                        return false;
                    }
                }


            }
            catch
            {
                Module.ShowMessage("Invalid Project !");
                return false;
            }

            return true;
        }

        public bool Save()
        {
            string xml = "<Projects>";
            xml += "<Project Caption=\"" + SecurityElement.Escape(frmMain.Instance.txtLoginCaption.Text) + "\"" +
                " Txt=\"" + SecurityElement.Escape(frmMain.Instance.txtLoginText.Text) + "\"" +
                " Image=\"" + SecurityElement.Escape(frmMain.Instance.txtLoginImage.Text) + "\" />";
            xml += "</Projects>";

            System.IO.File.WriteAllText(_Filepath, xml);

            return true;
        }
    }
}
