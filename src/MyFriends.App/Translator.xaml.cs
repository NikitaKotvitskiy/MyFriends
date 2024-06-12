using System.Text.RegularExpressions;

namespace MyFriends.App;

public partial class Translator : ContentPage
{
	public Translator()
	{
		InitializeComponent();
	}

	private void DoTranslate(object sender, EventArgs e)
	{
		var inputPattern = @"^[a-zA-Z]+$";
		var userInput = UserInput.Text;
		if (userInput == null || userInput == "")
		{
			TranslatedText.Text = String.Empty;
			return;
		}

		if (!Regex.IsMatch(userInput, inputPattern))
		{
			TranslatedText.Text = String.Empty;
			DisplayAlert("Why?", "You were supposed to write normal English letters, not anything else!", "Sorry :(");
		}

		userInput = userInput.ToLower();

		var retValue = "";
		foreach (var c in userInput)
		{
			if (c < 'a' + 3)
				retValue += 2;
			else if (c < 'a' + 6)
				retValue += 3;
			else if (c < 'a' + 9)
				retValue += 4;
			else if (c < 'a' + 12)
				retValue += 5;
			else if (c < 'a' + 15)
				retValue += 6;
			else if (c < 'a' + 19)
				retValue += 7;
			else if (c < 'a' + 22)
				retValue += 8;
			else
				retValue += 9;
		}

		TranslatedText.Text = retValue;
	}
}