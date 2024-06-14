namespace JapaneseCallouts.Computers;
public partial class PoliceComputerTemplate : Form
{
    public PoliceComputerTemplate()
    {
        InitializeComponent();
        foreach (var lang in Enum.GetNames(typeof(ELanguages)))
        {
            langComboBox.Items.Add(lang);
        }
        langComboBox.SelectedIndex = (int)Localization.Language;
    }
}
