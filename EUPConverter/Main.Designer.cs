namespace EUPConverter;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        openEUP = new OpenFileDialog();
        listBox = new ListBox();
        button = new Button();
        textBox = new TextBox();
        label1 = new Label();
        infoLabel = new Label();
        optionsLabel = new Label();
        sunnyCheckBox = new CheckBox();
        rainyCheckBox = new CheckBox();
        snowyCheckBox = new CheckBox();
        healthNum = new NumericUpDown();
        healthLabel = new Label();
        armorLabel = new Label();
        armorNum = new NumericUpDown();
        ((System.ComponentModel.ISupportInitialize)healthNum).BeginInit();
        ((System.ComponentModel.ISupportInitialize)armorNum).BeginInit();
        SuspendLayout();
        // 
        // openEUP
        // 
        openEUP.Filter = "EUP Preset Outfit|presetoutfits.ini|EUP Wardrobe|wardrobe.ini";
        // 
        // listBox
        // 
        listBox.FormattingEnabled = true;
        listBox.HorizontalScrollbar = true;
        listBox.ItemHeight = 15;
        listBox.Location = new Point(12, 12);
        listBox.Name = "listBox";
        listBox.Size = new Size(259, 274);
        listBox.TabIndex = 0;
        listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
        // 
        // button
        // 
        button.Location = new Point(450, 307);
        button.Name = "button";
        button.Size = new Size(79, 23);
        button.TabIndex = 2;
        button.Text = "Copy";
        button.UseVisualStyleBackColor = true;
        button.Click += button_Click;
        // 
        // textBox
        // 
        textBox.Cursor = Cursors.IBeam;
        textBox.Location = new Point(12, 307);
        textBox.Name = "textBox";
        textBox.ReadOnly = true;
        textBox.ScrollBars = ScrollBars.Horizontal;
        textBox.Size = new Size(432, 23);
        textBox.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(12, 289);
        label1.Name = "label1";
        label1.Size = new Size(284, 15);
        label1.TabIndex = 3;
        label1.Text = "Click to \"Copy\" to copy the xml for Japanese Callouts";
        // 
        // infoLabel
        // 
        infoLabel.AutoSize = true;
        infoLabel.Location = new Point(277, 12);
        infoLabel.Name = "infoLabel";
        infoLabel.Size = new Size(69, 15);
        infoLabel.TabIndex = 6;
        infoLabel.Text = "Information";
        // 
        // optionsLabel
        // 
        optionsLabel.AutoSize = true;
        optionsLabel.Location = new Point(401, 12);
        optionsLabel.Name = "optionsLabel";
        optionsLabel.Size = new Size(49, 15);
        optionsLabel.TabIndex = 7;
        optionsLabel.Text = "Options";
        // 
        // sunnyCheckBox
        // 
        sunnyCheckBox.AutoSize = true;
        sunnyCheckBox.Location = new Point(401, 30);
        sunnyCheckBox.Name = "sunnyCheckBox";
        sunnyCheckBox.Size = new Size(122, 19);
        sunnyCheckBox.TabIndex = 3;
        sunnyCheckBox.Text = "Available in sunny";
        sunnyCheckBox.UseVisualStyleBackColor = true;
        sunnyCheckBox.CheckedChanged += sunnyCheckBox_CheckedChanged;
        // 
        // rainyCheckBox
        // 
        rainyCheckBox.AutoSize = true;
        rainyCheckBox.Location = new Point(401, 55);
        rainyCheckBox.Name = "rainyCheckBox";
        rainyCheckBox.Size = new Size(116, 19);
        rainyCheckBox.TabIndex = 4;
        rainyCheckBox.Text = "Available in rainy";
        rainyCheckBox.UseVisualStyleBackColor = true;
        rainyCheckBox.CheckedChanged += rainyCheckBox_CheckedChanged;
        // 
        // snowyCheckBox
        // 
        snowyCheckBox.AutoSize = true;
        snowyCheckBox.Location = new Point(401, 80);
        snowyCheckBox.Name = "snowyCheckBox";
        snowyCheckBox.Size = new Size(121, 19);
        snowyCheckBox.TabIndex = 5;
        snowyCheckBox.Text = "Avaiable in snowy";
        snowyCheckBox.UseVisualStyleBackColor = true;
        snowyCheckBox.CheckedChanged += snowyCheckBox_CheckedChanged;
        // 
        // healthNum
        // 
        healthNum.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        healthNum.Location = new Point(401, 126);
        healthNum.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
        healthNum.Name = "healthNum";
        healthNum.Size = new Size(120, 23);
        healthNum.TabIndex = 6;
        healthNum.ValueChanged += healthNum_ValueChanged;
        // 
        // healthLabel
        // 
        healthLabel.AutoSize = true;
        healthLabel.Location = new Point(401, 108);
        healthLabel.Name = "healthLabel";
        healthLabel.Size = new Size(65, 15);
        healthLabel.TabIndex = 12;
        healthLabel.Text = "Ped Health";
        // 
        // armorLabel
        // 
        armorLabel.AutoSize = true;
        armorLabel.Location = new Point(401, 152);
        armorLabel.Name = "armorLabel";
        armorLabel.Size = new Size(63, 15);
        armorLabel.TabIndex = 14;
        armorLabel.Text = "Ped Armor";
        // 
        // armorNum
        // 
        armorNum.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        armorNum.Location = new Point(401, 170);
        armorNum.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
        armorNum.Name = "armorNum";
        armorNum.Size = new Size(120, 23);
        armorNum.TabIndex = 7;
        armorNum.ValueChanged += armorNum_ValueChanged;
        // 
        // Main
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(541, 339);
        Controls.Add(armorLabel);
        Controls.Add(armorNum);
        Controls.Add(healthLabel);
        Controls.Add(healthNum);
        Controls.Add(snowyCheckBox);
        Controls.Add(rainyCheckBox);
        Controls.Add(sunnyCheckBox);
        Controls.Add(optionsLabel);
        Controls.Add(infoLabel);
        Controls.Add(button);
        Controls.Add(textBox);
        Controls.Add(label1);
        Controls.Add(listBox);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Main";
        ShowIcon = false;
        Text = "Japanese Callouts - EUP Converter";
        Load += Main_Load;
        ((System.ComponentModel.ISupportInitialize)healthNum).EndInit();
        ((System.ComponentModel.ISupportInitialize)armorNum).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private OpenFileDialog openEUP;
    private ListBox listBox;
    private Button button;
    internal TextBox textBox;
    private Label label1;
    private Label infoLabel;
    private Label optionsLabel;
    private CheckBox sunnyCheckBox;
    private CheckBox rainyCheckBox;
    private CheckBox snowyCheckBox;
    private NumericUpDown healthNum;
    private Label healthLabel;
    private Label armorLabel;
    private NumericUpDown armorNum;
}
