namespace JapaneseCallouts.Computers;

partial class PoliceComputerTemplate
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
            this.langComboBox = new System.Windows.Forms.ComboBox();
            this.langLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // langComboBox
            // 
            this.langComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.langComboBox.FormattingEnabled = true;
            this.langComboBox.Location = new System.Drawing.Point(588, 12);
            this.langComboBox.Name = "langComboBox";
            this.langComboBox.Size = new System.Drawing.Size(174, 23);
            this.langComboBox.TabIndex = 0;
            // 
            // langLabel
            // 
            this.langLabel.AutoSize = true;
            this.langLabel.Location = new System.Drawing.Point(523, 15);
            this.langLabel.Name = "langLabel";
            this.langLabel.Size = new System.Drawing.Size(59, 15);
            this.langLabel.TabIndex = 1;
            this.langLabel.Text = "Language";
            // 
            // PoliceComputerTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(774, 463);
            this.Controls.Add(this.langLabel);
            this.Controls.Add(this.langComboBox);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PoliceComputerTemplate";
            this.Text = "[Admin] Console";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox langComboBox;
    private System.Windows.Forms.Label langLabel;
}