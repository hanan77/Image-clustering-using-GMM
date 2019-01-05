namespace GMM
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTestMM = new System.Windows.Forms.Button();
            this.btnTestClass = new System.Windows.Forms.Button();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.btnGMMND = new System.Windows.Forms.Button();
            this.txtNumClusters = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGMMImage = new System.Windows.Forms.Button();
            this.txtClusterID = new System.Windows.Forms.TextBox();
            this.btnClusterImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTestMM
            // 
            this.btnTestMM.Location = new System.Drawing.Point(34, 26);
            this.btnTestMM.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestMM.Name = "btnTestMM";
            this.btnTestMM.Size = new System.Drawing.Size(196, 37);
            this.btnTestMM.TabIndex = 0;
            this.btnTestMM.Text = "GMM 1D";
            this.btnTestMM.UseVisualStyleBackColor = true;
            this.btnTestMM.Click += new System.EventHandler(this.btnTestMM_Click);
            // 
            // btnTestClass
            // 
            this.btnTestClass.Location = new System.Drawing.Point(34, 141);
            this.btnTestClass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTestClass.Name = "btnTestClass";
            this.btnTestClass.Size = new System.Drawing.Size(196, 34);
            this.btnTestClass.TabIndex = 1;
            this.btnTestClass.Text = "Test Class";
            this.btnTestClass.UseVisualStyleBackColor = true;
            this.btnTestClass.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(148, 105);
            this.txtNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(84, 26);
            this.txtNum.TabIndex = 2;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(38, 105);
            this.lblNum.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(108, 20);
            this.lblNum.TabIndex = 3;
            this.lblNum.Text = "Enter Number";
            // 
            // pic1
            // 
            this.pic1.Location = new System.Drawing.Point(254, 26);
            this.pic1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(932, 688);
            this.pic1.TabIndex = 5;
            this.pic1.TabStop = false;
            // 
            // btnGMMND
            // 
            this.btnGMMND.Location = new System.Drawing.Point(34, 426);
            this.btnGMMND.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnGMMND.Name = "btnGMMND";
            this.btnGMMND.Size = new System.Drawing.Size(196, 39);
            this.btnGMMND.TabIndex = 6;
            this.btnGMMND.Text = "GMM ND";
            this.btnGMMND.UseVisualStyleBackColor = true;
            this.btnGMMND.Click += new System.EventHandler(this.btnGMMND_Click);
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(183, 391);
            this.txtNumClusters.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(48, 26);
            this.txtNumClusters.TabIndex = 7;
            this.txtNumClusters.Text = "4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 395);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "number of Clusters";
            // 
            // btnGMMImage
            // 
            this.btnGMMImage.Location = new System.Drawing.Point(34, 493);
            this.btnGMMImage.Name = "btnGMMImage";
            this.btnGMMImage.Size = new System.Drawing.Size(196, 45);
            this.btnGMMImage.TabIndex = 9;
            this.btnGMMImage.Text = "GMM Image";
            this.btnGMMImage.UseVisualStyleBackColor = true;
            this.btnGMMImage.Click += new System.EventHandler(this.btnGMMImage_Click);
            // 
            // txtClusterID
            // 
            this.txtClusterID.Location = new System.Drawing.Point(165, 569);
            this.txtClusterID.Name = "txtClusterID";
            this.txtClusterID.Size = new System.Drawing.Size(65, 26);
            this.txtClusterID.TabIndex = 10;
            // 
            // btnClusterImage
            // 
            this.btnClusterImage.Location = new System.Drawing.Point(34, 614);
            this.btnClusterImage.Name = "btnClusterImage";
            this.btnClusterImage.Size = new System.Drawing.Size(196, 51);
            this.btnClusterImage.TabIndex = 11;
            this.btnClusterImage.Text = "Cluster Image";
            this.btnClusterImage.UseVisualStyleBackColor = true;
            this.btnClusterImage.Click += new System.EventHandler(this.btnClusterImage_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 569);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cluster ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 677);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClusterImage);
            this.Controls.Add(this.txtClusterID);
            this.Controls.Add(this.btnGMMImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumClusters);
            this.Controls.Add(this.btnGMMND);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.lblNum);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.btnTestClass);
            this.Controls.Add(this.btnTestMM);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestMM;
        private System.Windows.Forms.Button btnTestClass;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.Button btnGMMND;
        private System.Windows.Forms.TextBox txtNumClusters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGMMImage;
        private System.Windows.Forms.TextBox txtClusterID;
        private System.Windows.Forms.Button btnClusterImage;
        private System.Windows.Forms.Label label2;
    }
}

