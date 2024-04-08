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
        label2 = new Label();
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
        button.Location = new Point(391, 307);
        button.Name = "button";
        button.Size = new Size(79, 23);
        button.TabIndex = 5;
        button.Text = "Copy";
        button.UseVisualStyleBackColor = true;
        button.Click += button_Click;
        //
        // textBox
        //
        textBox.Location = new Point(12, 307);
        textBox.Name = "textBox";
        textBox.Size = new Size(373, 23);
        textBox.TabIndex = 4;
        //
        // label1
        //
        label1.AutoSize = true;
        label1.Location = new Point(12, 289);
        label1.Name = "label1";
        label1.Size = new Size(205, 15);
        label1.TabIndex = 3;
        label1.Text = "Click to \"Copy\" to copy the xml for SB";
        //
        // label2
        //
        label2.AutoSize = true;
        label2.Location = new Point(277, 12);
        label2.Name = "label2";
        label2.Size = new Size(69, 15);
        label2.TabIndex = 6;
        label2.Text = "Information";
        //
        // Main
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(482, 339);
        Controls.Add(label2);
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
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private OpenFileDialog openEUP;
    private ListBox listBox;
    private Button button;
    internal TextBox textBox;
    private Label label1;
    private Label label2;
}
