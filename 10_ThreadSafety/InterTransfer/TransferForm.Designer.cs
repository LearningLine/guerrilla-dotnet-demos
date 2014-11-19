namespace InterTransfer
{
    partial class TransferForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label numberOfCellsLabel;
            System.Windows.Forms.Label numberOfTransfersLabel;
            System.Windows.Forms.Label numberofWorkersLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.button1 = new System.Windows.Forms.Button();
            this.bankTotal = new System.Windows.Forms.Label();
            this.auditTotal = new System.Windows.Forms.Label();
            this.auditTimer = new System.Windows.Forms.Timer(this.components);
            this.numberOfCellsTextBox = new System.Windows.Forms.TextBox();
            this.transferTestDefinitionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numberOfTransfersTextBox = new System.Windows.Forms.TextBox();
            this.numberofWorkersTextBox = new System.Windows.Forms.TextBox();
            this.noTransfersLabel = new System.Windows.Forms.Label();
            this.manualAuditButton = new System.Windows.Forms.Button();
            numberOfCellsLabel = new System.Windows.Forms.Label();
            numberOfTransfersLabel = new System.Windows.Forms.Label();
            numberofWorkersLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.transferTestDefinitionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // numberOfCellsLabel
            // 
            numberOfCellsLabel.AutoSize = true;
            numberOfCellsLabel.Location = new System.Drawing.Point(74, 52);
            numberOfCellsLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            numberOfCellsLabel.Name = "numberOfCellsLabel";
            numberOfCellsLabel.Size = new System.Drawing.Size(175, 25);
            numberOfCellsLabel.TabIndex = 4;
            numberOfCellsLabel.Text = "Number Of Cells:";
            // 
            // numberOfTransfersLabel
            // 
            numberOfTransfersLabel.AutoSize = true;
            numberOfTransfersLabel.Location = new System.Drawing.Point(74, 102);
            numberOfTransfersLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            numberOfTransfersLabel.Name = "numberOfTransfersLabel";
            numberOfTransfersLabel.Size = new System.Drawing.Size(218, 25);
            numberOfTransfersLabel.TabIndex = 6;
            numberOfTransfersLabel.Text = "Number Of Transfers:";
            // 
            // numberofWorkersLabel
            // 
            numberofWorkersLabel.AutoSize = true;
            numberofWorkersLabel.Location = new System.Drawing.Point(74, 152);
            numberofWorkersLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            numberofWorkersLabel.Name = "numberofWorkersLabel";
            numberofWorkersLabel.Size = new System.Drawing.Size(197, 25);
            numberofWorkersLabel.TabIndex = 8;
            numberofWorkersLabel.Text = "Numberof Workers:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(601, 49);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(156, 25);
            label1.TabIndex = 10;
            label1.Text = "Expected Total";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(602, 87);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(115, 25);
            label2.TabIndex = 11;
            label2.Text = "Audit Total";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(602, 132);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(118, 25);
            label3.TabIndex = 13;
            label3.Text = "NTransfers";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(726, 205);
            this.button1.Margin = new System.Windows.Forms.Padding(6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(252, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Simulation";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bankTotal
            // 
            this.bankTotal.AutoSize = true;
            this.bankTotal.Location = new System.Drawing.Point(788, 49);
            this.bankTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.bankTotal.Name = "bankTotal";
            this.bankTotal.Size = new System.Drawing.Size(96, 25);
            this.bankTotal.TabIndex = 1;
            this.bankTotal.Text = "XXXXXX";
            // 
            // auditTotal
            // 
            this.auditTotal.AutoSize = true;
            this.auditTotal.Location = new System.Drawing.Point(788, 87);
            this.auditTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.auditTotal.Name = "auditTotal";
            this.auditTotal.Size = new System.Drawing.Size(82, 25);
            this.auditTotal.TabIndex = 2;
            this.auditTotal.Text = "XXXXX";
            // 
            // auditTimer
            // 
            this.auditTimer.Tick += new System.EventHandler(this.DoAudit);
            // 
            // numberOfCellsTextBox
            // 
            this.numberOfCellsTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.transferTestDefinitionBindingSource, "NumberOfCells", true));
            this.numberOfCellsTextBox.Location = new System.Drawing.Point(302, 46);
            this.numberOfCellsTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.numberOfCellsTextBox.Name = "numberOfCellsTextBox";
            this.numberOfCellsTextBox.Size = new System.Drawing.Size(196, 31);
            this.numberOfCellsTextBox.TabIndex = 5;
            // 
            // transferTestDefinitionBindingSource
            // 
            this.transferTestDefinitionBindingSource.DataSource = typeof(InterTransfer.TransferTestDefinition);
            // 
            // numberOfTransfersTextBox
            // 
            this.numberOfTransfersTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.transferTestDefinitionBindingSource, "NumberOfTransfers", true));
            this.numberOfTransfersTextBox.Location = new System.Drawing.Point(302, 96);
            this.numberOfTransfersTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.numberOfTransfersTextBox.Name = "numberOfTransfersTextBox";
            this.numberOfTransfersTextBox.Size = new System.Drawing.Size(196, 31);
            this.numberOfTransfersTextBox.TabIndex = 7;
            // 
            // numberofWorkersTextBox
            // 
            this.numberofWorkersTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.transferTestDefinitionBindingSource, "NumberofWorkers", true));
            this.numberofWorkersTextBox.Location = new System.Drawing.Point(302, 146);
            this.numberofWorkersTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.numberofWorkersTextBox.Name = "numberofWorkersTextBox";
            this.numberofWorkersTextBox.Size = new System.Drawing.Size(196, 31);
            this.numberofWorkersTextBox.TabIndex = 9;
            // 
            // noTransfersLabel
            // 
            this.noTransfersLabel.AutoSize = true;
            this.noTransfersLabel.Location = new System.Drawing.Point(788, 132);
            this.noTransfersLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.noTransfersLabel.Name = "noTransfersLabel";
            this.noTransfersLabel.Size = new System.Drawing.Size(82, 25);
            this.noTransfersLabel.TabIndex = 12;
            this.noTransfersLabel.Text = "XXXXX";
            // 
            // manualAuditButton
            // 
            this.manualAuditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.manualAuditButton.Location = new System.Drawing.Point(811, 261);
            this.manualAuditButton.Margin = new System.Windows.Forms.Padding(6);
            this.manualAuditButton.Name = "manualAuditButton";
            this.manualAuditButton.Size = new System.Drawing.Size(167, 44);
            this.manualAuditButton.TabIndex = 14;
            this.manualAuditButton.Text = "Manual Audit";
            this.manualAuditButton.UseVisualStyleBackColor = true;
            this.manualAuditButton.Click += new System.EventHandler(this.DoAudit);
            // 
            // TransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 321);
            this.Controls.Add(this.manualAuditButton);
            this.Controls.Add(label3);
            this.Controls.Add(this.noTransfersLabel);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(numberOfCellsLabel);
            this.Controls.Add(this.numberOfCellsTextBox);
            this.Controls.Add(numberOfTransfersLabel);
            this.Controls.Add(this.numberOfTransfersTextBox);
            this.Controls.Add(numberofWorkersLabel);
            this.Controls.Add(this.numberofWorkersTextBox);
            this.Controls.Add(this.auditTotal);
            this.Controls.Add(this.bankTotal);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "TransferForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.transferTestDefinitionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label bankTotal;
        private System.Windows.Forms.Label auditTotal;
        private System.Windows.Forms.Timer auditTimer;
        private System.Windows.Forms.BindingSource transferTestDefinitionBindingSource;
        private System.Windows.Forms.TextBox numberOfCellsTextBox;
        private System.Windows.Forms.TextBox numberOfTransfersTextBox;
        private System.Windows.Forms.TextBox numberofWorkersTextBox;
        private System.Windows.Forms.Label noTransfersLabel;
        private System.Windows.Forms.Button manualAuditButton;
    }
}

