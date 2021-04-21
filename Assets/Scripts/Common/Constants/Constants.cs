

public static class Constants
{
    //Latent power description
    public static string Light = "Light";
    public static string Medium = "Medium";
    public static string Heavy = "Heavy";

    //Tags
    public static string DetailTag = "JournalDetail";
    public static string CaseLabelTag = "CaseLabel";
    public static string MenuTag = "MenuItem";
    public static string PlayerTag = "Player";
    public static string Scrollview = "Scrollview";
    public static string SkillButton = "SkillButton";

    //Inputs
    public static string Submit = "Submit";
    public static string Cancel = "Cancel";
    public static string Journal = "Journal";
    public static string Vertical = "Vertical";
    public static string Horizontal = "Horizontal";
    public static string Smoke = "Smoke";
    public static string CaseStatus = "CaseStatus";

    //Battle Buttons
    public static string Rest = "Rest";
    public static string Skills = "Skills";
    public static string Evidence = "Evidence";

    public static string GetLatentPowerDefinition(int power)
    {
        if(power > 0 && power < 10) {
            return Constants.Light;
        }
        else if(power > 9 && power < 20) {
            return Constants.Medium;
        }
        else if(power > 19) {
            return Constants.Heavy;
        }
        else {
            return string.Empty;
        }
    }
}
