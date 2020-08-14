using System;
using System.IO;
using System.Web;

namespace CSharpAssignment
{
    public partial class UploadFile : System.Web.UI.Page
    {
        public const int MAXIMUM_FILE_SIZE = 1000000;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetFile();
                if (LstFiles.Items.Count != 0)
                {
                    imgPreview.ImageUrl = "~/images/" + LstFiles.SelectedItem.Text;
                    lblMapPath.Text = "~/images/" + LstFiles.SelectedItem.Text;
                }
            }
        }

        public void GetFile()
        {
            try
            {
                String[] files = Directory.GetFiles(Server.MapPath("~/images"));
                for (int i = 0; i < files.Length; i++)
                {
                    files[i] = new FileInfo(files[i]).Name;
                }
                LstFiles.DataSource = files;
                LstFiles.DataBind();
                LstFiles.SelectedIndex = 0;
            }
            catch { }
        }

        public void ResetUploadMsg()
        {
            lblUploadMsg.Text = "";
            imgUploadMsg.ImageUrl = "~/images/icons/none.png";
        }

        public void ResetDeleteMsg()
        {
            lblDeleteMsg.Text = "";
            imgDeleteMsg.ImageUrl = "~/images/icons/none.png";
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {
            ResetDeleteMsg();

            HttpPostedFile file = fuFile.PostedFile;
            if (fuFile.HasFile == false)
            {
                lblUploadMsg.ForeColor = System.Drawing.Color.Red;
                lblUploadMsg.Text = "Select a file to upload";
                imgUploadMsg.ImageUrl = "~/images/icons/error.png";
            }
            else if (fuFile.HasFile == true && file.ContentLength > MAXIMUM_FILE_SIZE)
            {
                lblUploadMsg.ForeColor = System.Drawing.Color.Red;
                lblUploadMsg.Text = "File cannot be greater than 1 MB";
                imgUploadMsg.ImageUrl = "~/images/icons/error.png";
            }
            else
            {
                string imagePath = Server.MapPath("~/images/" + fuFile.FileName);
                fuFile.SaveAs(imagePath);

                imgPreview.ImageUrl = "~/images/" + fuFile.FileName;
                lblMapPath.Text = "~/images/" + fuFile.FileName;

                lblUploadMsg.ForeColor = System.Drawing.Color.Blue;
                lblUploadMsg.Text = "Upload succeeded";
                imgUploadMsg.ImageUrl = "~/images/icons/confirm.png";

                GetFile();
            }
        }

        protected void LstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            imgPreview.ImageUrl = "~/images/" + LstFiles.SelectedItem.Text;
            lblMapPath.Text = "~/images/" + LstFiles.SelectedItem.Text;
            ResetUploadMsg();
            ResetDeleteMsg();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            ResetUploadMsg();

            try
            {
                File.Delete(MapPath("images") + "\\" + LstFiles.SelectedItem.Text);
                GetFile();
                if (LstFiles.Items.Count != 0)
                {
                    imgPreview.ImageUrl = "~/images/" + LstFiles.SelectedItem.Text;
                    lblMapPath.Text = "~/images/" + LstFiles.SelectedItem.Text;
                }
                else
                {
                    imgPreview.ImageUrl = "";
                    lblMapPath.Text = "";
                }
            }
            catch
            {
                lblDeleteMsg.ForeColor = System.Drawing.Color.Red;
                lblDeleteMsg.Text = "Choose a file to delete";
                imgDeleteMsg.ImageUrl = "~/images/icons/error.png";
            }
        }
    }
}