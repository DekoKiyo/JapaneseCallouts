using IniParser;
using System.Text;

namespace EUPConverter;

public partial class Main : Form
{
    private Dictionary<string, (Gender gender, Preset preset)> AllPresets = [];

    public Main()
    {
        InitializeComponent();
    }

    private void Main_Load(object sender, EventArgs e)
    {
        openEUP.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        if (openEUP.ShowDialog() == DialogResult.OK)
        {
            ParseIni(openEUP.FileName);
        }
        else
        {
            MessageBox.Show("Please select the EUP outfit ini file.\nThe application will be closed.", "Warn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }
        TopMost = true;
        TopMost = false;
    }

    private void ParseIni(string path)
    {
        var parser = new FileIniDataParser();
        parser.Parser.Configuration.CommentString = "//";
        parser.Parser.Configuration.AllowDuplicateKeys = true;
        parser.Parser.Configuration.OverrideDuplicateKeys = true;
        var ini = parser.ReadFile(path);

        foreach (var item in ini.Sections)
        {
            var preset = new Preset();
            var gender = item.Keys["Gender"] is "Male" ? Gender.Male : Gender.Female;
            if (item.Keys["Hat"] is not null) preset.Hat = item.Keys["Hat"].Split(':');
            if (item.Keys["Glasses"] is not null) preset.Glasses = item.Keys["Glasses"].Split(':');
            if (item.Keys["Ear"] is not null) preset.Ear = item.Keys["Ear"].Split(':');
            if (item.Keys["Watch"] is not null) preset.Watch = item.Keys["Watch"].Split(':');
            if (item.Keys["Mask"] is not null) preset.Mask = item.Keys["Mask"].Split(':');
            if (item.Keys["Top"] is not null) preset.Top = item.Keys["Top"].Split(':');
            if (item.Keys["UpperSkin"] is not null) preset.UpperSkin = item.Keys["UpperSkin"].Split(':');
            if (item.Keys["Decal"] is not null) preset.Decal = item.Keys["Decal"].Split(':');
            if (item.Keys["UnderCoat"] is not null) preset.UnderCoat = item.Keys["UnderCoat"].Split(':');
            if (item.Keys["Pants"] is not null) preset.Pants = item.Keys["Pants"].Split(':');
            if (item.Keys["Shoes"] is not null) preset.Shoes = item.Keys["Shoes"].Split(':');
            if (item.Keys["Accessories"] is not null) preset.Accessories = item.Keys["Accessories"].Split(':');
            if (item.Keys["Armor"] is not null) preset.Armor = item.Keys["Armor"].Split(':');
            if (item.Keys["Parachute"] is not null) preset.Parachute = item.Keys["Parachute"].Split(':');
            listBox.Items.Add(item.SectionName);
            AllPresets.Add(item.SectionName, (gender, preset));
        }
    }

    private void listBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox.SelectedItem is not null)
        {
            var gender = AllPresets[listBox.SelectedItem as string].gender;
            // var sb = new StringBuilder("<Ped chance=\"100\" is_sunny=\"true\" is_rainy=\"false\" is_snowy=\"false\" ");
            var sb = new StringBuilder("<Ped chance=\"100\" ");
            var preset = AllPresets[listBox.SelectedItem as string].preset;
            if (preset.Hat[0] is not "0")
            {
                sb.Append($"comp_hat_model=\"{preset.Hat[0]}\" comp_hat_texture=\"{preset.Hat[1]}\" ");
            }
            if (preset.Glasses[0] is not "0")
            {
                sb.Append($"comp_glasses_model=\"{preset.Glasses[0]}\" comp_glasses_texture=\"{preset.Glasses[1]}\" ");
            }
            if (preset.Ear[0] is not "0")
            {
                sb.Append($"comp_ear_model=\"{preset.Ear[0]}\" comp_ear_texture=\"{preset.Ear[1]}\" ");
            }
            if (preset.Watch[0] is not "0")
            {
                sb.Append($"comp_watch_model=\"{preset.Watch[0]}\" comp_watch_texture=\"{preset.Watch[1]}\" ");
            }
            if (preset.Mask[0] is not "0")
            {
                sb.Append($"comp_mask_model=\"{preset.Mask[0]}\" comp_mask_texture=\"{preset.Mask[1]}\" ");
            }
            if (preset.Top[0] is not "0")
            {
                sb.Append($"comp_top_model=\"{preset.Top[0]}\" comp_top_texture=\"{preset.Top[1]}\" ");
            }
            if (preset.UpperSkin[0] is not "0")
            {
                sb.Append($"comp_upperskin_model=\"{preset.UpperSkin[0]}\" comp_upperskin_texture=\"{preset.UpperSkin[1]}\" ");
            }
            if (preset.Decal[0] is not "0")
            {
                sb.Append($"comp_decal_model=\"{preset.Decal[0]}\" comp_decal_texture=\"{preset.Decal[1]}\" ");
            }
            if (preset.UnderCoat[0] is not "0")
            {
                sb.Append($"comp_undercoat_model=\"{preset.UnderCoat[0]}\" comp_undercoat_texture=\"{preset.UnderCoat[1]}\" ");
            }
            if (preset.Pants[0] is not "0")
            {
                sb.Append($"comp_pants_model=\"{preset.Pants[0]}\" comp_pants_texture=\"{preset.Pants[1]}\" ");
            }
            if (preset.Shoes[0] is not "0")
            {
                sb.Append($"comp_shoes_model=\"{preset.Shoes[0]}\" comp_shoes_texture=\"{preset.Shoes[1]}\" ");
            }
            if (preset.Accessories[0] is not "0")
            {
                sb.Append($"comp_accessories_model=\"{preset.Accessories[0]}\" comp_accessories_texture=\"{preset.Accessories[1]}\" ");
            }
            if (preset.Armor[0] is not "0")
            {
                sb.Append($"comp_armor_model=\"{preset.Armor[0]}\" comp_armor_texture=\"{preset.Armor[1]}\" ");
            }
            if (preset.Parachute[0] is not "0")
            {
                sb.Append($"comp_parachute_model=\"{preset.Parachute[0]}\" comp_parachute_texture=\"{preset.Parachute[1]}\"");
            }
            if (gender is Gender.Male)
            {
                sb.Append(">MP_M_FREEMODE_01</Ped>");
            }
            else
            {
                sb.Append(">MP_F_FREEMODE_01</Ped>");
            }
            textBox.Text = sb.ToString();

            label2.Text = @$"Information:
Gender: {gender}
Prop Hat: {preset.Hat[0]}:{preset.Hat[1]}
Prop Glasses: {preset.Glasses[0]}:{preset.Glasses[1]}
Prop Ear: {preset.Ear[0]}:{preset.Ear[1]}
Prop Watch: {preset.Watch[0]}:{preset.Watch[1]}
Comp Mask: {preset.Mask[0]}:{preset.Mask[1]}
Comp Top: {preset.Top[0]}:{preset.Top[1]}
Comp UpperSkin: {preset.UpperSkin[0]}:{preset.UpperSkin[1]}
Comp Decal: {preset.Decal[0]}:{preset.Decal[1]}
Comp UnderCoat: {preset.UnderCoat[0]}:{preset.UnderCoat[1]}
Comp Pants: {preset.Pants[0]}:{preset.Pants[1]}
Comp Shoes: {preset.Shoes[0]}:{preset.Shoes[1]}
Comp Accessories: {preset.Accessories[0]}:{preset.Accessories[1]}
Comp Armor: {preset.Armor[0]}:{preset.Armor[1]}
Comp Parachute: {preset.Parachute[0]}:{preset.Parachute[1]}";
        }
        else
        {
            textBox.Text = "";
            label2.Text = "";
        }
    }

    private void button_Click(object sender, EventArgs e)
    {
        Clipboard.SetText(textBox.Text);
    }
}