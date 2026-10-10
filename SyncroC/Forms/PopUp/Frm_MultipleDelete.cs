using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using SyncroC.Utility;
using SyncroC.Network;

namespace SyncroC.Forms.PopUp
{
    public class Frm_MultipleDelete : SyncroC.Forms.PopUp.Frm_PopUp
    {
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Abort_YN;
        private SyncroC.Controls.TitledNumericUpDown tnd_DeleteFrom;
        private SyncroC.Controls.TitledNumericUpDown tnd_DeleteTo;
        private System.ComponentModel.IContainer components = null;

        public Frm_MultipleDelete()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show the form as a modal dialog
        /// </summary>
        /// <param name="owner">Window that will own the modal dialog</param>
        /// <param name="firstOrderToDeletePos">Position in the grid of first order to be deleted</param>
        /// <param name="lastOrderToDeletePos">Position in the grid of last order to be deleted.</param>
        public FosberDialogResult ShowDialog(IWin32Window owner, int firstOrderToDeletePos, int lastOrderToDeletePos)
        {
            tnd_DeleteFrom.Value = firstOrderToDeletePos.ToString();
            tnd_DeleteTo.Value = lastOrderToDeletePos.ToString();
            return new FosberDialogResult(this.ShowDialog(owner), this.data);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MultipleDelete));
          this.btn_OK = new System.Windows.Forms.Button();
          this.btn_Abort_YN = new System.Windows.Forms.Button();
          this.tnd_DeleteFrom = new SyncroC.Controls.TitledNumericUpDown();
          this.tnd_DeleteTo = new SyncroC.Controls.TitledNumericUpDown();
          this.SuspendLayout();
          // 
          // iml_AccessibilityImages
          // 
          this.iml_AccessibilityImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iml_AccessibilityImages.ImageStream")));
          this.iml_AccessibilityImages.Images.SetKeyName(0, "");
          this.iml_AccessibilityImages.Images.SetKeyName(1, "");
          // 
          // btn_OK
          // 
          this.btn_OK.BackColor = System.Drawing.Color.WhiteSmoke;
          this.btn_OK.Cursor = System.Windows.Forms.Cursors.Hand;
          this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
          this.btn_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.btn_OK.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btn_OK.Location = new System.Drawing.Point(25, 117);
          this.btn_OK.Name = "btn_OK";
          this.btn_OK.Size = new System.Drawing.Size(135, 57);
          this.btn_OK.TabIndex = 2;
          this.btn_OK.Text = "OK";
          this.btn_OK.UseVisualStyleBackColor = false;
          this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
          // 
          // btn_Abort_YN
          // 
          this.btn_Abort_YN.BackColor = System.Drawing.Color.WhiteSmoke;
          this.btn_Abort_YN.Cursor = System.Windows.Forms.Cursors.Hand;
          this.btn_Abort_YN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
          this.btn_Abort_YN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
          this.btn_Abort_YN.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.btn_Abort_YN.Location = new System.Drawing.Point(189, 117);
          this.btn_Abort_YN.Name = "btn_Abort_YN";
          this.btn_Abort_YN.Size = new System.Drawing.Size(135, 57);
          this.btn_Abort_YN.TabIndex = 3;
          this.btn_Abort_YN.Text = "ABORT";
          this.btn_Abort_YN.UseVisualStyleBackColor = false;
          this.btn_Abort_YN.Click += new System.EventHandler(this.btn_Abort_Click);
          // 
          // tnd_DeleteFrom
          // 
          this.tnd_DeleteFrom.DecimalPlaces = 0;
          this.tnd_DeleteFrom.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.tnd_DeleteFrom.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.tnd_DeleteFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.tnd_DeleteFrom.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
          this.tnd_DeleteFrom.Location = new System.Drawing.Point(25, 25);
          this.tnd_DeleteFrom.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
          this.tnd_DeleteFrom.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
          this.tnd_DeleteFrom.Name = "tnd_DeleteFrom";
          this.tnd_DeleteFrom.ReadOnly = false;
          this.tnd_DeleteFrom.Size = new System.Drawing.Size(135, 73);
          this.tnd_DeleteFrom.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
          this.tnd_DeleteFrom.TabIndex = 0;
          this.tnd_DeleteFrom.Text = "From";
          this.tnd_DeleteFrom.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          this.tnd_DeleteFrom.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.tnd_DeleteFrom.TranslateText = true;
          this.tnd_DeleteFrom.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
          this.tnd_DeleteFrom.Value = "0";
          this.tnd_DeleteFrom.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          // 
          // tnd_DeleteTo
          // 
          this.tnd_DeleteTo.DecimalPlaces = 0;
          this.tnd_DeleteTo.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.tnd_DeleteTo.FrameColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.tnd_DeleteTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.tnd_DeleteTo.InternalUnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
          this.tnd_DeleteTo.Location = new System.Drawing.Point(189, 25);
          this.tnd_DeleteTo.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
          this.tnd_DeleteTo.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
          this.tnd_DeleteTo.Name = "tnd_DeleteTo";
          this.tnd_DeleteTo.ReadOnly = false;
          this.tnd_DeleteTo.Size = new System.Drawing.Size(135, 73);
          this.tnd_DeleteTo.Style = SyncroC.Controls.TitledNumericUpDownStyles.Light;
          this.tnd_DeleteTo.TabIndex = 1;
          this.tnd_DeleteTo.Text = "To";
          this.tnd_DeleteTo.TitleBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          this.tnd_DeleteTo.TitleForeColor = SyncroC.Controls.Colors.SyncroColor.Black;
          this.tnd_DeleteTo.TranslateText = true;
          this.tnd_DeleteTo.UnitMeasure = SyncroC.Utility.UnitMeasure.NoUnitMeasure;
          this.tnd_DeleteTo.Value = "0";
          this.tnd_DeleteTo.ValueBackColor = SyncroC.Controls.Colors.SyncroColor.LightGray;
          // 
          // Frm_MultipleDelete
          // 
          this.AcceptButton = this.btn_OK;
          this.CancelButton = this.btn_Abort_YN;
          this.ClientSize = new System.Drawing.Size(350, 200);
          this.Controls.Add(this.tnd_DeleteTo);
          this.Controls.Add(this.tnd_DeleteFrom);
          this.Controls.Add(this.btn_Abort_YN);
          this.Controls.Add(this.btn_OK);
          this.Name = "Frm_MultipleDelete";
          this.Text = "Frm_MultipleDelete";
          this.ResumeLayout(false);

				}
        #endregion

        private void btn_OK_Click(object sender, System.EventArgs e)
        {
            try
            {
              this.data = new object[] { Convert.ToInt32(tnd_DeleteFrom.Value), Convert.ToInt32(tnd_DeleteTo.Value) };
            }
            catch (Exception)
            {
              this.data = new object[] { -1, -1 };
						}

            this.Dispose();
        }

        private void btn_Abort_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }
    }
}

