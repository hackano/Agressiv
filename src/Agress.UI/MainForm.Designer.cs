namespace Agress.UI
{
    partial class MainForm
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
			this.labelCaption = new System.Windows.Forms.Label();
			this.buttonLogin = new System.Windows.Forms.Button();
			this.buttonSalarySuggestion = new System.Windows.Forms.Button();
			this.buttonReg1 = new System.Windows.Forms.Button();
			this.buttonReg2 = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.textBoxInfo = new System.Windows.Forms.TextBox();
			this.buttonReg = new System.Windows.Forms.Button();
			this.buttonPrev = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.buttonNow = new System.Windows.Forms.Button();
			this.buttonSaveTime = new System.Windows.Forms.Button();
			this.buttonSalarySpec = new System.Windows.Forms.Button();
			this.buttonExpenses = new System.Windows.Forms.Button();
			this.textBoxReg1 = new System.Windows.Forms.TextBox();
			this.textBoxReg2 = new System.Windows.Forms.TextBox();
			this.buttonVersion = new System.Windows.Forms.Button();
			this.buttonMiles = new System.Windows.Forms.Button();
			this.buttonBroBizz = new System.Windows.Forms.Button();
			this.buttonMassage = new System.Windows.Forms.Button();
			this.buttonSaveExpenses = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelCaption
			// 
			this.labelCaption.AutoSize = true;
			this.labelCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelCaption.Location = new System.Drawing.Point(22, 30);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new System.Drawing.Size(508, 25);
			this.labelCaption.TabIndex = 0;
			this.labelCaption.Text = "Agresso automatisk tid och utgiftsregistreringsmotor";
			// 
			// buttonLogin
			// 
			this.buttonLogin.Location = new System.Drawing.Point(27, 82);
			this.buttonLogin.Name = "buttonLogin";
			this.buttonLogin.Size = new System.Drawing.Size(109, 23);
			this.buttonLogin.TabIndex = 1;
			this.buttonLogin.Text = "Öppna && Logga in";
			this.buttonLogin.UseVisualStyleBackColor = true;
			this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
			// 
			// buttonSalarySuggestion
			// 
			this.buttonSalarySuggestion.Location = new System.Drawing.Point(42, 111);
			this.buttonSalarySuggestion.Name = "buttonSalarySuggestion";
			this.buttonSalarySuggestion.Size = new System.Drawing.Size(73, 23);
			this.buttonSalarySuggestion.TabIndex = 2;
			this.buttonSalarySuggestion.Text = "Löneförslag";
			this.buttonSalarySuggestion.UseVisualStyleBackColor = true;
			this.buttonSalarySuggestion.Click += new System.EventHandler(this.buttonSalarySuggestion_Click);
			// 
			// buttonReg1
			// 
			this.buttonReg1.Location = new System.Drawing.Point(42, 232);
			this.buttonReg1.Name = "buttonReg1";
			this.buttonReg1.Size = new System.Drawing.Size(109, 23);
			this.buttonReg1.TabIndex = 3;
			this.buttonReg1.Text = "Registrera ->";
			this.buttonReg1.UseVisualStyleBackColor = true;
			this.buttonReg1.Click += new System.EventHandler(this.buttonReg1_Click);
			// 
			// buttonReg2
			// 
			this.buttonReg2.Location = new System.Drawing.Point(42, 261);
			this.buttonReg2.Name = "buttonReg2";
			this.buttonReg2.Size = new System.Drawing.Size(109, 23);
			this.buttonReg2.TabIndex = 4;
			this.buttonReg2.Text = "Registrera ->";
			this.buttonReg2.UseVisualStyleBackColor = true;
			this.buttonReg2.Click += new System.EventHandler(this.buttonReg2_Click);
			// 
			// buttonClose
			// 
			this.buttonClose.Location = new System.Drawing.Point(25, 522);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(106, 23);
			this.buttonClose.TabIndex = 5;
			this.buttonClose.Text = "Logga ut && Stäng";
			this.buttonClose.UseVisualStyleBackColor = true;
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// textBoxInfo
			// 
			this.textBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxInfo.Location = new System.Drawing.Point(212, 82);
			this.textBoxInfo.Multiline = true;
			this.textBoxInfo.Name = "textBoxInfo";
			this.textBoxInfo.ReadOnly = true;
			this.textBoxInfo.Size = new System.Drawing.Size(396, 82);
			this.textBoxInfo.TabIndex = 6;
			// 
			// buttonReg
			// 
			this.buttonReg.Location = new System.Drawing.Point(42, 140);
			this.buttonReg.Name = "buttonReg";
			this.buttonReg.Size = new System.Drawing.Size(40, 23);
			this.buttonReg.TabIndex = 7;
			this.buttonReg.Text = "Reg";
			this.buttonReg.UseVisualStyleBackColor = true;
			this.buttonReg.Click += new System.EventHandler(this.buttonReg_Click);
			// 
			// buttonPrev
			// 
			this.buttonPrev.Location = new System.Drawing.Point(88, 141);
			this.buttonPrev.Name = "buttonPrev";
			this.buttonPrev.Size = new System.Drawing.Size(27, 23);
			this.buttonPrev.TabIndex = 8;
			this.buttonPrev.Text = "<";
			this.buttonPrev.UseVisualStyleBackColor = true;
			this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
			// 
			// buttonNext
			// 
			this.buttonNext.Location = new System.Drawing.Point(154, 140);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(27, 23);
			this.buttonNext.TabIndex = 8;
			this.buttonNext.Text = ">";
			this.buttonNext.UseVisualStyleBackColor = true;
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// buttonNow
			// 
			this.buttonNow.Location = new System.Drawing.Point(121, 140);
			this.buttonNow.Name = "buttonNow";
			this.buttonNow.Size = new System.Drawing.Size(27, 23);
			this.buttonNow.TabIndex = 9;
			this.buttonNow.Text = "o";
			this.buttonNow.UseVisualStyleBackColor = true;
			this.buttonNow.Click += new System.EventHandler(this.buttonNow_Click);
			// 
			// buttonSaveTime
			// 
			this.buttonSaveTime.Location = new System.Drawing.Point(59, 291);
			this.buttonSaveTime.Name = "buttonSaveTime";
			this.buttonSaveTime.Size = new System.Drawing.Size(92, 23);
			this.buttonSaveTime.TabIndex = 10;
			this.buttonSaveTime.Text = "Spara";
			this.buttonSaveTime.UseVisualStyleBackColor = true;
			this.buttonSaveTime.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonSalarySpec
			// 
			this.buttonSalarySpec.Location = new System.Drawing.Point(121, 111);
			this.buttonSalarySpec.Name = "buttonSalarySpec";
			this.buttonSalarySpec.Size = new System.Drawing.Size(70, 23);
			this.buttonSalarySpec.TabIndex = 11;
			this.buttonSalarySpec.Text = "Lönespec";
			this.buttonSalarySpec.UseVisualStyleBackColor = true;
			this.buttonSalarySpec.Click += new System.EventHandler(this.buttonSalarySpec_Click);
			// 
			// buttonExpenses
			// 
			this.buttonExpenses.Location = new System.Drawing.Point(25, 326);
			this.buttonExpenses.Name = "buttonExpenses";
			this.buttonExpenses.Size = new System.Drawing.Size(106, 23);
			this.buttonExpenses.TabIndex = 12;
			this.buttonExpenses.Text = "Reseräkning";
			this.buttonExpenses.UseVisualStyleBackColor = true;
			this.buttonExpenses.Click += new System.EventHandler(this.buttonExpenses_Click);
			// 
			// textBoxReg1
			// 
			this.textBoxReg1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxReg1.Location = new System.Drawing.Point(195, 235);
			this.textBoxReg1.Name = "textBoxReg1";
			this.textBoxReg1.Size = new System.Drawing.Size(396, 20);
			this.textBoxReg1.TabIndex = 13;
			this.textBoxReg1.Text = "0; 10167; 10; Löpande; 2; 9,5; 0; 9,5; 0; 2; 0; 0";
			// 
			// textBoxReg2
			// 
			this.textBoxReg2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxReg2.Location = new System.Drawing.Point(195, 264);
			this.textBoxReg2.Name = "textBoxReg2";
			this.textBoxReg2.Size = new System.Drawing.Size(396, 20);
			this.textBoxReg2.TabIndex = 14;
			this.textBoxReg2.Text = "0; 10161; 10; Löpande.; 2; 0; 0; 0; 0; 1; 0; 0";
			// 
			// buttonVersion
			// 
			this.buttonVersion.Location = new System.Drawing.Point(25, 493);
			this.buttonVersion.Name = "buttonVersion";
			this.buttonVersion.Size = new System.Drawing.Size(106, 23);
			this.buttonVersion.TabIndex = 15;
			this.buttonVersion.Text = "Version";
			this.buttonVersion.UseVisualStyleBackColor = true;
			this.buttonVersion.Click += new System.EventHandler(this.buttonVersion_Click);
			// 
			// buttonMiles
			// 
			this.buttonMiles.Location = new System.Drawing.Point(42, 356);
			this.buttonMiles.Name = "buttonMiles";
			this.buttonMiles.Size = new System.Drawing.Size(89, 23);
			this.buttonMiles.TabIndex = 16;
			this.buttonMiles.Text = "Milersättning";
			this.buttonMiles.UseVisualStyleBackColor = true;
			this.buttonMiles.Click += new System.EventHandler(this.buttonMiles_Click);
			// 
			// buttonBroBizz
			// 
			this.buttonBroBizz.Location = new System.Drawing.Point(42, 385);
			this.buttonBroBizz.Name = "buttonBroBizz";
			this.buttonBroBizz.Size = new System.Drawing.Size(89, 23);
			this.buttonBroBizz.TabIndex = 16;
			this.buttonBroBizz.Text = "BroBizz";
			this.buttonBroBizz.UseVisualStyleBackColor = true;
			this.buttonBroBizz.Click += new System.EventHandler(this.buttonBroBizz_Click);
			// 
			// buttonMassage
			// 
			this.buttonMassage.Location = new System.Drawing.Point(42, 412);
			this.buttonMassage.Name = "buttonMassage";
			this.buttonMassage.Size = new System.Drawing.Size(89, 23);
			this.buttonMassage.TabIndex = 16;
			this.buttonMassage.Text = "Massage";
			this.buttonMassage.UseVisualStyleBackColor = true;
			this.buttonMassage.Click += new System.EventHandler(this.buttonMassage_Click);
			// 
			// buttonSaveExpenses
			// 
			this.buttonSaveExpenses.Location = new System.Drawing.Point(59, 441);
			this.buttonSaveExpenses.Name = "buttonSaveExpenses";
			this.buttonSaveExpenses.Size = new System.Drawing.Size(72, 23);
			this.buttonSaveExpenses.TabIndex = 10;
			this.buttonSaveExpenses.Text = "Spara";
			this.buttonSaveExpenses.UseVisualStyleBackColor = true;
			this.buttonSaveExpenses.Click += new System.EventHandler(this.buttonSaveExpenses_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(192, 219);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(242, 13);
			this.label1.TabIndex = 17;
			this.label1.Text = "Time Code, Project, Activity, Desc, Role, Hours[7]";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 745);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonMassage);
			this.Controls.Add(this.buttonBroBizz);
			this.Controls.Add(this.buttonMiles);
			this.Controls.Add(this.buttonVersion);
			this.Controls.Add(this.textBoxReg2);
			this.Controls.Add(this.textBoxReg1);
			this.Controls.Add(this.buttonExpenses);
			this.Controls.Add(this.buttonSalarySpec);
			this.Controls.Add(this.buttonSaveExpenses);
			this.Controls.Add(this.buttonSaveTime);
			this.Controls.Add(this.buttonNow);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonPrev);
			this.Controls.Add(this.buttonReg);
			this.Controls.Add(this.textBoxInfo);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonReg2);
			this.Controls.Add(this.buttonReg1);
			this.Controls.Add(this.buttonSalarySuggestion);
			this.Controls.Add(this.buttonLogin);
			this.Controls.Add(this.labelCaption);
			this.Name = "MainForm";
			this.Text = "Agresso Automator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonSalarySuggestion;
        private System.Windows.Forms.Button buttonReg1;
        private System.Windows.Forms.Button buttonReg2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Button buttonReg;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonNow;
        private System.Windows.Forms.Button buttonSaveTime;
        private System.Windows.Forms.Button buttonSalarySpec;
        private System.Windows.Forms.Button buttonExpenses;
        private System.Windows.Forms.TextBox textBoxReg1;
        private System.Windows.Forms.TextBox textBoxReg2;
        private System.Windows.Forms.Button buttonVersion;
        private System.Windows.Forms.Button buttonMiles;
        private System.Windows.Forms.Button buttonBroBizz;
        private System.Windows.Forms.Button buttonMassage;
        private System.Windows.Forms.Button buttonSaveExpenses;
		private System.Windows.Forms.Label label1;
    }
}

